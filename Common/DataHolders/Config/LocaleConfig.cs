using System.Collections.Generic;

namespace CloneCAD.Common.DataHolders
{
    public class LocaleConfig : GenericConfig
    {
        private readonly Dictionary<string, string> ConfigCache;

        public LocaleConfig() : base()
        {
            ConfigCache = new Dictionary<string, string>();
        }

        public LocaleConfig(string file) : base(file)
        {
            ConfigCache = new Dictionary<string, string>();
        }

        public LocaleConfig(List<string> requiredKeys) : base(requiredKeys)
        {
            ConfigCache = new Dictionary<string, string>();
        }

        public LocaleConfig(List<string> requiredKeys, string path) : base(requiredKeys, path)
        {
            ConfigCache = new Dictionary<string, string>();
        }

        public new void Load()
        {
            base.Load();
            ConfigCache.Clear();
        }

        public new string this[string key]
        {
            get
            {
                if (!Contains(key))
                    return "LOCALE ERROR (" + key + ")";

                if (ConfigCache.ContainsKey(key))
                    return ConfigCache[key];

                ConfigCache.Add(key, base[key].Replace("\\n", "\n"));
                return ConfigCache[key];
            }
        }

        public string this[string key, params object[] objs]
        {
            get
            {
                if (!Contains(key))
                    return "LOCALE ERROR (" + key + ")";

                if (ConfigCache.ContainsKey(key))
                    return string.Format(ConfigCache[key], objs);

                ConfigCache.Add(key, base[key].Replace("\\n", "\n"));
                return string.Format(ConfigCache[key], objs);
            }
        }
    }
}
