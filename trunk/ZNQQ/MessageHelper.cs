using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.Threading;

namespace ZNQQ
{
    public class MessageHelper
    {
        string _qq;
        public string QQ
        { get { return this._qq; } }
        string _pass;
        //-----为了心跳------
        private System.Threading.Timer timerEvery; 
        //            timerEvery = new System.Threading.Timer(new TimerCallback(timerCall), this, 20000, 20000);
        //---------------网络传输要用到的----------------------------
        UDPClass udp = new UDPClass();


        byte[] bytes_checker = new byte[100000];
        int check_point = 0;
        //
        int id0x0825 = 3;
        int id0x0826 = 3;
        int id0x0828 = 3;
        //IPAddress _qqclientip;
        IPEndPoint _qqclientEP = new IPEndPoint(0,0);//本地外网
        IPEndPoint _machineEP = new IPEndPoint(0,0);//本机

        #region 会话变量

        byte[] loginTime=new byte[4];//登陆时间

        byte[] qqbytes { get { return Tools.QQtoBytes(this._qq); } }//QQ字节
        byte[] md5pass { get { return Tools.HexStringToBytes(MD5Helper.ToMD5(this._pass)); } }

        byte[] key0x0825= Tools.Random16Bytes;
        byte[] key0x0826 = Tools.Random16Bytes;
        byte[] keyfor0x0826recv = Tools.HexStringToBytes("CC EE 20 01 D7 B8 73 58 81 6D B7 A1 86 00 00 00");//C4 D2 20 01 D7 B8 73 58 81 6D B7 A1 86 A1 AE 78 {0xAA,0xC5,0x38,0x0E,0x9E,0x88,0xE2,0x6F,0x3A,0x5C,0x17,0xD8,0xC0,0x3A,0xF8,0x6B};//Tools.Random16Bytes;

        byte[] KeyForVerify
        {
            get {
                byte[] temp = new byte[24];
                Array.Copy(md5pass, 0, temp, 0, 16);
                Array.Copy(qqbytes, 0, temp, 20, 4);
                return MD5Helper.ToMD5(temp);
            }
        }

        byte[] keyfor0x0828recv = new byte[16];
        byte[] sessionKey = new byte[16];//0x0828收到中
        #endregion


        void append_bytes(byte[] bytes)
        {
            return;
            Array.Copy(bytes, 0, bytes_checker, check_point, bytes.Length);
            check_point += bytes.Length;
        }

        //---调试信息委托出去-----
        public delegate void DebugHelper(string debug);
        //private DebugHelper debugHelper;
        public event DebugHelper debugHelper;
        //--------




        public MessageHelper(string QQ,string PASS)
        {
            this._qq = QQ;
            this._pass = PASS;
            udp.messageHelper+=new UDPClass.MessageHelper(udp_messageHelper);
        }

        void udp_messageHelper(byte[] msg)
        {
            // Invoke(new TCPClass.MessageHelper(ShowMessage), new object[] { msg });
            //debugHelper("收到UDP消息");
            short cmd =Tools.BytesToPort(Tools.RB(msg,3,2));
            switch (cmd)
            {
                case 0x0825:
                    this.OnReceive_0x0825(msg);
                    break;
                case 0x0826:
                    this.OnReceive_0x0826(msg);
                    break;
                case 0x0828:
                    this.OnReceive_0x0828(msg);
                    break;
                case 0x001D:
                    this.OnReceive_0x001D(msg);
                    break;
                case 0x01BB:
                    this.OnReceive_0x01BB(msg);
                    break;
                case 0x0134:
                    this.OnReceive_0x0134(msg);
                    break;
                case 0x00EC:
                    this.OnReceive_0x00EC(msg);
                    break;
                case 0x00B9:
                    this.OnReceive_0x00B9(msg);
                    break;
                case 0x00D1:
                    this.OnReceive_0x00D1(msg);
                    break;
                case 0x0017://被动技能
                    this.OnReceive_0x0017(msg);
                    break;
                case 0x00CE://被动技能收到好友消息
                    this.OnReceive_0x00CE(msg);
                    break;
                default:
                    debugHelper(string.Format("收到意外命令:{0}", "0X" + cmd.ToString("x4")));
                    this.OnReceive_Unexpected(msg);
                    break;

            }
        }

         void Send_0x0825()
        {
            this.debugHelper("发送0x0825==>>");
            SP0X0825 sp = new SP0X0825(this,this.id0x0825);
            sp.QQ = this._qq;
            sp.key = this.key0x0825;
            sp.PrepareData(qqbytes);
            udp.Send(sp.bytes);
            this.debugHelper(string.Format("{0}", Tools.BytesToHexString(sp.raw_data)));
        }

        void OnReceive_0x0825(byte[] bytes)
        {
            this.debugHelper("进入0x0825处理<<==");

            byte[] data = new byte[bytes.Length - 14 - 1];
            Array.Copy(bytes,14 ,data,0,bytes.Length -14-1);
            //解包
            QQCrypt cry = new QQCrypt();
            byte[] raw_data = cry.QQ_Decrypt(data,this.key0x0825);

            this.debugHelper(string.Format("     key:{0}", Tools.BytesToHexString(this.key0x0825)));
            this.debugHelper(string.Format("raw_data:{0}", Tools.BytesToHexString(raw_data)));
            this.debugHelper(string.Format("    data:{0}", Tools.BytesToHexString(data)));

            //解包取值
            byte[] result = new byte[1]; Array.Copy(raw_data, 0, result, 0, 1);
            byte[] subcmd = new byte[2]; Array.Copy(raw_data, 1, subcmd, 0, 2);
            byte[] token_length = new byte[2]; Array.Copy(raw_data, 3, token_length, 0, 2);
            byte[] token = new byte[0x38]; Array.Copy(raw_data, 5, token, 0, 56); //0x38=56
            byte[] unkknown = new byte[6]; Array.Copy(raw_data, 61, unkknown, 0, 6);
             Array.Copy(raw_data, 67, this.loginTime, 0, 4);
            byte[] qqip = new byte[4]; Array.Copy(raw_data, 71, qqip, 0, 4);
            byte[] qqport = new byte[4]; Array.Copy(raw_data, 75, qqport, 0, 2);
            byte[] t00 = new byte[2]; Array.Copy(raw_data, 77, t00, 0, 2);
            //byte[] redirect = new byte[data1.Length - 79]; Array.Copy(data1, 79, redirect, 0, data1.Length - 79);
            //byte[] redirect_ip = new byte[4]; Array.Copy(redirect, 16, redirect_ip, 0, 4);

            //debugHelper(string.Format("result :{0}", Tools.BytesToHexString(result)));
            short m_subcmd = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(subcmd, 0));
            //debugHelper(string.Format("subcmd :0x{0}", subcmd[0].ToString("x2") + subcmd[1].ToString("x2")));
            short m_token_length = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(token_length, 0));
            //debugHelper(string.Format("token_length :0x{0:X}({0:D})", m_token_length));
            //debugHelper(string.Format("token :{0}", Tools.BytesToHexString(token)));
           
            IPAddress m_qqip = new IPAddress(qqip);
            this._qqclientEP.Address = Tools.BytesToIPAddress(qqip);
            //this._qqclientEP.Port = Tools.BytesToPort(qqport);
            debugHelper(string.Format("qqip :{0}",m_qqip.ToString()));
            if (result[0] != 0x00)
            {
                byte[] redirect = new byte[raw_data.Length - 79]; Array.Copy(raw_data, 79, redirect, 0, raw_data.Length - 79);
                byte[] redirect_ip = new byte[4]; Array.Copy(redirect, 16, redirect_ip, 0, 4);
                debugHelper(string.Format("redirect :{0}", Tools.BytesToHexString(redirect) + string.Format("(length:{0})", Tools.BytesToHexString(redirect).Length / 2)));
                IPAddress m_redirect_ip = new IPAddress(redirect_ip);
                debugHelper(string.Format("redirect_ip :{0}", m_redirect_ip.ToString()));
                udp.Redirect(m_redirect_ip.ToString());
                this.Send_0x0825();
            }
            else
            {
                this.Send_0x0826(token,m_qqip);
            }

            append_bytes(raw_data);
            append_bytes(this.key0x0825);
            append_bytes(data);
            append_bytes(bytes);
        }
        void Send_0x0826(byte[] token0x0825,IPAddress QQClientIP)
        {
            this.debugHelper("发送0x0826==>>");
            SP0X0826 sp = new SP0X0826(this,this.id0x0826);
            sp.key0x0826 = this.key0x0826;
            sp.keyfor0x0826recv = this.keyfor0x0826recv;
            sp.keyForVerify = this.KeyForVerify;
            sp.md5Pass = this.md5pass;
            sp.PrepareData(token0x0825, QQClientIP, this.loginTime, qqbytes);
            udp.Send(sp.bytes);

            this.debugHelper(Tools.BytesToHexString( sp.raw_data));
            this.debugHelper(Tools.BytesToHexString(sp.verify_data));
        }
        void OnReceive_0x0826(byte[] bytes)
        {
            append_bytes(bytes);

            this.debugHelper("进入0x0826处理<<==");
            if ( 743 <=bytes.Length && bytes.Length <= 839)
            {
                byte[] data = new byte[bytes.Length-14-1];//728bytes
                Array.Copy( bytes,14,data,0,data.Length);
                //byte[] key = new byte[16];
                byte[] raw_data =new QQCrypt().QQ_Decrypt(data, this.keyfor0x0826recv);
                //this.debugHelper(string.Format("返回{0}字节:", bytes.Length.ToString()) + Tools.BytesToHexString(bytes));
                //this.debugHelper(string.Format("data:{0}",  Tools.BytesToHexString(data)));
                //this.debugHelper(string.Format("key:{0}", Tools.BytesToHexString(key)));
                //this.debugHelper(string.Format("raw_data:{0}", Tools.BytesToHexString(raw_data)));
                byte[] token = new byte[56];
                byte[] token1 = new byte[112];
                Array.Copy(raw_data, 255, this.loginTime, 0, 4);
                Array.Copy(raw_data, 25, token, 0, 56);
                Array.Copy(raw_data, 269, token1, 0, 112);
                byte[] keyfor0x0828send = new byte[16];
                Array.Copy(raw_data, 7, keyfor0x0828send, 0, 16);
                this.keyfor0x0828recv = new byte[16];
                Array.Copy(raw_data, 235, this.keyfor0x0828recv, 0, 16);
                debugHelper(string.Format("keyfor0x0828recv:{0}", Tools.BytesToHexString(this.keyfor0x0828recv)));
                debugHelper(string.Format("{0}", Tools.BytesToHexString(raw_data)));
                this.Send_0x0828(token,token1, keyfor0x0828send);
                
            }
            else
            {
                this.debugHelper(string.Format("返回{0}字节:", bytes.Length.ToString()) + Tools.BytesToHexString(bytes));
            }

            //this.Send_0x0828();
        }
        void Send_0x0828(byte[] token0x0826,byte[] token_70,byte[] key0x0826)
        {
            this.debugHelper("发送0x0828==>>");
            SP0X08282013 sp = new SP0X08282013(this, this.id0x0828);
            sp.token_38 = token0x0826;
            sp.token_70 = token_70;
            sp.key0x0826 = key0x0826;
            sp.QQRemoteIP = udp.RemoteIPAddress;
            sp.PrepareData(null,this._qqclientEP,this.loginTime,this.qqbytes);
            udp.Send(sp.bytes);
            debugHelper(Tools.BytesToHexString(sp.raw_data));
        }
        void OnReceive_0x0828(byte[] bytes)
        {
            this.debugHelper("进入0x0828处理<<==");
            if (bytes.Length == 399) //成功
            {
                this.debugHelper(string.Format("返回{0}字节:", bytes.Length.ToString()) + Tools.BytesToHexString(bytes));
                byte[] data = new byte[384];
                Array.Copy(bytes, 14, data, 0, 384);
                byte[] raw_data = new byte[371];
                Array.Copy(new QQCrypt().QQ_Decrypt(data, this.keyfor0x0828recv), raw_data, 371);
                Array.Copy(raw_data, 25, this.sessionKey, 0, 16);
                debugHelper(string.Format("sessionKey:{0}", Tools.BytesToHexString(this.sessionKey)));

                //this.Send_0x001D();
                //this.Send_0x01BB();
                //this.Send_0x0134();
                this.Send_0x00EC();//亮头相
                //this.Send_0x00B9();
                //this.Send_0x00D1();
                //this.Send_0x010B();
                //this.Send_0x0017();0x0017是被动命令
                timerEvery = new System.Threading.Timer(new TimerCallback(HeartBeat), this, 60000, 60000);
            }
            else
            {
                this.debugHelper(string.Format("返回{0}字节:", bytes.Length.ToString()) + Tools.BytesToHexString(bytes));
            }
            this.debugHelper(string.Format("qqclientip:{0},MachineIP:{1},RemoteIPAddress:{2}", this._qqclientEP.Address, Tools.MachineIP, udp.RemoteIPAddress.ToString()));
            this.debugHelper(string.Format("qqclientip:{0},MachineIP:{1},RemoteIPAddress:{2}", 
               Tools.BytesToHexString( this._qqclientEP.Address.GetAddressBytes()), 
               Tools.BytesToHexString(  Tools.MachineIP.GetAddressBytes()), 
               Tools.BytesToHexString( udp.RemoteIPAddress.GetAddressBytes())));
        }


        public void Login()
        {
            this.debugHelper(string.Format("登陆开始 QQ:{0} Pass:{1}",this._qq ,this._pass));
            udp.Connection();
            this.Send_0x0825();
            
        }

        public void SendMSG()
        {
            this.Send_0x00CD();
        }

        void Send_0x001D()
        {
            this.debugHelper("发送0x001D==>>");
            SP0x001D sp = new SP0x001D();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }

        void Send_0x00CD()//向好友发送消息
        {
            this.debugHelper("发送0x00CD==>>");
            SP0x00CD sp = new SP0x00CD();
            byte[] qqfrom = this.qqbytes;
            byte[] qqto = Tools.QQtoBytes("1776594476");
            sp.PrepareData(this.sessionKey,qqfrom,qqto,"asdfsa" );
            udp.Send(sp.bytes);
        }

        void OnReceive_0x001D(byte[] bytes)
        {
            if (bytes.Length == 95)
            {
                this.debugHelper("进入0x001D处理<<==");
                debugHelper(string.Format("0x001D[{0}]:{1}",bytes.Length, Tools.BytesToHexString(bytes)));
                byte[] data = new byte[80];
                byte[] raw_data = new byte[68];
                Array.Copy(bytes,14,data,0,80);
                Array.Copy(new QQCrypt().QQ_Decrypt(data, this.sessionKey), 0, raw_data,0, 68);

            }
            else
            {
                this.debugHelper(string.Format("返回{0}字节:", bytes.Length.ToString()) + Tools.BytesToHexString(bytes));
            }

        }

        void Send_0x01BB()
        {
            this.debugHelper("发送0x01BB==>>");
            SP0x01BB sp = new SP0x01BB();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x01BB(byte[] bytes)
        {
            this.debugHelper("进入0x01BB处理<<==");
            debugHelper(string.Format("0x01BB[{0}]:{1}",bytes.Length , Tools.BytesToHexString(bytes)));
        }
        void Send_0x0134()
        {
            this.debugHelper("发送0x0134==>>");
            SP0x0134 sp = new SP0x0134();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x0134(byte[] bytes)
        {
            this.debugHelper("进入0x0134处理<<==");
            debugHelper(string.Format("0x0134[{0}]:{1}",bytes.Length, Tools.BytesToHexString(bytes)));
        }

        void Send_0x00EC()
        {
            this.debugHelper("发送0x00EC==>>");
            SP0x00EC2013 sp = new SP0x00EC2013();
            sp.PrepareData(this.sessionKey,this.qqbytes);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x00EC(byte[] bytes)
        {
            this.debugHelper("进入0x00EC处理<<==");
            byte[] data = new QQCrypt().QQ_Decrypt(Tools.RB(bytes,14,16), this.sessionKey);
            debugHelper(string.Format("0x00EC[{0}]:{1}", data.Length, Tools.BytesToHexString(data)));
        }
        void Send_0x00B9()
        {
            this.debugHelper("发送0x00B9==>>");
            SP0x00B9 sp = new SP0x00B9();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x00B9(byte[] bytes)
        {
            this.debugHelper("进入0x00B9处理<<==");
        }
        void Send_0x00D1()
        {
            this.debugHelper("发送0x00D1==>>");
            SP0x00D1 sp = new SP0x00D1();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x00D1(byte[] bytes)
        {
            this.debugHelper("进入0x00D1处理<<==");
        }
        //unsafe void Send_0x001D()
        //{
        //    this.debugHelper("发送0x001D==>>");
        //    SP0x001D sp = new SP0x001D();
        //    sp.PrepareData(this.sessionKey);
        //    udp.Send(sp.bytes);
        //}
        //unsafe void OnReceive_0x001D()
        //{
        //    this.debugHelper("进入0x001D处理<<==");
        //}

        //unsafe void Send_0x001D()
        //{
        //    this.debugHelper("发送0x001D==>>");
        //    SP0x001D sp = new SP0x001D();
        //    sp.PrepareData(this.sessionKey);
        //    udp.Send(sp.bytes);
        //}
        //unsafe void OnReceive_0x001D()
        //{
        //    this.debugHelper("进入0x001D处理<<==");
        //}
        void Send_0x0058()
        {
            this.debugHelper("发送0x0058==>>");
            SP0x00582013 sp = new SP0x00582013();
            sp.PrepareData(this.sessionKey,this._qq);
            udp.Send(sp.bytes);
            this.debugHelper(Tools.BytesToHexString(sp.bytes));
        }
        void OnReceive_0x0058(byte[] bytes)
        {
            this.debugHelper("进入0x0058处理<<==");
        }
        void Send_0x0062()
        {
            this.debugHelper("发送0x0062==>>");
            SP0x00622013 sp = new SP0x00622013();
            sp.PrepareData(this.sessionKey,this.qqbytes);
            udp.Send(sp.bytes);
            sp.PrepareData(this.sessionKey, this.qqbytes);
            udp.Send(sp.bytes);
            sp.PrepareData(this.sessionKey, this.qqbytes);
            udp.Send(sp.bytes);
            sp.PrepareData(this.sessionKey, this.qqbytes);
            udp.Send(sp.bytes);
        }
        void OnReceive_0x0062(byte[] bytes)
        {
            this.debugHelper("进入0x0062处理<<==");
        }

        private void HeartBeat(Object obj)
        {
            this.Send_0x0058();
        }

        public void LogOut()
        {
            this.Send_0x0062();
            if (this.timerEvery!=null)
            this.timerEvery.Dispose();
        }

































        unsafe void Send_0x010B()
        {
            this.debugHelper("发送0x010B==>>");
            SP0x010B sp = new SP0x010B();
            sp.PrepareData(this.sessionKey);
            udp.Send(sp.bytes);
        }
        unsafe void OnReceive_0x010B()
        {
            this.debugHelper("进入0x010B处理<<==");
        }

        unsafe void Send_0x0017()
        {
            this.debugHelper("发送0x0017==>>");
        }
        unsafe void OnReceive_0x0017(byte[] bytes)
        {
            this.debugHelper("进入0x0017处理<<==");
            this.debugHelper(Tools.BytesToHexString(bytes));
        }

        unsafe void OnReceive_0x00CE(byte[] bytes)
        {
            this.debugHelper("进入0x00CE处理<<==");
            //this.debugHelper(Tools.BytesToHexString(bytes));
            byte[] data = new QQCrypt().QQ_Decrypt(Tools.RB(bytes, 14, bytes.Length - 15), this.sessionKey);
            switch ((int)data[23])
            {
                case 0x1b:
                    this.debugHelper("收到好友消息：" + Encoding.UTF8.GetString(Tools.RB(data, 142, (int)data[141])));
                    break;
                case 0x0d:
                    this.debugHelper("收到??消息：" + Encoding.UTF8.GetString(Tools.RB(data, 128, (int)data[127])));
                    break;
                default:
                    this.debugHelper(Tools.BytesToHexString(data));
                    break;
            }
        }

        unsafe void OnReceive_Unexpected(byte[] bytes)
        {
            this.debugHelper("进入Unexpected处理<<==");
            this.debugHelper(Tools.BytesToHexString(bytes));
        }
        //0x001D
        //0x01BB
        //0x0134
        //0x00EC
        //0x00B9
        //0x00D1-返回令牌表
        //0x0001
        //0x0195
        //0x0027
        //0x0169
        //0x019B

        //0x0081
        //0x0114
        //0x0017
        //0x0058心跳包，每分钟一次

        //0x0062离线四遍
    }
}
