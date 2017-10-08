using System;
using System.Collections.Generic;

namespace CloneCAD.Common.DataHolders
{
    [Serializable]
    public class Civilian
    {
        public uint ID { get; }

        public string Name { get; set; }
        public string RegisteredPlate { get; set; }
        public List<string> RegisteredWeapons { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string AssociatedBusiness { get; set; }

        public Civilian(uint id, string name = null, string registeredPlate = null, List<string> registeredWeapons = null, List<Ticket> tickets = null, string associatedBusiness = null)
        {
            Name = name;

            RegisteredPlate = registeredPlate;
            RegisteredWeapons = registeredWeapons ?? new List<string>();
            Tickets = tickets ?? new List<Ticket>();
            AssociatedBusiness = associatedBusiness;
            ID = id;
        }

        public override string ToString() =>
            ID.ToSplitID();
    }
}
