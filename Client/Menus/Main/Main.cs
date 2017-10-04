using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Client.DataHolders;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class Main : MaterialForm
    {
        private Config cfg;
        private ColorScheme scheme;
        private MaterialSkinManager.Themes theme;

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
                CivLauncher civ = new CivLauncher(cfg);
                civ.FormClosed += (Sender, Args) =>
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                };

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
                PopoMenu popo = new PopoMenu(cfg);
                popo.FormClosed += (Sender, Args) =>
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                };

                popo.ShowDialog();

                if (close.Checked)
                    Close();
            }
            else if (dispatch.Checked)
            {
                DispatchMenu dis = new DispatchMenu(cfg);
                dis.FormClosed += (Sender, Args) =>
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                };

                dis.ShowDialog();

                if (close.Checked)
                    Close();
            }
        }
    }
}
