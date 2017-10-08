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
            this.PriceBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.DescriptionBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.IDBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.WarningRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.TicketRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.CitationRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.FixItRadial = new MaterialSkin.Controls.MaterialRadioButton();
            this.GiveTicketBtn = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // PriceBox
            // 
            this.PriceBox.Depth = 0;
            this.PriceBox.Hint = "Ticket price";
            this.PriceBox.Location = new System.Drawing.Point(128, 154);
            this.PriceBox.MaxLength = 4;
            this.PriceBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.PriceBox.Name = "PriceBox";
            this.PriceBox.PasswordChar = '\0';
            this.PriceBox.SelectedText = "";
            this.PriceBox.SelectionLength = 0;
            this.PriceBox.SelectionStart = 0;
            this.PriceBox.Size = new System.Drawing.Size(110, 23);
            this.PriceBox.TabIndex = 1;
            this.PriceBox.TabStop = false;
            this.PriceBox.UseSystemPasswordChar = false;
            this.PriceBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.PriceBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PriceBox_KeyPress);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Depth = 0;
            this.DescriptionBox.Hint = "Ticket description";
            this.DescriptionBox.Location = new System.Drawing.Point(12, 183);
            this.DescriptionBox.MaxLength = 30;
            this.DescriptionBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.PasswordChar = '\0';
            this.DescriptionBox.SelectedText = "";
            this.DescriptionBox.SelectionLength = 0;
            this.DescriptionBox.SelectionStart = 0;
            this.DescriptionBox.Size = new System.Drawing.Size(226, 23);
            this.DescriptionBox.TabIndex = 2;
            this.DescriptionBox.TabStop = false;
            this.DescriptionBox.UseSystemPasswordChar = false;
            this.DescriptionBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            // 
            // IDBox
            // 
            this.IDBox.Depth = 0;
            this.IDBox.Hint = "Civilian ID";
            this.IDBox.Location = new System.Drawing.Point(12, 154);
            this.IDBox.MaxLength = 11;
            this.IDBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.IDBox.Name = "IDBox";
            this.IDBox.PasswordChar = '\0';
            this.IDBox.SelectedText = "";
            this.IDBox.SelectionLength = 0;
            this.IDBox.SelectionStart = 0;
            this.IDBox.Size = new System.Drawing.Size(110, 23);
            this.IDBox.TabIndex = 0;
            this.IDBox.TabStop = false;
            this.IDBox.UseSystemPasswordChar = false;
            this.IDBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.IDBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IDBox_KeyPress);
            // 
            // WarningRadial
            // 
            this.WarningRadial.AutoSize = true;
            this.WarningRadial.Checked = true;
            this.WarningRadial.Depth = 0;
            this.WarningRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.WarningRadial.Location = new System.Drawing.Point(9, 74);
            this.WarningRadial.Margin = new System.Windows.Forms.Padding(0);
            this.WarningRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.WarningRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.WarningRadial.Name = "WarningRadial";
            this.WarningRadial.Ripple = true;
            this.WarningRadial.Size = new System.Drawing.Size(80, 30);
            this.WarningRadial.TabIndex = 3;
            this.WarningRadial.TabStop = true;
            this.WarningRadial.Text = "Warning";
            this.WarningRadial.UseVisualStyleBackColor = true;
            // 
            // TicketRadial
            // 
            this.TicketRadial.AutoSize = true;
            this.TicketRadial.Depth = 0;
            this.TicketRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.TicketRadial.Location = new System.Drawing.Point(164, 104);
            this.TicketRadial.Margin = new System.Windows.Forms.Padding(0);
            this.TicketRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.TicketRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.TicketRadial.Name = "TicketRadial";
            this.TicketRadial.Ripple = true;
            this.TicketRadial.Size = new System.Drawing.Size(67, 30);
            this.TicketRadial.TabIndex = 4;
            this.TicketRadial.Text = "Ticket";
            this.TicketRadial.UseVisualStyleBackColor = true;
            // 
            // CitationRadial
            // 
            this.CitationRadial.AutoSize = true;
            this.CitationRadial.Depth = 0;
            this.CitationRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.CitationRadial.Location = new System.Drawing.Point(164, 74);
            this.CitationRadial.Margin = new System.Windows.Forms.Padding(0);
            this.CitationRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.CitationRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.CitationRadial.Name = "CitationRadial";
            this.CitationRadial.Ripple = true;
            this.CitationRadial.Size = new System.Drawing.Size(77, 30);
            this.CitationRadial.TabIndex = 5;
            this.CitationRadial.Text = "Citation";
            this.CitationRadial.UseVisualStyleBackColor = true;
            // 
            // FixItRadial
            // 
            this.FixItRadial.AutoSize = true;
            this.FixItRadial.Depth = 0;
            this.FixItRadial.Font = new System.Drawing.Font("Roboto", 10F);
            this.FixItRadial.Location = new System.Drawing.Point(9, 104);
            this.FixItRadial.Margin = new System.Windows.Forms.Padding(0);
            this.FixItRadial.MouseLocation = new System.Drawing.Point(-1, -1);
            this.FixItRadial.MouseState = MaterialSkin.MouseState.HOVER;
            this.FixItRadial.Name = "FixItRadial";
            this.FixItRadial.Ripple = true;
            this.FixItRadial.Size = new System.Drawing.Size(59, 30);
            this.FixItRadial.TabIndex = 6;
            this.FixItRadial.Text = "Fix-it";
            this.FixItRadial.UseVisualStyleBackColor = true;
            // 
            // GiveTicketBtn
            // 
            this.GiveTicketBtn.AutoSize = true;
            this.GiveTicketBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GiveTicketBtn.Depth = 0;
            this.GiveTicketBtn.Icon = null;
            this.GiveTicketBtn.Location = new System.Drawing.Point(12, 212);
            this.GiveTicketBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.GiveTicketBtn.Name = "GiveTicketBtn";
            this.GiveTicketBtn.Primary = true;
            this.GiveTicketBtn.Size = new System.Drawing.Size(100, 36);
            this.GiveTicketBtn.TabIndex = 7;
            this.GiveTicketBtn.Text = "Give ticket";
            this.GiveTicketBtn.UseVisualStyleBackColor = true;
            this.GiveTicketBtn.Click += new System.EventHandler(this.GiveTicketBtn_Click);
            // 
            // PopoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 260);
            this.Controls.Add(this.GiveTicketBtn);
            this.Controls.Add(this.FixItRadial);
            this.Controls.Add(this.CitationRadial);
            this.Controls.Add(this.TicketRadial);
            this.Controls.Add(this.WarningRadial);
            this.Controls.Add(this.IDBox);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.PriceBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PopoMenu";
            this.Text = "Police";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField PriceBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField DescriptionBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField IDBox;
        private MaterialSkin.Controls.MaterialRadioButton WarningRadial;
        private MaterialSkin.Controls.MaterialRadioButton TicketRadial;
        private MaterialSkin.Controls.MaterialRadioButton CitationRadial;
        private MaterialSkin.Controls.MaterialRadioButton FixItRadial;
        private MaterialSkin.Controls.MaterialRaisedButton GiveTicketBtn;
    }
}