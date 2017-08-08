using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCRPDBServer
{
    public enum ClientFilterType { None, Whitelist, Blacklist }

    public class Config
    {
        public string FilePath { get; private set; }

        public string IP { get; private set; }
        public int Port { get; private set; }
        public ClientFilterType Filter { get; private set; }
        public string[] FilteredIPs { get; private set; }
        public string Log { get; private set; }

        public Config(string FilePath)
        {
            this.FilePath = FilePath;

            Refresh();
        }

        public void Refresh()
        {
            string[] lines = File.ReadAllLines(FilePath);

            foreach (string[] line in lines.Where(x => !x.StartsWith(";")).Select(x => x.Split('=').Select(y => y.Trim()).ToArray()))
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

                        Filter = (ClientFilterType)_Filter;
                        break;

                    case "FilteredIPs":
                        if (string.IsNullOrWhiteSpace(line[1]))
                            FilteredIPs = new string[0];
                        else
                            FilteredIPs = line[1].Split(',').Select(x => x.Trim()).ToArray();
                        break;

                    case "Log":
                        Log = line[1];
                        break;
                }
        }
    }
}
