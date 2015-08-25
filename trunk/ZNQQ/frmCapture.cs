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
    public partial class frmCapture : Form
    {
        ZNQQ.DataCap1.ZNRSocket znrs = new DataCap1.ZNRSocket();
        public frmCapture()
        {
            InitializeComponent();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (this.btnSwitch.Text == "start")
            {
                this.znrs.CreateAndBindSocket(Tools.MachineIP.ToString());
                this.znrs.PacketArrival += new ZNQQ.DataCap1.ZNRSocket.PacketArrivedEventHandler(rs_PacketArrival);
                this.znrs.Run();
                this.btnSwitch.Text = "stop";
            }
            else
            {
                this.znrs.PacketArrival -= new ZNQQ.DataCap1.ZNRSocket.PacketArrivedEventHandler(rs_PacketArrival);
                znrs.Shutdown();
                this.btnSwitch.Text = "start";
            }
        }

        void rs_PacketArrival(object sender, ZNQQ.DataCap1.ZNRSocket.PacketArrivedEventArgs args)
        {
        }
    }
}
