using System.Collections.Generic;
using Server.DataHolders.Static;

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
