using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneCADServer
{
    public class Alias
    {
        public string IP { get; private set; }
        public string Name { get; private set; }

        public Alias(string IP, string Name)
        {
            this.IP = IP;
            this.Name = Name;
        }

        public static Alias Parse(string str)
        {
            string[] vals = str.Split(':');

            return new Alias(vals[0], vals[1]);
        }

        public static bool TryParse(string str, out Alias alias)
        {
            try { alias = Parse(str); return true; } catch { alias = null; return false; }
        }
    }
}
