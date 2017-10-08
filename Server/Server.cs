using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using CloneCAD.Common;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;
using CloneCAD.Server.DataHolders;
using CloneCAD.Server.DataHolders.Static;

namespace CloneCAD.Server
{
    public class Server
    {
        #region on close hook
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler();

        private bool OnExit()
        {
            Civilians.Save();

            return true;
        }
        #endregion

        private readonly TcpListener Listener;
        private readonly StorableValue<CivilianDictionary> Civilians;

        public Config Config;

        public Server(Config config, CivilianDictionary civilians)
        {
            Console.Title = "CloneCAD Server";

            Config = config;
            Civilians = new StorableValue<CivilianDictionary>(civilians)
            {
                FilePath = Program.CIV_EXPORT_PATH
            };

            Listener = new TcpListener(IPAddress.Parse(Config["IP"]), Config.Port);

            SetConsoleCtrlHandler(OnExit, true);
        }

        public void Start()
        {
            try
            {
                Listener.Start();
            }
            catch (SocketException)
            {
                Functions.Error(Config.Locale, "PortInUse", 1);
            }

            Log.WriteLine("Listening for connections...");

            while (true)
                ThreadPool.QueueUserWorkItem(Connect, Listener.AcceptSocket());
        }

        private void Connect(object socketO)
        {
            Socket socket = (Socket)socketO;

            NetRequestHandler handler = new NetRequestHandler(socket, false);

            handler.NetFunctions.Add("GetCivilian", new NetFunction(GetCivilian));
            handler.NetFunctions.Add("ReserveCivilian", new NetFunction(ReserveCivilian));
            handler.NetFunctions.Add("TicketCivilian", new NetFunction(TicketCivilian));
            handler.NetFunctions.Add("DeleteCivilian", new NetFunction(DeleteCivilian));

            handler.NetFunctions.Add("CheckPlate", new NetFunction(CheckPlate));
            handler.NetFunctions.Add("CheckName", new NetFunction(CheckName));

            handler.NetEvents.Add("UpdateCivilian", new NetEvent(UpdateCivilian));

            handler.Receive();
        }

        #region netfunctions
        private async Task<object> GetCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Civ, Permission.Dispatch))
                return null;
            
            uint civilianID = (uint) objs[0];

            if (Civilians.Value.ContainsKey(civilianID))
            {
                Log.WriteLine("Retrieved civilian (" + civilianID.ToSplitID() + ")", handler.IP, Log.Status.Succeeded);
                return Civilians.Value[civilianID];
            }

            Log.WriteLine("Retrieved civilian (" + civilianID.ToSplitID() + ")", handler.IP, Log.Status.Failed);

            await Task.FromResult(0);
            return null;
        }

        private async Task<object> ReserveCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Civ))
                return null;

            Civilian civilian = new Civilian(GetRandomID());
            Civilians.Value.Add(civilian);

            Log.WriteLine("Reserved civilian (" + civilian.ID.ToSplitID() + ")", handler.IP);

            await Task.FromResult(0);
            return civilian;
        }

        private async Task<object> TicketCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Police))
                return false;

            uint civilianID = (uint)objs[0];

            if (Civilians.Value.ContainsKey(civilianID))
            {
                Civilian civilian = Civilians.Value[civilianID];
                Ticket ticket = (Ticket)objs[1];

                civilian.Tickets.Add(ticket);
                Log.WriteLine("Ticketed civilian (" + civilian.ID.ToSplitID() + ")", handler.IP, Log.Status.Succeeded);
                return true;
            }

            Log.WriteLine("Ticketed civilian (" + civilianID.ToSplitID() + ")", handler.IP, Log.Status.Failed);

            await Task.FromResult(0);
            return false;
        }

        private async Task<object> DeleteCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Civ))
                return false;

            uint id = (uint)objs[0];

            uint civilianID = (uint)objs[0];

            if (Civilians.Value.ContainsKey(civilianID))
            {
                Civilians.Value.Remove(civilianID);

                Log.WriteLine("Deleted civilian (" + id.ToSplitID() + ")", handler.IP, Log.Status.Succeeded);
                return true;
            }

            Log.WriteLine("Deleted civilian (" + id.ToSplitID() + ")", handler.IP, Log.Status.Failed);

            await Task.FromResult(0);
            return false;
        }

        private async Task UpdateCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Civ))
                return;

            Civilian civilian = (Civilian) objs[0];

            if (Civilians.Value.ContainsKey(civilian.ID))
            {
                List<Ticket> tickets = Civilians.Value[civilian.ID].Tickets;

                Civilians.Value[civilian.ID] = civilian;
                Civilians.Value[civilian.ID].Tickets = tickets;

                Log.WriteLine("Updated civilian (" + civilian.ID.ToSplitID() + ")", handler.IP, Log.Status.Succeeded);
            }
            else
            {
                Civilians.Value.Add(civilian);

                Log.WriteLine("Saved civilian (" + civilian.ID.ToSplitID() + ")", handler.IP, Log.Status.Succeeded);
            }

            await Task.FromResult(0);
        }

        private async Task<object> CheckPlate(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Dispatch))
                return null;

            string plate = (string) objs[0];

            if (Civilians.Value.ContainsPlate(plate))
            {
                Log.WriteLine("Plate (" + plate + ") checked", handler.IP, Log.Status.Succeeded);
                return Civilians.Value.GetFromPlate(plate).ID;
            }

            Log.WriteLine("Plate (" + plate + ") checked", handler.IP, Log.Status.Failed);

            await Task.FromResult(0);
            return 0;
        }

        private async Task<object> CheckName(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Dispatch))
                return null;

            string name = (string) objs[0];

            if (Civilians.Value.ContainsName(name))
            {
                Log.WriteLine("Name (" + name + ") checked", handler.IP, Log.Status.Succeeded);
                return Civilians.Value[name].ID;
            }

            Log.WriteLine("Name (" + name + ") checked", handler.IP, Log.Status.Failed);

            await Task.FromResult(0);
            return 0;
        }
        #endregion

        private uint GetRandomID()
        {
            uint id = 0;
            bool firstTime = true;

            while (firstTime || Civilians.Value.ContainsKey(id))
            {
                byte[] digits = new byte[9];
                Random random = new Random();

                for (int i = 0; i < digits.Length; i++)
                    digits[i] = (byte)random.Next(0, 10);

                for (int i = 0; i < digits.Length; i++)
                    id += (uint)(digits[i] * Math.Pow(10, i));

                firstTime = false;
            }

            return id;
        }
    }
}
