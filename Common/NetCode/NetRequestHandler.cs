using System.Collections.Generic;

namespace CloneCAD.Common.NetCode
{
    public class NetRequestHandler
    {
        public Dictionary<string, NetEvent> NetEvents { get; }

        public NetRequestHandler()
        {
            NetEvents = new Dictionary<string, NetEvent>();
        }

        public void Handle(NetRequest Request)
        {
            switch (Request.Metadata)
            {
                case NetRequestMetadata.Invocation:
                    NetEvents[(string)Request.Data[0]].Invoke(Request.Data)
                    break;
            }
        }
    }
}
