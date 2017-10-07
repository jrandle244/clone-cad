using MaterialSkin.Controls;
using System.Windows.Forms;

namespace CloneCAD.Client.Menus
{
    public partial class RegWeaponMenu : MaterialForm
    {
        public RegWeaponMenu()
        {
            InitializeComponent();
        }

        private void WeaponName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Close();
        }
    }
}
