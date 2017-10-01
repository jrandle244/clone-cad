namespace Client
{
    partial class CivLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CivLauncher));
            this.civs = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.create = new MaterialSkin.Controls.MaterialRaisedButton();
            this.delete = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // civs
            // 
            this.civs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.civs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.civs.Depth = 0;
            this.civs.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.civs.FullRowSelect = true;
            this.civs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.civs.Location = new System.Drawing.Point(0, 64);
            this.civs.MouseLocation = new System.Drawing.Point(-1, -1);
            this.civs.MouseState = MaterialSkin.MouseState.OUT;
            this.civs.MultiSelect = false;
            this.civs.Name = "civs";
            this.civs.OwnerDraw = true;
            this.civs.Size = new System.Drawing.Size(300, 236);
            this.civs.TabIndex = 0;
            this.civs.UseCompatibleStateImageBehavior = false;
            this.civs.View = System.Windows.Forms.View.Details;
            this.civs.DoubleClick += new System.EventHandler(this.civs_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 238;
            // 
            // create
            // 
            this.create.AutoSize = true;
            this.create.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.create.Depth = 0;
            this.create.Icon = null;
            this.create.Location = new System.Drawing.Point(12, 306);
            this.create.MouseState = MaterialSkin.MouseState.HOVER;
            this.create.Name = "create";
            this.create.Primary = true;
            this.create.Size = new System.Drawing.Size(71, 36);
            this.create.TabIndex = 1;
            this.create.Text = "Create";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.create_Click);
            // 
            // delete
            // 
            this.delete.AutoSize = true;
            this.delete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.delete.Depth = 0;
            this.delete.Icon = null;
            this.delete.Location = new System.Drawing.Point(217, 306);
            this.delete.MouseState = MaterialSkin.MouseState.HOVER;
            this.delete.Name = "delete";
            this.delete.Primary = true;
            this.delete.Size = new System.Drawing.Size(69, 36);
            this.delete.TabIndex = 2;
            this.delete.Text = "Delete";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // CivLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 354);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.create);
            this.Controls.Add(this.civs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CivLauncher";
            this.Text = "Civilian Selector";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CivLauncher_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialListView civs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialRaisedButton create;
        private MaterialSkin.Controls.MaterialRaisedButton delete;
    }
}