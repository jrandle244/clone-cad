using MaterialSkin;
using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace BCRPDB
{
    public partial class DispatchMenu : MaterialForm
    {
        Socket socket;

        public DispatchMenu()
        {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Red700, Primary.Red500, Primary.Red900, Accent.Red100, TextShade.WHITE);
        }

        private void launch_Click(object sender, EventArgs ev) =>
            Launch();

        private void Launch()
        {
            ushort ID = 1;

            if (!string.IsNullOrWhiteSpace(id.Text))
                ID = ushort.Parse(id.Text);
            else if (!string.IsNullOrWhiteSpace(plate.Text))
            {
                socket.Connect(Main.cfg.IP, Main.cfg.Port);

                socket.Send(new byte[] { 2 }.Concat(Encoding.UTF8.GetBytes(plate.Text)).ToArray());

                byte[] b = new byte[1001];
                int e = socket.Receive(b);
                byte tag = b[0];
                e = e - 1;

                switch (tag)
                {
                    case 0:
                        ID = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);
                        break;

                    case 1:
                        if (MessageBox.Show("Plate was not able to be found.", "BCRPDB", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                        {
                            Launch();
                            return;
                        }
                        break;
                }
            }
            else if (!string.IsNullOrWhiteSpace(name.Text))
            {
                socket.Connect(Main.cfg.IP, Main.cfg.Port);

                socket.Send(new byte[] { 5 }.Concat(Encoding.UTF8.GetBytes(plate.Text)).ToArray());

                byte[] b = new byte[1001];
                int e = socket.Receive(b);
                byte tag = b[0];
                e = e - 1;

                switch (tag)
                {
                    case 0:
                        ID = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);
                        break;

                    case 1:
                        if (MessageBox.Show("Name was not able to be found.", "BCRPDB", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                        {
                            Launch();
                            return;
                        }
                        break;
                }
            }
            else
                return;

            CivView civ = new CivView(ID);

            civ.Show();
            civ.Sync();

            ThreadPool.QueueUserWorkItem(x =>
            {
                while (!civ.closed)
                    Thread.Sleep(10);
            });
        }
    }
}
