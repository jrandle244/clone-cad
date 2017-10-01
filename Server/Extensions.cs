using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Extensions
    {
        public static string GetAlias(this List<Alias> aliases, string ip)
        {
            Alias a = aliases.Find(x => x.IP == ip);

            if (a == null)
                return ip;

            return a.Name;
        }
    }
}
