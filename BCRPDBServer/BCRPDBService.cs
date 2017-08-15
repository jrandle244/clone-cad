using BCRPDBServer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCRPDBServer
{
    partial class BCRPDBService : ServiceBase
    {
        public BCRPDBService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (args.Length == 2)
            {
                Settings.Default.settings = args[0].Substring(1);
                Settings.Default.Save();

                MessageBox.Show("The config path has been set.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }

            if (string.IsNullOrWhiteSpace(Settings.Default.settings))
            {
                MessageBox.Show("The config path is invalid. View the readme and make sure your save paths are correct.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            cfg = new Config(Settings.Default.settings);
            log = new Log(cfg.Log, cfg.Aliases);
            list = new TcpListener(IPAddress.Parse(cfg.IP), cfg.Port);
            Civilians = new List<Civ>();

            if (File.Exists(cfg.Database))
                foreach (string line in File.ReadLines(cfg.Database))
                    Civilians.Add(Civ.Parse(line, File.GetLastWriteTime(cfg.Database)));

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

            ThreadPool.QueueUserWorkItem(x =>
            {
                while (true)
                    ThreadPool.QueueUserWorkItem(Connect, list.AcceptSocket());
            });
        }

        protected override void OnStop()
        {
            Log.WriteLine("Saving and closing...");
            File.WriteAllLines(cfg.Database, Civilians.Select(x => x.ToString()));
        }

        static TcpListener list;
        static List<Civ> Civilians;
        public static Config cfg;
        static Log log;

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
                            break;

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
                            break;

                        string plate = Encoding.UTF8.GetString(b.Take(e).ToArray());

                        civ = Civilians.Find(x => x.RegisteredPlate == plate);

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

                        civ = Civilians.Find(x => x.CivID == ushort.Parse(vars[0]));

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

                        civ = Civilians.Find(x => x.CivID == id);

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

                        civ = Civilians.Find(x => x.Name == name);

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
