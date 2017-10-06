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
        private readonly Dictionary<string, object> CachedNetFunctions;
        private readonly Dictionary<string, object> CachedNetValues;

        public Dictionary<string, NetEvent> NetEvents { get; set; }
        public Dictionary<string, NetFunction<object>> NetFunctions { get; set; }
        public Dictionary<string, object> NetValues { get; set; }
        public string IP { get; }
        public int Port { get; }

        public NetRequestHandler(Socket socket)
        {
            string[] vals = socket.RemoteEndPoint.ToString().Split(':');
            IP = vals[0];
            Port = int.Parse(vals[1]);

            NetEvents = new Dictionary<string, NetEvent>();
            NetFunctions = new Dictionary<string, NetFunction<object>>();
            NetValues = new Dictionary<string, object>();

            CachedNetValues = new Dictionary<string, object>();
            CachedNetFunctions = new Dictionary<string, object>();

            S = socket;
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
                    case NetRequestMetadata.Invocation:
                        new Thread(() => HandleInvocation(netRequest).Wait());
                        break;

                    case NetRequestMetadata.ValueRequest:
                        new Thread(() => HandleValueRequest(netRequest).Wait());
                        break;

                    case NetRequestMetadata.ValueReturn:
                        new Thread(() => HandleValueReturn(netRequest).Wait());
                        break;

                    case NetRequestMetadata.FunctionRequest:
                        new Thread(() => HandleFunctionRequest(netRequest).Wait());
                        break;

                    case NetRequestMetadata.FunctionReturn:
                        new Thread(() => HandleFunctionReturn(netRequest).Wait());
                        break;
                }
            }
        }

        #region handlers

        private async Task<bool> HandleInvocation(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.Invocation)
                return false;

            string netEvent = (string) Request.Data.Value[0];
            object[] parameters = Request.Data.Value.Skip(1).ToArray();

            if (!NetEvents.ContainsKey(netEvent))
                return false;

            await NetEvents[netEvent].Invoke(parameters);

            return true;
        }

        private async Task<bool> HandleValueRequest(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.ValueRequest)
                return false;

            string valueName = (string) Request.Data.Value[0];

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

        private async Task<bool> HandleValueReturn(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.ValueReturn)
                return false;

            string valueName = (string) Request.Data.Value[0];
            bool existed = (bool) Request.Data.Value[1];

            if (!existed)
                return true;

            CachedNetValues.Add(valueName, Request.Data.Value[2]);

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleFunctionRequest(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.FunctionRequest)
                return false;

            string valueName = (string) Request.Data.Value[0];
            object[] parameters = Request.Data.Value.Skip(1).ToArray();

            if (!NetFunctions.ContainsKey(valueName))
            {
                try
                {
                    S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.FunctionReturn, valueName,
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
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.FunctionReturn, valueName, true,
                    NetFunctions[valueName].Invoke(parameters))).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleFunctionReturn(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.FunctionReturn)
                return false;

            string valueName = (string) Request.Data.Value[0];
            bool existed = (bool) Request.Data.Value[1];

            if (!existed)
                return true;

            CachedNetFunctions.Add(valueName, Request.Data.Value[2]);

            await Task.FromResult(0);
            return true;
        }

        #endregion

        public async Task<bool> TriggetNetEvent(string NetEventName, params object[] Parameters)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.Invocation, NetEventName,
                    Parameters)).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        public async Task<T> GetNetValue<T>(string NetValueName)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, NetValueName))
                    .Bytes);
            }
            catch (SocketException)
            {
                throw new InvalidOperationException("NetValue does not exist!");
            }

            while (!CachedNetValues.ContainsKey(NetValueName))
                await Task.Delay(10);

            return (T) CachedNetValues[NetValueName];
        }

        public async Task<Tuple<bool, T>> TryGetNetValue<T>(string NetValueName)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, NetValueName))
                    .Bytes);
            }
            catch (SocketException)
            {
                return new Tuple<bool, T>(false, (T) new object());
            }

            while (!CachedNetValues.ContainsKey(NetValueName))
                await Task.Delay(10);

            return new Tuple<bool, T>(true, (T) CachedNetValues[NetValueName]);
        }

        public async Task<T> GetNetFunction<T>(string NetFunctionName)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(
                    new NetRequest(NetRequestMetadata.FunctionRequest, NetFunctionName)).Bytes);
            }
            catch (SocketException)
            {
                throw new InvalidOperationException("NetFunction does not exist!");
            }

            while (!CachedNetFunctions.ContainsKey(NetFunctionName))
                await Task.Delay(10);

            return (T) CachedNetValues[NetFunctionName];
        }

        public async Task<Tuple<bool, T>> TryGetNetFunction<T>(string NetFunctionName)
        {
            try
            {
                S.Send(new StorableValue<NetRequest>(
                    new NetRequest(NetRequestMetadata.FunctionRequest, NetFunctionName)).Bytes);
            }
            catch (SocketException)
            {
                return new Tuple<bool, T>(false, (T) new object());
            }

            while (!CachedNetFunctions.ContainsKey(NetFunctionName))
                await Task.Delay(10);

            return new Tuple<bool, T>(true, (T) CachedNetValues[NetFunctionName]);
        }
    }
}
