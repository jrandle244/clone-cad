using BCRPDB.Properties;
using MaterialSkin;
using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace BCRPDB
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
                CivMenu civ = new CivMenu(10);
                civ.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a)
                {
                    Visible = true;
                    SkinManager.ColorScheme = scheme;
                    SkinManager.Theme = theme;
                });

                civ.Show(this);
                civ.Sync(false);

                new Thread(new ThreadStart(() =>
                {
                    while (!civ.closed)
                        Thread.Sleep(10);

                    if (close.Checked)
                        Invoke((MethodInvoker)delegate { Close(); });
                })).Start();
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
            }
        }
    }
}
