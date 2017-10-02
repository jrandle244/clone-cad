namespace Server.DataHolders.Static
{
    public class Alias
    {
        public string IP { get; }
        public string Name { get; }

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
