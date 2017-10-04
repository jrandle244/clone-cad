﻿using CloneCAD.Common.DataHolders;
using CloneCAD.Server.DataHolders;
using CloneCAD.Server.DataHolders.Static;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CloneCAD.Server
{
    static class Program
    {
        private const string CIV_EXPORT = "Civilians.odf";

        private static TcpListener list;
        private static StorableValue<CivilianDictionary> civilians;
        public static Config cfg;
        private static Log log;
        private static bool saving;

        static void Main(string[] args)
        {
            try
            {
                VirtualMain();
            }
            catch
            {
                Functions.Error(cfg.Locale, "UnhandledException", 2);
            }
        }

        /// <summary>
        /// This is the would-be Main, however Main has the try catch block around this for exception catching
        /// </summary>
        private static void VirtualMain()
        {
            Console.Title = "CloneCAD Server";

            if (File.Exists("server-settings.ini"))
                cfg = new Config("server-settings.ini");
            else
            {
                Functions.Error("No \"server-settings.ini\" file exists. Check the README.md on GitHub to get a default \"server-settings.ini\".", 1);
            }

            log = new Log(cfg.Log, cfg.Locale, cfg.Aliases);
            list = new TcpListener(IPAddress.Parse(cfg.IP), cfg.Port);
            saving = false;

            civilians = File.Exists(CIV_EXPORT) ? new StorableValue<CivilianDictionary>(CIV_EXPORT) : new StorableValue<CivilianDictionary>(new CivilianDictionary());

            try
            {
                list.Start();
            }
            catch (SocketException)
            {
                Functions.Error(cfg.Locale, "PortInUse", 1);
            }

            Log.WriteLine("Listening for connections...");

            while (true)
                ThreadPool.QueueUserWorkItem(Connect, list.AcceptSocket());
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            if (saving)
                return;

            saving = true;
            Log.WriteLine("Saving...");
            File.WriteAllLines(CIV_EXPORT, civilians.Select(x => x.ToString()));
            saving = false;
        }

        static void Connect(object socketO)
        {
            Socket socket = (Socket)socketO;
            string ip = socket.RemoteEndPoint.ToString().Split(':')[0];

            while (socket.Connected)
            {
                byte[] b = new byte[1001];

                int e;
                e = socket.Receive(b) - 1;

                byte tag = b[0];
                b = b.Skip(1).ToArray();

                if (e == -1)
                {
                    socket.Disconnect(true);
                    return;
                }

                Civ civ;
                ushort id;

                switch (tag)
                {
                    //Get civ
                    case 0:
                        id = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);

                        if (id == 0)
                        {
                            if (!cfg.HasPerm(ip, Permission.Civ))
                                break;

                            civ = new Civ(GetLowestID());
                            Log.WriteLine("Reserved civ #" + civ.CivID + ".", ip);

                            civilians.Add(civ);
                            socket.Send(new byte[] { 0 }.Concat(civ.ToBytes()).ToArray());
                        }
                        else
                            try
                            {
                                if (!cfg.HasPerm(ip, Permission.Civ) && !cfg.HasPerm(ip, Permission.Dispatch))
                                    break;

                                socket.Send(new byte[] { 0 }.Concat(civilians[id].ToBytes()).ToArray());
                                Log.WriteLine("Sent civ #" + id + ".", ip);
                            }
                            catch
                            {
                                socket.Send(new byte[] { 1 });
                                Log.WriteLine("Retrieving civ #" + id + " returned empty.", ip);
                            }
                        break;

                    //Update civ
                    case 1:
                        if (!cfg.HasPerm(ip, Permission.Civ))
                            break;

                        civ = Civ.ToCiv(b.Take(e).ToArray());

                        if (civilians.ContainsID(civ.CivID))
                        {
                            List<Ticket> Tickets = civilians[civ.CivID].Tickets;

                            civilians[civ.CivID] = civ;
                            civilians[civ.CivID].Tickets = Tickets;

                            Log.WriteLine("Updated civ #" + civ.CivID + ".", ip);
                        }
                        else
                        {
                            civilians.Add(civ);
                            Log.WriteLine("Saved civ #" + civ.CivID + ".", ip);
                        }
                        break;

                    //Plate check
                    case 2:
                        if (!cfg.HasPerm(ip, Permission.Dispatch))
                            break;

                        string plate = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = civilians.GetFromPlate(plate);

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Plate check \"" + plate + "\" returned empty.", ip);
                            break;
                        }

                        socket.Send(new byte[] { 0 }.Concat(BitConverter.GetBytes(civ.CivID)).ToArray());

                        Log.WriteLine("Plate checked \"" + plate + "\".", ip);
                        break;

                    //Add ticket
                    case 3:
                        if (!cfg.HasPerm(ip, Permission.Police))
                            break;

                        string[] vars = Encoding.UTF8.GetString(b.Take(e).ToArray()).Split('|');
                        
                        civ = civilians[ushort.Parse(vars[0])];
                        
                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Ticketing civ #" + vars[0] + " returned empty.", ip);
                            break;
                        }

                        civ.Tickets.Add(Ticket.Parse(vars[1]));
                        socket.Send(new byte[] { 0 });
                        Log.WriteLine("Ticketed civ #" + vars[0] + ".", ip);
                        break;

                    //Delete records on a civ but still reserve it
                    case 4:
                        if (!cfg.HasPerm(ip, Permission.Civ))
                            break;

                        id = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);

                        civ = civilians[id];

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Deleting civ #" + id + " returned empty.", ip);
                            break;
                        }

                        civilians.Remove(civ);

                        socket.Send(new byte[] { 0 });

                        Log.WriteLine("Deleted civ #" + id, ip);
                        break;
                    
                    //Name check
                    case 5:
                        if (!cfg.HasPerm(ip, Permission.Dispatch))
                            break;

                        string name = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = civilians[name];

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Name check on \"" + name + "\" returned empty.", ip);
                            break;
                        }

                        socket.Send(new byte[] { 0 }.Concat(BitConverter.GetBytes(civ.CivID)).ToArray());

                        Log.WriteLine("Name checked \"" + name + "\".");
                        break;
                }
            }
        }

        static ushort GetLowestID()
        {
            List<ushort> civIDs = civilians.Select(x => x.CivID).ToList();

            for (ushort i = 1; i < ushort.MaxValue; i++)
                if (!civIDs.Contains(i))
                    return i;

            return ushort.MaxValue;
        }
    }
}
