using CloneCAD.Common.DataHolders;

namespace CloneCAD.Common.NetCode
{
    public enum NetRequestMetadata { InvocationRequest, InvocationReturn, ValueRequest, ValueReturn, FunctionRequest, FunctionReturn }

    public class NetRequest
    {
        public StorableValue<object[]> Data { get; }
        public NetRequestMetadata Metadata { get; set; }

        public NetRequest(NetRequestMetadata metadata, params object[] data)
        {
            Metadata = metadata;
            Data = new StorableValue<object[]>(data);
        }
    }
}
