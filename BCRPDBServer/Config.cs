using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCRPDBServer
{
    public enum FilterType { None, Whitelist, Blacklist }
    public enum Permission { Civ, Police, Dispatch }

    public class Config
    {
        public string FilePath { get; private set; }

        public string IP { get; private set; }
        public int Port { get; private set; }
        public FilterType Filter { get; private set; }
        public string[] FilteredCivIPs { get; private set; }
        public string[] FilteredPoliceIPs { get; private set; }
        public string[] FilteredDispatchIPs { get; private set; }
        public string Log { get; private set; }
        public string Database { get; private set; }
        public List<Alias> Aliases;

        public Config(string FilePath)
        {
            this.FilePath = FilePath;
            Aliases = new List<Alias>();

            Refresh();
        }

        public void Refresh()
        {
            string[] lines = File.ReadAllLines(FilePath);
            Database = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Civilians.db");

            foreach (string[] line in lines.Where(x => !x.StartsWith(";")).Select(x => x.Split('=').Select(y => y.Trim()).ToArray()))
            {
                try
                {
                    switch (line[0])
                    {
                        case "IP":
                            IP = line[1];
                            break;

                        case "Port":
                            int _Port;

                            if (!int.TryParse(line[1], out _Port) || _Port < 1024 || _Port > 65536)
                            {
                                MessageBox.Show("The port is invalid.\nMake sure it is a positive integer within 1025-65535.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Environment.Exit(0);
                            }

                            Port = _Port;
                            break;

                        case "Filter":
                            int _Filter;

                            if (!int.TryParse(line[1], out _Filter) || _Filter < 0 || _Filter > 2)
                            {
                                MessageBox.Show("The filter type is invalid.\nMake sure it is within 0-2.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Environment.Exit(0);
                            }

                            Filter = (FilterType)_Filter;
                            break;

                        case "FilteredCivIPs":
                            if (string.IsNullOrWhiteSpace(line[1]))
                                FilteredCivIPs = new string[0];
                            else
                                FilteredCivIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                            break;

                        case "FilteredPoliceIPs":
                            if (string.IsNullOrWhiteSpace(line[1]))
                                FilteredPoliceIPs = new string[0];
                            else
                                FilteredPoliceIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                            break;

                        case "FilteredDispatchIPs":
                            if (string.IsNullOrWhiteSpace(line[1]))
                                FilteredDispatchIPs = new string[0];
                            else
                                FilteredDispatchIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                            break;

                        case "Log":
                            Log = line[1];
                            break;

                        case "Aliases":
                            if (string.IsNullOrWhiteSpace(line[1]))
                                Aliases = new List<Alias>();
                            else
                                Aliases = line[1].Split(',').Select(x => x.Trim()).Select(x => Alias.Parse(x)).ToList();
                            break;
                    }
                }
                catch
                {
                    MessageBox.Show("Fatal config error on key " + line[0] + ". Make sure it is properly configured.", "BCRPDB Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool HasPerm(string ip, Permission perm)
        {
            if (Filter == FilterType.None)
                return true;

            if (Filter == FilterType.Blacklist)
            {
                if (perm == Permission.Civ && !FilteredCivIPs.Contains(ip))
                    return true;

                if (perm == Permission.Police && !FilteredPoliceIPs.Contains(ip))
                    return true;

                if (perm == Permission.Dispatch && !FilteredDispatchIPs.Contains(ip))
                    return true;
            }
            
            if (Filter == FilterType.Whitelist)
            {
                if (perm == Permission.Civ && FilteredCivIPs.Contains(ip))
                    return true;

                if (perm == Permission.Police && FilteredPoliceIPs.Contains(ip))
                    return true;

                if (perm == Permission.Dispatch && FilteredDispatchIPs.Contains(ip))
                    return true;
            }

            return false;
        }
    }
}
