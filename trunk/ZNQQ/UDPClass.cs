using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ZNQQ
{
    public class UDPClass
    {
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        public IPAddress RemoteIPAddress
        {
            get { return RemoteIpEndPoint.Address; }
            set { RemoteIpEndPoint.Address = value; }
        }
        private UdpClient udp = new UdpClient();//可指定接口也可动态接口

        public delegate void MessageHelper(byte[] msg);
        //private MessageHelper messageHelper;
        public event MessageHelper messageHelper;

        private Thread th;

        private bool IsExit = false;

        public bool IsExit1
        {
            get { return IsExit; }
            set { IsExit = value; }
        }

        public int Connection()
        {
            this.IsExit = true;
            //udp.Connect("123.151.40.53", 8000);
            udp.Connect("119.147.45.111", 8000);
            //119.147.45.111
            //udp.Connect("123.151.40.98", 8000);
            //123.151.40.53--2
            //123.151.40.209--502
            //183.60.50.11 --521
            //183.60.18.111--1
            //183.60.16.214--2
            //119.147.193.17--2
            //183.60.18.111--1
            //
            th = new Thread(new ThreadStart(GetData));
            th.IsBackground = true;
            th.Start();
            return 1;
        }
        public void Redirect(string ip)
        {
            udp.Connect(ip, 8000);
        }
        private void GetData()
        {

            while (IsExit)
            {
                byte[] message = new byte[100000];

                //try
                //{
                    message = udp.Receive(ref RemoteIpEndPoint);
                    messageHelper(message);
                //}
                //catch (Exception ex)
                //{
                //}

            }
        }

        public void Send(string data)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                udp.Send(bytes, bytes.Length);
            }
            catch(Exception ex)
            {
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                udp.Send(data, data.Length);
            }
            catch(Exception ex)
            {
            }
        }

        public void Close()
        {
            this.udp.Close();
            this.udp = null;
            IsExit = false;
        }


        //检测是否掉线
        public void IsConnection()
        {
            this.udp.Client.Close();
            this.udp.Close();
            this.udp = null;
        }

    }
}
