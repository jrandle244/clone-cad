namespace CloneCAD.Common.DataHolders
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
    }
}
