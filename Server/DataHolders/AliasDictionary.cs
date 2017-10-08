using CloneCAD.Common.DataHolders;

using System.Collections.Generic;
using System.Linq;

namespace CloneCAD.Server.DataHolders
{
    public class AliasDictionary : Dictionary<string, string>
    {
        private ErrorHandler Handler;
        public LocaleConfig Locale;

        public AliasDictionary(LocaleConfig locale) : base()
        {
            Locale = locale;
            Handler = new ErrorHandler(locale);
        }

        public AliasDictionary(LocaleConfig locale, string list) : this(locale) => 
            Load(list);

        public void Load(string list)
        {
            Clear();

            foreach (string element in list.Split(',').Select(x => x.Trim()))
            {
                string[] vals = element.Split(':').Select(x => x.Trim()).ToArray();

                if (vals.Length != 2)
                    Handler.Error("InvalidAlias", 1);

                Add(vals[0], vals[1]);
            }
        }

        public new string this[string ip]
        {
            get => ContainsKey(ip) ? base[ip] : ip;
            set
            {
                if (ContainsKey(ip))
                    base[ip] = value;
                else
                    Add(ip, value);
            }
        }
    }
}
