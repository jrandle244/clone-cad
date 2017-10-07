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
using CloneCAD.Common.NetCode;
using CloneCAD.Server.DataHolders.Static;

#pragma warning disable IDE1006 //Naming rule violation, gtfo

namespace CloneCAD.Client.Menus
{
    public partial class CivLauncher : MaterialForm
    {
        private readonly Config Config;
        private Socket S;
        
        public CivilianDictionary Civilians { get; set; }

        public CivLauncher(Config config)
        {
            Config = config;
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            InitializeComponent();
            Civilians = new CivilianDictionary();

            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(Primary.Green500, Primary.Green700, Primary.Green300, Accent.Green700, TextShade.WHITE);
        }

        private bool RefreshCiv(uint id, out Civilian civ)
        {
            try
            {
                S.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                civ = null;
                return false;
            }

            NetRequestHandler handler = new NetRequestHandler(S);

            Tuple<bool, Civilian> tryTriggerResult = handler.TryTriggerNetFunction<Civilian>("GetCivilian", id).GetAwaiter().GetResult();

            S.Disconnect(true);
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);

            Functions.GetFailTest(tryTriggerResult.Item1);

            civ = tryTriggerResult.Item1 ? tryTriggerResult.Item2 : null;
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
                    if (!RefreshCiv(id, out Civilian civ))
                        continue;

                    Invoke((MethodInvoker)delegate
                    {
                        Civilians.Add(civ);
                        civs.Items.Add(new ListViewItem(new[] { civ.ID.ToString(), civ.Name }));
                    });
                }
            });

            File.WriteAllText("ids.cfg", string.Join(",", Civilians.Select(x => x.Key.ToString()).ToArray()));
        }

        private void Civs_DoubleClick(object sender, EventArgs e)
        {
            CivMenu civ = new CivMenu(Config, ushort.Parse(civs.Items[civs.SelectedItems[0].Index].SubItems[0].Text));

            civ.Show();
            civ.Sync();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            CivMenu civMenu = new CivMenu(Config, 0);

            civMenu.Show();
            civMenu.Download();

            ThreadPool.QueueUserWorkItem(x =>
            {
                Thread.Sleep(3000);

                Invoke((MethodInvoker) delegate
                {
                    Civilians.Add(civMenu.LocalCivilian);
#pragma warning disable CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception
                    civs.Items.Add(new ListViewItem(new[] {civMenu.StartingID.ToString(), ""}));
#pragma warning restore CS1690 // Accessing a member on a field of a marshal-by-reference class may cause a runtime exception

                    File.WriteAllText("ids.cfg", string.Join(",", Civilians.Select(y => y.Key.ToString()).ToArray()));
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

            try
            {
                S.Connect(Config.IP, Config.Port);
            }
            catch (SocketException)
            {
                return;
            }

            NetRequestHandler handler = new NetRequestHandler(S);

            Tuple<bool, bool> tryTriggerResult = handler.TryTriggerNetFunction<bool>("DeleteCivilian", uint.Parse(civs.SelectedItems[0].SubItems[0].Text)).GetAwaiter().GetResult();

            S.Disconnect(true);
            S = new Socket(SocketType.Stream, ProtocolType.Tcp);
            
            Functions.GetFailTest(tryTriggerResult.Item1);

            if (tryTriggerResult.Item2)
                Sync(Civilians.Select(x => x.Key).ToArray());
            else
                MessageBox.Show("Your civilian was not able to be deleted. This is most likely an error in reserving civs.", "CloneCAD", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Civilians.Remove(uint.Parse(civs.SelectedItems[0].SubItems[0].Text));
            civs.Items.RemoveAt(civs.SelectedItems[0].Index);

            File.WriteAllText("ids.cfg", string.Join(",", Civilians.Select(x => x.Key.ToString()).ToArray()));
        }
    }
}
