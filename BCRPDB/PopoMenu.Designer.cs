namespace BCRPDB
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
            this.price = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.context = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.id = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.warning = new MaterialSkin.Controls.MaterialRadioButton();
            this.ticket = new MaterialSkin.Controls.MaterialRadioButton();
            this.citation = new MaterialSkin.Controls.MaterialRadioButton();
            this.fixit = new MaterialSkin.Controls.MaterialRadioButton();
            this.SuspendLayout();
            // 
            // price
            // 
            this.price.Depth = 0;
            this.price.Hint = "Ticket price";
            this.price.Location = new System.Drawing.Point(128, 154);
            this.price.MaxLength = 4;
            this.price.MouseState = MaterialSkin.MouseState.HOVER;
            this.price.Name = "price";
            this.price.PasswordChar = '\0';
            this.price.SelectedText = "";
            this.price.SelectionLength = 0;
            this.price.SelectionStart = 0;
            this.price.Size = new System.Drawing.Size(110, 23);
            this.price.TabIndex = 1;
            this.price.TabStop = false;
            this.price.UseSystemPasswordChar = false;
            this.price.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_KeyPress);
            // 
            // context
            // 
            this.context.Depth = 0;
            this.context.Hint = "Ticket description";
            this.context.Location = new System.Drawing.Point(12, 183);
            this.context.MaxLength = 30;
            this.context.MouseState = MaterialSkin.MouseState.HOVER;
            this.context.Name = "context";
            this.context.PasswordChar = '\0';
            this.context.SelectedText = "";
            this.context.SelectionLength = 0;
            this.context.SelectionStart = 0;
            this.context.Size = new System.Drawing.Size(226, 23);
            this.context.TabIndex = 2;
            this.context.TabStop = false;
            this.context.UseSystemPasswordChar = false;
            this.context.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            // 
            // id
            // 
            this.id.Depth = 0;
            this.id.Hint = "Civilian ID";
            this.id.Location = new System.Drawing.Point(12, 154);
            this.id.MaxLength = 5;
            this.id.MouseState = MaterialSkin.MouseState.HOVER;
            this.id.Name = "id";
            this.id.PasswordChar = '\0';
            this.id.SelectedText = "";
            this.id.SelectionLength = 0;
            this.id.SelectionStart = 0;
            this.id.Size = new System.Drawing.Size(110, 23);
            this.id.TabIndex = 0;
            this.id.TabStop = false;
            this.id.UseSystemPasswordChar = false;
            this.id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyDown);
            this.id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.id_KeyPress);
            // 
            // warning
            // 
            this.warning.AutoSize = true;
            this.warning.Checked = true;
            this.warning.Depth = 0;
            this.warning.Font = new System.Drawing.Font("Roboto", 10F);
            this.warning.Location = new System.Drawing.Point(9, 74);
            this.warning.Margin = new System.Windows.Forms.Padding(0);
            this.warning.MouseLocation = new System.Drawing.Point(-1, -1);
            this.warning.MouseState = MaterialSkin.MouseState.HOVER;
            this.warning.Name = "warning";
            this.warning.Ripple = true;
            this.warning.Size = new System.Drawing.Size(80, 30);
            this.warning.TabIndex = 3;
            this.warning.TabStop = true;
            this.warning.Text = "Warning";
            this.warning.UseVisualStyleBackColor = true;
            // 
            // ticket
            // 
            this.ticket.AutoSize = true;
            this.ticket.Depth = 0;
            this.ticket.Font = new System.Drawing.Font("Roboto", 10F);
            this.ticket.Location = new System.Drawing.Point(164, 104);
            this.ticket.Margin = new System.Windows.Forms.Padding(0);
            this.ticket.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ticket.MouseState = MaterialSkin.MouseState.HOVER;
            this.ticket.Name = "ticket";
            this.ticket.Ripple = true;
            this.ticket.Size = new System.Drawing.Size(67, 30);
            this.ticket.TabIndex = 4;
            this.ticket.Text = "Ticket";
            this.ticket.UseVisualStyleBackColor = true;
            // 
            // citation
            // 
            this.citation.AutoSize = true;
            this.citation.Depth = 0;
            this.citation.Font = new System.Drawing.Font("Roboto", 10F);
            this.citation.Location = new System.Drawing.Point(164, 74);
            this.citation.Margin = new System.Windows.Forms.Padding(0);
            this.citation.MouseLocation = new System.Drawing.Point(-1, -1);
            this.citation.MouseState = MaterialSkin.MouseState.HOVER;
            this.citation.Name = "citation";
            this.citation.Ripple = true;
            this.citation.Size = new System.Drawing.Size(77, 30);
            this.citation.TabIndex = 5;
            this.citation.Text = "Citation";
            this.citation.UseVisualStyleBackColor = true;
            // 
            // fixit
            // 
            this.fixit.AutoSize = true;
            this.fixit.Depth = 0;
            this.fixit.Font = new System.Drawing.Font("Roboto", 10F);
            this.fixit.Location = new System.Drawing.Point(9, 104);
            this.fixit.Margin = new System.Windows.Forms.Padding(0);
            this.fixit.MouseLocation = new System.Drawing.Point(-1, -1);
            this.fixit.MouseState = MaterialSkin.MouseState.HOVER;
            this.fixit.Name = "fixit";
            this.fixit.Ripple = true;
            this.fixit.Size = new System.Drawing.Size(59, 30);
            this.fixit.TabIndex = 6;
            this.fixit.Text = "Fix-it";
            this.fixit.UseVisualStyleBackColor = true;
            // 
            // PopoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 218);
            this.Controls.Add(this.fixit);
            this.Controls.Add(this.citation);
            this.Controls.Add(this.ticket);
            this.Controls.Add(this.warning);
            this.Controls.Add(this.id);
            this.Controls.Add(this.context);
            this.Controls.Add(this.price);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PopoMenu";
            this.Text = "Police";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PopoMenu_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField price;
        private MaterialSkin.Controls.MaterialSingleLineTextField context;
        private MaterialSkin.Controls.MaterialSingleLineTextField id;
        private MaterialSkin.Controls.MaterialRadioButton warning;
        private MaterialSkin.Controls.MaterialRadioButton ticket;
        private MaterialSkin.Controls.MaterialRadioButton citation;
        private MaterialSkin.Controls.MaterialRadioButton fixit;
    }
}