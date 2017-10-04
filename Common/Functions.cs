﻿using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace CloneCAD.Common
{
    public static class Functions
    {
        public static void TriggerNetEvent(this Socket Socket, string NetEventName, params object[] NetEventParameters)
        {
            StorableValue<NetRequest> request = new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.Invocation, NetEventName, NetEventParameters));
            Socket.Send(request.Bytes);
        }

        public static T RequestValue<T>(this Socket Socket, string ValueName, NetRequestHandler NetEventHandler)
        {
            StorableValue<NetRequest> request = new StorableValue<NetRequest>(new NetRequest(NetRequestMetadata.Invocation, ValueName, null));
            Socket.Send(request.Bytes);

            byte[] buffer = new byte[5000];
            int end = Socket.Receive(buffer);
            buffer = buffer.Take(end).ToArray();

            NetRequest netRequest = new NetRequest(buffer);
            if (netRequest.Metadata == NetRequestMetadata.ValueReturn)
                return (T)netRequest.Data.Value[0];

            NetEventHandler.Handle(netRequest);
        }

        public static void ExceptionHandlerBackend(this Exception e, int ExitCode)
        {
            string errorFile = "error0.dump";

            {
                ushort reiteration = 0;

                while (File.Exists(errorFile))
                    errorFile = "error" + ++reiteration + ".dump";
            }

            Environment.Exit(ExitCode);
        }
    }
}
