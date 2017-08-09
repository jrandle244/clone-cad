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
    public partial class PopoMenu : MaterialForm
    {
        Socket client;
        public bool closed = false;

        public PopoMenu()
        {
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightBlue500, Primary.LightBlue900, Primary.LightBlue300, Accent.Blue100, TextShade.WHITE);
        }

        private void SendTicket(ushort ID, Ticket ticket)
        {
            try
            {
                client.Connect(Main.cfg.IP, Main.cfg.Port);
            }
            catch
            {
                if (MessageBox.Show("Couldn't connect to the server to give the client the ticket.", "BCRPDB", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
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
                    MessageBox.Show("The civilian has been given the ticket.", "BCRPDB", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case 1:
                    MessageBox.Show("Your civilian was not found.", "BCRPDB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void PopoMenu_FormClosed(object sender, FormClosedEventArgs e) =>
            closed = true;

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
    }
}
