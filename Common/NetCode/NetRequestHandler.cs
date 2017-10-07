using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using CloneCAD.Common.DataHolders;

namespace CloneCAD.Common.NetCode
{
    public class NetRequestHandler
    {
        private readonly Socket S;
        private readonly Dictionary<string, bool> CachedNetEvents;
        private readonly Dictionary<string, Tuple<bool, object>> CachedNetFunctions;
        private readonly Dictionary<string, Tuple<bool, object>> CachedNetValues;
        private readonly Dictionary<int, Thread> Threads;

        public Dictionary<string, NetEvent> NetEvents { get; set; }
        public Dictionary<string, NetFunction> NetFunctions { get; set; }
        public Dictionary<string, object> NetValues { get; set; }
        public string IP { get; }
        public int Port { get; }

        public NetRequestHandler(Socket socket)
        {
            string[] vals = socket.RemoteEndPoint.ToString().Split(':');
            IP = vals[0];
            Port = int.Parse(vals[1]);

            NetEvents = new Dictionary<string, NetEvent>();
            NetFunctions = new Dictionary<string, NetFunction>();
            NetValues = new Dictionary<string, object>();

            CachedNetValues = new Dictionary<string, Tuple<bool, object>>();
            CachedNetFunctions = new Dictionary<string, Tuple<bool, object>>();
            CachedNetEvents = new Dictionary<string, bool>();

            S = socket;
            Threads = new Dictionary<int, Thread>();
            Thread listenThread = new Thread(Listener);
            listenThread.Start();
        }

        private void Listener()
        {
            while (S.Connected)
            {
                byte[] buffer = new byte[5000];
                int end;
                try
                {
                    end = S.Receive(buffer);
                }
                catch (SocketException)
                {
                    break;
                }
                buffer = buffer.Take(end).ToArray();
                NetRequest netRequest = new StorableValue<NetRequest>(buffer).Value;

                switch (netRequest.Metadata)
                {
                    case NetRequestMetadata.InvocationRequest:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleInvocationRequest(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;

                    case NetRequestMetadata.InvocationReturn:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleInvocationReturn(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;

                    case NetRequestMetadata.ValueRequest:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleValueRequest(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;

                    case NetRequestMetadata.ValueReturn:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleValueReturn(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;

                    case NetRequestMetadata.FunctionRequest:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleFunctionRequest(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;

                    case NetRequestMetadata.FunctionReturn:
                        Threads.Add(Threads.Count, new Thread(() =>
                        {
                            HandleFunctionReturn(netRequest).Wait();
                            Threads.Remove(Threads.Count);
                        }));
                        break;
                }
            }
        }

        #region handlers

        private async Task<bool> HandleInvocationRequest(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.InvocationRequest)
                return false;

            string netEvent = (string) request.Data.Value[0];

            if (!NetEvents.ContainsKey(netEvent))
            {
                try
                {
                    S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.InvocationReturn, netEvent, false)).Bytes);
                }
                catch
                {
                    return false;
                }
            }

            object[] parameters = request.Data.Value.Skip(1).ToArray();

            await NetEvents[netEvent].Invoke(this, parameters);

            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueReturn, netEvent, true)).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleInvocationReturn(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.InvocationReturn)
                return false;

            string valueName = (string)request.Data.Value[0];
            bool existed = (bool)request.Data.Value[1];

            CachedNetEvents.Add(valueName, existed);

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleValueRequest(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.ValueRequest)
                return false;

            string valueName = (string) request.Data.Value[0];

            if (!NetValues.ContainsKey(valueName))
            {
                try
                {
                    S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueReturn, valueName,
                        false)).Bytes);
                }
                catch (SocketException)
                {
                    return false;
                }

                return true;
            }

            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueReturn, valueName, true,
                    NetValues[valueName])).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleValueReturn(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.ValueReturn)
                return false;

            string valueName = (string) request.Data.Value[0];
            bool existed = (bool) request.Data.Value[1];

            if (!existed)
            {
                CachedNetValues.Add(valueName, new Tuple<bool, object>(false, null));
                return true;
            }

            CachedNetValues.Add(valueName, new Tuple<bool, object>(true, request.Data.Value[2]));

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleFunctionRequest(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.FunctionRequest)
                return false;

            string functionName = (string) request.Data.Value[0];
            object[] parameters = request.Data.Value.Skip(1).ToArray();

            if (!NetFunctions.ContainsKey(functionName))
            {
                try
                {
                    S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.FunctionReturn, functionName,
                        false
                    )).Bytes);
                }
                catch (SocketException)
                {
                    return false;
                }

                return true;
            }

            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.FunctionReturn, functionName, true,
                    NetFunctions[functionName].Invoke(this, parameters))).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleFunctionReturn(NetRequest request)
        {
            if (request.Metadata != NetRequestMetadata.FunctionReturn)
                return false;

            string functionName = (string) request.Data.Value[0];
            bool existed = (bool) request.Data.Value[1];

            if (!existed)
            {
                CachedNetFunctions.Add(functionName, new Tuple<bool, object>(false, null));
                return true;
            }

            CachedNetFunctions.Add(functionName, new Tuple<bool, object>(true, request.Data.Value[2]));

            await Task.FromResult(0);
            return true;
        }

        #endregion

        public async Task<bool> TriggetNetEvent(string netEventName, params object[] parameters)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.InvocationRequest, netEventName,
                    parameters)).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        public async Task<T> GetNetValue<T>(string netValueName)
        {
            S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, netValueName))
                .Bytes);

            while (!CachedNetValues.ContainsKey(netValueName))
                await Task.Delay(10);

            Tuple<bool, T> tuple = new Tuple<bool, T>(CachedNetValues[netValueName].Item1, (T)CachedNetValues[netValueName].Item2);
            CachedNetFunctions.Remove(netValueName);

            if (!tuple.Item1)
                throw new InvalidOperationException("The NetValue does not exist!");

            return tuple.Item2;
        }

        public async Task<Tuple<bool, T>> TryGetNetValue<T>(string netValueName)
        {
            S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, netValueName))
                .Bytes);

            while (!CachedNetValues.ContainsKey(netValueName))
                await Task.Delay(10);
            
            Tuple<bool, T> tuple = new Tuple<bool, T>(CachedNetValues[netValueName].Item1, (T) CachedNetValues[netValueName].Item2);
            CachedNetValues.Remove(netValueName);
            return tuple;
        }

        public async Task<T> TriggerNetFunction<T>(string netFunctionName, params object[] parameters)
        {
            S.Send(new StorableValue<NetRequest>(
                new NetRequest(NetRequestMetadata.FunctionRequest, netFunctionName, parameters)).Bytes);

            while (!CachedNetFunctions.ContainsKey(netFunctionName))
                await Task.Delay(10);

            Tuple<bool, T> tuple = new Tuple<bool, T>(CachedNetFunctions[netFunctionName].Item1, (T)CachedNetValues[netFunctionName].Item2);
            CachedNetFunctions.Remove(netFunctionName);

            if (!tuple.Item1)
                throw new InvalidOperationException("The NetFunction does not exist!");

            return tuple.Item2;
        }

        public async Task<Tuple<bool, T>> TryTriggerNetFunction<T>(string netFunctionName, params object[] parameters)
        {
            S.Send(new StorableValue<NetRequest>(
                new NetRequest(NetRequestMetadata.FunctionRequest, netFunctionName, parameters)).Bytes);

            while (!CachedNetFunctions.ContainsKey(netFunctionName))
                await Task.Delay(10);

            Tuple<bool, T> tuple = new Tuple<bool, T>(CachedNetFunctions[netFunctionName].Item1, (T)CachedNetValues[netFunctionName].Item2);
            CachedNetFunctions.Remove(netFunctionName);
            return tuple;
        }
    }
}
