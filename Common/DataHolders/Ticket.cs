using System;using System.Runtime.CompilerServices;

namespace CloneCAD.Common.DataHolders
{
    public enum TicketType { Fixit, Warning, Citation, Ticket }

    [Serializable]
    public class Ticket
    {
        public ushort Price { get; }
        public TicketType Type { get; }
        public string Description { get; }

        public Ticket(ushort price, TicketType type, string description)
        {
            Price = price;
            Type = type;
            Description = description;
        }
    }
}
