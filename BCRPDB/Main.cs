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

        public Main()
        {
            cfg = new Config("settings.ini");

            InitializeComponent();
        }

        private void launch_Click(object sender, EventArgs e)
        {
            Visible = false;

            if (civ.Checked)
            {
                CivMenu civ = new CivMenu(Settings.Default.CivID);
                civ.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a) { Visible = true; });

                civ.Show(this);
                civ.Sync(false);

                new Thread(new ThreadStart(() =>
                {
                    while (!civ.closed)
                        Thread.Sleep(10);

                    if (close.Checked)
                        {
                            Settings.Default.CivID = civ.ID;
                            Settings.Default.Save();
                            Invoke((MethodInvoker)delegate { Close(); });
                        }
                })).Start();
            }
            else if (popo.Checked)
            {
                PopoMenu popo = new PopoMenu();
                popo.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a) { Visible = true; });

                popo.Show(this);

                new Thread(new ThreadStart(() =>
                {
                    while (!popo.closed)
                        Thread.Sleep(10);

                    if (close.Checked)
                        Invoke((MethodInvoker)delegate { Close(); });
                })).Start();
            }
            else
            {
                DispatchMenu dis = new DispatchMenu();
                dis.FormClosed += new FormClosedEventHandler(delegate (Object o, FormClosedEventArgs a) { Visible = true; });

                dis.ShowDialog();
            }
        }
    }
}
