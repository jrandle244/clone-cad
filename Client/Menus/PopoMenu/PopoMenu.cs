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

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class PopoMenu : MaterialForm
    {
        private readonly Config cfg;
        private Socket client;

        public PopoMenu(Config Config)
        {
            cfg = Config;
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
            
            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.LightBlue500, Primary.LightBlue900, Primary.LightBlue300, Accent.Blue700, TextShade.WHITE);
        }

        private void SendTicket(ushort ID, Ticket ticket)
        {
            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch
            {
                if (MessageBox.Show("Couldn't connect to the server to give the client the ticket.", "CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    SendTicket(ID, ticket);

                return;
            }

            client.Send(new byte[] { 3 }.Concat(Encoding.UTF8.GetBytes(ID + "|" + ticket)).ToArray());

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
                    MessageBox.Show("The civilian has been given the ticket.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case 1:
                    MessageBox.Show("Your civilian was not found.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
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
