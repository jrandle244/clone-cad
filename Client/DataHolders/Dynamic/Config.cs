﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Client.DataHolders.Dynamic
{
    public class Config
    {
        public string IP { get; }
        public int Port { get; }

        public Config(string FilePath)
        {
            string[] lines = File.ReadAllLines(FilePath);

            foreach (string[] line in lines.Where(x => !x.StartsWith(";")).Select(x => x.Split('=').Select(y => y.Trim()).ToArray()))
                switch (line[0])
                {
                    case "IP":
                        if (line[1] == "changeme")
                        {
                            MessageBox.Show("Looks like you forgot to change the config.\nPlease edit your config and then come back ༼ つ ◕_◕ ༽つ", "Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }

                        IP = line[1];
                        break;

                    case "Port":
                        int _Port;

                        if (!int.TryParse(line[1], out _Port) || _Port < 1024 || _Port > 65536)
                        {
                            MessageBox.Show("The port is invalid.\nMake sure it is a positive integer within 1025-65535.", "Client Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }

                        Port = _Port;
                        break;
                }
        }
    }
}
