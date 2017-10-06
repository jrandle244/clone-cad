using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneCAD.Common.DataHolders
{
    public class Civilian
    {
        public ulong ID { get; }

        public string Name { get; set; }
        public string RegisteredPlate { get; set; }
        public List<string> RegisteredWeapons { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string AssociatedBusiness { get; set; }

        public Civilian(ulong ID, string Name = null, string RegisteredPlate = null, List<string> RegisteredWeapons = null, List<Ticket> Tickets = null, string AssociatedBusiness = null)
        {
            this.Name = Name;

            this.RegisteredPlate = RegisteredPlate;
            this.RegisteredWeapons = RegisteredWeapons ?? new List<string>();
            this.Tickets = Tickets ?? new List<Ticket>();
            this.AssociatedBusiness = AssociatedBusiness;
            this.ID = ID;
        }
    }
}
