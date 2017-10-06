using System;
using System.Collections.Generic;

namespace CloneCAD.Common.DataHolders
{
    public class LocaleConfig : GenericConfig
    {
        private readonly Dictionary<string, string> configCache;

        public LocaleConfig() : base()
        {
            configCache = new Dictionary<string, string>();
        }

        public LocaleConfig(string File) : base(File)
        {
            configCache = new Dictionary<string, string>();
        }

        public LocaleConfig(List<string> RequiredKeys) : base(RequiredKeys)
        {
            configCache = new Dictionary<string, string>();
        }

        public LocaleConfig(List<string> RequiredKeys, string Path) : base(RequiredKeys, Path)
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
