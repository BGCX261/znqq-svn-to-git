using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZNQQ
{
    public partial class frmMain : Form
    {
        frmHome home = new frmHome();

        Form1 form1 = new Form1();
        Form2 form2 = new Form2();
        PCQQForm pcqqform = new PCQQForm();
        QQListForm qqlistform = new QQListForm();
        frmCapture frmcapture = new frmCapture();

        public frmMain()
        {
            InitializeComponent();
            home.MdiParent = this;
            home.WindowState = FormWindowState.Maximized;
            home.Show();

            form1.MdiParent = this;
            form1.WindowState = FormWindowState.Maximized;
            form1.Show();

            form2.MdiParent = this;
            form2.WindowState = FormWindowState.Maximized;
            form2.Show();

            pcqqform.MdiParent = this;
            pcqqform.WindowState = FormWindowState.Maximized;
            pcqqform.Show();

            qqlistform.MdiParent = this;
            qqlistform.WindowState = FormWindowState.Maximized;
            qqlistform.Show();

            frmcapture.MdiParent = this;
            frmcapture.WindowState = FormWindowState.Maximized;
            frmcapture.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }
    }
}
