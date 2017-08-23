using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCRPDB
{
    public class Civ
    {
        public ushort CivID { get; private set; }

        public string Name { get; set; }
        public string RegisteredPlate { get; set; }
        public List<string> RegisteredWeapons { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string AssociatedBusiness { get; set; }

        public Civ(ushort CivID, string Name = null, string RegisteredPlate = null, List<string> RegisteredWeapons = null, List<Ticket> Tickets = null, string AssociatedBusiness = null)
        {
            this.Name = Name;

            this.RegisteredPlate = RegisteredPlate;
            this.RegisteredWeapons = RegisteredWeapons ?? new List<string>();
            this.Tickets = Tickets ?? new List<Ticket>();
            this.AssociatedBusiness = AssociatedBusiness ?? null;
            this.CivID = CivID;
        }

        public override string ToString()
        {
            string name = string.IsNullOrWhiteSpace(Name) ? "" : "|a" + Name;
            string plate = string.IsNullOrWhiteSpace(RegisteredPlate) ? "" : "|b" + RegisteredPlate;
            string weps = RegisteredWeapons.Count == 0 ? "" : "|c" + string.Join(",", RegisteredWeapons);
            string tickets = Tickets.Count == 0 ? "" : "|d" + string.Join(",", Tickets.Select(x => x.Price + "," + x.Type + "," + x.Description));
            string business = string.IsNullOrWhiteSpace(AssociatedBusiness) ? "" : "|e" + AssociatedBusiness;

            return CivID + name + plate + weps + business;
        }

        public byte[] ToBytes() =>
            Encoding.UTF8.GetBytes(ToString());

        public static Civ ToCiv(byte[] bytes) =>
            Parse(Encoding.UTF8.GetString(bytes));

        public static Civ Parse(string str)
        {
            List<string> vals = str.Split('|').ToList();
            List<string> dataVals = vals.Skip(1).ToList();

            string name = str.Contains("|a") ? GetVal(dataVals, "a") : null;
            string plate = str.Contains("|b") ? GetVal(dataVals, "b") : null;
            List<string> weps = str.Contains("|c") ? GetVal(dataVals, "c").Split(',').ToList() : null;
            List<Ticket> tickets = str.Contains("|d") ? GetVal(dataVals, "d").Split(',').Select(x => Ticket.Parse(x)).ToList() : null;
            string business = str.Contains("|e") ? GetVal(dataVals, "e") : null;

            return new Civ(ushort.Parse(vals[0]), name, plate, weps, tickets, business);
        }

        public static bool TryParse(string str, out Civ Civ)
        {
            try { Civ = Parse(str); return true; } catch { Civ = new Civ(0); return false; }
        }

        private static string GetVal(List<string> vals, string key) =>
            vals.Find(x => x.StartsWith(key)).Substring(key.Length);

        private static uint SubtractNoOverflow(uint uint1, uint uint2)
        {
            uint returnVal;

            if ((returnVal = uint1 - uint2) > uint1)
                returnVal = 0;

            return returnVal;
        }
    }
}
