using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Client.DataHolders;

using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.DataHolders;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class DispatchMenu : MaterialForm
    {
        private readonly Config Config;
        private readonly ErrorHandler Handler;

        public DispatchMenu(Config config)
        {
            Config = config;
            Handler = new ErrorHandler(config.Locale);

            InitializeComponent();
            LoadLocale(config.Locale);

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Red700, Primary.Red500, Primary.Red900, Accent.Red100, TextShade.WHITE);
        }

        private async void LaunchBtn_Click(object sender, EventArgs ev) =>
            await Launch(IDBox.Text, NameBox.Text, PlateBox.Text);

        private async Task Launch(string id, string name, string plate)
        {
            uint receivedID = 1;

            if (string.IsNullOrWhiteSpace(id))
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    s.Connect(Config.IP, Config.Port);
                }
                catch (SocketException)
                {
                    if (MessageBox.Show(Config.Locale["CouldntConnectMsg"], @"CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        await Launch(id, name, plate);

                    return;
                }

                NetRequestHandler handler = new NetRequestHandler(s);

                if (!string.IsNullOrWhiteSpace(plate))
                {
                    Tuple<NetRequestResult, uint> tryTriggerResult = await handler.TryTriggerNetFunction<uint>("CheckPlate", plate);

                    s.Shutdown(SocketShutdown.Both);
                    s.Close();

                    Handler.GetFailTest(tryTriggerResult.Item1);

                    if (tryTriggerResult.Item2 == 0 && MessageBox.Show(Config.Locale["PlateCheckEmptyMsg"], @"CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                        await Launch(id, name, plate);

                    if (tryTriggerResult.Item2 == 0)
                        return;

                    receivedID = tryTriggerResult.Item2;
                }
                else if (!string.IsNullOrWhiteSpace(name))
                {
                    Tuple<NetRequestResult, uint> tryTriggerResult = await handler.TryTriggerNetFunction<uint>("CheckName", name);

                    s.Shutdown(SocketShutdown.Both);
                    s.Close();

                    Handler.GetFailTest(tryTriggerResult.Item1);

                    if (tryTriggerResult.Item2 == 0 && MessageBox.Show(Config.Locale["NameCheckEmptyMsg"], @"CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Retry)
                        await Launch(id, name, plate);

                    if (tryTriggerResult.Item2 == 0)
                        return;

                    receivedID = tryTriggerResult.Item2;
                }
            }
            else if (!IDBox.Text.TryToRawID(out receivedID))
            {
                Handler.Error("UnableToConvertIDMsg");
                return;
            }
            
            CivView civ = new CivView(Config, receivedID);

            civ.Show();
            await civ.Download();
        }

        private new async void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                await Launch(IDBox.Text, NameBox.Text, PlateBox.Text);
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

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["DispatchText"];

            IDBox.Hint = locale["CivilianIDHint"];
            PlateBox.Hint = locale["LicensePlateHint"];
            NameBox.Hint = locale["FullNameHint"];

            LaunchBtn.Text = locale["ViewCivilianButton"];
        }
    }
}
