using CloneCAD.Common.DataHolders;

using System;
using System.Windows.Forms;

namespace CloneCAD.Client.DataHolders
{
    public class Config : GenericConfig
    {
        public string IP => this["IP"];
        public int Port { get; private set; }
        public LocaleConfig Locale { get; private set; }

        public Config(string FilePath) : base(FilePath)
        {
            try
            {
                Load();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("The config file has reoccuring values. Please remove the reoccuring values.");
                Environment.Exit(1);
            }
        }

        public new void Load()
        {
            base.Load();

            if (!Contains("Locale"))
            {
                MessageBox.Show("There is no locale value. You must set it to something.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            Locale = new LocaleConfig(this["Locale"]);

            if (!Contains("IP") || this["IP"] == "changeme")
            {
                MessageBox.Show(Locale["ChangeMeIP"], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            if (!Contains("Port") || !int.TryParse(this["Port"], out int port) || port < 1024 || port > 65536)
            {
                MessageBox.Show(Locale["InvalidPort"], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            else
                Port = port;
        }
    }
}
