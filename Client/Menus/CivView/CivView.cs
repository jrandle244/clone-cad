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
    public partial class CivView : MaterialForm
    {
        private readonly Config Config;
        private readonly ErrorHandler Handler;
        private Socket S;

        public Civilian LocalCivilian { get; private set; }
        public uint StartingID { get; }

        public CivView(Config config, uint startingID)
        {
            Config = config;
            Handler = new ErrorHandler(config.Locale);
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            StartingID = startingID;

            InitializeComponent();
            LoadLocale(config.Locale);
        }

        private async Task RefreshCiv()
        {
            try
            {
                S.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return;
            }

            NetRequestHandler handler = new NetRequestHandler(S);

            Task<Tuple<NetRequestResult, Civilian>> tryTriggerResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", StartingID);

            await handler.Receive();
            await tryTriggerResult;

            Handler.GetFailTest(tryTriggerResult.Result.Item1);

            S.Shutdown(SocketShutdown.Both);
            S.Close();
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            LocalCivilian = tryTriggerResult.Result.Item2;
        }

        private async void SyncBtn_Click(object sender, EventArgs e) =>
            await Download();

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
        }

        private async void Timer_Tick(object sender, EventArgs e) =>
            await Download();

        private new void KeyPress(object sender, KeyPressEventArgs e) =>
            e.Handled = true;

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["CivilianRecordReadOnlyText"];

            IDLabel.Text = locale["IDTextExec", ""];

            NameBox.Hint = locale["FullNameHint"];
            BusinessBox.Hint = locale["AssociatedBusinessHint"];
            PlateBox.Hint = locale["LicensePlateHint"];

            SyncBtn.Text = locale["SyncButton"];

            SyncCheck.Text = locale["SyncedCheckbox"];

            columnHeader1.Text = locale["PriceColumn"];
            columnHeader2.Text = locale["TypeColumn"];
            columnHeader3.Text = locale["IDColumn"];
            columnHeader4.Text = locale["DescriptionColumn"];
        }
    }
}