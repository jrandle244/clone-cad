﻿using System;
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
        public List<KeyValuePair<string, string>> Tickets { get; set; }
        public string AssociatedBusiness { get; set; }
        public uint BusinessCooldown { get; set; }

        public Civ(ushort CivID, string Name = null, string RegisteredPlate = null, List<string> RegisteredWeapons = null, List<KeyValuePair<string, string>> Tickets = null, string AssociatedBusiness = null, uint BusinessCooldown = 0)
        {
            this.Name = Name;

            this.RegisteredPlate = RegisteredPlate;
            this.RegisteredWeapons = RegisteredWeapons ?? new List<string>();
            this.Tickets = Tickets ?? new List<KeyValuePair<string, string>>();
            this.AssociatedBusiness = AssociatedBusiness ?? null;
            this.BusinessCooldown = BusinessCooldown;
            this.CivID = CivID;
        }

        public override string ToString()
        {
            string name = string.IsNullOrWhiteSpace(Name) ? "" : "|a" + Name;
            string plate = string.IsNullOrWhiteSpace(RegisteredPlate) ? "" : "|b" + RegisteredPlate;
            string weps = RegisteredWeapons.Count == 0 ? "" : "|c" + string.Join(",", RegisteredWeapons);
            string tickets = Tickets.Count == 0 ? "" : "|d" + string.Join(",", Tickets.Select(x => x.Key + "~" + x.Value));
            string business = string.IsNullOrWhiteSpace(AssociatedBusiness) ? "" : "|e" + AssociatedBusiness;
            string businessCooldown = BusinessCooldown == 0 ? "" : "|f" + BusinessCooldown;

            return CivID + name + plate + weps + business + businessCooldown;
        }

        public byte[] ToBytes() =>
            Encoding.UTF8.GetBytes(ToString());

        public static Civ ToCiv(byte[] bytes) =>
            Parse(Encoding.UTF8.GetString(bytes), DateTime.Now);

        public static Civ Parse(string str, DateTime writeDate)
        {
            List<string> vals = str.Split('|').ToList();
            List<string> dataVals = vals.Skip(1).ToList();

            string name = str.Contains("|a") ? GetVal(dataVals, "a") : null;
            string plate = str.Contains("|b") ? GetVal(dataVals, "b") : null;
            List<string> weps = str.Contains("|c") ? GetVal(dataVals, "c").Split(',').ToList() : null;
            List<KeyValuePair<string, string>> tickets = str.Contains("|d") ? GetVal(dataVals, "d").Split(',').Select(x => new KeyValuePair<string, string>(x.Split('~')[0], x.Split('~')[1])).ToList() : null;
            string business = str.Contains("|e") ? GetVal(dataVals, "e") : null;
            uint businessCooldown;

            try { businessCooldown = str.Contains("|f") ? SubtractNoOverflow(uint.Parse(GetVal(dataVals, "f")), (uint)DateTime.Now.Subtract(writeDate).TotalSeconds) : 0; }
            catch { businessCooldown = 0; }

            return new Civ(ushort.Parse(vals[0]), name, plate, weps, tickets, business, businessCooldown);
        }

        public static bool TryParse(string str, DateTime writeDate, out Civ Civ)
        {
            try { Civ = Parse(str, writeDate); return true; } catch { Civ = new Civ(0); return false; }
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
