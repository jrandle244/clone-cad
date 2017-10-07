using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;
using CloneCAD.Client.DataHolders;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class CivMenu : MaterialForm
    {
        private readonly Config Config;
        private byte DownloadedTimeout;

        public Civilian LocalCivilian { get; private set; }
        public uint StartingID { get; }

        public CivMenu(Config config, uint startingID)
        {
            Config = config;

            StartingID = startingID;
            DownloadedTimeout = 0;

            InitializeComponent();
        }

        private void AddWepBtn_Click(object sender, EventArgs e)
        {
            RegWeaponMenu menu = new RegWeaponMenu();

            menu.ShowDialog();

            RegisteredWepList.Items.Add(menu.WeaponName.Text);
        }

        private void RemWepBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in RegisteredWepList.SelectedItems)
                item.Remove();

            SyncCheck.Checked = false;
        }

        private bool RefreshCiv()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Tuple<NetRequestResult, Civilian> tryGetResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", LocalCivilian?.ID ?? StartingID).GetAwaiter().GetResult();

            Functions.GetFailTest(tryGetResult.Item1);

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            LocalCivilian = tryGetResult.Item2;

            return true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!SyncCheck.Checked && LocalCivilian != null)
                Sync();
            else
            {
                if (LocalCivilian == null && DownloadedTimeout == 1)
                {
                    Reserve();
                }
                else if (LocalCivilian == null)
                    DownloadedTimeout++;
                else if (SyncCheck.Checked)
                    Download();
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private void BusinessBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private void PlateBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private void SyncBtn_Click(object sender, EventArgs e)
        {
            if (LocalCivilian == null)
            {
                Reserve();
                return;
            }

            Sync();
        }

        public void Sync()
        {
            if (LocalCivilian != null)
            {
                Upload();
                Download();
            }
            else if (StartingID == 0)
            {
                Reserve();
            }
            else
                Download();
        }

        public void Reserve()
        {
            ThreadPool.QueueUserWorkItem(z =>
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                s.Connect(Config.IP, Config.Port);

                NetRequestHandler handler = new NetRequestHandler(s);

                Tuple<NetRequestResult, Civilian> tryGetResult = handler.TryTriggerNetFunction<Civilian>("ReserveCivilian").GetAwaiter().GetResult();

                s.Shutdown(SocketShutdown.Both);
                s.Close();

                Functions.GetFailTest(tryGetResult.Item1);

                LocalCivilian = tryGetResult.Item2;
            });
        }

        public void Download()
        {
            ThreadPool.QueueUserWorkItem(z =>
            {
                RefreshCiv();

                Invoke((MethodInvoker)delegate
                {
                    IDLabel.Text = "Your civilian ID:\n" + LocalCivilian.ID.ToSplitID();

                    {
                        int[] nameS = { NameBox.SelectionStart, NameBox.SelectionLength };

                        NameBox.Text = LocalCivilian.Name;
                        if (NameBox.Text.Length != 0)
                        {
                            NameBox.SelectionStart = nameS[0];
                            NameBox.SelectionLength = nameS[1];
                        }
                    }

                    {
                        int[] plateS = { PlateBox.SelectionStart, PlateBox.SelectionLength };

                        PlateBox.Text = LocalCivilian.RegisteredPlate;
                        if (PlateBox.Text.Length != 0)
                        {
                            PlateBox.SelectionStart = plateS[0];
                            PlateBox.SelectionLength = plateS[1];
                        }
                    }

                    {
                        int[] businessS = { BusinessBox.SelectionStart, BusinessBox.SelectionLength };

                        BusinessBox.Text = LocalCivilian.AssociatedBusiness;
                        if (BusinessBox.Text.Length != 0)
                        {
                            BusinessBox.SelectionStart = businessS[0];
                            BusinessBox.SelectionLength = businessS[1];
                        }
                    }

                    {
                        int[] weaponS = (from ListViewItem item in RegisteredWepList.SelectedItems select item.Index)
                            .ToArray();

                        RegisteredWepList.Items.Clear();
                        LocalCivilian.RegisteredWeapons.ForEach(x => RegisteredWepList.Items.Add(x));

                        if (RegisteredWepList.Items.Count != 0)
                            foreach (int i in weaponS)
                                RegisteredWepList.Items[i].Selected = true;
                    }

                    {
                        int ticketS = TicketList.SelectedItems.Count > 0 ? TicketList.SelectedItems[0].Index : -1;

                        TicketList.Items.Clear();
                        LocalCivilian.Tickets.ForEach(x =>
                            TicketList.Items.Add(new ListViewItem(new[] { x.Price.ToString(), x.Type, x.Description })));

                        if (TicketList.Items.Count != 0 && ticketS != -1)
                            TicketList.Items[ticketS].Selected = true;
                    }

                    SyncCheck.Checked = true;
                });
            });
        }

        private bool UpdateCiv()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            if (handler.TryTriggerNetEvent("UpdateCivilian", LocalCivilian).GetAwaiter().GetResult() != NetRequestResult.Completed)
            {
                s.Shutdown(SocketShutdown.Both);
                s.Close();

                return false;
            }

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            return true;
        }

        public void Upload() =>
            ThreadPool.QueueUserWorkItem(z =>
            {
                Invoke((MethodInvoker)delegate
                {
                    LocalCivilian.Name = NameBox.Text;
                    LocalCivilian.RegisteredPlate = PlateBox.Text;
                    LocalCivilian.AssociatedBusiness = BusinessBox.Text;
                    
                    LocalCivilian.RegisteredWeapons =
                        (from ListViewItem wep in RegisteredWepList.Items select wep.Text).ToList();
                });

                UpdateCiv();
            });

        private void CivMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SyncCheck.Checked && MessageBox.Show("Your civilian is not synced to the server.\nIf you exit your civilian will not be saved!", "CloneCAD", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void PlateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
                e.Handled = true;

            if (char.IsLetter(e.KeyChar))
                e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void NameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
                e.Handled = true;
        }
    }
}
