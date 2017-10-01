using MaterialSkin;
using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloneCAD
{
    public partial class RegWeaponMenu : MaterialForm
    {
        public RegWeaponMenu()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void WeaponName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Close();
        }
    }
}
