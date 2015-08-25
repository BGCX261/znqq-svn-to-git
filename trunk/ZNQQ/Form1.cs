using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.IO;

namespace ZNQQ
{
    public partial class Form1 : Form
    {

        List<QQUser> userlist = new List<QQUser>();
        List<QQService> serviceList = new List<QQService>();
        public Form1()
        {
            InitializeComponent();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            foreach (QQUser user in userlist)
            {
                //Thread th = new Thread(new ParameterizedThreadStart(Initial));
                //th.IsBackground = true;
                //th.Start(user);
                QQService service = new QQService(user);
                service.debugHelper+=new QQService.DebugHelper(this.service_debugHelper);
                serviceList.Add(service);
            }

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem xy = listView1.GetItemAt(e.X, e.Y);
                if (xy != null)
                {
                    Point point = this.PointToClient(listView1.PointToScreen(new Point(e.X, e.Y)));
                    this.contextMenuStrip1.Show(this, point);
                }
            }
        }


        //-----------
        TCPClass tcp;
        private void Initial(Object obj)
        {
            QQUser user = obj as QQUser;
            tcp = new TCPClass();
            int i = tcp.Connection();
            if (i == 0)
            {
                MessageBox.Show("连接服务器失败！请检查网络！");
                return;
            }
            tcp.messageHelper += new TCPClass.MessageHelper(tcp_messageHelper);
            this.Login(user);
        }
        void tcp_messageHelper(byte[] msg)
        {
            Invoke(new TCPClass.MessageHelper(ShowMessage), new object[] { msg });
        }
        void service_debugHelper(string debug)
        {
            Invoke(new QQService.DebugHelper(AppendDebug), debug);
        }
        void AppendDebug(string debug)
        {
            this.rtbDebug.AppendText(debug + "\r\n");
            this.rtbDebug.ScrollToCaret();
        }
        private void ShowMessage(byte[] msg)
        {
            string cmd = Encoding.UTF8.GetString(msg, 0, msg.Length);
            if (cmd.IndexOf("VER") == -1)
            {
                return;
            }
            else
            {
                this.rtbDebug.AppendText(cmd+"\r\n");
            }
            string[] data = cmd.Split('&');
            data[data.Length - 1] = data[data.Length - 1].Split('\r')[0];
            switch (data[1])
            {
                case "CMD=Login":
                    switch (data[5])
                    {
                        case "RS=0":
                            //登录成功！   获取好友列表 
                            this.rtbDebug.AppendText("登录成功！   获取好友列表\r\n");
                            this.rtbDebug.AppendText("正在获取好友列表......\r\n");
                            //this.GetQQList("0");
                            break;
                        case "RS=1":
                            this.rtbDebug.AppendText("密码错误！\r\n");
                            break;
                    }
                    break;
            //    case "CMD=VERIFYCODE"://要输入验证码
            //        string vc = data[7].Split('=')[1];
            //        byte[] img = Tools.HexStringToBytes(vc);

            //        Bitmap bitmap = new Bitmap(new System.IO.MemoryStream(img));
            //        Validate fmss = new Validate(this.tcp, bitmap);
            //        fmss.Show();
            //        //Tools.SaveImage(img);

            //        break;
            //    case "CMD=SIMPLEINFO2": //好友列表
            //        string np = data[5].Split('=')[1];
            //        if (np != "65535")
            //        {
            //            this.GetQQList(np);
            //        }
            //        else
            //        {
            //            y = 1;
            //        }
            //        if (y == 1)
            //        {
            //            this.GetQQListIsOnline("0");
            //            y = 0;
            //        }
            //        this.QQListHelper(data[7], data[9]);
            //        InitialQQList();
            //        break;
            //    case "CMD=QUERY_STAT2":
            //        if (data[4] == "RES=20")
            //        {
            //           this.rtbDebug.AppendText("您的帐号在别处登录，您已被迫下线！\r\n");
            //            //CloseForms();
            //            this.Login();
            //            return;
            //        }
            //        string fn = data[5].Split('=')[1];
            //        string[] un = data[8].Split(',');
            //        if (fn != "1")
            //        {
            //            this.GetQQListIsOnline(un[un.Length - 2]);
            //        }

            //        this.QQListOnlineHelper(data[8], data[7]);
            //        break;
            //    case "CMD=update_stat"://好友上线下线或改为忙碌
            //        UpdateStat(data[6].Split('=')[1], data[5].Split('=')[1]);
            //        break;
                case "CMD=Server_Msg": //收到消息
                    string qqid = (data[7].Split('='))[1];
                    string msgs = ((data[8].Split('='))[1].Split('\n'))[0];
                    string nk = "";
                    this.rtbDebug.AppendText(string.Format("来自[{0}]消息：{1}\r\n",data[7],data[8]));

                    this.SendQQMsg("1837219426", data[7].Substring(3), "我的聊天室http://www.2345.com/?k2205198,大家支持");
                    //foreach (ChatForming item in QQUser.ChatFormings)
                    //{
                    //    if (item.Uid == qqid && item.State == 0)
                    //    {
                    //        item.chatForm.MsgHelper(msgs, DateTime.Now);
                    //        return;
                    //    }
                    //    else if (item.Uid == qqid && item.State == 1)
                    //    {
                    //        item.chatForm.Shows(msgs, DateTime.Now);
                    //        return;
                    //    }
                    //}

                    //foreach (ListViewItem item in this.listView_QQList.Items)
                    //{
                    //    if (item.Text == qqid)
                    //    {
                    //        nk = item.SubItems[1].Text;
                    //        break;
                    //    }
                    //}

                    //Message message = new Message(qqid, nk, msgs);
                    //this.msg.Add(message);
                    //this.label_msg.Text = "您有未读消息(点击查看)......";
                    //this.pictureBox1.Visible = true;



                    break;
            //    case "CMD=GETINFO"://查看好友资料 (数据到达)
            //        TextBox textBox_QQInfo = new TextBox();
            //        textBox_QQInfo.Dock = DockStyle.Fill;
            //        textBox_QQInfo.Multiline = true;
            //        textBox_QQInfo.Text = cmd;
            //        Form fms = new Form();
            //        fms.Controls.Add(textBox_QQInfo);
            //        fms.Show();
            //        break;
            }
        }
        public string GetSjs3Byte()
        {
            Random rnd = new Random();
            int i = rnd.Next(200, 999);
            return i.ToString();
        }
        /// <summary>
        /// 发送登录命令
        /// </summary>
        private void Login(QQUser user)
        {
            string str = string.Format("VER=1.4&CON=1&CMD=Login&SEQ={0}&UIN={1}&PS={2}&M5=1&LG=0&LC=812822641C978097&GD=TW00QOJ9KUVD753S&CKE=\n", GetSjs3Byte(), user.QQID, user.Password);
            tcp.Send(str);
        }

        private void SendQQMsg(string qqidfrom, string qqidto, string msg)
        {
            string str = string.Format("VER=1.4&CON=1&CMD=CLTMSG&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={2}&MG={3}\n", GetSjs3Byte(), qqidfrom, qqidto, msg);
            tcp.Send(str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                StreamReader txtStreamReader = new StreamReader(this.openFileDialog1.FileName);
                string[] seg;
                userlist.Clear();
                listView1.Items.Clear();
                while(txtStreamReader.Peek() != -1)
                {
                    seg = txtStreamReader.ReadLine().Replace("\r\n", "").Split(new string[]{"----" }, StringSplitOptions.None);
                    userlist.Add(new QQUser(seg[0], MD5Helper.ToMD5(seg[1])));
                    this.listView1.Items.Add(seg[0]);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.MinimizeBox = true;
            this.Hide();
        }

        private void 操作AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MaximizeBox = true;
            this.Show();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.MaximizeBox = true;
            this.Show();
        }   
    }
}
