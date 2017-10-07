using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloneCAD.Common.DataHolders
{
    public class GenericConfig
    {
        private readonly Dictionary<string, string> Values;
        
        private List<string> _RequiredKeys;
        public List<string> RequiredKeys
        {
            get => _RequiredKeys;
            set
            {
                _RequiredKeys = value;

                _RequiredKeys.ForEach(x =>
                {
                    if (!Contains(x))
                        throw new InvalidOperationException("Current locale does not contain key " + x);
                });
            }
        }

        public string Path { get; set; }

        public GenericConfig()
        {
            Values = new Dictionary<string, string>();
            _RequiredKeys = new List<string>();
        }

        public GenericConfig(List<string> requiredKeys) : this()
        {
            _RequiredKeys = requiredKeys;
        }

        public GenericConfig(string path) : this()
        {
            Path = path;

            Load();
        }

        public GenericConfig(List<string> requiredKeys, string path) : this(requiredKeys)
        {
            Path = path;

            Load();
        }

        public bool Contains(string key) =>
            Values.ContainsKey(key);

        public void Load()
        {
            if (string.IsNullOrWhiteSpace(Path))
                throw new NullReferenceException("Cannot load from a file if the " + nameof(Path) + " property is null!");

            if (!File.Exists(Path))
                throw new FileNotFoundException("The file does not exist!");

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

                if (Values.ContainsKey(rawVals[0]))
                    Values[rawVals[0]] = rawVals[1];

                Values.Add(rawVals[0], string.IsNullOrWhiteSpace(rawVals[1]) ? null : rawVals[1]);
            }

            reader.Close();

            _RequiredKeys.ForEach(x =>
            {
                if (!Contains(x))
                    throw new KeyNotFoundException("Current locale does not contain key " + x);
            });
        }

        public string this[string key] => Values[key];
    }
}
