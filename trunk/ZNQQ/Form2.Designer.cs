namespace ZNQQ
{
    partial class Form2
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
            this.rtbRawData = new System.Windows.Forms.RichTextBox();
            this.rtbData = new System.Windows.Forms.RichTextBox();
            this.rtbKEY = new System.Windows.Forms.RichTextBox();
            this.btnEnCrypt = new System.Windows.Forms.Button();
            this.btnDeCrypt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbRawData
            // 
            this.rtbRawData.Location = new System.Drawing.Point(12, 12);
            this.rtbRawData.Name = "rtbRawData";
            this.rtbRawData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbRawData.Size = new System.Drawing.Size(684, 206);
            this.rtbRawData.TabIndex = 0;
            this.rtbRawData.Text = "";
            // 
            // rtbData
            // 
            this.rtbData.Location = new System.Drawing.Point(12, 259);
            this.rtbData.Name = "rtbData";
            this.rtbData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbData.Size = new System.Drawing.Size(684, 240);
            this.rtbData.TabIndex = 1;
            this.rtbData.Text = "";
            // 
            // rtbKEY
            // 
            this.rtbKEY.Location = new System.Drawing.Point(12, 224);
            this.rtbKEY.Name = "rtbKEY";
            this.rtbKEY.Size = new System.Drawing.Size(489, 29);
            this.rtbKEY.TabIndex = 2;
            this.rtbKEY.Text = "";
            // 
            // btnEnCrypt
            // 
            this.btnEnCrypt.Location = new System.Drawing.Point(621, 224);
            this.btnEnCrypt.Name = "btnEnCrypt";
            this.btnEnCrypt.Size = new System.Drawing.Size(75, 29);
            this.btnEnCrypt.TabIndex = 3;
            this.btnEnCrypt.Text = "加密";
            this.btnEnCrypt.UseVisualStyleBackColor = true;
            this.btnEnCrypt.Click += new System.EventHandler(this.btnEnCrypt_Click);
            // 
            // btnDeCrypt
            // 
            this.btnDeCrypt.Location = new System.Drawing.Point(527, 224);
            this.btnDeCrypt.Name = "btnDeCrypt";
            this.btnDeCrypt.Size = new System.Drawing.Size(75, 29);
            this.btnDeCrypt.TabIndex = 4;
            this.btnDeCrypt.Text = "解密";
            this.btnDeCrypt.UseVisualStyleBackColor = true;
            this.btnDeCrypt.Click += new System.EventHandler(this.btnDeCrypt_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 511);
            this.Controls.Add(this.btnDeCrypt);
            this.Controls.Add(this.btnEnCrypt);
            this.Controls.Add(this.rtbKEY);
            this.Controls.Add(this.rtbData);
            this.Controls.Add(this.rtbRawData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbRawData;
        private System.Windows.Forms.RichTextBox rtbData;
        private System.Windows.Forms.RichTextBox rtbKEY;
        private System.Windows.Forms.Button btnEnCrypt;
        private System.Windows.Forms.Button btnDeCrypt;
    }
}