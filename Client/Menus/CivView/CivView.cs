using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;
using CloneCAD.Client.DataHolders;

using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using CloneCAD.Common.NetCode;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class CivView : MaterialForm
    {
        private readonly Config Config;
        private Socket S;
        private bool Downloaded;

        public Civilian LocalCivilian { get; private set; }
        public uint StartingID { get; private set; }

        public CivView(Config config, uint startingID)
        {
            Config = config;
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            StartingID = startingID;
            Downloaded = false;

            InitializeComponent();
        }

        private bool RefreshCiv()
        {
            try
            {
                S.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return false;
            }

            NetRequestHandler handler = new NetRequestHandler(S);

            Tuple<bool, Civilian> tryGetResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", LocalCivilian.ID).GetAwaiter().GetResult();

            Functions.GetFailTest(tryGetResult.Item1);

            S.Disconnect(true);
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            LocalCivilian = tryGetResult.Item2;

            return true;
        }

        private void syncBtn_Click(object sender, EventArgs e)
        {
            if (!Downloaded)
            {
                Downloaded = true;
                StartingID = 0;
            }

            Download();
        }

        public void Download() =>
            ThreadPool.QueueUserWorkItem(z =>
            {
                RefreshCiv();

                Invoke((MethodInvoker)delegate
                {
                    idDisp.Text = "Civilian ID:\n" + LocalCivilian.ID.ToString();

                    {
                        int[] nameS = { name.SelectionStart, name.SelectionLength };

                        name.Text = LocalCivilian.Name;
                        if (name.Text.Length != 0)
                        {
                            name.SelectionStart = nameS[0];
                            name.SelectionLength = nameS[1];
                        }
                    }

                    {
                        int[] plateS = { plate.SelectionStart, plate.SelectionLength };

                        plate.Text = LocalCivilian.RegisteredPlate;
                        if (plate.Text.Length != 0)
                        {
                            plate.SelectionStart = plateS[0];
                            plate.SelectionLength = plateS[1];
                        }
                    }

                    {
                        int[] businessS = { business.SelectionStart, business.SelectionLength };

                        business.Text = LocalCivilian.AssociatedBusiness;
                        if (business.Text.Length != 0)
                        {
                            business.SelectionStart = businessS[0];
                            business.SelectionLength = businessS[1];
                        }
                    }

                    {
                        int[] weaponS = (from ListViewItem item in regWepList.SelectedItems select item.Index)
                            .ToArray();

                        regWepList.Items.Clear();
                        LocalCivilian.RegisteredWeapons.ForEach(x => regWepList.Items.Add(x));

                        if (regWepList.Items.Count != 0)
                            foreach (int i in weaponS)
                                regWepList.Items[i].Selected = true;
                    }

                    {
                        int ticketS = ticketList.SelectedItems[0].Index;

                        ticketList.Items.Clear();
                        LocalCivilian.Tickets.ForEach(x =>
                            ticketList.Items.Add(new ListViewItem(new[] { x.Price.ToString(), x.Type, x.Description })));

                        if (ticketList.Items.Count != 0)
                            ticketList.Items[ticketS].Selected = true;
                    }

                    sync.Checked = true;
                });
            });

        private new void KeyPress(object sender, KeyPressEventArgs e) =>
            e.Handled = true;

        private void timer_Tick(object sender, EventArgs e) =>
            Download();
    }
}