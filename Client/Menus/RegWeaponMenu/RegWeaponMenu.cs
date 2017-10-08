using MaterialSkin.Controls;
using System.Windows.Forms;
using CloneCAD.Common.DataHolders;

namespace CloneCAD.Client.Menus
{
    public partial class RegWeaponMenu : MaterialForm
    {
        public RegWeaponMenu(LocaleConfig locale)
        {
            InitializeComponent();
            LoadLocale(locale);
        }

        private void WeaponName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Close();
        }

        public void LoadLocale(LocaleConfig locale) =>
            WeaponName.Hint = locale["WeaponNameHint"];
    }
}
