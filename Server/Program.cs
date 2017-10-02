using Server.DataHolders.Dynamic;
using Server.DataHolders.Static;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    static class Program
    {
        private static TcpListener list;
        private static CivilianDictionary Civilians;
        public static Config cfg;
        private static Log log;
        private static bool saving;

        static void Main(string[] args)
        {
            Console.Title = "Client Server";

            if (File.Exists("server-settings.ini"))
                cfg = new Config("server-settings.ini");
            else
            {
                Console.WriteLine("No \"server-settings.ini\" file exists. Check the README.md on GitHub to get a default \"server-settings.ini\".");
                Console.ReadKey();
                Environment.Exit(1);
            }
            log = new Log(cfg.Log, cfg.Aliases);
            list = new TcpListener(IPAddress.Parse(cfg.IP), cfg.Port);
            Civilians = new CivilianDictionary();
            saving = false;

            if (File.Exists("Civilians.db"))
                foreach (string line in File.ReadLines("Civilians.db"))
                {
                    Civ civ = Civ.Parse(line);
                    Civilians.Add(civ);
                }

            //try
            //{
                list.Start();
            //}
            //catch
            /*{
                Console.WriteLine("The specified port (" + cfg.Port + ") is already in use.");
                Console.ReadKey();
                Environment.Exit(0);
            }*/

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
            File.WriteAllLines("Civilians.db", Civilians.Select(x => x.ToString()));
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

                            Civilians.Add(civ);
                            socket.Send(new byte[] { 0 }.Concat(civ.ToBytes()).ToArray());
                        }
                        else
                            try
                            {
                                if (!cfg.HasPerm(ip, Permission.Civ) && !cfg.HasPerm(ip, Permission.Dispatch))
                                    break;

                                socket.Send(new byte[] { 0 }.Concat(Civilians[id].ToBytes()).ToArray());
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

                        if (Civilians.ContainsID(civ.CivID))
                        {
                            List<Ticket> Tickets = Civilians[civ.CivID].Tickets;

                            Civilians[civ.CivID] = civ;
                            Civilians[civ.CivID].Tickets = Tickets;

                            Log.WriteLine("Updated civ #" + civ.CivID + ".", ip);
                        }
                        else
                        {
                            Civilians.Add(civ);
                            Log.WriteLine("Saved civ #" + civ.CivID + ".", ip);
                        }
                        break;

                    //Plate check
                    case 2:
                        if (!cfg.HasPerm(ip, Permission.Dispatch))
                            break;

                        string plate = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = Civilians.GetFromPlate(plate);

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
                        
                        civ = Civilians[ushort.Parse(vars[0])];
                        
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

                        civ = Civilians[id];

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Deleting civ #" + id + " returned empty.", ip);
                            break;
                        }

                        Civilians.Remove(civ);

                        socket.Send(new byte[] { 0 });

                        Log.WriteLine("Deleted civ #" + id, ip);
                        break;
                    
                    //Name check
                    case 5:
                        if (!cfg.HasPerm(ip, Permission.Dispatch))
                            break;

                        string name = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = Civilians[name];

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
            List<ushort> civIDs = Civilians.Select(x => x.CivID).ToList();

            for (ushort i = 1; i < ushort.MaxValue; i++)
                if (!civIDs.Contains(i))
                    return i;

            return ushort.MaxValue;
        }
    }
}
