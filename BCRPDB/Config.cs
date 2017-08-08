using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCRPDB
{
    public class Config
    {
        public string IP { get; private set; }
        public int Port { get; private set; }

        public Config(string FilePath)
        {
            string[] lines = File.ReadAllLines(FilePath);

            foreach (string[] line in lines.Where(x => !x.StartsWith(";")).Select(x => x.Split('=').Select(y => y.Trim())))
                switch (line[0])
                {
                    case "IP":
                        IP = line[1];
                        break;

                    case "Port":
                        int _Port;

                        if (int.TryParse(line[1], out _Port) || _Port < 1024 || _Port > 65536)
                        {
                            MessageBox.Show("The port is not valid.\nMake sure it is a positive integer above 1024 and below 65536.", "BCRPDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(0);
                        }

                        Port = _Port;
                        break;
                }
        }
    }
}
