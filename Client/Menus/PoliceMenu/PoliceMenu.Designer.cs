namespace CloneCAD.Client.Menus
{
    partial class PopoMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopoMenu));
            this.Price = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.Context = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.ID = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.WarningMode = new MaterialSkin.Controls.MaterialRadioButton();
            this.TicketMode = new MaterialSkin.Controls.MaterialRadioButton();
            this.CitationMode = new MaterialSkin.Controls.MaterialRadioButton();
            this.FixItMode = new MaterialSkin.Controls.MaterialRadioButton();
            this.GiveTicket = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // Price
            // 
            this.Price.Depth = 0;
            this.Price.Hint = "Ticket price";
            this.Price.Location = new System.Drawing.Point(128, 154);
            this.Price.MaxLength = 4;
            this.Price.MouseState = MaterialSkin.MouseState.HOVER;
            this.Price.Name = "Price";
            this.Price.PasswordChar = '\0';
            this.Price.SelectedText = "";
            this.Price.SelectionLength = 0;
            this.Price.SelectionStart = 0;
            this.Price.Size = new System.Drawing.Size(110, 23);
            this.Price.TabIndex = 1;
            this.Price.TabStop = false;
            this.Price.UseSystemPasswordChar = false;
            this.Price.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.Price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Price_KeyPress);
            // 
            // Context
            // 
            this.Context.Depth = 0;
            this.Context.Hint = "Ticket description";
            this.Context.Location = new System.Drawing.Point(12, 183);
            this.Context.MaxLength = 30;
            this.Context.MouseState = MaterialSkin.MouseState.HOVER;
            this.Context.Name = "Context";
            this.Context.PasswordChar = '\0';
            this.Context.SelectedText = "";
            this.Context.SelectionLength = 0;
            this.Context.SelectionStart = 0;
            this.Context.Size = new System.Drawing.Size(226, 23);
            this.Context.TabIndex = 2;
            this.Context.TabStop = false;
            this.Context.UseSystemPasswordChar = false;
            this.Context.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            // 
            // ID
            // 
            this.ID.Depth = 0;
            this.ID.Hint = "Civilian ID";
            this.ID.Location = new System.Drawing.Point(12, 154);
            this.ID.MaxLength = 11;
            this.ID.MouseState = MaterialSkin.MouseState.HOVER;
            this.ID.Name = "ID";
            this.ID.PasswordChar = '\0';
            this.ID.SelectedText = "";
            this.ID.SelectionLength = 0;
            this.ID.SelectionStart = 0;
            this.ID.Size = new System.Drawing.Size(110, 23);
            this.ID.TabIndex = 0;
            this.ID.TabStop = false;
            this.ID.UseSystemPasswordChar = false;
            this.ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ID_KeyPress);
            // 
            // WarningMode
            // 
            this.WarningMode.AutoSize = true;
            this.WarningMode.Checked = true;
            this.WarningMode.Depth = 0;
            this.WarningMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.WarningMode.Location = new System.Drawing.Point(9, 74);
            this.WarningMode.Margin = new System.Windows.Forms.Padding(0);
            this.WarningMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.WarningMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.WarningMode.Name = "WarningMode";
            this.WarningMode.Ripple = true;
            this.WarningMode.Size = new System.Drawing.Size(80, 30);
            this.WarningMode.TabIndex = 3;
            this.WarningMode.TabStop = true;
            this.WarningMode.Text = "Warning";
            this.WarningMode.UseVisualStyleBackColor = true;
            // 
            // TicketMode
            // 
            this.TicketMode.AutoSize = true;
            this.TicketMode.Depth = 0;
            this.TicketMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.TicketMode.Location = new System.Drawing.Point(164, 104);
            this.TicketMode.Margin = new System.Windows.Forms.Padding(0);
            this.TicketMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TicketMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.TicketMode.Name = "TicketMode";
            this.TicketMode.Ripple = true;
            this.TicketMode.Size = new System.Drawing.Size(67, 30);
            this.TicketMode.TabIndex = 4;
            this.TicketMode.Text = "Ticket";
            this.TicketMode.UseVisualStyleBackColor = true;
            // 
            // CitationMode
            // 
            this.CitationMode.AutoSize = true;
            this.CitationMode.Depth = 0;
            this.CitationMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.CitationMode.Location = new System.Drawing.Point(164, 74);
            this.CitationMode.Margin = new System.Windows.Forms.Padding(0);
            this.CitationMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CitationMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.CitationMode.Name = "CitationMode";
            this.CitationMode.Ripple = true;
            this.CitationMode.Size = new System.Drawing.Size(77, 30);
            this.CitationMode.TabIndex = 5;
            this.CitationMode.Text = "Citation";
            this.CitationMode.UseVisualStyleBackColor = true;
            // 
            // FixItMode
            // 
            this.FixItMode.AutoSize = true;
            this.FixItMode.Depth = 0;
            this.FixItMode.Font = new System.Drawing.Font("Roboto", 10F);
            this.FixItMode.Location = new System.Drawing.Point(9, 104);
            this.FixItMode.Margin = new System.Windows.Forms.Padding(0);
            this.FixItMode.MouseLocation = new System.Drawing.Point(-1, -1);
            this.FixItMode.MouseState = MaterialSkin.MouseState.HOVER;
            this.FixItMode.Name = "FixItMode";
            this.FixItMode.Ripple = true;
            this.FixItMode.Size = new System.Drawing.Size(59, 30);
            this.FixItMode.TabIndex = 6;
            this.FixItMode.Text = "Fix-it";
            this.FixItMode.UseVisualStyleBackColor = true;
            // 
            // GiveTicket
            // 
            this.GiveTicket.AutoSize = true;
            this.GiveTicket.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GiveTicket.Depth = 0;
            this.GiveTicket.Icon = null;
            this.GiveTicket.Location = new System.Drawing.Point(12, 212);
            this.GiveTicket.MouseState = MaterialSkin.MouseState.HOVER;
            this.GiveTicket.Name = "GiveTicket";
            this.GiveTicket.Primary = true;
            this.GiveTicket.Size = new System.Drawing.Size(100, 36);
            this.GiveTicket.TabIndex = 7;
            this.GiveTicket.Text = "Give ticket";
            this.GiveTicket.UseVisualStyleBackColor = true;
            this.GiveTicket.Click += new System.EventHandler(this.GiveTicket_Click);
            // 
            // PopoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 260);
            this.Controls.Add(this.GiveTicket);
            this.Controls.Add(this.FixItMode);
            this.Controls.Add(this.CitationMode);
            this.Controls.Add(this.TicketMode);
            this.Controls.Add(this.WarningMode);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.Context);
            this.Controls.Add(this.Price);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PoliceMenu";
            this.Text = "Police";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField Price;
        private MaterialSkin.Controls.MaterialSingleLineTextField Context;
        private MaterialSkin.Controls.MaterialSingleLineTextField ID;
        private MaterialSkin.Controls.MaterialRadioButton WarningMode;
        private MaterialSkin.Controls.MaterialRadioButton TicketMode;
        private MaterialSkin.Controls.MaterialRadioButton CitationMode;
        private MaterialSkin.Controls.MaterialRadioButton FixItMode;
        private MaterialSkin.Controls.MaterialRaisedButton GiveTicket;
    }
}