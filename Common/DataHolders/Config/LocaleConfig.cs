using System;
using System.Collections.Generic;

namespace CloneCAD.Common.DataHolders
{
    public class LocaleConfig : GenericConfig
    {
        private readonly Dictionary<string, string> configCache;

        private List<string> requiredKeys;
        public List<string> RequiredKeys
        {
            get => requiredKeys;
            set
            {
                requiredKeys = value;

                foreach (string key in requiredKeys)
                    if (!Contains(key))
                        throw new InvalidOperationException("Current locale does not contain key " + key);
            }
        }

        public LocaleConfig() : base()
        {
            configCache = new Dictionary<string, string>();
        }

        public LocaleConfig(string File) : base(File)
        {
            configCache = new Dictionary<string, string>();
        }

        public new void Load()
        {
            base.Load();
            configCache.Clear();
        }

        public new string this[string key]
        {
            get
            {
                if (!Contains(key))
                    return "LOCALE ERROR";

                if (configCache.ContainsKey(key))
                    return configCache[key];

                configCache.Add(key, base[key].Replace("\\n", "\n"));
                return configCache[key];
            }
        }
    }
}
