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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            new PCQQForm().Show();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            new QQListForm().Show();
        }
    }
}
