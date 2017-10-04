using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;
using CloneCAD.Client.DataHolders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

#pragma warning disable IDE1006 //Naming rule violation, gtfo

namespace CloneCAD.Client.Menus
{
    public partial class CivLauncher : MaterialForm
    {
        private readonly Config cfg;
        private Socket client;
        
        public List<Civ> Civs { get; set; }
        public bool closed = false;

        public CivLauncher(Config Config)
        {
            cfg = Config;
            client = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
            Civs = new List<Civ>();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Green500, Primary.Green700, Primary.Green300, Accent.Green700, TextShade.WHITE);
        }

        private bool RefreshCiv(ushort ID, out Civ civ)
        {
            Civ civI = null;
            civ = civI;

            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            client.Send(new byte[] { 0 }.Concat(BitConverter.GetBytes(ID)).ToArray());

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
                    civI = Civ.ToCiv(b.Take(e).ToArray());
                    break;

                case 1:
                    civ = civI;
                    return false;
            }

            civ = civI;
            return true;
        }

        public void Sync(ushort[] ids)
        {
            if (ids == null)
                return;

            Civs.Clear();

            ThreadPool.QueueUserWorkItem(z =>
            {
                for (ushort i = 0; i < ids.Length; i++)
                {
                    if (!RefreshCiv(ids[i], out Civ civ))
                        return;

                    Invoke((MethodInvoker)delegate
                    {
                        Civs.Add(civ);
                        civs.Items.Add(new ListViewItem(new[] { civ.CivID.ToString(), civ.Name }));
                    });
                }
            });

            File.WriteAllText("ids.cfg", string.Join(",", Civs.Select(x => x.CivID.ToString()).ToArray()));
        }

        private void civs_DoubleClick(object sender, EventArgs e)
        {
            CivView civ = new CivView(cfg, ushort.Parse(civs.Items[civs.SelectedItems[0].Index].SubItems[0].Text));

            civ.Show();
            civ.Sync();
        }

        private void create_Click(object sender, EventArgs e)
        {
            CivMenu civ = new CivMenu(cfg, 0);

            civ.Show();
            civ.Sync(false);

            ThreadPool.QueueUserWorkItem(x =>
            {
                Thread.Sleep(3000);

                Invoke((MethodInvoker)delegate
                {
                    Civs.Add(civ.localCiv);
                    #pragma warning disable CS1690 //Gets rid of "Acessing a member on 'CivMenu.ID' may cause a runtime exception because it is a field of a marshal-by-reference class
                    civs.Items.Add(new ListViewItem(new[] { civ.ID.ToString(), "" }));
                    #pragma warning restore CS1690 //Restors warning just in case

                    File.WriteAllText("ids.cfg", string.Join(",", Civs.Select(y => y.CivID.ToString()).ToArray()));
                });

                while (civ.closed == false)
                    Thread.Sleep(10);

                Invoke((MethodInvoker)delegate { Close(); });
            });
        }

        private void delete_Click(object sender, EventArgs ev)
        {
            if (civs.SelectedItems.Count == 0)
                return;

            try
            {
                client.Connect(cfg.IP, cfg.Port);
            }
            catch (SocketException)
            {
                return;
            }

            client.Send(new byte[] { 4 }.Concat(BitConverter.GetBytes(ushort.Parse(civs.SelectedItems[0].SubItems[0].Text))).ToArray());

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
                    Sync(Civs.Select(x => x.CivID).ToArray());
                    break;

                case 1:
                    MessageBox.Show("Your civilian was not able to be deleted. This is most likely an error in reserving civs.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            Civs.Remove(Civs.Find(x => x.CivID == ushort.Parse(civs.SelectedItems[0].SubItems[0].Text)));
            civs.Items.RemoveAt(civs.SelectedItems[0].Index);

            File.WriteAllText("ids.cfg", string.Join(",", Civs.Select(x => x.CivID.ToString()).ToArray()));
        }

        private void CivLauncher_FormClosed(object sender, FormClosedEventArgs e) =>
            closed = true;
    }
}
