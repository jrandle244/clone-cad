using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCRPDBServer
{
    static class Program
    {
        static TcpListener list;
        static List<Civ> Civilians;
        static Config cfg;
        static Log log;

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler();
        static EventHandler _handler;

        static void Main(string[] args)
        {
            Console.Title = "BCRPDB Server";

            cfg = new Config("server-settings.ini");
            log = new Log(cfg.Log);
            list = new TcpListener(IPAddress.Parse(cfg.IP), cfg.Port);
            Civilians = new List<Civ>();

            _handler += new EventHandler(Exit);
            SetConsoleCtrlHandler(Exit, true);

            if (File.Exists("Civilians.db"))
                foreach (string line in File.ReadLines("Civilians.db"))
                    Civilians.Add(Civ.Parse(line, File.GetLastWriteTime("Civilians.db")));

            try
            {
                list.Start();
            }
            catch
            {
                MessageBox.Show("The specified port (" + cfg.Port + ") is already in use.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            Log.WriteLine("Listening for connections...");

            while (true)
                ThreadPool.QueueUserWorkItem(Connect, list.AcceptSocket());
        }

        private static bool Exit()
        {
            Log.WriteLine("Saving and closing...");
            File.WriteAllLines("Civilians.db", Civilians.Select(x => x.ToString()));

            return true;
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
                                return;

                            civ = new Civ(GetLowestID());
                            Log.WriteLine("Reserved civ #" + civ.CivID + ".", ip);

                            Civilians.Add(civ);
                            socket.Send(new byte[] { 0 }.Concat(civ.ToBytes()).ToArray());
                        }
                        else
                            try
                            {
                                if (!cfg.HasPerm(ip, Permission.Civ) || !cfg.HasPerm(ip, Permission.Dispatch))
                                    return;

                                socket.Send(new byte[] { 0 }.Concat(Civilians.Find(x => x.CivID == id).ToBytes()).ToArray());
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
                            return;

                        civ = Civ.ToCiv(b.Take(e).ToArray());
                        Civ fCiv = Civilians.Find(x => x.CivID == civ.CivID);

                        if (fCiv != null)
                        {
                            int i = Civilians.IndexOf(fCiv);
                            List<Ticket> Tickets = Civilians[i].Tickets;

                            Civilians[i] = civ;
                            Civilians[i].Tickets = Tickets;

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
                            return;

                        string plate = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = Civilians.Find(x => x.RegisteredPlate == plate);

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Plate check \"" + plate + "\" returned empty.", ip);
                            return;
                        }

                        socket.Send(BitConverter.GetBytes(civ.CivID));
                        Log.WriteLine("Sent civ #" + civ.CivID + " (Plate check).", ip);
                        break;

                    //Add ticket
                    case 3:
                        if (!cfg.HasPerm(ip, Permission.Police))
                            return;

                        string[] vars = Encoding.UTF8.GetString(b.Take(e).ToArray()).Split('|');
                        
                        civ = Civilians.Find(x => x.CivID == ushort.Parse(vars[0]));
                        
                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Ticketing civ #" + vars[0] + " returned empty.", ip);
                        }

                        civ.Tickets.Add(Ticket.Parse(vars[1]));
                        socket.Send(new byte[] { 0 });
                        Log.WriteLine("Ticketed civ #" + vars[0] + ".", ip);
                        break;

                    //Delete records on a civ but still reserve it
                    case 4:
                        if (!cfg.HasPerm(ip, Permission.Civ))
                            return;

                        id = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);

                        civ = Civilians.Find(x => x.CivID == id);

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Log.WriteLine("Deleting civ #" + id + " returned empty.", ip);
                        }

                        Civilians.Remove(civ);

                        civ = new Civ(GetLowestID());
                        Civilians.Add(civ);

                        socket.Send(new byte[] { 0 }.Concat(civ.ToBytes()).ToArray());

                        Log.WriteLine("Deleted civ #" + id + " and reserved civ #" + civ.CivID, ip);
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
