namespace CloneCAD.Client.Menus
{
    partial class DispatchMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DispatchMenu));
            this.IDBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.PlateBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.NameBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.LaunchBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // IDBox
            // 
            this.IDBox.Depth = 0;
            this.IDBox.Hint = "Civilian ID";
            this.IDBox.Location = new System.Drawing.Point(12, 109);
            this.IDBox.MaxLength = 11;
            this.IDBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.IDBox.Name = "IDBox";
            this.IDBox.PasswordChar = '\0';
            this.IDBox.SelectedText = "";
            this.IDBox.SelectionLength = 0;
            this.IDBox.SelectionStart = 0;
            this.IDBox.Size = new System.Drawing.Size(97, 23);
            this.IDBox.TabIndex = 0;
            this.IDBox.TabStop = false;
            this.IDBox.UseSystemPasswordChar = false;
            this.IDBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.IDBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IDBox_KeyPress);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 77);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(97, 19);
            this.materialLabel1.TabIndex = 3;
            this.materialLabel1.Text = "View civilian:";
            // 
            // PlateBox
            // 
            this.PlateBox.Depth = 0;
            this.PlateBox.Hint = "License plate";
            this.PlateBox.Location = new System.Drawing.Point(115, 109);
            this.PlateBox.MaxLength = 8;
            this.PlateBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.PlateBox.Name = "PlateBox";
            this.PlateBox.PasswordChar = '\0';
            this.PlateBox.SelectedText = "";
            this.PlateBox.SelectionLength = 0;
            this.PlateBox.SelectionStart = 0;
            this.PlateBox.Size = new System.Drawing.Size(97, 23);
            this.PlateBox.TabIndex = 1;
            this.PlateBox.TabStop = false;
            this.PlateBox.UseSystemPasswordChar = false;
            this.PlateBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.PlateBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PlateBox_KeyPress);
            // 
            // NameBox
            // 
            this.NameBox.Depth = 0;
            this.NameBox.Hint = "Name";
            this.NameBox.Location = new System.Drawing.Point(12, 138);
            this.NameBox.MaxLength = 30;
            this.NameBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.NameBox.Name = "NameBox";
            this.NameBox.PasswordChar = '\0';
            this.NameBox.SelectedText = "";
            this.NameBox.SelectionLength = 0;
            this.NameBox.SelectionStart = 0;
            this.NameBox.Size = new System.Drawing.Size(200, 23);
            this.NameBox.TabIndex = 4;
            this.NameBox.TabStop = false;
            this.NameBox.UseSystemPasswordChar = false;
            this.NameBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.NameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NameBox_KeyPress);
            // 
            // LaunchBtn
            // 
            this.LaunchBtn.AutoSize = true;
            this.LaunchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LaunchBtn.Depth = 0;
            this.LaunchBtn.Icon = null;
            this.LaunchBtn.Location = new System.Drawing.Point(12, 167);
            this.LaunchBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LaunchBtn.Name = "LaunchBtn";
            this.LaunchBtn.Primary = true;
            this.LaunchBtn.Size = new System.Drawing.Size(113, 36);
            this.LaunchBtn.TabIndex = 5;
            this.LaunchBtn.Text = "View civilian";
            this.LaunchBtn.UseVisualStyleBackColor = true;
            this.LaunchBtn.Click += new System.EventHandler(this.launch_Click);
            // 
            // DispatchMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 215);
            this.Controls.Add(this.LaunchBtn);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.PlateBox);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.IDBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DispatchMenu";
            this.Text = "Dispatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField IDBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField PlateBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField NameBox;
        private MaterialSkin.Controls.MaterialRaisedButton LaunchBtn;
    }
}