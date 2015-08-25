using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using App.BLL;
using App.Model;

namespace ZNQQ
{
    public partial class QQListForm : Form
    {
        IList<QqnumInfo> qqList = new App.BLL.QQNUM().ISelect();
        IList<MessageHelper> msgHelperList = new List<MessageHelper>();
        BindingSource bsList = new BindingSource();
        public QQListForm()
        {
            InitializeComponent();
            bsList.DataSource = qqList;
            this.gridControl1.DataSource = bsList;
        }

        private void QQListForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //MessageHelper msgHelper = new MessageHelper("121852835", "networkdog456");
            //MessageHelper msgHelper = new MessageHelper("1774671592", "a111111");
            //MessageHelper msgHelper = new MessageHelper("1776594476", "a111111");
            //msgHelper.debugHelper += new MessageHelper.DebugHelper(msgHelper_debugHelper);
            //msgHelper.Login();
            //msgHelperList.Add(msgHelper);
            try
            {
                foreach (QqnumInfo ins in qqList)
                {
                    MessageHelper msgHelper = new MessageHelper(ins.QQ,ins.PASS);
                    msgHelper.debugHelper += new MessageHelper.DebugHelper(msgHelper_debugHelper);
                    msgHelper.Login();
                    msgHelperList.Add(msgHelper);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void msgHelper_debugHelper(string debug)
        {
            Invoke(new MessageHelper.DebugHelper(AppendDebug), debug);
        }

        void AppendDebug(string debug)
        {
            this.richTextBox1.AppendText(debug + "\r\n");
            this.richTextBox1.ScrollToCaret();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (MessageHelper ins in msgHelperList)
                {
                    ins.LogOut();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            new PCQQForm().ShowDialog();
        }
    }
}
