namespace ZNQQ
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("周润发", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("刘德华", 1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("周星驰", 2);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "吴孟达",
            "111",
            "222",
            "333"}, 3);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList32x32 = new System.Windows.Forms.ImageList(this.components);
            this.imageList16x16 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.操作AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.rtbDebug = new System.Windows.Forms.RichTextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem1.ToolTipText = "英雄本色";
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4});
            this.listView1.LabelEdit = true;
            this.listView1.LargeImageList = this.imageList32x32;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(670, 337);
            this.listView1.SmallImageList = this.imageList16x16;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemActivate += new System.EventHandler(this.listView1_ItemActivate);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // imageList32x32
            // 
            this.imageList32x32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList32x32.ImageStream")));
            this.imageList32x32.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList32x32.Images.SetKeyName(0, "13797.gif");
            this.imageList32x32.Images.SetKeyName(1, "13798.gif");
            this.imageList32x32.Images.SetKeyName(2, "13799.gif");
            this.imageList32x32.Images.SetKeyName(3, "13800.gif");
            this.imageList32x32.Images.SetKeyName(4, "13803.gif");
            this.imageList32x32.Images.SetKeyName(5, "13804.gif");
            this.imageList32x32.Images.SetKeyName(6, "13809.gif");
            this.imageList32x32.Images.SetKeyName(7, "13810.gif");
            this.imageList32x32.Images.SetKeyName(8, "13811.gif");
            this.imageList32x32.Images.SetKeyName(9, "13812.gif");
            this.imageList32x32.Images.SetKeyName(10, "13914.gif");
            this.imageList32x32.Images.SetKeyName(11, "13915.gif");
            this.imageList32x32.Images.SetKeyName(12, "13916.gif");
            this.imageList32x32.Images.SetKeyName(13, "13918.gif");
            this.imageList32x32.Images.SetKeyName(14, "13919.gif");
            this.imageList32x32.Images.SetKeyName(15, "13920.gif");
            this.imageList32x32.Images.SetKeyName(16, "13926.gif");
            this.imageList32x32.Images.SetKeyName(17, "13927.gif");
            this.imageList32x32.Images.SetKeyName(18, "13953.gif");
            this.imageList32x32.Images.SetKeyName(19, "13954.gif");
            this.imageList32x32.Images.SetKeyName(20, "13955.gif");
            this.imageList32x32.Images.SetKeyName(21, "13956.gif");
            this.imageList32x32.Images.SetKeyName(22, "13957.gif");
            this.imageList32x32.Images.SetKeyName(23, "13958.gif");
            this.imageList32x32.Images.SetKeyName(24, "13959.gif");
            this.imageList32x32.Images.SetKeyName(25, "13960.gif");
            this.imageList32x32.Images.SetKeyName(26, "13961.gif");
            this.imageList32x32.Images.SetKeyName(27, "13962.gif");
            this.imageList32x32.Images.SetKeyName(28, "13963.gif");
            this.imageList32x32.Images.SetKeyName(29, "13971.gif");
            this.imageList32x32.Images.SetKeyName(30, "13972.gif");
            this.imageList32x32.Images.SetKeyName(31, "13973.gif");
            this.imageList32x32.Images.SetKeyName(32, "13974.gif");
            this.imageList32x32.Images.SetKeyName(33, "13975.gif");
            this.imageList32x32.Images.SetKeyName(34, "13976.gif");
            this.imageList32x32.Images.SetKeyName(35, "13977.gif");
            this.imageList32x32.Images.SetKeyName(36, "13979.gif");
            this.imageList32x32.Images.SetKeyName(37, "13980.gif");
            // 
            // imageList16x16
            // 
            this.imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16x16.ImageStream")));
            this.imageList16x16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16x16.Images.SetKeyName(0, "13797.gif");
            this.imageList16x16.Images.SetKeyName(1, "13798.gif");
            this.imageList16x16.Images.SetKeyName(2, "13799.gif");
            this.imageList16x16.Images.SetKeyName(3, "13800.gif");
            this.imageList16x16.Images.SetKeyName(4, "13803.gif");
            this.imageList16x16.Images.SetKeyName(5, "13804.gif");
            this.imageList16x16.Images.SetKeyName(6, "13809.gif");
            this.imageList16x16.Images.SetKeyName(7, "13810.gif");
            this.imageList16x16.Images.SetKeyName(8, "13811.gif");
            this.imageList16x16.Images.SetKeyName(9, "13812.gif");
            this.imageList16x16.Images.SetKeyName(10, "13914.gif");
            this.imageList16x16.Images.SetKeyName(11, "13915.gif");
            this.imageList16x16.Images.SetKeyName(12, "13916.gif");
            this.imageList16x16.Images.SetKeyName(13, "13918.gif");
            this.imageList16x16.Images.SetKeyName(14, "13919.gif");
            this.imageList16x16.Images.SetKeyName(15, "13920.gif");
            this.imageList16x16.Images.SetKeyName(16, "13926.gif");
            this.imageList16x16.Images.SetKeyName(17, "13927.gif");
            this.imageList16x16.Images.SetKeyName(18, "13953.gif");
            this.imageList16x16.Images.SetKeyName(19, "13954.gif");
            this.imageList16x16.Images.SetKeyName(20, "13955.gif");
            this.imageList16x16.Images.SetKeyName(21, "13956.gif");
            this.imageList16x16.Images.SetKeyName(22, "13957.gif");
            this.imageList16x16.Images.SetKeyName(23, "13958.gif");
            this.imageList16x16.Images.SetKeyName(24, "13959.gif");
            this.imageList16x16.Images.SetKeyName(25, "13960.gif");
            this.imageList16x16.Images.SetKeyName(26, "13961.gif");
            this.imageList16x16.Images.SetKeyName(27, "13962.gif");
            this.imageList16x16.Images.SetKeyName(28, "13963.gif");
            this.imageList16x16.Images.SetKeyName(29, "13971.gif");
            this.imageList16x16.Images.SetKeyName(30, "13972.gif");
            this.imageList16x16.Images.SetKeyName(31, "13973.gif");
            this.imageList16x16.Images.SetKeyName(32, "13974.gif");
            this.imageList16x16.Images.SetKeyName(33, "13975.gif");
            this.imageList16x16.Images.SetKeyName(34, "13976.gif");
            this.imageList16x16.Images.SetKeyName(35, "13977.gif");
            this.imageList16x16.Images.SetKeyName(36, "13979.gif");
            this.imageList16x16.Images.SetKeyName(37, "13980.gif");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.操作AToolStripMenuItem,
            this.操作BToolStripMenuItem,
            this.操作CToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 70);
            // 
            // 操作AToolStripMenuItem
            // 
            this.操作AToolStripMenuItem.Name = "操作AToolStripMenuItem";
            this.操作AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.操作AToolStripMenuItem.Text = "操作A";
            this.操作AToolStripMenuItem.Click += new System.EventHandler(this.操作AToolStripMenuItem_Click);
            // 
            // 操作BToolStripMenuItem
            // 
            this.操作BToolStripMenuItem.Name = "操作BToolStripMenuItem";
            this.操作BToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.操作BToolStripMenuItem.Text = "操作B";
            // 
            // 操作CToolStripMenuItem
            // 
            this.操作CToolStripMenuItem.Name = "操作CToolStripMenuItem";
            this.操作CToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.操作CToolStripMenuItem.Text = "操作C";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(607, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbDebug
            // 
            this.rtbDebug.Location = new System.Drawing.Point(12, 374);
            this.rtbDebug.Name = "rtbDebug";
            this.rtbDebug.Size = new System.Drawing.Size(670, 96);
            this.rtbDebug.TabIndex = 2;
            this.rtbDebug.Text = "";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 516);
            this.Controls.Add(this.rtbDebug);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList16x16;
        private System.Windows.Forms.ImageList imageList32x32;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 操作AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作CToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox rtbDebug;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

