namespace CloneCAD
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
            this.civ = new MaterialSkin.Controls.MaterialRadioButton();
            this.popo = new MaterialSkin.Controls.MaterialRadioButton();
            this.dispatch = new MaterialSkin.Controls.MaterialRadioButton();
            this.launch = new MaterialSkin.Controls.MaterialRaisedButton();
            this.close = new MaterialSkin.Controls.MaterialCheckBox();
            this.SuspendLayout();
            // 
            // civ
            // 
            this.civ.AutoSize = true;
            this.civ.Depth = 0;
            this.civ.Font = new System.Drawing.Font("Roboto", 10F);
            this.civ.Location = new System.Drawing.Point(9, 77);
            this.civ.Margin = new System.Windows.Forms.Padding(0);
            this.civ.MouseLocation = new System.Drawing.Point(-1, -1);
            this.civ.MouseState = MaterialSkin.MouseState.HOVER;
            this.civ.Name = "civ";
            this.civ.Ripple = true;
            this.civ.Size = new System.Drawing.Size(74, 30);
            this.civ.TabIndex = 0;
            this.civ.TabStop = true;
            this.civ.Text = "Civilian";
            this.civ.UseVisualStyleBackColor = true;
            // 
            // popo
            // 
            this.popo.AutoSize = true;
            this.popo.Depth = 0;
            this.popo.Font = new System.Drawing.Font("Roboto", 10F);
            this.popo.Location = new System.Drawing.Point(9, 111);
            this.popo.Margin = new System.Windows.Forms.Padding(0);
            this.popo.MouseLocation = new System.Drawing.Point(-1, -1);
            this.popo.MouseState = MaterialSkin.MouseState.HOVER;
            this.popo.Name = "popo";
            this.popo.Ripple = true;
            this.popo.Size = new System.Drawing.Size(67, 30);
            this.popo.TabIndex = 1;
            this.popo.TabStop = true;
            this.popo.Text = "Police";
            this.popo.UseVisualStyleBackColor = true;
            // 
            // dispatch
            // 
            this.dispatch.AutoSize = true;
            this.dispatch.Depth = 0;
            this.dispatch.Font = new System.Drawing.Font("Roboto", 10F);
            this.dispatch.Location = new System.Drawing.Point(9, 145);
            this.dispatch.Margin = new System.Windows.Forms.Padding(0);
            this.dispatch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.dispatch.MouseState = MaterialSkin.MouseState.HOVER;
            this.dispatch.Name = "dispatch";
            this.dispatch.Ripple = true;
            this.dispatch.Size = new System.Drawing.Size(83, 30);
            this.dispatch.TabIndex = 2;
            this.dispatch.TabStop = true;
            this.dispatch.Text = "Dispatch";
            this.dispatch.UseVisualStyleBackColor = true;
            // 
            // launch
            // 
            this.launch.AutoSize = true;
            this.launch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.launch.Depth = 0;
            this.launch.Icon = null;
            this.launch.Location = new System.Drawing.Point(12, 240);
            this.launch.MouseState = MaterialSkin.MouseState.HOVER;
            this.launch.Name = "launch";
            this.launch.Primary = true;
            this.launch.Size = new System.Drawing.Size(75, 36);
            this.launch.TabIndex = 3;
            this.launch.Text = "launch";
            this.launch.UseVisualStyleBackColor = true;
            this.launch.Click += new System.EventHandler(this.launch_Click);
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Checked = true;
            this.close.CheckState = System.Windows.Forms.CheckState.Checked;
            this.close.Depth = 0;
            this.close.Font = new System.Drawing.Font("Roboto", 10F);
            this.close.Location = new System.Drawing.Point(12, 203);
            this.close.Margin = new System.Windows.Forms.Padding(0);
            this.close.MouseLocation = new System.Drawing.Point(-1, -1);
            this.close.MouseState = MaterialSkin.MouseState.HOVER;
            this.close.Name = "close";
            this.close.Ripple = true;
            this.close.Size = new System.Drawing.Size(128, 30);
            this.close.TabIndex = 4;
            this.close.Text = "Close on launch";
            this.close.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 288);
            this.Controls.Add(this.close);
            this.Controls.Add(this.launch);
            this.Controls.Add(this.dispatch);
            this.Controls.Add(this.popo);
            this.Controls.Add(this.civ);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialRadioButton civ;
        private MaterialSkin.Controls.MaterialRadioButton popo;
        private MaterialSkin.Controls.MaterialRadioButton dispatch;
        private MaterialSkin.Controls.MaterialRaisedButton launch;
        private MaterialSkin.Controls.MaterialCheckBox close;
    }
}

