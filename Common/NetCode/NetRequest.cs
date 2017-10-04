﻿using CloneCAD.Common.DataHolders;

namespace CloneCAD.Common.NetCode
{
    public enum NetRequestMetadata { Invocation, ValueRequest, ValueReturn }

    public class NetRequest
    {
        public StorableValue<object[]> Data { get; }
        public NetRequestMetadata Metadata { get; set; }

        public NetRequest(NetRequestMetadata Metadata, params object[] Data)
        {
            this.Metadata = Metadata;
            this.Data = new StorableValue<object[]>(Data);

        }

        public NetRequest(byte[] Bytes)
        {
            NetRequest convertedRequest = new StorableValue<NetRequest>(Bytes).Value;

            Metadata = convertedRequest.Metadata;
            Data = convertedRequest.Data;
        }
    }
}
