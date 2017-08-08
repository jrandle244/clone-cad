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

        private void SendTicket(ushort ID, string ticket, string context)
        {
            try
            {
                client.Connect(Main.cfg.IP, Main.cfg.Port);
            }
            catch
            {
                if (MessageBox.Show("Couldn't connect to the server to give the client the ticket.", "BCRPDB", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    SendTicket(ID, ticket, context);

                return;
            }

            client.Send(new byte[] { 3 }.Concat(Encoding.UTF8.GetBytes(ID + "|" + ticket + "|" + context)).ToArray());

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
                    MessageBox.Show("The civilian has been giving the ticket.", "BCRPDB", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void context_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            ushort idS = ushort.Parse(id.Text);
            string typeS = type.Text;
            string contextS = context.Text;

            ThreadPool.QueueUserWorkItem(x => SendTicket(idS, typeS, contextS));
        }

        private void id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            ushort idS = ushort.Parse(id.Text);
            string typeS = type.Text;
            string contextS = context.Text;

            ThreadPool.QueueUserWorkItem(x => SendTicket(idS, typeS, contextS));
        }

        private void type_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            ushort idS = ushort.Parse(id.Text);
            string typeS = type.Text;
            string contextS = context.Text;

            ThreadPool.QueueUserWorkItem(x => SendTicket(idS, typeS, contextS));
        }
    }
}
