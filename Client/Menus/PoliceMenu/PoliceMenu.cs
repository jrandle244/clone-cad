using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CloneCAD.Client.DataHolders;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class PopoMenu : MaterialForm
    {
        private readonly Config Config;
        private Socket S;

        public PopoMenu(Config config)
        {
            Config = config;
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
            
            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.LightBlue500, Primary.LightBlue900, Primary.LightBlue300, Accent.Blue700, TextShade.WHITE);
        }

        private void SendTicket(uint id, Ticket ticket)
        {
            try
            {
                S.Connect(Config.IP, Config.Port);
            }
            catch
            {
                if (MessageBox.Show("Couldn't connect to the server to give the client the ticket.", "CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    SendTicket(id, ticket);

                return;
            }

            NetRequestHandler handler = new NetRequestHandler(S);

            Tuple<bool, bool> tryGetResult = handler.TryTriggerNetFunction<bool>("TicketCivilian", id, ticket).GetAwaiter().GetResult();

            S.Disconnect(true);
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Functions.GetFailTest(tryGetResult.Item1);

            if (tryGetResult.Item2)
                MessageBox.Show("The civilian has been given the ticket.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("The civilian was not found.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private Ticket PrepTicket()
        {
            string type = fixit.Checked ? "Fix-it" : "";
            type = warning.Checked ? "Warning" : type;
            type = citation.Checked ? "Citation" : type;
            type = ticket.Checked ? "Ticket" : type;

            return new Ticket(ushort.Parse(price.Text), type, context.Text);
        }

        private new void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ThreadPool.QueueUserWorkItem(x => SendTicket(ushort.Parse(id.Text), PrepTicket()));
        }

        private void giveTicket_Click(object sender, EventArgs e) =>
            ThreadPool.QueueUserWorkItem(x => SendTicket(ushort.Parse(id.Text), PrepTicket()));
    }
}
