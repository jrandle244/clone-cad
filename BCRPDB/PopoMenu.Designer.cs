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
            this.type = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.context = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.id = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // type
            // 
            this.type.Depth = 0;
            this.type.Hint = "Ticket type";
            this.type.Location = new System.Drawing.Point(90, 78);
            this.type.MaxLength = 32767;
            this.type.MouseState = MaterialSkin.MouseState.HOVER;
            this.type.Name = "type";
            this.type.PasswordChar = '\0';
            this.type.SelectedText = "";
            this.type.SelectionLength = 0;
            this.type.SelectionStart = 0;
            this.type.Size = new System.Drawing.Size(148, 23);
            this.type.TabIndex = 1;
            this.type.TabStop = false;
            this.type.UseSystemPasswordChar = false;
            this.type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.type_KeyDown);
            // 
            // context
            // 
            this.context.Depth = 0;
            this.context.Hint = "Ticket context";
            this.context.Location = new System.Drawing.Point(12, 107);
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
            this.context.KeyDown += new System.Windows.Forms.KeyEventHandler(this.context_KeyDown);
            // 
            // id
            // 
            this.id.Depth = 0;
            this.id.Hint = "ID";
            this.id.Location = new System.Drawing.Point(12, 78);
            this.id.MaxLength = 5;
            this.id.MouseState = MaterialSkin.MouseState.HOVER;
            this.id.Name = "id";
            this.id.PasswordChar = '\0';
            this.id.SelectedText = "";
            this.id.SelectionLength = 0;
            this.id.SelectionStart = 0;
            this.id.Size = new System.Drawing.Size(72, 23);
            this.id.TabIndex = 0;
            this.id.TabStop = false;
            this.id.UseSystemPasswordChar = false;
            this.id.KeyDown += new System.Windows.Forms.KeyEventHandler(this.id_KeyDown);
            this.id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.id_KeyPress);
            // 
            // PopoMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 142);
            this.Controls.Add(this.id);
            this.Controls.Add(this.context);
            this.Controls.Add(this.type);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PopoMenu";
            this.Text = "Police";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PopoMenu_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField type;
        private MaterialSkin.Controls.MaterialSingleLineTextField context;
        private MaterialSkin.Controls.MaterialSingleLineTextField id;
    }
}