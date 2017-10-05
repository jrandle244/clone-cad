using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloneCAD.Common.DataHolders
{
    public class GenericConfig
    {
        private readonly Dictionary<string, string> values;
        
        private List<string> requiredKeys;
        public List<string> RequiredKeys
        {
            get => requiredKeys;
            set
            {
                requiredKeys = value;

                requiredKeys.ForEach(x =>
                {
                    if (!Contains(x))
                        throw new InvalidOperationException("Current locale does not contain key " + x);
                });
            }
        }

        public string Path { get; set; }

        public GenericConfig()
        {
            values = new Dictionary<string, string>();
            requiredKeys = new List<string>();
        }

        public GenericConfig(List<string> RequiredKeys) : this()
        {
            requiredKeys = RequiredKeys;
        }

        public GenericConfig(string Path) : this()
        {
            this.Path = Path;

            Load();
        }

        public GenericConfig(List<string> RequiredKeys, string Path) : this(RequiredKeys)
        {
            this.Path = Path;

            Load();
        }

        public bool Contains(string key) =>
            values.ContainsKey(key);

        public void Load()
        {
            if (string.IsNullOrWhiteSpace(Path))
                throw new InvalidOperationException("Cannot load from a file if the " + nameof(Path) + " property is null!");

            if (!File.Exists(Path))
                throw new InvalidOperationException("The file does not exist!");

            StreamReader reader = new StreamReader(Path);

            while (!reader.EndOfStream)
            {
                string raw = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(raw) || raw.StartsWith(";"))
                    continue;

                string[] rawVals = raw.Split('=');
                rawVals = new[] { rawVals[0], string.Join(" ", rawVals.Skip(1)) }.Select(x => x.Trim()).ToArray();

                if (string.IsNullOrWhiteSpace(rawVals[0]))
                    continue;

                if (values.ContainsKey(rawVals[0]))
                    throw new InvalidOperationException("The config has already loaded an element with the same key.");

                values.Add(rawVals[0], string.IsNullOrWhiteSpace(rawVals[1]) ? null : rawVals[1]);
            }

            requiredKeys.ForEach(x =>
            {
                if (!Contains(x))
                    throw new InvalidOperationException("Current locale does not contain key " + x);
            });
        }

        public string this[string key] => values[key];
    }
}
