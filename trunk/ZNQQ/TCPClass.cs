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
    public class TCPClass
        {
            private TcpClient tcp;
            private NetworkStream networkStream;
            private BinaryReader br;
            private BinaryWriter bw;
            public delegate void MessageHelper(byte[] msg);
            //private MessageHelper messageHelper;
            public event MessageHelper messageHelper;
            public delegate void IsNotConnection();
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
                try
                {
                    tcp = new TcpClient("211.136.236.88", 14000);

                }
                catch
                {

                    return 0;
                }
                networkStream = tcp.GetStream();
                br = new BinaryReader(networkStream, Encoding.UTF8);
                bw = new BinaryWriter(networkStream, Encoding.UTF8);

                th = new Thread(new ThreadStart(GetData));
                th.IsBackground = true;
                th.Start();
                return 1;
            }

            private void GetData()
            {

                while (IsExit)
                {
                    byte[] message = new byte[100000];

                    try
                    {
                        int i = br.Read(message, 0, message.Length);
                        messageHelper(message);
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }

            public void Send(string data)
            {
                try
                {
                    bw.Write(data);
                    bw.Flush();
                }
                catch
                {
                }
            }

            public void Send(byte[] data)
            {
                try
                {
                    bw.Write(data);
                    bw.Flush();
                }
                catch
                {
                }
            }

            public void Close()
            {
                this.br.Close();
                this.bw.Close();
                this.tcp.Close();
                this.tcp = null;
                IsExit = false;
            }


            //检测是否掉线
            public void IsConnection()
            {
                this.tcp.Client.Close();
                this.tcp.Close();
                this.tcp = null;
            }

        }
}
