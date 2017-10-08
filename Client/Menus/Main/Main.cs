using MaterialSkin;
using MaterialSkin.Controls;
using CloneCAD.Client.DataHolders;

using System;
using System.IO;
using System.Linq;
using CloneCAD.Common.DataHolders;

#pragma warning disable IDE1006

namespace CloneCAD.Client.Menus
{
    public partial class Main : MaterialForm
    {
        private readonly Config Config;
        private readonly ColorScheme Scheme;
        private readonly MaterialSkinManager.Themes Theme;

        public Main(Config config)
        {
            Config = config;
            Scheme = SkinManager.ColorScheme;
            Theme = SkinManager.Theme;

            InitializeComponent();
            LoadLocale(config.Locale);
        }

        private void Launch_Click(object sender, EventArgs e)
        {
            Visible = false;

            if (CivilianRadial.Checked)
            {
                CivLauncher civLauncher = new CivLauncher(Config);

                civLauncher.FormClosed += delegate
                {
                    StorableValue<uint[]> ids = new StorableValue<uint[]>
                    {
                        FilePath = Program.ID_PATH,
                        Value = civLauncher.Civilians.Keys.ToArray()
                    };

                    ids.Save();

                    if (CloseCheckbox.Checked)
                        Close();
                    else
                    {
                        Visible = true;
                        SkinManager.ColorScheme = Scheme;
                        SkinManager.Theme = Theme;
                    }
                };

                civLauncher.Show();

                if (File.Exists(Program.ID_PATH))
                {
                    StorableValue<uint[]> ids = new StorableValue<uint[]>(Program.ID_PATH);

                    civLauncher.Sync(ids.Value);
                }
            }
            else if (PoliceRadial.Checked)
            {
                PopoMenu policeMenu = new PopoMenu(Config);
                policeMenu.FormClosed += delegate
                {
                    if (CloseCheckbox.Checked)
                        Close();
                    else
                    {
                        Visible = true;
                        SkinManager.ColorScheme = Scheme;
                        SkinManager.Theme = Theme;
                    }
                };

                policeMenu.Show();
            }
            else if (DispatchRadial.Checked)
            {
                DispatchMenu dispatchMenu = new DispatchMenu(Config);
                dispatchMenu.FormClosed += delegate
                {
                    if (CloseCheckbox.Checked)
                        Close();
                    else
                    {
                        Visible = true;
                        SkinManager.ColorScheme = Scheme;
                        SkinManager.Theme = Theme;
                    }
                };

                dispatchMenu.Show();
            }
        }

        private void LoadLocale(LocaleConfig locale)
        {
            Text = locale["MainMenuText"];

            LaunchBtn.Text = locale["LaunchButton"];

            CloseCheckbox.Text = locale["CloseOnLaunchCheckbox"];

            CivilianRadial.Text = locale["CivilianSelectorText"];
            PoliceRadial.Text = locale["PoliceText"];
            DispatchRadial.Text = locale["DispatchText"];
        }
    }
}
