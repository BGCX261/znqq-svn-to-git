using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using App.BLL;
using App.Model;

namespace ZNQQ
{
    public class SP0X08282013
    {
        string string_bytes =
 @"
02 30 37 08 28 0B A0 69 C7 56 E8 02 00 00 00 01 01 01 00 00 65 EF 00 30 00 3A 00 38 9B F2 2E A7 E8 4E E5 3C 1E 34 EF 75 CC E7 B4 C5 01 98 41 45 2F 97 91 6C DF F1 2C E9 B7 49 1C CF A6 4D F4 2E 36 43 A7 D9 83 C4 3B 95 CB 73 50 DF C5 4C 34 3B 16 9A 2C F5 4E CE BA DA 1A E9 C5 85 FC 23 D7 58 98 81 84 F0 E9 6D 7C B5 C1 35 96 67 EE 2E 3F D5 5F F9 85 48 A5 21 B1 CE B1 68 14 80 4A 3A 27 6D EB D5 90 6B 4A 3E 7C 8E 29 98 FE 45 23 57 DC B1 63 9F C6 7F 80 67 A4 40 39 71 00 AD 28 DD DF 36 79 9E E9 C1 4B 5F D9 81 B6 EA 94 58 BD 8F 6B B6 5C 48 73 84 79 6C 0E 3E C9 02 B9 B0 F4 BD 45 F7 AB F2 29 E5 EF F9 40 88 39 73 3F B5 82 0E A3 C9 F1 55 37 1D 2D 33 BE BA 5B 7D AE B7 65 D1 B1 F9 E8 C9 68 87 89 4E 21 66 A1 B8 15 9E 7D 8E 06 95 0F 06 34 00 F5 82 70 39 D8 A2 9B 30 08 FB 68 87 C8 82 BA 9B 4B 1B 7A E1 3A 01 1F AB EE 5B D3 3F 2D 42 7D DA F5 03 AB 27 B8 33 37 67 18 10 8C 8E 9E 40 55 13 FA CA A9 9B E7 CD 8D 03 E9 7E 2A A5 7F 88 6C 45 3F 7E 76 60 3D D8 D1 72 76 A7 D9 89 94 1E A6 87 BD 9E 5D AF 98 20 BF E1 00 5B 80 22 02 3D D4 08 0A BE 9F 2F 11 C2 31 15 3D C4 BB A1 BC 15 B0 DE 23 53 BA 80 09 9C FE 25 29 CB 0B B8 17 F1 F7 79 51 C3 F1 3E E4 27 25 61 F1 24 9E CE 95 85 DC 96 9C 32 D7 78 B4 6D A1 DA B0 BA F8 51 33 A3 95 A8 20 92 01 8B CE 0B 6C 3D 75 C0 54 20 73 94 C7 C8 57 B1 69 BB 72 A2 B5 55 CB 61 E4 4C CC 18 F1 18 3A C8 40 D5 A9 CE DF 08 30 F2 AF 30 96 1D 4F EB A6 3B 62 EE F7 E8 75 B6 DD 87 39 F7 F7 18 9E F6 14 EB 94 F6 AE F9 4C 43 7C 0D 8D A4 1F 0D FA A0 86 09 6E 70 81 38 D1 E7 7B FE 88 45 53 BD 75 E1 43 EF 29 DD A5 2E C8 59 95 8D 6D AA 39 05 9C D9 39 B8 AA 53 00 77 B1 1F BD 49 40 C3 11 35 28 B7 52 87 EA E9 A0 04 E1 52 97 7E 58 83 95 6C 9B 10 93 F5 B4 9F 17 08 92 F0 03
";
        string string_raw_data =
 @"
00 07 00 80 00 04 50 E1 79 0E B7 3F CF AD 00 00 00 00 00 70 2D 39 AC EE 23 D1 26 5C 55 6F 12 61 0C 77 3F 09 59 82 92 C1 47 E2 2F DA 8B 87 07 EC 0B FB B1 07 31 AF FA E8 C1 22 92 16 1C 44 CE 0D 46 38 AC FC B7 32 36 30 51 1F 23 C3 50 E2 AB 16 A1 72 CD 33 AC FE AF 72 5E F1 FB A3 B9 17 EB 17 EE 4F AE 0A 89 66 27 00 B3 54 44 29 A1 32 4A 0C 9C BD EB B8 58 E6 5A D6 E0 5C FC A6 B6 59 CA C6 F1 DB AB D6 00 0C 00 16 00 02 00 00 00 00 00 00 00 00 00 00 77 93 2D CE 1F 40 00 00 00 00 00 15 00 30 00 01 01 58 36 8F 1F 00 10 00 80 2F 32 EF E4 56 82 DA 0D 26 A4 24 AC D1 CC 02 62 47 AC 21 00 10 27 54 F4 7C 39 47 FC B9 F1 8A EA 52 C7 BE CC 41 00 18 00 16 00 01 00 00 04 13 00 00 00 01 00 00 12 F7 69 C7 56 E8 00 00 00 00 00 1F 00 22 00 01 CA E6 5F 96 A4 75 F0 B9 C5 98 7F F0 E5 76 9C 35 B8 F7 30 5E A6 90 B7 2D A9 3B B9 D1 8F 8F FF AB 01 05 00 88 00 01 01 02 00 40 02 01 03 3C 01 03 00 00 4E CF 6D D0 57 DB B9 B9 2A 83 91 BB 98 FE 26 E1 B8 AC F6 71 13 CF B0 DC B1 48 BA 54 BA 15 D2 F6 43 E4 89 75 E2 E1 E3 EE 60 E7 FC 25 96 35 D3 0A 74 5F 61 E4 31 F1 C4 E8 00 40 02 02 03 3C 01 03 00 00 C0 D1 A4 34 0D 89 A3 B4 B0 81 C8 8D E1 C4 CB 21 9B 4F E0 5D F7 2E F3 6C C3 AE 2F 16 ED BE B3 F6 00 46 91 CE D1 C9 6D F1 1D B8 0D 71 B6 59 05 E7 69 41 A4 22 F3 D1 C3 59 01 0B 00 38 00 01 B6 D0 EC 6C 19 10 49 2D 54 44 84 B1 74 69 6E 43 9C 10 00 00 00 00 00 00 00 02 00 18 0B 4A 93 36 AD B6 2E 42 22 37 A0 A0 C0 69 B5 E2 95 21 F7 99 70 A1 35 08 00 00 00 2D 00 06 00 01 C0 A8 58 01
";
//=======================
        public byte[] raw_data;
        byte[] data;
        public byte[] bytes;

        public SP0X08282013(MessageHelper msgHelper,int id)
        {
            QqnumInfo ins = new QQNUM().ISelect(string.Format("QQ='{0}'", msgHelper.QQ))[0];
            this.bytes = Tools.HexStringToBytes(this.string_bytes);
            this.raw_data = Tools.HexStringToBytes(ins.SP0X0828);
            //this.raw_data = Tools.HexStringToBytes(this.string_raw_data);
            this.InitSp(msgHelper);
        }
        public void InitSp(MessageHelper msgHelper)
        {
        }

        public short SEQ
        {
            get
            {
                return Tools.BytesToPort(Tools.RB(bytes, 5, 2));
            }
            set
            {
                Tools.WB(bytes, 5, Tools.PortToBytes(value));
            }
        }

        public IPAddress QQClentIP
        {
            get
            {
                return Tools.BytesToIPAddress(Tools.RB(raw_data, 10, 4));
            }
            set
            {
                Tools.WB(raw_data, 10, Tools.IPAddressToBytes(value));
            }
        }
        public short QQClentPort
        {
            get
            {
                return Tools.BytesToPort(Tools.RB(raw_data, 14, 2));
            }
            set
            {
                Tools.WB(raw_data, 14, Tools.PortToBytes(value));
            }
        }

        public IPAddress QQRemoteIP
        {
            get
            {
                return Tools.BytesToIPAddress(Tools.RB(raw_data, 148, 4));
            }
            set
            {
                Tools.WB(raw_data, 148, Tools.IPAddressToBytes(value));
            }
        }

        public IPAddress MachineIP
        {
            get
            {
                return Tools.BytesToIPAddress(Tools.RB(raw_data, raw_data.Length-4, 4));
            }
            set
            {
                Tools.WB(raw_data, raw_data.Length - 4, Tools.IPAddressToBytes(value));
            }
        }

        public byte[] token_38
        {
            get 
            {
                return Tools.RB(bytes, 28, 56);
            }
            set 
            {
                Tools.WB(bytes, 28, value);
            }
        }
        byte[] _key0x0828 = new byte[16];
        public byte[] key0x0826
        {
            get 
                 
            {
                return Tools.RB(_key0x0828, 0, 16);
            }
            set 
            {
                Tools.WB(_key0x0828, 0, value);
            }
        }
        public byte[] token_70  
        {
            get 
            {
                return Tools.RB(raw_data, 20, 112);
            }
            set 
            {
                Tools.WB(raw_data, 20, value);
            }
        }
        public void PrepareData(byte[] token0x0825,IPEndPoint qqclienpEP,byte[] loginTime,byte[] qqbytes)
        {
            Array.Copy(qqbytes, 0, this.bytes, 7, 4);
            Array.Copy(qqbytes, 0, this.raw_data, 228+8, 4);//
            //this.debugHelper("时间:" + Tools.BytesToDatetime(time).ToString("yyyy-MM-dd HH:mm:ss"));
            Array.Copy(loginTime, 0, raw_data, 6, 4);
            
            this.QQClentIP = qqclienpEP.Address;
            //this.QQClentPort = (short)qqclienpEP.Port;
            this.MachineIP = Tools.MachineIP;
            //Array.Copy(token_70, 0, raw_data, 20, 112);
            byte[] temp = new byte[84 + new QQCrypt().QQ_Encrypt(raw_data, key0x0826).Length + 1];
            Array.Copy(bytes, 0, temp, 0, bytes.Length); temp[temp.Length - 1] = 0x03;
            Array.Copy(new QQCrypt().QQ_Encrypt(raw_data, key0x0826), 0, temp, 84, new QQCrypt().QQ_Encrypt(raw_data, key0x0826).Length);
            bytes = temp;
        }
    }
}
