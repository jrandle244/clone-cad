namespace CloneCAD.Client.Menus
{
    partial class CivView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CivView));
            this.NameBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.BusinessBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.PlateBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.TicketList = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SyncBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.RegisteredWepList = new MaterialSkin.Controls.MaterialListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.IDLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.SyncCheck = new MaterialSkin.Controls.MaterialCheckBox();
            this.SuspendLayout();
            // 
            // NameBox
            // 
            this.NameBox.Depth = 0;
            this.NameBox.Hint = "Full Name";
            this.NameBox.Location = new System.Drawing.Point(12, 75);
            this.NameBox.MaxLength = 30;
            this.NameBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.NameBox.Name = "NameBox";
            this.NameBox.PasswordChar = '\0';
            this.NameBox.SelectedText = "";
            this.NameBox.SelectionLength = 0;
            this.NameBox.SelectionStart = 0;
            this.NameBox.Size = new System.Drawing.Size(264, 23);
            this.NameBox.TabIndex = 0;
            this.NameBox.TabStop = false;
            this.NameBox.UseSystemPasswordChar = false;
            // 
            // BusinessBox
            // 
            this.BusinessBox.Depth = 0;
            this.BusinessBox.Hint = "Associated Business";
            this.BusinessBox.Location = new System.Drawing.Point(12, 105);
            this.BusinessBox.MaxLength = 50;
            this.BusinessBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.BusinessBox.Name = "BusinessBox";
            this.BusinessBox.PasswordChar = '\0';
            this.BusinessBox.SelectedText = "";
            this.BusinessBox.SelectionLength = 0;
            this.BusinessBox.SelectionStart = 0;
            this.BusinessBox.Size = new System.Drawing.Size(264, 23);
            this.BusinessBox.TabIndex = 1;
            this.BusinessBox.TabStop = false;
            this.BusinessBox.UseSystemPasswordChar = false;
            // 
            // PlateBox
            // 
            this.PlateBox.Depth = 0;
            this.PlateBox.Hint = "License Plate";
            this.PlateBox.Location = new System.Drawing.Point(12, 135);
            this.PlateBox.MaxLength = 8;
            this.PlateBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.PlateBox.Name = "PlateBox";
            this.PlateBox.PasswordChar = '\0';
            this.PlateBox.SelectedText = "";
            this.PlateBox.SelectionLength = 0;
            this.PlateBox.SelectionStart = 0;
            this.PlateBox.Size = new System.Drawing.Size(132, 23);
            this.PlateBox.TabIndex = 2;
            this.PlateBox.TabStop = false;
            this.PlateBox.UseSystemPasswordChar = false;
            // 
            // TicketList
            // 
            this.TicketList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TicketList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.TicketList.Depth = 0;
            this.TicketList.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.TicketList.FullRowSelect = true;
            this.TicketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.TicketList.Location = new System.Drawing.Point(574, 97);
            this.TicketList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TicketList.MouseState = MaterialSkin.MouseState.OUT;
            this.TicketList.MultiSelect = false;
            this.TicketList.Name = "TicketList";
            this.TicketList.OwnerDraw = true;
            this.TicketList.Size = new System.Drawing.Size(369, 257);
            this.TicketList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TicketList.TabIndex = 8;
            this.TicketList.UseCompatibleStateImageBehavior = false;
            this.TicketList.View = System.Windows.Forms.View.Details;
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
            // SyncBtn
            // 
            this.SyncBtn.AutoSize = true;
            this.SyncBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SyncBtn.Depth = 0;
            this.SyncBtn.Icon = null;
            this.SyncBtn.Location = new System.Drawing.Point(12, 318);
            this.SyncBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.SyncBtn.Name = "SyncBtn";
            this.SyncBtn.Primary = true;
            this.SyncBtn.Size = new System.Drawing.Size(56, 36);
            this.SyncBtn.TabIndex = 3;
            this.SyncBtn.Text = "Sync";
            this.SyncBtn.UseVisualStyleBackColor = true;
            this.SyncBtn.Click += new System.EventHandler(this.SyncBtn_Click);
            // 
            // RegisteredWepList
            // 
            this.RegisteredWepList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RegisteredWepList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.RegisteredWepList.Depth = 0;
            this.RegisteredWepList.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.RegisteredWepList.FullRowSelect = true;
            this.RegisteredWepList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.RegisteredWepList.Location = new System.Drawing.Point(312, 97);
            this.RegisteredWepList.MouseLocation = new System.Drawing.Point(-1, -1);
            this.RegisteredWepList.MouseState = MaterialSkin.MouseState.OUT;
            this.RegisteredWepList.Name = "RegisteredWepList";
            this.RegisteredWepList.OwnerDraw = true;
            this.RegisteredWepList.Size = new System.Drawing.Size(226, 201);
            this.RegisteredWepList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.RegisteredWepList.TabIndex = 6;
            this.RegisteredWepList.UseCompatibleStateImageBehavior = false;
            this.RegisteredWepList.View = System.Windows.Forms.View.Details;
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
            // IDLabel
            // 
            this.IDLabel.AutoSize = true;
            this.IDLabel.Depth = 0;
            this.IDLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.IDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.IDLabel.Location = new System.Drawing.Point(12, 224);
            this.IDLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(117, 19);
            this.IDLabel.TabIndex = 10;
            this.IDLabel.Text = "Your civilian ID: ";
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
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 5000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // SyncCheck
            // 
            this.SyncCheck.AutoSize = true;
            this.SyncCheck.Depth = 0;
            this.SyncCheck.Enabled = false;
            this.SyncCheck.Font = new System.Drawing.Font("Roboto", 10F);
            this.SyncCheck.Location = new System.Drawing.Point(201, 327);
            this.SyncCheck.Margin = new System.Windows.Forms.Padding(0);
            this.SyncCheck.MouseLocation = new System.Drawing.Point(-1, -1);
            this.SyncCheck.MouseState = MaterialSkin.MouseState.HOVER;
            this.SyncCheck.Name = "SyncCheck";
            this.SyncCheck.Ripple = true;
            this.SyncCheck.Size = new System.Drawing.Size(75, 30);
            this.SyncCheck.TabIndex = 9;
            this.SyncCheck.Text = "Synced";
            this.SyncCheck.UseVisualStyleBackColor = true;
            // 
            // CivView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 366);
            this.Controls.Add(this.SyncCheck);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.RegisteredWepList);
            this.Controls.Add(this.SyncBtn);
            this.Controls.Add(this.TicketList);
            this.Controls.Add(this.PlateBox);
            this.Controls.Add(this.BusinessBox);
            this.Controls.Add(this.NameBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CivView";
            this.Text = "Civilian Record (read-only)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField NameBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField BusinessBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField PlateBox;
        private MaterialSkin.Controls.MaterialListView TicketList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private MaterialSkin.Controls.MaterialRaisedButton SyncBtn;
        private MaterialSkin.Controls.MaterialListView RegisteredWepList;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialLabel IDLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private System.Windows.Forms.Timer Timer;
        private MaterialSkin.Controls.MaterialCheckBox SyncCheck;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}