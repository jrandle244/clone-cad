using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;
using CloneCAD.Client.DataHolders;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloneCAD.Common;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class CivMenu : MaterialForm
    {
        private readonly Config Config;
        private readonly ErrorHandler Handler;
        private byte DownloadedTimeout;

        public Civilian LocalCivilian { get; private set; }
        public event EventHandler CivilianDownloaded;
        public uint StartingID { get; }

        public CivMenu(Config config, uint startingID)
        {
            Config = config;
            Handler = new ErrorHandler(config.Locale);

            StartingID = startingID;
            DownloadedTimeout = 0;

            InitializeComponent();
            LoadLocale(config.Locale);
        }

        private void AddWepBtn_Click(object sender, EventArgs e)
        {
            RegWeaponMenu menu = new RegWeaponMenu(Config.Locale);

            menu.ShowDialog();

            if (!string.IsNullOrWhiteSpace(menu.WeaponName.Text))
            {
                RegisteredWepList.Items.Add(menu.WeaponName.Text);
                SyncCheck.Checked = false;
            }
        }

        private void RemWepBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in RegisteredWepList.SelectedItems)
                item.Remove();

            SyncCheck.Checked = false;
        }

        private async Task RefreshCiv()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Task<Tuple<NetRequestResult, Civilian>> tryTriggerResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", LocalCivilian?.ID ?? StartingID);

            await handler.Receive();
            await tryTriggerResult;

            Handler.GetFailTest(tryTriggerResult.Result.Item1);

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            LocalCivilian = tryTriggerResult.Result.Item2;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (!SyncCheck.Checked && LocalCivilian != null)
                await Sync();
            else
            {
                switch (LocalCivilian)
                {
                    case null when DownloadedTimeout == 1:
                        await Reserve();
                        break;
                    case null:
                        DownloadedTimeout++;
                        break;
                    default:
                        if (SyncCheck.Checked)
                            ThreadPool.QueueUserWorkItem(x => Download().Wait());
                        break;
                }
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private void BusinessBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private void PlateBox_TextChanged(object sender, EventArgs e) =>
            SyncCheck.Checked = false;

        private async void SyncBtn_Click(object sender, EventArgs e)
        {
            if (LocalCivilian == null)
            {
                await Reserve();
                return;
            }

            await Sync();
        }

        public async Task Sync()
        {
            if (LocalCivilian != null)
            {
                if (!SyncCheck.Checked)
                {
                    await Upload();
                    await Download();
                }
                else
                    await Download();
            }
            else if (StartingID == 0)
            {
                if (await Reserve())
                    CivilianDownloaded?.Invoke(this, EventArgs.Empty);
            }
            else
                await Download();
        }

        public async Task<bool> Reserve()
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

            Task<Tuple<NetRequestResult, Civilian>> tryTriggerResult = handler.TryTriggerNetFunction<Civilian>("ReserveCivilian");
                
            await handler.Receive();
            await tryTriggerResult;

            LocalCivilian = tryTriggerResult.Result.Item2;


            s.Shutdown(SocketShutdown.Both);
            s.Close();

            Handler.GetFailTest(tryTriggerResult.Result.Item1);

            LocalCivilian = tryTriggerResult.Result.Item2;

            return true;
        }

        public async Task Download()
        {
            await RefreshCiv();

            Invoke((MethodInvoker)delegate
            {
                IDLabel.Text = Config.Locale["IDTextExec", LocalCivilian.ID.ToSplitID()];

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
                        TicketList.Items.Add(new ListViewItem(new[] { x.Price.ToString(), x.Type.ToString(), x.Description })));

                    if (TicketList.Items.Count != 0 && ticketS != -1)
                        TicketList.Items[ticketS].Selected = true;
                }

                SyncCheck.Checked = true;
            });

            await Task.FromResult(0);
        }

        private async Task UpdateCiv()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Task netEvent = handler.TryTriggerNetEvent("UpdateCivilian", LocalCivilian);

            await handler.Receive();
            await netEvent;

            s.Shutdown(SocketShutdown.Both);
            s.Close();
        }

        public async Task Upload()
        {
            Invoke((MethodInvoker)delegate
            {
                LocalCivilian.Name = NameBox.Text;
                LocalCivilian.RegisteredPlate = PlateBox.Text;
                LocalCivilian.AssociatedBusiness = BusinessBox.Text;

                LocalCivilian.RegisteredWeapons =
                    (from ListViewItem wep in RegisteredWepList.Items select wep.Text).ToList();
            });

            await UpdateCiv();

            await Task.FromResult(0);
        }

        private void CivMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SyncCheck.Checked && MessageBox.Show(Config.Locale["CivilianNotSyncedMsg"], @"CloneCAD", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
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

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["CivilianRecordText"];

            IDLabel.Text = locale["IDTextExec", ""];

            NameBox.Hint = locale["FullNameHint"];
            BusinessBox.Hint = locale["AssociatedBusinessHint"];
            PlateBox.Hint = locale["LicensePlateHint"];

            SyncBtn.Text = locale["SyncButton"];
            AddWepBtn.Text = locale["AddWeaponButton"];
            RemWepBtn.Text = locale["RemoveWeaponButton"];

            SyncCheck.Text = locale["SyncedCheckbox"];

            columnHeader1.Text = locale["PriceColumn"];
            columnHeader2.Text = locale["TypeColumn"];
            columnHeader3.Text = locale["IDColumn"];
            columnHeader4.Text = locale["DescriptionColumn"];
        }
    }
}
