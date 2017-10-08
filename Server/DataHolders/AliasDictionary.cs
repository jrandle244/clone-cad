using CloneCAD.Common.DataHolders;

using System.Collections.Generic;
using System.Linq;

namespace CloneCAD.Server.DataHolders
{
    public class AliasDictionary : Dictionary<string, string>
    {
        public LocaleConfig Locale;

        public AliasDictionary(LocaleConfig Locale) : base()
        {
            this.Locale = Locale;
        }

        public AliasDictionary(LocaleConfig Locale, string List) : base()
        {
            this.Locale = Locale;

            Load(List);
        }

        public void Load(string List)
        {
            Clear();

            foreach (string element in List.Split(',').Select(x => x.Trim()))
            {
                string[] vals = element.Split(':').Select(x => x.Trim()).ToArray();

                if (vals.Length != 2)
                    ServerFunctions.Error(Locale, "InvalidAlias", 1);

                Add(vals[0], vals[1]);
            }
        }

        public new string this[string IP]
        {
            get => ContainsKey(IP) ? base[IP] : IP;
            set
            {
                if (ContainsKey(IP))
                    base[IP] = value;
                else
                    Add(IP, value);
            }
        }
    }
}
