using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneCADServer
{
    public class Ticket
    {
        public ushort Price { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }

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
