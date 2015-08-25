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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnDeCrypt_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = Tools.HexStringToBytes(this.rtbData.Text);
                byte[] key = Tools.HexStringToBytes(this.rtbKEY.Text);
                byte[] raw_data = Tools.HexStringToBytes(this.rtbRawData.Text);
                raw_data = new QQCrypt().QQ_Decrypt(data, key);
                this.rtbRawData.Text = Tools.BytesToHexString(raw_data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEnCrypt_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = Tools.HexStringToBytes(this.rtbData.Text);
                byte[] key = Tools.HexStringToBytes(this.rtbKEY.Text);
                byte[] raw_data = Tools.HexStringToBytes(this.rtbRawData.Text);
                data = new QQCrypt().QQ_Encrypt(raw_data, key);
                this.rtbData.Text = Tools.BytesToHexString(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
