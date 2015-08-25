namespace ZNQQ
{
    partial class PCQQForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查找ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cbR0x0825 = new System.Windows.Forms.CheckBox();
            this.cbS0x0825 = new System.Windows.Forms.CheckBox();
            this.cbR0x0826 = new System.Windows.Forms.CheckBox();
            this.cbS0x0826 = new System.Windows.Forms.CheckBox();
            this.cbR0x0828 = new System.Windows.Forms.CheckBox();
            this.cbS0x0828 = new System.Windows.Forms.CheckBox();
            this.cbR0x00CD = new System.Windows.Forms.CheckBox();
            this.cbS0x00CD = new System.Windows.Forms.CheckBox();
            this.cbSNKEY = new System.Windows.Forms.CheckBox();
            this.cbWrap = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.cbAutoScroll = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(992, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "logIn";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbDebug
            // 
            this.rtbDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDebug.AutoWordSelection = true;
            this.rtbDebug.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbDebug.EnableAutoDragDrop = true;
            this.rtbDebug.Location = new System.Drawing.Point(3, 2);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbDebug.ShowSelectionMargin = true;
            this.rtbDebug.Size = new System.Drawing.Size(1074, 313);
            this.rtbDebug.TabIndex = 3;
            this.rtbDebug.Text = "";
            this.rtbDebug.TextChanged += new System.EventHandler(this.rtbDebug_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查找ToolStripMenuItem,
            this.全选ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 查找ToolStripMenuItem
            // 
            this.查找ToolStripMenuItem.Name = "查找ToolStripMenuItem";
            this.查找ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.查找ToolStripMenuItem.Text = "查找";
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.Location = new System.Drawing.Point(911, 350);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "loginOut";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button3.Location = new System.Drawing.Point(830, 350);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "start";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbR0x0825
            // 
            this.cbR0x0825.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbR0x0825.AutoSize = true;
            this.cbR0x0825.Location = new System.Drawing.Point(13, 357);
            this.cbR0x0825.Name = "cbR0x0825";
            this.cbR0x0825.Size = new System.Drawing.Size(66, 16);
            this.cbR0x0825.TabIndex = 6;
            this.cbR0x0825.Text = "R0x0825";
            this.cbR0x0825.UseVisualStyleBackColor = true;
            // 
            // cbS0x0825
            // 
            this.cbS0x0825.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbS0x0825.AutoSize = true;
            this.cbS0x0825.Location = new System.Drawing.Point(97, 357);
            this.cbS0x0825.Name = "cbS0x0825";
            this.cbS0x0825.Size = new System.Drawing.Size(66, 16);
            this.cbS0x0825.TabIndex = 7;
            this.cbS0x0825.Text = "S0x0825";
            this.cbS0x0825.UseVisualStyleBackColor = true;
            // 
            // cbR0x0826
            // 
            this.cbR0x0826.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbR0x0826.AutoSize = true;
            this.cbR0x0826.Location = new System.Drawing.Point(181, 357);
            this.cbR0x0826.Name = "cbR0x0826";
            this.cbR0x0826.Size = new System.Drawing.Size(66, 16);
            this.cbR0x0826.TabIndex = 8;
            this.cbR0x0826.Text = "R0x0826";
            this.cbR0x0826.UseVisualStyleBackColor = true;
            // 
            // cbS0x0826
            // 
            this.cbS0x0826.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbS0x0826.AutoSize = true;
            this.cbS0x0826.Location = new System.Drawing.Point(265, 357);
            this.cbS0x0826.Name = "cbS0x0826";
            this.cbS0x0826.Size = new System.Drawing.Size(66, 16);
            this.cbS0x0826.TabIndex = 9;
            this.cbS0x0826.Text = "S0x0826";
            this.cbS0x0826.UseVisualStyleBackColor = true;
            // 
            // cbR0x0828
            // 
            this.cbR0x0828.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbR0x0828.AutoSize = true;
            this.cbR0x0828.Location = new System.Drawing.Point(349, 357);
            this.cbR0x0828.Name = "cbR0x0828";
            this.cbR0x0828.Size = new System.Drawing.Size(66, 16);
            this.cbR0x0828.TabIndex = 10;
            this.cbR0x0828.Text = "R0x0828";
            this.cbR0x0828.UseVisualStyleBackColor = true;
            // 
            // cbS0x0828
            // 
            this.cbS0x0828.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbS0x0828.AutoSize = true;
            this.cbS0x0828.Location = new System.Drawing.Point(433, 357);
            this.cbS0x0828.Name = "cbS0x0828";
            this.cbS0x0828.Size = new System.Drawing.Size(66, 16);
            this.cbS0x0828.TabIndex = 11;
            this.cbS0x0828.Text = "S0x0828";
            this.cbS0x0828.UseVisualStyleBackColor = true;
            // 
            // cbR0x00CD
            // 
            this.cbR0x00CD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbR0x00CD.AutoSize = true;
            this.cbR0x00CD.Location = new System.Drawing.Point(517, 357);
            this.cbR0x00CD.Name = "cbR0x00CD";
            this.cbR0x00CD.Size = new System.Drawing.Size(66, 16);
            this.cbR0x00CD.TabIndex = 12;
            this.cbR0x00CD.Text = "R0x00CD";
            this.cbR0x00CD.UseVisualStyleBackColor = true;
            // 
            // cbS0x00CD
            // 
            this.cbS0x00CD.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbS0x00CD.AutoSize = true;
            this.cbS0x00CD.Location = new System.Drawing.Point(601, 357);
            this.cbS0x00CD.Name = "cbS0x00CD";
            this.cbS0x00CD.Size = new System.Drawing.Size(66, 16);
            this.cbS0x00CD.TabIndex = 13;
            this.cbS0x00CD.Text = "S0x00CD";
            this.cbS0x00CD.UseVisualStyleBackColor = true;
            // 
            // cbSNKEY
            // 
            this.cbSNKEY.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbSNKEY.AutoSize = true;
            this.cbSNKEY.Location = new System.Drawing.Point(685, 357);
            this.cbSNKEY.Name = "cbSNKEY";
            this.cbSNKEY.Size = new System.Drawing.Size(54, 16);
            this.cbSNKEY.TabIndex = 14;
            this.cbSNKEY.Text = "SNKEY";
            this.cbSNKEY.UseVisualStyleBackColor = true;
            // 
            // cbWrap
            // 
            this.cbWrap.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbWrap.AutoSize = true;
            this.cbWrap.Checked = true;
            this.cbWrap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbWrap.Location = new System.Drawing.Point(756, 357);
            this.cbWrap.Name = "cbWrap";
            this.cbWrap.Size = new System.Drawing.Size(48, 16);
            this.cbWrap.TabIndex = 15;
            this.cbWrap.Text = "Wrap";
            this.cbWrap.UseVisualStyleBackColor = true;
            this.cbWrap.CheckedChanged += new System.EventHandler(this.cbWrap_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSend.Location = new System.Drawing.Point(830, 321);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cbAutoScroll
            // 
            this.cbAutoScroll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbAutoScroll.AutoSize = true;
            this.cbAutoScroll.Location = new System.Drawing.Point(13, 327);
            this.cbAutoScroll.Name = "cbAutoScroll";
            this.cbAutoScroll.Size = new System.Drawing.Size(72, 16);
            this.cbAutoScroll.TabIndex = 17;
            this.cbAutoScroll.Text = "自动滚屏";
            this.cbAutoScroll.UseVisualStyleBackColor = true;
            // 
            // PCQQForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 385);
            this.Controls.Add(this.cbAutoScroll);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cbWrap);
            this.Controls.Add(this.cbSNKEY);
            this.Controls.Add(this.cbS0x00CD);
            this.Controls.Add(this.cbR0x00CD);
            this.Controls.Add(this.cbS0x0828);
            this.Controls.Add(this.cbR0x0828);
            this.Controls.Add(this.cbS0x0826);
            this.Controls.Add(this.cbR0x0826);
            this.Controls.Add(this.cbS0x0825);
            this.Controls.Add(this.cbR0x0825);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.rtbDebug);
            this.Controls.Add(this.button1);
            this.Name = "PCQQForm";
            this.Text = "PCQQForm";
            this.Load += new System.EventHandler(this.PCQQForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.RichTextBox rtbDebug;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox cbR0x0825;
        private System.Windows.Forms.CheckBox cbS0x0825;
        private System.Windows.Forms.CheckBox cbR0x0826;
        private System.Windows.Forms.CheckBox cbS0x0826;
        private System.Windows.Forms.CheckBox cbR0x0828;
        private System.Windows.Forms.CheckBox cbS0x0828;
        private System.Windows.Forms.CheckBox cbR0x00CD;
        private System.Windows.Forms.CheckBox cbS0x00CD;
        private System.Windows.Forms.CheckBox cbSNKEY;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查找ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbWrap;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox cbAutoScroll;
    }
}