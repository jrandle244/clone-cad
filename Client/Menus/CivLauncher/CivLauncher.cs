using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Common.DataHolders;
using CloneCAD.Client.DataHolders;

using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
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

        private bool RefreshCiv(uint id, out Civilian civ)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                s.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                civ = null;
                return false;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Tuple<NetRequestResult, Civilian> tryTriggerResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", id).GetAwaiter().GetResult();

            s.Shutdown(SocketShutdown.Both);
            s.Close();

            Handler.GetFailTest(tryTriggerResult.Item1);

            civ = tryTriggerResult.Item2;
            return true;
        }

        public void Sync(uint[] ids)
        {
            if (ids == null)
                return;

            Civilians.Clear();

            ThreadPool.QueueUserWorkItem(z =>
            {
                foreach (uint id in ids)
                {
                    if (!RefreshCiv(id, out Civilian civ) || civ == null)
                        continue;

                    Invoke((MethodInvoker)delegate
                    {
                        Civilians.Add(civ);
                        civs.Items.Add(new ListViewItem(new[] { civ.ID.ToSplitID(), civ.Name }));
                    });
                }
            });
        }

        private void Civs_DoubleClick(object sender, EventArgs e)
        {
            CivMenu civ = new CivMenu(Config, civs.Items[civs.SelectedItems[0].Index].SubItems[0].Text.ToRawID());

            civ.Show();
            civ.Sync();

            bool open = true;

            civ.FormClosed += delegate { open = false; };

            ThreadPool.QueueUserWorkItem(x =>
            {
                while (open)
                {
                    Invoke((MethodInvoker) delegate
                    {
                        if (civ.LocalCivilian != null)
                            civs.Items[civs.SelectedItems[0].Index].SubItems[1].Text = civ.LocalCivilian.Name;
                    });
                    Thread.Sleep(5000);
                }
            });
        }

        private void Create_Click(object sender, EventArgs e)
        {
            CivMenu civMenu = new CivMenu(Config, 0);
            
            civMenu.Show();
            civMenu.Sync();

            ThreadPool.QueueUserWorkItem(x =>
            {
                while (civMenu.LocalCivilian == null)
                    Thread.Sleep(1000);

                Invoke((MethodInvoker) delegate
                {
                    Civilians.Add(civMenu.LocalCivilian);
#pragma warning disable CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
                    civs.Items.Add(new ListViewItem(new[] {civMenu.StartingID.ToString(), ""}));
#pragma warning restore CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
                });

                civMenu.Closed += delegate
                {
                    Invoke((MethodInvoker)Close);
                };
            });
        }

        private void Delete_Click(object sender, EventArgs ev)
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
                if (MessageBox.Show(Config.Locale["CouldntConnectMsg"], @"CloneCAD",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    Delete_Click(sender, ev);

                return;
            }

            NetRequestHandler handler = new NetRequestHandler(s);

            Tuple<NetRequestResult, bool> tryTriggerResult = handler.TryTriggerNetFunction<bool>("DeleteCivilian", civs.SelectedItems[0].SubItems[0].Text.ToRawID()).GetAwaiter().GetResult();

            s.Shutdown(SocketShutdown.Both);
            s.Close();
            
            Handler.GetFailTest(tryTriggerResult.Item1);

            if (tryTriggerResult.Item2)
                Sync(Civilians.Select(x => x.Key).ToArray());
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
