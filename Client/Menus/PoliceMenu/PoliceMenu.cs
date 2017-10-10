using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;

using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloneCAD.Client.DataHolders;
using CloneCAD.Common;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class PopoMenu : MaterialForm
    {
        private readonly Config Config;
        private readonly ErrorHandler Handler;

        public PopoMenu(Config config)
        {
            Config = config;
            Handler = new ErrorHandler(config.Locale);

            InitializeComponent();
            LoadLocale(config.Locale);

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.LightBlue500, Primary.LightBlue900, Primary.LightBlue300,
                Accent.Blue700, TextShade.WHITE);
        }

        private async Task SendTicket(uint id, Ticket ticket)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch
            {
                if (MessageBox.Show(Config.Locale["CouldntConnectMsg"], @"CloneCAD", MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error) == DialogResult.Retry)
                    await SendTicket(id, ticket);

                return;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Task<Tuple<NetRequestResult, bool>> tryTriggerResult =
                handler.TryTriggerNetFunction<bool>("TicketCivilian", id, ticket);

            await handler.Receive();
            await tryTriggerResult;

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            Handler.GetFailTest(tryTriggerResult.Result.Item1);

            if (tryTriggerResult.Result.Item2)
                MessageBox.Show(Config.Locale["TicketGivenMsg"], @"CloneCAD", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
                Handler.Error("TicketEmptyMsg");
        }

        private void IDBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
                e.Handled = true;
        }

        private void PriceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private Ticket PrepTicket()
        {
            TicketType type = default(TicketType);
            type = FixItRadial.Checked ? TicketType.Fixit : type;
            type = WarningRadial.Checked ? TicketType.Warning : type;
            type = CitationRadial.Checked ? TicketType.Citation : type;
            type = TicketRadial.Checked ? TicketType.Ticket : type;

            return new Ticket(ushort.Parse(PriceBox.Text), type, DescriptionBox.Text);
        }

        private new async void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (IDBox.Text.TryToRawID(out uint parsedID))
                    await SendTicket(parsedID, PrepTicket());
                else
                    Handler.Error("UnableToConvertIDMsg");
            }
        }

        private async void GiveTicketBtn_Click(object sender, EventArgs e)
        {
            if (!IDBox.Text.TryToRawID(out uint parsedID))
            {
                Handler.Error("UnableToConvertIDMsg");
                return;
            }

            await SendTicket(parsedID, PrepTicket());
        }

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["PoliceText"];

            FixItRadial.Text = locale["FixItTicket"];
            WarningRadial.Text = locale["WarningTicket"];
            CitationRadial.Text = locale["CitationTicket"];
            TicketRadial.Text = locale["TicketTicket"];

            IDBox.Hint = locale["CivilianIDHint"];
            PriceBox.Hint = locale["TicketPriceHint"];
            DescriptionBox.Hint = locale["TicketDescriptionHint"];
        }
    }
}
