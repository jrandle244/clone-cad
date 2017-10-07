using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Client.DataHolders;

using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class Main : MaterialForm
    {
        private readonly Config Config;
        private readonly ColorScheme Scheme;
        private readonly MaterialSkinManager.Themes Theme;

        public Main()
        {
            Config = new Config("settings.ini");
            Scheme = SkinManager.ColorScheme;
            Theme = SkinManager.Theme;

            InitializeComponent();
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            Visible = false;

            if (CivilianRadial.Checked)
            {
                CivLauncher civLauncher = new CivLauncher(Config);
                civLauncher.FormClosed += delegate
                {
                    Visible = true;
                    SkinManager.ColorScheme = Scheme;
                    SkinManager.Theme = Theme;
                };

                civLauncher.Show();
                if (File.Exists("ids.cfg") && !string.IsNullOrWhiteSpace(File.ReadAllText("ids.cfg")))
                    civLauncher.Sync(File.ReadAllText("ids.cfg").Split(',').Select(x => uint.Parse(x.Trim())).ToArray());

                civLauncher.Closed += delegate
                {
                    if (CloseCheckbox.Checked)
                        Close();
                };
            }
            else if (PoliceRadial.Checked)
            {
                PopoMenu policeMenu = new PopoMenu(Config);
                policeMenu.FormClosed += delegate
                {
                    Visible = true;
                    SkinManager.ColorScheme = Scheme;
                    SkinManager.Theme = Theme;
                };

                policeMenu.Show();

                policeMenu.Closed += delegate
                {
                    if (CloseCheckbox.Checked)
                        Close();
                };
            }
            else if (DispatchRadial.Checked)
            {
                DispatchMenu dispatchMenu = new DispatchMenu(Config);
                dispatchMenu.FormClosed += delegate
                {
                    Visible = true;
                    SkinManager.ColorScheme = Scheme;
                    SkinManager.Theme = Theme;
                };

                dispatchMenu.ShowDialog();

                dispatchMenu.Closed += delegate
                {
                    if (CloseCheckbox.Checked)
                        Close();
                };
            }
        }
    }
}
