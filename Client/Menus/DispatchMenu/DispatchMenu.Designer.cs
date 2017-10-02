namespace Client.Menus
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
            this.id = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.plate = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.name = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.launch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.Depth = 0;
            this.id.Hint = "Civilian ID";
            this.id.Location = new System.Drawing.Point(12, 109);
            this.id.MaxLength = 4;
            this.id.MouseState = MaterialSkin.MouseState.HOVER;
            this.id.Name = "id";
            this.id.PasswordChar = '\0';
            this.id.SelectedText = "";
            this.id.SelectionLength = 0;
            this.id.SelectionStart = 0;
            this.id.Size = new System.Drawing.Size(97, 23);
            this.id.TabIndex = 0;
            this.id.TabStop = false;
            this.id.UseSystemPasswordChar = false;
            this.id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.id_KeyPress);
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
            // plate
            // 
            this.plate.Depth = 0;
            this.plate.Hint = "License plate";
            this.plate.Location = new System.Drawing.Point(115, 109);
            this.plate.MaxLength = 8;
            this.plate.MouseState = MaterialSkin.MouseState.HOVER;
            this.plate.Name = "plate";
            this.plate.PasswordChar = '\0';
            this.plate.SelectedText = "";
            this.plate.SelectionLength = 0;
            this.plate.SelectionStart = 0;
            this.plate.Size = new System.Drawing.Size(97, 23);
            this.plate.TabIndex = 1;
            this.plate.TabStop = false;
            this.plate.UseSystemPasswordChar = false;
            this.plate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.plate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.plate_KeyPress);
            // 
            // name
            // 
            this.name.Depth = 0;
            this.name.Hint = "Name";
            this.name.Location = new System.Drawing.Point(12, 138);
            this.name.MaxLength = 30;
            this.name.MouseState = MaterialSkin.MouseState.HOVER;
            this.name.Name = "name";
            this.name.PasswordChar = '\0';
            this.name.SelectedText = "";
            this.name.SelectionLength = 0;
            this.name.SelectionStart = 0;
            this.name.Size = new System.Drawing.Size(200, 23);
            this.name.TabIndex = 4;
            this.name.TabStop = false;
            this.name.UseSystemPasswordChar = false;
            this.name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.name_KeyPress);
            // 
            // launch
            // 
            this.launch.AutoSize = true;
            this.launch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.launch.Depth = 0;
            this.launch.Icon = null;
            this.launch.Location = new System.Drawing.Point(12, 167);
            this.launch.MouseState = MaterialSkin.MouseState.HOVER;
            this.launch.Name = "launch";
            this.launch.Primary = true;
            this.launch.Size = new System.Drawing.Size(113, 36);
            this.launch.TabIndex = 5;
            this.launch.Text = "View civilian";
            this.launch.UseVisualStyleBackColor = true;
            this.launch.Click += new System.EventHandler(this.launch_Click);
            // 
            // DispatchMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 215);
            this.Controls.Add(this.launch);
            this.Controls.Add(this.name);
            this.Controls.Add(this.plate);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.id);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DispatchMenu";
            this.Text = "Dispatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField id;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField plate;
        private MaterialSkin.Controls.MaterialSingleLineTextField name;
        private MaterialSkin.Controls.MaterialRaisedButton launch;
    }
}