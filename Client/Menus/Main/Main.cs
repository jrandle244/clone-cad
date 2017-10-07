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
                    StorableValue<uint[]> ids = new StorableValue<uint[]>
                    {
                        FilePath = "IDs.odf",
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

                if (File.Exists("IDs.odf"))
                {
                    StorableValue<uint[]> ids = new StorableValue<uint[]>("IDs.odf");

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
    }
}
