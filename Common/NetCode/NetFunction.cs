using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneCAD.Common.NetCode
{
    public class NetFunction<T>
    {
        private Func<NetRequestHandler, object[], Task<T>> Callback;

        public NetFunction(Func<NetRequestHandler, object[], Task<T>> functionCallback) =>
            Callback = functionCallback;

        public static NetFunction<T> operator +(NetFunction<T> netProperty, Func<NetRequestHandler, object[], Task<T>> functionCallback)
        {
            netProperty.Callback = (functionCallback);
            return netProperty;
        }

        public async Task<T> Invoke(NetRequestHandler handler, object[] args) =>
            await Callback.Invoke(handler, args);
    }
}
