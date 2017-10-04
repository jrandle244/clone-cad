﻿namespace CloneCAD.Client.Menus
{
    partial class CivMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CivMenu));
            this.name = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.business = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.plate = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.ticketList = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.syncBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.regWepList = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.addWep = new MaterialSkin.Controls.MaterialRaisedButton();
            this.remWep = new MaterialSkin.Controls.MaterialRaisedButton();
            this.idDisp = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.sync = new MaterialSkin.Controls.MaterialCheckBox();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Depth = 0;
            this.name.Hint = "Full Name";
            this.name.Location = new System.Drawing.Point(12, 75);
            this.name.MaxLength = 30;
            this.name.MouseState = MaterialSkin.MouseState.HOVER;
            this.name.Name = "name";
            this.name.PasswordChar = '\0';
            this.name.SelectedText = "";
            this.name.SelectionLength = 0;
            this.name.SelectionStart = 0;
            this.name.Size = new System.Drawing.Size(264, 23);
            this.name.TabIndex = 0;
            this.name.TabStop = false;
            this.name.UseSystemPasswordChar = false;
            this.name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.name_KeyPress);
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // business
            // 
            this.business.Depth = 0;
            this.business.Hint = "Associated Business";
            this.business.Location = new System.Drawing.Point(12, 105);
            this.business.MaxLength = 50;
            this.business.MouseState = MaterialSkin.MouseState.HOVER;
            this.business.Name = "business";
            this.business.PasswordChar = '\0';
            this.business.SelectedText = "";
            this.business.SelectionLength = 0;
            this.business.SelectionStart = 0;
            this.business.Size = new System.Drawing.Size(264, 23);
            this.business.TabIndex = 1;
            this.business.TabStop = false;
            this.business.UseSystemPasswordChar = false;
            this.business.TextChanged += new System.EventHandler(this.business_TextChanged);
            // 
            // plate
            // 
            this.plate.Depth = 0;
            this.plate.Hint = "License Plate";
            this.plate.Location = new System.Drawing.Point(12, 135);
            this.plate.MaxLength = 8;
            this.plate.MouseState = MaterialSkin.MouseState.HOVER;
            this.plate.Name = "plate";
            this.plate.PasswordChar = '\0';
            this.plate.SelectedText = "";
            this.plate.SelectionLength = 0;
            this.plate.SelectionStart = 0;
            this.plate.Size = new System.Drawing.Size(132, 23);
            this.plate.TabIndex = 2;
            this.plate.TabStop = false;
            this.plate.UseSystemPasswordChar = false;
            this.plate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.plate_KeyPress);
            this.plate.TextChanged += new System.EventHandler(this.plate_TextChanged);
            // 
            // ticketList
            // 
            this.ticketList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ticketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.ticketList.Depth = 0;
            this.ticketList.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.ticketList.FullRowSelect = true;
            this.ticketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ticketList.Location = new System.Drawing.Point(574, 97);
            this.ticketList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ticketList.MouseState = MaterialSkin.MouseState.OUT;
            this.ticketList.MultiSelect = false;
            this.ticketList.Name = "ticketList";
            this.ticketList.OwnerDraw = true;
            this.ticketList.Size = new System.Drawing.Size(369, 257);
            this.ticketList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ticketList.TabIndex = 8;
            this.ticketList.UseCompatibleStateImageBehavior = false;
            this.ticketList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Price";
            this.columnHeader1.Width = 64;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            this.columnHeader2.Width = 89;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 213;
            // 
            // syncBtn
            // 
            this.syncBtn.AutoSize = true;
            this.syncBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.syncBtn.Depth = 0;
            this.syncBtn.Icon = null;
            this.syncBtn.Location = new System.Drawing.Point(12, 318);
            this.syncBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.syncBtn.Name = "syncBtn";
            this.syncBtn.Primary = true;
            this.syncBtn.Size = new System.Drawing.Size(56, 36);
            this.syncBtn.TabIndex = 3;
            this.syncBtn.Text = "Sync";
            this.syncBtn.UseVisualStyleBackColor = true;
            this.syncBtn.Click += new System.EventHandler(this.syncBtn_Click);
            // 
            // regWepList
            // 
            this.regWepList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.regWepList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.regWepList.Depth = 0;
            this.regWepList.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.regWepList.FullRowSelect = true;
            this.regWepList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.regWepList.Location = new System.Drawing.Point(312, 97);
            this.regWepList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.regWepList.MouseState = MaterialSkin.MouseState.OUT;
            this.regWepList.Name = "regWepList";
            this.regWepList.OwnerDraw = true;
            this.regWepList.Size = new System.Drawing.Size(226, 201);
            this.regWepList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.regWepList.TabIndex = 6;
            this.regWepList.UseCompatibleStateImageBehavior = false;
            this.regWepList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            this.columnHeader3.Width = 221;
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(282, 63);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(24, 304);
            this.materialDivider1.TabIndex = 6;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialDivider2
            // 
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(544, 63);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(24, 304);
            this.materialDivider2.TabIndex = 7;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // addWep
            // 
            this.addWep.AutoSize = true;
            this.addWep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addWep.Depth = 0;
            this.addWep.Icon = null;
            this.addWep.Location = new System.Drawing.Point(312, 318);
            this.addWep.MouseState = MaterialSkin.MouseState.HOVER;
            this.addWep.Name = "addWep";
            this.addWep.Primary = true;
            this.addWep.Size = new System.Drawing.Size(48, 36);
            this.addWep.TabIndex = 5;
            this.addWep.Text = "Add";
            this.addWep.UseVisualStyleBackColor = true;
            this.addWep.Click += new System.EventHandler(this.addWep_Click);
            // 
            // remWep
            // 
            this.remWep.AutoSize = true;
            this.remWep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.remWep.Depth = 0;
            this.remWep.Icon = null;
            this.remWep.Location = new System.Drawing.Point(463, 318);
            this.remWep.MouseState = MaterialSkin.MouseState.HOVER;
            this.remWep.Name = "remWep";
            this.remWep.Primary = true;
            this.remWep.Size = new System.Drawing.Size(75, 36);
            this.remWep.TabIndex = 7;
            this.remWep.Text = "Remove";
            this.remWep.UseVisualStyleBackColor = true;
            this.remWep.Click += new System.EventHandler(this.remWep_Click);
            // 
            // idDisp
            // 
            this.idDisp.AutoSize = true;
            this.idDisp.Depth = 0;
            this.idDisp.Font = new System.Drawing.Font("Roboto", 11F);
            this.idDisp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.idDisp.Location = new System.Drawing.Point(12, 224);
            this.idDisp.MouseState = MaterialSkin.MouseState.HOVER;
            this.idDisp.Name = "idDisp";
            this.idDisp.Size = new System.Drawing.Size(117, 19);
            this.idDisp.TabIndex = 10;
            this.idDisp.Text = "Your civilian ID: ";
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(312, 75);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(148, 19);
            this.materialLabel1.TabIndex = 11;
            this.materialLabel1.Text = "Registered weapons:";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(574, 75);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(63, 19);
            this.materialLabel2.TabIndex = 12;
            this.materialLabel2.Text = "Tickets:";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 5000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // sync
            // 
            this.sync.AutoSize = true;
            this.sync.Depth = 0;
            this.sync.Enabled = false;
            this.sync.Font = new System.Drawing.Font("Roboto", 10F);
            this.sync.Location = new System.Drawing.Point(201, 327);
            this.sync.Margin = new System.Windows.Forms.Padding(0);
            this.sync.MouseLocation = new System.Drawing.Point(-1, -1);
            this.sync.MouseState = MaterialSkin.MouseState.HOVER;
            this.sync.Name = "sync";
            this.sync.Ripple = true;
            this.sync.Size = new System.Drawing.Size(75, 30);
            this.sync.TabIndex = 9;
            this.sync.Text = "Synced";
            this.sync.UseVisualStyleBackColor = true;
            // 
            // CivMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 366);
            this.Controls.Add(this.sync);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.idDisp);
            this.Controls.Add(this.remWep);
            this.Controls.Add(this.addWep);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.regWepList);
            this.Controls.Add(this.syncBtn);
            this.Controls.Add(this.ticketList);
            this.Controls.Add(this.plate);
            this.Controls.Add(this.business);
            this.Controls.Add(this.name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CivMenu";
            this.Text = "Civilian Record";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CivMenu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CivMenu_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField name;
        private MaterialSkin.Controls.MaterialSingleLineTextField business;
        private MaterialSkin.Controls.MaterialSingleLineTextField plate;
        private MaterialSkin.Controls.MaterialListView ticketList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialRaisedButton syncBtn;
        private MaterialSkin.Controls.MaterialListView regWepList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialRaisedButton addWep;
        private MaterialSkin.Controls.MaterialRaisedButton remWep;
        private MaterialSkin.Controls.MaterialLabel idDisp;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.Timer timer;
        private MaterialSkin.Controls.MaterialCheckBox sync;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}