namespace CARFSecurerWinFormApp
{
    partial class FQueryEncryptedText
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.lEncryptName = new System.Windows.Forms.Label();
            this.comboBoxEncryptName = new System.Windows.Forms.ComboBox();
            this.lText = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbShowEncryptedText = new System.Windows.Forms.CheckBox();
            this.tbEncryptedText = new System.Windows.Forms.TextBox();
            this.lEncryptedText = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.cbShowText = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lEncryptName);
            this.panelMain.Controls.Add(this.comboBoxEncryptName);
            this.panelMain.Controls.Add(this.lText);
            this.panelMain.Controls.Add(this.btnClear);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.cbShowEncryptedText);
            this.panelMain.Controls.Add(this.tbEncryptedText);
            this.panelMain.Controls.Add(this.lEncryptedText);
            this.panelMain.Controls.Add(this.tbText);
            this.panelMain.Controls.Add(this.cbShowText);
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(278, 128);
            this.panelMain.TabIndex = 8;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.Layout += new System.Windows.Forms.LayoutEventHandler(this.panelMain_Layout);
            // 
            // lEncryptName
            // 
            this.lEncryptName.AutoSize = true;
            this.lEncryptName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lEncryptName.Location = new System.Drawing.Point(7, 7);
            this.lEncryptName.Name = "lEncryptName";
            this.lEncryptName.Size = new System.Drawing.Size(101, 12);
            this.lEncryptName.TabIndex = 17;
            this.lEncryptName.Text = "Encryption Name:";
            this.lEncryptName.Click += new System.EventHandler(this.lEncryptName_Click);
            // 
            // comboBoxEncryptName
            // 
            this.comboBoxEncryptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncryptName.FormattingEnabled = true;
            this.comboBoxEncryptName.Location = new System.Drawing.Point(108, 3);
            this.comboBoxEncryptName.Name = "comboBoxEncryptName";
            this.comboBoxEncryptName.Size = new System.Drawing.Size(104, 20);
            this.comboBoxEncryptName.TabIndex = 16;
            this.comboBoxEncryptName.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncryptName_SelectedIndexChanged);
            // 
            // lText
            // 
            this.lText.AutoSize = true;
            this.lText.Location = new System.Drawing.Point(7, 34);
            this.lText.Name = "lText";
            this.lText.Size = new System.Drawing.Size(35, 12);
            this.lText.TabIndex = 8;
            this.lText.Text = "Text:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(110, 100);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "C&lear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(191, 100);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbShowEncryptedText
            // 
            this.cbShowEncryptedText.AutoSize = true;
            this.cbShowEncryptedText.Location = new System.Drawing.Point(218, 60);
            this.cbShowEncryptedText.Name = "cbShowEncryptedText";
            this.cbShowEncryptedText.Size = new System.Drawing.Size(48, 16);
            this.cbShowEncryptedText.TabIndex = 13;
            this.cbShowEncryptedText.Text = "Show";
            this.cbShowEncryptedText.UseVisualStyleBackColor = true;
            this.cbShowEncryptedText.CheckedChanged += new System.EventHandler(this.cbShowEncryptedText_CheckedChanged);
            // 
            // tbEncryptedText
            // 
            this.tbEncryptedText.Location = new System.Drawing.Point(108, 58);
            this.tbEncryptedText.Name = "tbEncryptedText";
            this.tbEncryptedText.ReadOnly = true;
            this.tbEncryptedText.Size = new System.Drawing.Size(104, 21);
            this.tbEncryptedText.TabIndex = 12;
            // 
            // lEncryptedText
            // 
            this.lEncryptedText.AutoSize = true;
            this.lEncryptedText.Location = new System.Drawing.Point(7, 61);
            this.lEncryptedText.Name = "lEncryptedText";
            this.lEncryptedText.Size = new System.Drawing.Size(95, 12);
            this.lEncryptedText.TabIndex = 11;
            this.lEncryptedText.Text = "Encrypted Text:";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(48, 30);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(164, 21);
            this.tbText.TabIndex = 10;
            this.tbText.UseSystemPasswordChar = true;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // cbShowText
            // 
            this.cbShowText.AutoSize = true;
            this.cbShowText.Location = new System.Drawing.Point(218, 33);
            this.cbShowText.Name = "cbShowText";
            this.cbShowText.Size = new System.Drawing.Size(48, 16);
            this.cbShowText.TabIndex = 9;
            this.cbShowText.Text = "Show";
            this.cbShowText.UseVisualStyleBackColor = true;
            this.cbShowText.CheckedChanged += new System.EventHandler(this.cbShowText_CheckedChanged);
            // 
            // FQueryEncryptedText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 152);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FQueryEncryptedText";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FQueryPassword";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ComboBox comboBoxEncryptName;
        private System.Windows.Forms.Label lText;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox cbShowEncryptedText;
        private System.Windows.Forms.TextBox tbEncryptedText;
        private System.Windows.Forms.Label lEncryptedText;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.CheckBox cbShowText;
        private System.Windows.Forms.Label lEncryptName;

    }
}