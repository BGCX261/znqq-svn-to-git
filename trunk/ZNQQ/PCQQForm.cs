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
using App.Common;

namespace ZNQQ
{
    public partial class PCQQForm : Form
    {
        MessageHelper msgHelper;

        DataCap.RawSocket rs;
        QQPACK pack = new QQPACK();
        public PCQQForm()
        {
            App.Model.QqnumInfo ins = new QqnumInfo();
            msgHelper = new MessageHelper("121852835", "networkdog456");
            //msgHelper = new MessageHelper("1774671592", "a111111");
            msgHelper.debugHelper += new MessageHelper.DebugHelper(msgHelper_debugHelper);
            InitializeComponent();
            this.InitForCapData();//初始化抓包
        }

        void msgHelper_debugHelper(string debug)
        {
            Invoke(new MessageHelper.DebugHelper(AppendDebug), debug);
        }
           
        void AppendDebug(string debug)
        {
            this.rtbDebug.AppendText(debug + "\r\n");
            if(cbAutoScroll.Checked)
            this.rtbDebug.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            msgHelper.Login();
        }

        public String bytes2HexString(byte[] b)
        {
            String ret = "";

            foreach (byte item in b)
            {
                ret += item.ToString("X2");
            }

            return ret;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            msgHelper.LogOut();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.button3.Text == "start")
            {
                this.rs.PacketArrival += new ZNQQ.DataCap.RawSocket.PacketArrivedEventHandler(rs_PacketArrival);
                this.button3.Text = "stop";
            }
            else
            {
                this.rs.PacketArrival -= new ZNQQ.DataCap.RawSocket.PacketArrivedEventHandler(rs_PacketArrival);
                this.button3.Text = "start";
            }
        }
        public void InitForCapData()
        {            
            this.rs = new DataCap.RawSocket();
            this.rs.CreateAndBindSocket(Tools.MachineIP.ToString());
            //this.rs.PacketArrival += new ZNQQ.DataCap.RawSocket.PacketArrivedEventHandler(rs_PacketArrival);
            rs.Run();
        }

        
        byte[] sp0x0825;
        byte[] key0x0825 = new byte[16];//0825发包[26,16]

        byte[] rp0x0825;
       
        byte[] sp0x0826;
        byte[] key0x0826 = new byte[16];//0826发包[26,16]
        byte[] keyfor0x0826recv = new byte[16];//0826验证包(120字节解密后)内容[88,16]
        byte[] md5pass = new byte[16];
        byte[] verifykey = Tools.GetVerifyKey("121852835", "networkdog456");
        byte[] verifybytes;

        byte[] rp0x0826;
        byte[] keyfor0x0828send = new byte[16];//0826收包内容[7,16]
        byte[] keyfor0x0828recv = new byte[16];//0826收包内容[235,16]

        byte[] sp0x0828;

        byte[] rp0x0828;
        byte[] snkey;

        byte[] rp0x00CD;//发送消息

        void rs_PacketArrival(object sender, ZNQQ.DataCap.RawSocket.PacketArrivedEventArgs args)
        {
            //throw new NotImplementedException();
            if (args.Protocol == "UDP:")
            {
                byte[] bytes = new byte[args.MessageLength-8];
                Array.Copy(args.MessageBuffer, 8 , bytes, 0, args.MessageLength-8);
                //if (bytes[0] != 0x02) return;
                //this.msgHelper_debugHelper(System.DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss") + Tools.BytesToHexString(Tools.RB(bytes, 3, 2)));
                //if (Tools.BytesToQQ(Tools.RB(bytes, 7, 4)) != "121852835") return;//过滤QQ号
                string QQ = Tools.BytesToQQ(Tools.RB(bytes, 7, 4));

                //this.msgHelper_debugHelper(string.Format("目标:{0},{1}", args.DestinationAddress, args.DestinationPort));
                switch(Tools.BytesToPort(Tools.RB(bytes,3, 2)))
                {

                    case 0x0825:
                        if (bytes.Length == 115)//发包
                        {
                           
                            this.key0x0825 = Tools.RB(bytes, 26, 16);
                            this.sp0x0825= new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 42, 72), this.key0x0825);

                            if (!this.cbR0x0825.Checked) return;
                            this.msgHelper_debugHelper("S0x0825_bytes:" + Tools.BytesToHexString(bytes));
                            this.msgHelper_debugHelper("S0x0825:"+Tools.BytesToHexString(this.sp0x0825));
                        }
                        if (bytes.Length == 111)//收包
                        {
                            this.rp0x0825 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, 96), this.key0x0825);

                            if (!this.cbS0x0825.Checked) return;
                            this.msgHelper_debugHelper("R0x0825_bytes:" + Tools.BytesToHexString(bytes));
                            this.msgHelper_debugHelper("R0x0825:"+Tools.BytesToHexString(this.rp0x0825));
                        }
                        break;
                    case 0x0826:
                        if (bytes.Length == 499)//发包
                        {
                            this.key0x0826 = Tools.RB(bytes, 26, 16);
                            this.sp0x0826 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 42, 456), this.key0x0826);

                            QqnumInfo ins = new QQNUM().ISelect(string.Format("QQ='{0}'", QQ))[0];
                            this.verifykey = Tools.GetVerifyKey(ins.QQ, ins.PASS);

                            this.verifybytes = new QQCrypt().QQ_Decrypt(Tools.RB(this.sp0x0826, 74, 120),this.verifykey);
                            this.keyfor0x0826recv = Tools.RB(this.verifybytes, 88, 16);
                            
                            if (!this.cbS0x0826.Checked) return;
                            this.msgHelper_debugHelper("S0x0826_bytes:" + Tools.BytesToHexString(bytes));
                            this.msgHelper_debugHelper("S0x0826:" + Tools.BytesToHexString(this.sp0x0826));
                            this.msgHelper_debugHelper("verifybytes:" + Tools.BytesToHexString(this.verifybytes));
                        }
                        //if (bytes.Length == 743)//收包
                        //{
                        //    this.rp0x0826 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, 728), this.keyfor0x0826recv);

                        //    this.keyfor0x0828send = Tools.RB(this.rp0x0826,7, 16);
                        //    this.keyfor0x0828recv = Tools.RB(this.rp0x0826, 235, 16);

                        //    if (!this.cbR0x0826.Checked) return;
                        //    this.msgHelper_debugHelper("R0x0826_bytes:" + Tools.BytesToHexString(bytes));
                        //    this.msgHelper_debugHelper("R0x0826:" + Tools.BytesToHexString(this.rp0x0826));
                        //}
                        //if (bytes.Length == 759)//收包
                        //{
                        //    this.rp0x0826 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, 744), this.keyfor0x0826recv);
                        //    this.keyfor0x0828send = Tools.RB(this.rp0x0826, 7, 16);
                        //    this.keyfor0x0828recv = Tools.RB(this.rp0x0826, 235, 16);

                        //    if (!this.cbR0x0826.Checked) return;
                        //    this.msgHelper_debugHelper("R0x0826_bytes:" + Tools.BytesToHexString(bytes));
                        //    this.msgHelper_debugHelper("R0x0826:" + Tools.BytesToHexString(this.rp0x0826));
                        //    this.msgHelper_debugHelper("verifybytes:" + Tools.BytesToHexString(this.verifybytes));
                        //}
                        if (743 <= bytes.Length && bytes.Length <= 839)//收包
                        {
                            this.rp0x0826 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, bytes.Length - 14 - 1), this.keyfor0x0826recv);

                            this.keyfor0x0828send = Tools.RB(this.rp0x0826, 7, 16);
                            this.keyfor0x0828recv = Tools.RB(this.rp0x0826, 235, 16);
                            if (!this.cbR0x0826.Checked) return;
                            this.msgHelper_debugHelper("R0x0826_bytes:" + Tools.BytesToHexString(bytes));
                            this.msgHelper_debugHelper("R0x0826:" + Tools.BytesToHexString(this.rp0x0826));
                        }
                        if (bytes.Length == 855)//收包-需要验证
                        {
                            this.msgHelper_debugHelper("需要验证:" + Tools.BytesToHexString(bytes));
                        }
                        break;
                    case 0x0828:
                        if (557 <= bytes.Length && bytes.Length<=581)//发包
                        {
                            this.sp0x0828 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 84, bytes.Length-84-1), this.keyfor0x0828send);

                            if (!this.cbS0x0828.Checked) return;
                            this.msgHelper_debugHelper("S0x0828_bytes:" + Tools.BytesToHexString(bytes));
                            this.msgHelper_debugHelper("S0x0828:" + Tools.BytesToHexString(this.sp0x0828));
                        }
                        if (bytes.Length == 399)//收包
                        {
                            this.rp0x0828 = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, 384), this.keyfor0x0828recv);
                            this.snkey = Tools.RB(this.rp0x0828, 25, 16);

                            if (this.cbR0x0828.Checked)
                            {
                                this.msgHelper_debugHelper("R0x0828_bytes:" + Tools.BytesToHexString(bytes));
                                this.msgHelper_debugHelper("R0x0828:" + Tools.BytesToHexString(this.rp0x0828));
                            }
                            if (this.cbSNKEY.Checked)
                            {
                                this.msgHelper_debugHelper("snkey:" + Tools.BytesToHexString(this.snkey));
                            }
                            pack.QQ = Tools.BytesToQQ(Tools.RB(bytes,7,4));
                            pack.SP0X0825 = Tools.BytesToHexString(this.sp0x0825);
                            pack.RP0X0825 = Tools.BytesToHexString(this.rp0x0825);
                            pack.SP0X0826 = Tools.BytesToHexString(this.sp0x0826);
                            pack.VERIFYKEY = Tools.BytesToHexString(this.verifybytes);
                            pack.RP0X0826 = Tools.BytesToHexString(this.rp0x0826);
                            pack.SP0X0828 = Tools.BytesToHexString(this.sp0x0828);
                            pack.RP0X0828 = Tools.BytesToHexString(this.rp0x0828);
                            pack.Create();
                            QqnumInfo ins = new QqnumInfo();
                            ins.QQ = Tools.BytesToQQ(Tools.RB(bytes, 7, 4));
                            ins.SP0X0825 = Tools.BytesToHexString(this.sp0x0825);
                            ins.RP0X0825 = Tools.BytesToHexString(this.rp0x0825);
                            ins.SP0X0826 = Tools.BytesToHexString(this.sp0x0826);
                            ins.VERIFYKEY = Tools.BytesToHexString(this.verifybytes);
                            ins.RP0X0826 = Tools.BytesToHexString(this.rp0x0826);
                            ins.SP0X0828 = Tools.BytesToHexString(this.sp0x0828);
                            ins.RP0X0828 = Tools.BytesToHexString(this.rp0x0828);
                            XResult rst= new XResult();
                            new QQNUM().Update(ins,rst);
                        }
                        break;
                    case 0x00CD:
                        if (bytes.Length > 31)
                        {
                            this.rp0x00CD=new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 22, bytes.Length - 23), this.snkey);

                            if (!this.cbS0x00CD.Checked) return;
                            this.msgHelper_debugHelper("R0x00CD:" + Tools.BytesToHexString(this.rp0x00CD));
                            if (rp0x00CD.Length >116)
                            {
                                this.msgHelper_debugHelper("R0x00CD:" + Encoding.UTF8.GetString(Tools.RB(this.rp0x00CD, 116, this.rp0x00CD.Length - 116)));
                            }
                        }
                        if (bytes.Length == 31)
                        {
                            if (!this.cbR0x00CD.Checked) return;
                            this.msgHelper_debugHelper("R0x00CD:" + Tools.BytesToHexString(new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, bytes.Length - 15), this.snkey)));
                        }
                        break;
                    case 0x00EC:
                        this.msgHelper_debugHelper("0x00EC:" + Tools.BytesToHexString(bytes));
                        break;
                    default:
                       // this.msgHelper_debugHelper(Tools.BytesToHexString(bytes));
                        break;
                }
                //this.msgHelper_debugHelper(Tools.BytesToHexString(bytes));
            }
        }

        private void cbWrap_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbWrap.Checked)
            {
                this.rtbDebug.WordWrap = true;
            }
            else
            {
                this.rtbDebug.WordWrap = false;
            }
        }

        private void PCQQForm_Load(object sender, EventArgs e)
        {

        }

        private void rtbDebug_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            this.msgHelper.SendMSG();
        }


    }
}
