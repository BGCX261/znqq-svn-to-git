using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Drawing;
using System.Net;

using System.Runtime;

namespace ZNQQ
{
    public class Tools
    {
        /// <summary>
        /// 把十六进制字符串转在byte[]
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToBytes(string hex)
        {
            hex = hex.Replace(" ", "").Replace("\n","").Replace("\r","");
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }
        public static string BytesToHexString(byte[] bytes)
        {
            string ret="";
            foreach (byte item in bytes )
            {
                ret += " "+item.ToString("x2").ToUpper();
            }
            return ret;
        }
        /// <summary>
        /// 将图片流保存为图片
        /// </summary>
        /// <param name="img"></param>
        public static void SaveImage(byte[] img)
        {
            FileStream fs = new FileStream(DateTime.Now.ToString("MMddhhmmss") + ".png", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(img);
            sw.Close();
            fs.Close();
        }

        public static byte[] DatetimeToBytes(DateTime dt)
        {
            TimeSpan ts = dt.Subtract(new DateTime(1970, 1, 1, 8, 0, 0));
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)ts.TotalSeconds));
        }
        public static DateTime BytesToDatetime(byte[] bytes)
        {
            int seconds = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes,0));
            return new DateTime(1970, 1, 1, 8, 0, 0).AddSeconds(seconds);
        }
        public static byte[] NowToBytes()
        {
            TimeSpan ts = System.DateTime.Now.Subtract(new DateTime(1970, 1, 1, 8, 0, 0));
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder((Int32)ts.TotalSeconds));
        }
        public static byte[] QQtoBytes(string QQ)
        {
            UInt32 uQQ = Convert.ToUInt32(QQ);
            Int32 qq = (Int32)uQQ;
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(qq));
        }
        public static string BytesToQQ(byte[] bytes)
        {
            return ((UInt32)(IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes, 0)))).ToString();
        }
        public static IPAddress BytesToIPAddress(byte[] bytes)
        {
            return new IPAddress(bytes);
        }
        public static byte[] IPAddressToBytes(IPAddress ipaddress)
        {
            return ipaddress.GetAddressBytes();
        }
        public static byte[] PortToBytes(short port)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(port));
        }
        public static Int16 BytesToPort(byte[] bytes)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(bytes, 0));
        }

        public static byte[] RB(byte[] bytes,int index, int length)
        {
            byte[] ret = new byte[length];
            try
            {
                Array.Copy(bytes, index, ret, 0, length);
            }
            catch (Exception ex)
            {
            }
            return ret;
        }
        public static void WB( byte[] des, int index,byte[] res)
        {
            Array.Copy(res, 0, des, index, res.Length);
        }

        public static IPAddress MachineIP
        {
            get
            {
                IPHostEntry MyEntry = Dns.GetHostByName(Dns.GetHostName());
                IPAddress MyAddress = new IPAddress(MyEntry.AddressList[0].Address);
                return MyAddress;
            }
        }
        public static byte[] Random16Bytes
        {
            get
            {
                Random r = new Random();
                return MD5Helper.ToMD5(BitConverter.GetBytes(r.Next()));
            }
        }

        public static byte[] GetVerifyKey(string QQ,string Pass)
        {
            //byte[] temp = { 
            //                  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
            //                  0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 
            //              };
            byte[] temp = new byte[24];
            Array.Copy(Tools.HexStringToBytes(MD5Helper.ToMD5(Pass)), 0, temp, 0, 16);
            Array.Copy(Tools.QQtoBytes(QQ), 0, temp, 20, 4);
            return MD5Helper.ToMD5(temp);
        }

        public static byte[] combbyte(byte[] a, byte[] b) 
        { 
            byte[] c = new byte[a.Length + b.Length]; 
            a.CopyTo(c, 0); 
            b.CopyTo(c, a.Length); 
            return c; 
        }

    }
}
