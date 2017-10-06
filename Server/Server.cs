using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;
using CloneCAD.Server.DataHolders;
using CloneCAD.Server.DataHolders.Static;

namespace CloneCAD.Server
{
    public class Server
    {
        private readonly TcpListener Listener;
        private readonly StorableValue<CivilianDictionary> Civilians;
        private Dictionary<string, object> NetValueCivilians => Civilians.Value.Values.ToDictionary<Civilian, string, object>(civilian => "Civilian:" + civilian.ID, civilian => civilian);

        public Config Config;

        public Server(Config config, CivilianDictionary civilians)
        {
            Console.Title = "CloneCAD Server";

            Config = config;
            Civilians = new StorableValue<CivilianDictionary>(civilians);

            Listener = new TcpListener(IPAddress.Parse(Config["IP"]), Config.Port);
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

            NetRequestHandler handler = new NetRequestHandler(socket)
            {
                NetValues = NetValueCivilians
            };

            handler.NetFunctions.Add("ReserveCivilian", new NetFunction<object>(ReserveCivilian));
            handler.NetFunctions.Add("CheckPlate", new NetFunction<object>(CheckPlate));

            handler.NetEvents.Add("UpdateCivilian", new NetEvent(UpdateCivilian));

            /* old netcode
                    //Get civ
                    case 0:
                        id = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);

                        if (id == 0)
                        {
                            if (!Config.HasPerm(ip, Permission.Civ))
                                break;

                            civ = new Civilian(GetLowestID());
                            Log.WriteLine("Reserved civ #" + civ.CivID + ".", ip);

                            Civilians.Add(civ);
                            socket.Send(new byte[] { 0 }.Concat(civ.ToBytes()).ToArray());
                        }
                        else
                            try
                            {
                                if (!Config.HasPerm(ip, Permission.Civ) && !Config.HasPerm(ip, Permission.Dispatch))
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
                        if (!Config.HasPerm(ip, Permission.Civ))
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
                        if (!Config.HasPerm(ip, Permission.Dispatch))
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
                        if (!Config.HasPerm(ip, Permission.Police))
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
                        if (!Config.HasPerm(ip, Permission.Civ))
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
                        if (!Config.HasPerm(ip, Permission.Dispatch))
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
            */
        }

        #region netfunctions
        private async Task<object> ReserveCivilian(NetRequestHandler handler, object[] objs)
        {
            if (!Config.HasPerm(handler.IP, Permission.Civ))
                return null;

            Civilian civilian = new Civilian(GetRandomID());
            Log.WriteLine("Reserved civilian (" + civilian.ID + ").", handler.IP);

            Civilians.Value.Add(civilian);
            handler.NetValues.Add("Civilian:" + civilian.ID, civilian);

            await Task.FromResult(0);
            return civilian;
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

                handler.NetValues["Civilian:" + civilian.ID] = Civilians.Value[civilian.ID];

                Log.WriteLine("Updated civilian (" + civilian.ID + ").", handler.IP);
            }
            else
            {
                Civilians.Value.Add(civilian);
                handler.NetValues.Add("Civilian:" + civilian.ID, civilian);

                Log.WriteLine("Saved civilian (" + civilian.ID + ").", handler.IP);
            }

            await Task.FromResult(0);
        }

        private async Task<object> CheckPlate(NetRequestHandler arg1, object[] arg2)
        {
            throw new NotImplementedException();
        }
        #endregion

        private static uint GetRandomID()
        {
            byte[] buffer = new byte[32];
            Random random = new Random();
            random.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
