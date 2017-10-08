namespace CloneCAD.Client.Menus
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.CivilianRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.PoliceRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.DispatchRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.LaunchBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.CloseCheckbox = new MaterialSkin.Controls.MaterialCheckBox();
            this.SuspendLayout();
            // 
            // CivilianRadial
            // 
            this.CivilianRadial.AutoSize = true;
            this.CivilianRadial.Depth = 0;
            this.CivilianRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.CivilianRadial.Location = new System.Drawing.Point(9, 77);
            this.CivilianRadial.Margin = new System.Windows.Forms.Padding(0);
            this.CivilianRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CivilianRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.CivilianRadial.Name = "CivilianRadial";
            this.CivilianRadial.Ripple = true;
            this.CivilianRadial.Size = new System.Drawing.Size(128, 30);
            this.CivilianRadial.TabIndex = 0;
            this.CivilianRadial.TabStop = true;
            this.CivilianRadial.Text = "Civilian Selector";
            this.CivilianRadial.UseVisualStyleBackColor = true;
            // 
            // PoliceRadial
            // 
            this.PoliceRadial.AutoSize = true;
            this.PoliceRadial.Depth = 0;
            this.PoliceRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.PoliceRadial.Location = new System.Drawing.Point(9, 111);
            this.PoliceRadial.Margin = new System.Windows.Forms.Padding(0);
            this.PoliceRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.PoliceRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.PoliceRadial.Name = "PoliceRadial";
            this.PoliceRadial.Ripple = true;
            this.PoliceRadial.Size = new System.Drawing.Size(67, 30);
            this.PoliceRadial.TabIndex = 1;
            this.PoliceRadial.TabStop = true;
            this.PoliceRadial.Text = "Police";
            this.PoliceRadial.UseVisualStyleBackColor = true;
            // 
            // DispatchRadial
            // 
            this.DispatchRadial.AutoSize = true;
            this.DispatchRadial.Depth = 0;
            this.DispatchRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.DispatchRadial.Location = new System.Drawing.Point(9, 145);
            this.DispatchRadial.Margin = new System.Windows.Forms.Padding(0);
            this.DispatchRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.DispatchRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.DispatchRadial.Name = "DispatchRadial";
            this.DispatchRadial.Ripple = true;
            this.DispatchRadial.Size = new System.Drawing.Size(83, 30);
            this.DispatchRadial.TabIndex = 2;
            this.DispatchRadial.TabStop = true;
            this.DispatchRadial.Text = "Dispatch";
            this.DispatchRadial.UseVisualStyleBackColor = true;
            // 
            // LaunchBtn
            // 
            this.LaunchBtn.AutoSize = true;
            this.LaunchBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LaunchBtn.Depth = 0;
            this.LaunchBtn.Icon = null;
            this.LaunchBtn.Location = new System.Drawing.Point(12, 240);
            this.LaunchBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LaunchBtn.Name = "LaunchBtn";
            this.LaunchBtn.Primary = true;
            this.LaunchBtn.Size = new System.Drawing.Size(75, 36);
            this.LaunchBtn.TabIndex = 3;
            this.LaunchBtn.Text = "Launch";
            this.LaunchBtn.UseVisualStyleBackColor = true;
            this.LaunchBtn.Click += new System.EventHandler(this.Launch_Click);
            // 
            // CloseCheckbox
            // 
            this.CloseCheckbox.AutoSize = true;
            this.CloseCheckbox.Checked = true;
            this.CloseCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CloseCheckbox.Depth = 0;
            this.CloseCheckbox.Font = new System.Drawing.Font("Roboto", 10F);
            this.CloseCheckbox.Location = new System.Drawing.Point(12, 203);
            this.CloseCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.CloseCheckbox.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CloseCheckbox.MouseState = MaterialSkin.MouseState.HOVER;
            this.CloseCheckbox.Name = "CloseCheckbox";
            this.CloseCheckbox.Ripple = true;
            this.CloseCheckbox.Size = new System.Drawing.Size(128, 30);
            this.CloseCheckbox.TabIndex = 4;
            this.CloseCheckbox.Text = "Close on launch";
            this.CloseCheckbox.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 288);
            this.Controls.Add(this.CloseCheckbox);
            this.Controls.Add(this.LaunchBtn);
            this.Controls.Add(this.DispatchRadial);
            this.Controls.Add(this.PoliceRadial);
            this.Controls.Add(this.CivilianRadial);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRadioButton CivilianRadial;
        private MaterialSkin.Controls.MaterialRadioButton PoliceRadial;
        private MaterialSkin.Controls.MaterialRadioButton DispatchRadial;
        private MaterialSkin.Controls.MaterialRaisedButton LaunchBtn;
        private MaterialSkin.Controls.MaterialCheckBox CloseCheckbox;
    }
}

