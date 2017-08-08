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

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler();
        static EventHandler _handler;

        static void Main(string[] args)
        {
            Console.Title = "BCRPDB Server";

            cfg = new Config("bcrpdbserver.cfg");
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

            Console.WriteLine("Listening for connections...");

            while (true)
                ThreadPool.QueueUserWorkItem(Connect, list.AcceptSocket());
        }

        private static bool Exit()
        {
            Console.WriteLine("Saving and closing...");
            File.WriteAllLines("Civilians.db", Civilians.Select(x => x.ToString()));

            return true;
        }

        static void Connect(object socketO)
        {
            Socket socket = (Socket)socketO;
            string ip = socket.RemoteEndPoint.ToString().Split(':')[0];

            if (cfg.Filter == ClientFilterType.Whitelist)
            {
                if (!cfg.FilteredIPs.Contains(ip))
                {
                    socket.Disconnect(true);
                    return;
                }
            }
            else if (cfg.Filter == ClientFilterType.Blacklist)
            {
                if (cfg.FilteredIPs.Contains(ip))
                {
                    socket.Disconnect(true);
                    return;
                }
            }

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

                switch (tag)
                {
                    //Get civ
                    case 0:
                        ushort id = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);

                        if (id == 0)
                        {
                            civ = new Civ(GetLowestID());
                            Console.WriteLine("[" + ip + "] Reserved civ #" + civ.CivID + ".");

                            Civilians.Add(civ);
                            socket.Send(civ.ToBytes());
                        }
                        else
                            try
                            {
                                socket.Send(new byte[] { 0 }.Concat(Civilians.Find(x => x.CivID == id).ToBytes()).ToArray());
                                Console.WriteLine("[" + ip + "] Sent civ #" + id + ".");
                            }
                            catch
                            {
                                socket.Send(new byte[] { 1 });
                                Console.WriteLine("[" + ip + "] Retrieving civ #" + id + " returned empty.");
                            }
                        break;

                    //Update civ
                    case 1:
                        civ = Civ.ToCiv(b.Take(e).ToArray());
                        Civ fCiv = Civilians.Find(x => x.CivID == civ.CivID);

                        if (fCiv != null)
                        {
                            int i = Civilians.IndexOf(fCiv);
                            List<KeyValuePair<string, string>> Tickets = Civilians[i].Tickets;

                            Civilians[i] = civ;
                            Civilians[i].Tickets = Tickets;

                            Console.WriteLine("[" + ip + "] Updated civ #" + civ.CivID + ".");
                        }
                        else
                        {
                            Civilians.Add(civ);
                            Console.WriteLine("[" + ip + "] Saved civ #" + civ.CivID + ".");
                        }
                        break;

                    //Plate check
                    case 2:
                        string plate = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = Civilians.Find(x => x.RegisteredPlate == plate);

                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Console.WriteLine("[" + ip + "] Plate check \"" + plate + "\" returned empty.");
                            return;
                        }

                        socket.Send(BitConverter.GetBytes(civ.CivID));
                        Console.WriteLine("[" + ip + "] Sent civ #" + civ.CivID + " (Plate check).");
                        break;

                    //Add ticket
                    case 3:
                        string[] vars = Encoding.UTF8.GetString(b.Take(e).ToArray()).Split('|');
                        
                        civ = Civilians.Find(x => x.CivID == ushort.Parse(vars[0]));
                        
                        if (civ == null)
                        {
                            socket.Send(new byte[] { 1 });
                            Console.WriteLine("[" + ip + "] Ticketing civ #" + vars[0] + " returned empty.");
                        }

                        civ.Tickets.Add(new KeyValuePair<string, string>(vars[1], vars[2]));
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
