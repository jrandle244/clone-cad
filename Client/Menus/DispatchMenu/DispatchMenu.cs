using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Client.DataHolders;

using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class DispatchMenu : MaterialForm
    {
        private readonly Config Config;
        private Socket S;

        public DispatchMenu(Config config)
        {
            Config = config;
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Red700, Primary.Red500, Primary.Red900, Accent.Red100, TextShade.WHITE);
        }

        private void launch_Click(object sender, EventArgs ev) =>
            ThreadPool.QueueUserWorkItem(x => Launch(IDBox.Text, NameBox.Text, PlateBox.Text));

        private void Launch(string id, string name, string plate)
        {
            uint receivedID = 1;

            if (string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    S.Connect(Config.IP, Config.Port);
                }
                catch (SocketException)
                {
                    if (MessageBox.Show("Couldn't connect to the server to get the civilian ID.", "CloneCAD",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        Launch(id, name, plate);

                    return;
                }

                NetRequestHandler handler = new NetRequestHandler(S);

                if (!string.IsNullOrWhiteSpace(plate))
                {
                    Tuple<NetRequestResult, uint> tryGetResult =
                        handler.TryTriggerNetFunction<uint>("CheckPlate", plate).GetAwaiter().GetResult();

                    S.Shutdown(SocketShutdown.Both);
                    S.Close();
                    S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    Functions.GetFailTest(tryGetResult.Item1);

                    if (tryGetResult.Item2 == 0 && MessageBox.Show("Plate was not able to be found.", "CloneCAD",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                    {
                        Launch(id, name, plate);
                        return;
                    }

                    receivedID = tryGetResult.Item2;
                }
                else if (!string.IsNullOrWhiteSpace(name))
                {
                    Tuple<NetRequestResult, uint> tryGetResult = handler.TryTriggerNetFunction<uint>("CheckName", plate)
                        .GetAwaiter().GetResult();

                    S.Shutdown(SocketShutdown.Both);
                    S.Close();
                    S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    Functions.GetFailTest(tryGetResult.Item1);

                    if (tryGetResult.Item2 == 0 && MessageBox.Show("Name was not able to be found.", "CloneCAD",
                            MessageBoxButtons.RetryCancel, MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button2) ==
                        DialogResult.Retry)
                    {
                        Launch(id, name, plate);
                        return;
                    }

                    receivedID = tryGetResult.Item2;
                }
            }
            else
            {
                if (!uint.TryParse(IDBox.Text, out receivedID))
                {
                    try
                    {
                        receivedID = IDBox.Text.ToRawID();
                    }
                    catch
                    {
                        MessageBox.Show("ID was unable to be converted to an unsigned integer.", "CloneCAD");
                    }
                }
            }

            Invoke((MethodInvoker)delegate
            {
                CivView civ = new CivView(Config, receivedID);

                civ.Show();
                civ.Download();
            });
        }

        private new void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ThreadPool.QueueUserWorkItem(x => Launch(IDBox.Text, NameBox.Text, PlateBox.Text));
        }

        private void IDBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
                e.Handled = true;
            else if (!string.IsNullOrEmpty(NameBox.Text) || !string.IsNullOrEmpty(PlateBox.Text))
            {
                NameBox.Text = "";
                PlateBox.Text = "";
            }
        }

        private void PlateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);

                if (!string.IsNullOrEmpty(IDBox.Text) || !string.IsNullOrEmpty(NameBox.Text))
                {
                    IDBox.Text = "";
                    NameBox.Text = "";
                }
            }
        }

        private void NameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
            else if (!string.IsNullOrEmpty(IDBox.Text) || !string.IsNullOrEmpty(PlateBox.Text))
            {
                IDBox.Text = "";
                PlateBox.Text = "";
            }
        }
    }
}
