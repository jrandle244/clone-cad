using Client.Properties;
using MaterialSkin;
using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace Client
{
    public partial class Main : MaterialForm
    {
        public static Config cfg;
        public ColorScheme scheme;
        public MaterialSkinManager.Themes theme;

        public Main()
        {
            cfg = new Config("settings.ini");
            scheme = SkinManager.ColorScheme;
            theme = SkinManager.Theme;

            InitializeComponent();
        }

        private void launch_Click(object sender, EventArgs e)
        {
            Visible = false;

            if (civ.Checked)
            {
                CivLauncher civ = new CivLauncher();
                civ.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a)
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                });

                civ.Show();
                if (File.Exists("ids.cfg") && !string.IsNullOrWhiteSpace(File.ReadAllText("ids.cfg")))
                    civ.Sync(File.ReadAllText("ids.cfg").Split(',').Select(x => ushort.Parse(x.Trim())).ToArray());

                ThreadPool.QueueUserWorkItem(x =>
                {
                    while (!civ.closed)
                        Thread.Sleep(10);

                    Invoke((MethodInvoker)delegate
                    {
                        if (close.Checked)
                            Close();
                    });
                });
            }
            else if (popo.Checked)
            {
                PopoMenu popo = new PopoMenu();
                popo.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a)
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                });

                popo.ShowDialog();

                if (close.Checked)
                    Close();
            }
            else if (dispatch.Checked)
            {
                DispatchMenu dis = new DispatchMenu();
                dis.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a)
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                });

                dis.ShowDialog();

                if (close.Checked)
                    Close();
            }
        }
    }
}
