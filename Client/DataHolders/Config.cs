using CloneCAD.Common.DataHolders;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloneCAD.Client.DataHolders
{
    public class Config : GenericConfig
    {
        public string IP => this["IP"];
        public int Port { get; private set; }
        public LocaleConfig Locale { get; private set; }

        public Config(string filePath) : base(new List<string>
        {
            "Locale",
            "IP",
            "Port"
        })
        {
            Path = filePath;

            Load();
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

            if (this["IP"] == "changeme")
            {
                MessageBox.Show(Locale["ChangeMeIP"], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            if (!int.TryParse(this["Port"], out int port) || port < 1024 || port > 65536)
            {
                MessageBox.Show(Locale["InvalidPort"], "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            else
                Port = port;
        }
    }
}
