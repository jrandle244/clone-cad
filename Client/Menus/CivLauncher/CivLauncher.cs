using MaterialSkin;
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
using CloneCAD.Server.DataHolders.Static;

namespace CloneCAD.Client.Menus
{
    public partial class CivLauncher : MaterialForm
    {
        private readonly Config Config;
        private readonly ErrorHandler Handler;
        
        public CivilianDictionary Civilians { get; set; }

        public CivLauncher(Config config)
        {
            Config = config;
            Handler = new ErrorHandler(config.Locale);

            InitializeComponent();
            LoadLocale(config.Locale);
            Civilians = new CivilianDictionary();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Green500, Primary.Green700, Primary.Green300, Accent.Green700, TextShade.WHITE);
        }

        private async Task<Tuple<bool, Civilian>> RefreshCiv(uint id)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return new Tuple<bool, Civilian>(false, null);
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Tuple<NetRequestResult, Civilian> tryTriggerResult = await handler.TryTriggerNetFunction<Civilian>("GetCivilian", id, Civilians.ContainsKey(id) ? Civilians[id].GetHashCode() : 0);

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            Handler.GetFailTest(tryTriggerResult.Item1);
            
            return new Tuple<bool, Civilian>(tryTriggerResult.Item1 == NetRequestResult.Completed, tryTriggerResult.Item2);
        }

        public async Task Sync(uint[] ids)
        {
            if (ids == null)
                return;

            Civilians.Clear();
            
            foreach (uint id in ids)
            {
                Tuple<bool, Civilian> returnVal = await RefreshCiv(id);
                if (!returnVal.Item1 || returnVal.Item2 == null)
                    continue;

                Civilians.Add(returnVal.Item2);
                civs.Items.Add(new ListViewItem(new[] { returnVal.Item2.ID.ToSplitID(), returnVal.Item2.Name }));
            }
        }

        private void Civs_DoubleClick(object sender, EventArgs e)
        {
            int index = civs.SelectedItems[0].Index;

            CivMenu civMenu = new CivMenu(Config, civs.Items[index].SubItems[0].Text.ToRawID());

            civMenu.Closed += delegate
            {
                Invoke((MethodInvoker)Close);
            };

            civMenu.NameBox.TextChanged += delegate
            {
                civs.Items[index].SubItems[1].Text = civMenu.NameBox.Text;
            };


            civMenu.Show();
            ThreadPool.QueueUserWorkItem(async x => await civMenu.Sync());
        }

        private void Create_Click(object sender, EventArgs e)
        {
            CivMenu civMenu = new CivMenu(Config, 0);

            civMenu.Closed += delegate
            {
                Invoke((MethodInvoker)Close);
            };

            int index = -1;

            civMenu.CivilianDownloaded += delegate
            {
                Civilians.Add(civMenu.LocalCivilian);

                ListViewItem item = new ListViewItem(new[] { civMenu.LocalCivilian.ID.ToSplitID(), "" });
#pragma warning disable CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
                civs.Items.Add(item);
#pragma warning restore CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
                index = civs.Items.IndexOf(item);
            };

            civMenu.NameBox.TextChanged += delegate
            {
                civs.Items[index].SubItems[1].Text = civMenu.NameBox.Text;
            };

            civMenu.Show();
            ThreadPool.QueueUserWorkItem(async x => await civMenu.Sync());
        }

        private async void Delete_Click(object sender, EventArgs ev)
        {
            if (civs.SelectedItems.Count == 0)
                return;

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                if (MessageBox.Show(Config.Locale["CouldntConnectMsg"], @"CloneCAD", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    Delete_Click(sender, ev);

                return;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Tuple<NetRequestResult, bool> tryTriggerResult = await handler.TryTriggerNetFunction<bool>("DeleteCivilian", civs.SelectedItems[0].SubItems[0].Text.ToRawID());

            s.Shutdown(SocketShutdown.Both);
            s.Close();
            
            Handler.GetFailTest(tryTriggerResult.Item1);

            if (tryTriggerResult.Item2)
                await Sync(Civilians.Select(x => x.Key).ToArray());
            else
                Handler.Error(Config.Locale["CivilianNotDeletedMsg"]);

            Civilians.Remove(uint.Parse(civs.SelectedItems[0].SubItems[0].Text));
            civs.Items.RemoveAt(civs.SelectedItems[0].Index);
        }

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["CivilianSelectorText"];

            columnHeader1.Text = locale["IDColumn"];
            columnHeader2.Text = locale["NameColumn"];

            create.Text = locale["CreateButton"];
            delete.Text = locale["DeleteButton"];
        }
    }
}
