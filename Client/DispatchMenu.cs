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

namespace Client
{
    public partial class DispatchMenu : MaterialForm
    {
        Socket client;

        public DispatchMenu()
        {
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Red700, Primary.Red500, Primary.Red900, Accent.Red100, TextShade.WHITE);
        }

        private void launch_Click(object sender, EventArgs ev) =>
            ThreadPool.QueueUserWorkItem(x => Launch(id.Text, name.Text, plate.Text));

        private void Launch(string id, string name, string plate)
        {
            ushort ID = 1;

            if (!string.IsNullOrWhiteSpace(id))
                ID = ushort.Parse(id);
            else if (!string.IsNullOrWhiteSpace(plate))
            {
                try
                {
                    client.Connect(Main.cfg.IP, Main.cfg.Port);
                }
                catch (SocketException)
                {
                    if (MessageBox.Show("Couldn't connect to the server to get the civilian ID.", "Client", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        Launch(id, name, plate);

                    return;
                }

                client.Send(new byte[] { 2 }.Concat(Encoding.UTF8.GetBytes(plate)).ToArray());

                byte[] b = new byte[1001];
                int e = client.Receive(b);
                byte tag = b[0];
                b = b.Skip(1).ToArray();
                e = e - 1;

                client.Disconnect(true);
                client = new Socket(SocketType.Stream, ProtocolType.Tcp);

                switch (tag)
                {
                    case 0:
                        ID = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);
                        break;

                    case 1:
                        if (MessageBox.Show("Plate was not able to be found.", "Client", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                        {
                            Launch(id, name, plate);
                            return;
                        }
                        break;
                }
            }
            else if (!string.IsNullOrWhiteSpace(name))
            {
                try
                {
                    client.Connect(Main.cfg.IP, Main.cfg.Port);
                }
                catch (SocketException)
                {
                    if (MessageBox.Show("Couldn't connect to the server to get the civilian ID.", "Client", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        Launch(id, name, plate);

                    return;
                }

                client.Send(new byte[] { 5 }.Concat(Encoding.UTF8.GetBytes(name)).ToArray());

                byte[] b = new byte[1001];
                int e = client.Receive(b);
                byte tag = b[0];
                b = b.Skip(1).ToArray();
                e = e - 1;

                client.Disconnect(true);
                client = new Socket(SocketType.Stream, ProtocolType.Tcp);


                switch (tag)
                {
                    case 0:
                        ID = BitConverter.ToUInt16(b.Take(e).ToArray(), 0);
                        break;

                    case 1:
                        if (MessageBox.Show("Name was not able to be found.", "Client", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                            Launch(id, name, plate);

                        return;
                }
            }
            else
                return;

            Invoke((MethodInvoker)delegate
            {
                CivView civ = new CivView(ID);

                civ.Show();
                civ.Sync();
            });
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            else if (!string.IsNullOrEmpty(name.Text) || !string.IsNullOrEmpty(plate.Text))
            {
                name.Text = "";
                plate.Text = "";
            }
        }

        private void plate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = e.KeyChar.ToString().ToUpper()[0];

                if (!string.IsNullOrEmpty(id.Text) || !string.IsNullOrEmpty(name.Text))
                {
                    id.Text = "";
                    name.Text = "";
                }
            }
        }

        private void name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
            else if (!string.IsNullOrEmpty(id.Text) || !string.IsNullOrEmpty(plate.Text))
            {
                id.Text = "";
                plate.Text = "";
            }
        }

        private new void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ThreadPool.QueueUserWorkItem(x => Launch(id.Text, name.Text, plate.Text));
        }
    }
}
