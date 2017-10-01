using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Ticket
    {
        public ushort Price { get; }
        public string Type { get; }
        public string Description { get; }

        public Ticket(ushort Price, string Type, string Description)
        {
            this.Price = Price;
            this.Type = Type;
            this.Description = Description;
        }

        public override string ToString() =>
            Price + "~" + Type + "~" + Description;

        public static Ticket Parse(string str)
        {
            string[] vals = str.Split('~');

            return new Ticket(ushort.Parse(vals[0]), vals[1], vals[2]);
        }

        public static bool TryParse(string str, out Ticket ticket)
        {
            try { ticket = Parse(str); return true; } catch { ticket = null; return false; }
        }
    }
}
