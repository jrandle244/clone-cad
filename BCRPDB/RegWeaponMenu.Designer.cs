namespace BCRPDB
{
    partial class RegWeaponMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegWeaponMenu));
            this.WeaponName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // WeaponName
            // 
            this.WeaponName.Depth = 0;
            this.WeaponName.Hint = "Weapon Name";
            this.WeaponName.Location = new System.Drawing.Point(12, 80);
            this.WeaponName.MaxLength = 32767;
            this.WeaponName.MouseState = MaterialSkin.MouseState.HOVER;
            this.WeaponName.Name = "WeaponName";
            this.WeaponName.PasswordChar = '\0';
            this.WeaponName.SelectedText = "";
            this.WeaponName.SelectionLength = 0;
            this.WeaponName.SelectionStart = 0;
            this.WeaponName.Size = new System.Drawing.Size(275, 23);
            this.WeaponName.TabIndex = 0;
            this.WeaponName.TabStop = false;
            this.WeaponName.UseSystemPasswordChar = false;
            this.WeaponName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WeaponName_KeyDown);
            // 
            // RegWeaponMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 118);
            this.Controls.Add(this.WeaponName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegWeaponMenu";
            this.Text = "Register Weapon";
            this.ResumeLayout(false);

        }

        #endregion
        public MaterialSkin.Controls.MaterialSingleLineTextField WeaponName;
    }
}