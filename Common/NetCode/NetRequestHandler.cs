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
        private Socket s;
        private Thread listenThread;

        public Dictionary<string, NetEvent> NetEvents { get; }
        public Dictionary<string, object> HostedNetValues { get; }
        public Dictionary<string, object> CachedNetValues { get; }

        public NetRequestHandler(Socket Socket)
        {
            NetEvents = new Dictionary<string, NetEvent>();
            HostedNetValues = new Dictionary<string, dynamic>();
            CachedNetValues = new Dictionary<string, object>();

            s = Socket;
            listenThread = new Thread(listener);
            listenThread.Start();
        }

        private void listener()
        {
            while (s.Connected)
            {
                byte[] buffer = new byte[5000];
                int end;
                try
                {
                    end = s.Receive(buffer);
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
                        HandleInvocation(netRequest).Wait();
                        break;

                    case NetRequestMetadata.ValueRequest:
                        HandleValueRequest(netRequest).Wait();
                        break;

                    case NetRequestMetadata.ValueReturn:
                        HandleValueReturned(netRequest).Wait();
                        break;
                }
            }
        }

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

            if (!HostedNetValues.ContainsKey(valueName))
            {
                try
                {
                    s.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueReturn, valueName, false)).Bytes);
                }
                catch (SocketException)
                {
                    return false;
                }

                return true;
            }

            try
            {
                s.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueReturn, valueName, true,
                    HostedNetValues[valueName])).Bytes);
            }
            catch (SocketException)
            {
                return false;
            }

            await Task.FromResult(0);
            return true;
        }

        private async Task<bool> HandleValueReturned(NetRequest Request)
        {
            if (Request.Metadata != NetRequestMetadata.ValueReturn)
                return false;

            string valueName = (string) Request.Data.Value[0];
            bool existed = (bool) Request.Data.Value[1];

            if (!existed)
                return true;

            object value = Request.Data.Value[2];

            CachedNetValues.Add(valueName, value);

            await Task.FromResult(0);
            return true;
        }

        public async Task<bool> TriggetNetEvent(string NetEventName, params object[] Parameters)
        {
            try
            {
                s.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.Invocation, NetEventName, Parameters)).Bytes);
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
                s.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, NetValueName)).Bytes);
            }
            catch (SocketException)
            {
                throw new InvalidOperationException("NetValue does not exist!");
            }

            while (!CachedNetValues.ContainsKey(NetValueName))
                await Task.Delay(10);

            return (T)CachedNetValues[NetValueName];
        }

        public async Task<bool> TryGetNetValue<T>(string NetValueName, out T NetValue)
        {
            try
            {
                s.Send(new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.ValueRequest, NetValueName)).Bytes);
            }
            catch (SocketException)
            {
                throw new InvalidOperationException("NetValue does not exist!");
            }

            while (!CachedNetValues.ContainsKey(NetValueName))
                await Task.Delay(10);

            return (T)CachedNetValues[NetValueName];
        }
    }
}
