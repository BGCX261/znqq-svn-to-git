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
    public class SP0X0828
    {
       string string_bytes =
@"
02 2F 59 08 28 4E 07 69 C7 56 E8 02 00 00 00 01 01 01 00 00 65 B1 00 30 00 3A 00 38 33 08 9D 9D E8 2A 4C B7 6B 67 E7 0E 83 C4 8D 9D 09 92 DD A5 7E 24 7E 9E 33 EB 31 CD E1 B2 B6 42 73 61 68 FE 40 2E EC 46 F7 C8 D0 6D 01 8F 17 9F 3B 69 58 43 FB 7B 14 EB 19 6C 4D 34 19 87 AC 65 94 78 53 DD DC FB FE 73 00 39 DD 0E 07 63 A2 50 1E EF A1 BE 1F C0 EF D9 38 0A AB 7D 2C 70 90 D2 DF F3 A7 A8 84 8C 89 E5 87 F9 66 C1 D4 D8 7D 8C 8F 8C 4D 6A B7 F4 6B 25 44 CF BE B0 49 DA 0A D0 EA DD 5B B1 04 E4 0D E9 37 D0 A5 6C B0 CB 62 00 90 31 6E 6F B4 AB 7F C8 11 CE A8 15 97 D4 6C FA 1A 01 B8 66 82 57 71 34 65 89 2E EC 17 93 1E B4 40 24 4B 6F E0 B3 40 29 16 4C 99 5A 2B FE F9 D0 86 3E 34 83 E9 E9 83 F0 91 EC D8 02 15 16 4F 21 CF 2F 4A CB D4 A6 73 E3 79 38 1A B6 91 0C B6 43 81 2B E4 60 57 5F D6 09 EE 1A B6 18 83 A2 92 2B 20 81 7D A6 6B 4A 58 01 7C 90 7B A8 EC EF 47 B7 F8 A6 C3 96 DF C3 DF 71 F6 61 BF C5 29 3F A2 E9 97 38 51 CF 6A 63 C1 D3 EC 04 89 8E 83 F0 C8 5C 79 C5 44 5E A9 CC 76 DD 43 1E 94 5E 52 D8 CF 41 7F 67 84 29 04 1A B1 69 77 DB E6 C7 1B E4 AD CE 13 09 58 48 D8 F1 B5 02 6F 11 FE 89 E4 4B 14 3D 01 8B A4 D4 68 68 BA 27 75 3A 48 39 4C D4 99 CC 36 1A 48 2B 20 98 EF E1 FA 30 72 6E F5 BC 16 3A EE 2F E1 49 AC 09 ED E2 CF 0A 0C 30 0F 8A E2 68 3B 89 DF FE 0C 0A 83 65 08 86 2C F8 99 4A D3 1C EE 85 D5 4B 69 4B F1 31 EC 14 56 4E 7D 70 77 BC A2 5D AC 69 0E A4 33 15 F7 2C 63 3C 6A F0 1E 88 C5 0B C6 1A B9 FD F1 98 C1 21 9B E4 54 A1 E7 DB 30 6B 84 0B C0 26 39 28 A4 80 2E 1E A0 08 F5 5A D4 78 88 C2 58 C5 48 7B 8C 46 35 4C 49 85 75 37 AF 77 ED F1 9C 91 CC 8D 35 C7 4B BF 9C 3E B3 AF 05 53 00 37 20 14 97 B7 64 CA 59 50 0F C2 40 96 F0 21 6E D8 42 CC 7E B1 6F F6 06 3B F6 86 63 D4 03
";
       string string_raw_data =
@"
00 07 00 80 00 04 50 B9 B0 37 DD 04 8B 63 00 00 00 00 00 70 DC 49 9C DD 27 B2 8A D2 87 7C 21 31 01 24 8F C2 43 E1 A5 BE 29 28 1A 54 E6 FF 86 F9 90 FA 66 87 69 41 1A 0C 4D F6 3E ED 24 EC 07 02 52 6B 8E F2 A9 31 2E D2 B8 D4 DB 9C 58 42 0B CA 0D 91 B9 60 6C F7 54 23 66 48 FE 53 4E 90 D7 D1 95 B2 70 92 7F 2D E8 2F CE 52 C8 D9 F8 23 B8 F8 CB 3C 55 62 F5 E4 0C DD 52 75 DC 55 2C DF 28 A1 94 A3 AA 5E 00 0C 00 16 00 02 00 00 00 00 00 00 00 00 00 00 70 5A 54 3B 1F 40 00 00 00 00 00 15 00 30 00 01 01 FE FF 03 B7 00 10 DF 43 72 1E 35 B4 51 9B A5 D4 93 5D 04 2F 1B D7 02 3E 2E C6 09 00 10 96 04 27 F9 E8 C8 55 F5 2A 37 C7 9C A5 C1 1C 68 00 18 00 16 00 01 00 00 04 03 00 00 00 01 00 00 12 B5 69 C7 56 E8 00 00 00 00 00 1F 00 22 00 01 67 03 2C 4E DC 28 44 0B 73 69 B0 3F DD 1D 48 36 38 4E 92 16 5C 9C AD 12 92 DF C2 F2 4D B9 05 FC 01 05 00 88 00 01 01 02 00 40 02 01 03 3C 01 03 00 00 BB 28 58 E7 F5 E4 EB F4 C4 17 F3 A7 4D 67 A7 A8 84 16 5F 38 26 B5 4A D3 4F B0 6B 6E AB 3C 30 EB BD 3D 9B B5 28 71 B2 7B EC CB 7D 27 35 DD EC 7D F0 E3 40 DE DD CB 34 FD 00 40 02 02 03 3C 01 03 00 00 89 0B 21 FF E1 B4 EF 15 A4 1E 03 CB 3B A2 20 69 75 57 A9 CB 50 C8 5E 1B 21 BB 4F 6E 5F C4 D1 35 59 71 3F 2D 37 C8 51 A7 14 52 DF 7C 17 34 63 4F 2A 27 F0 63 A9 FA F7 1A 01 0B 00 20 00 01 23 8B AE D4 67 47 AA 9A C8 BA 43 B2 34 B7 03 C6 45 10 00 00 00 00 00 00 00 00 00 00 00 00 00 2D 00 06 00 01 C0 A8 A1 3D
";

//       string string_bytes =
//@"
//02 30 37 08 28 3D F3 69 C7 56 E8 02 00 00 00 01 01 01 00 00 65 EF 00 30 00 3A 00 38 F5 76 DA 31 83 C8 C7 29 3C 8B C0 3C 25 96 1F 78 C4 A6 D8 ED C3 E6 94 04 F0 B5 3C 48 75 95 8E 3C 96 1D 66 A4 C7 75 41 D8 71 79 35 0C 19 F0 8B 80 44 91 E0 DD C4 98 A9 84 DC A8 9A A9 DB 4C 03 40 25 8B A6 26 68 34 44 B3 56 15 67 45 C3 52 80 76 64 2E 50 49 57 83 5E 9B 3E 9C CF 7B 87 8E 2B CB AE 27 0A 69 76 41 2C 08 AB 0B C2 44 0D D5 DC 10 C7 95 2D 7D 14 85 36 15 F5 D0 3E 1E 25 12 C4 99 AE 00 9E 3B 5E DD 70 52 F3 EF 05 78 B1 7A A1 51 87 CE F1 CD E6 DA 10 32 CF 84 54 48 F4 ED 16 32 A5 41 D6 D1 B0 F0 E3 70 47 E2 AB 2E 70 D6 70 AA 95 6B 95 40 61 E0 85 03 55 3C AA 85 7C 4D 8B 11 59 1D D3 0B 10 8F 9C B9 9A 64 10 9B A4 4E 23 8F 33 55 AB 6E 02 DA 7E CF 39 E4 5B B7 72 EA F0 21 1A 21 BF 3D 60 8C 93 27 16 C7 1A FD DE AB B7 CE C7 AD A4 38 83 3A BE 15 CA 39 BB EE 81 04 25 D0 81 FF E8 D2 75 AF 65 71 98 C0 01 C7 5F A2 C1 21 AA C0 6D 3B B9 45 30 18 40 53 ED A9 64 B0 6E D4 CE CD 10 FD C5 BB BA 18 7C E5 33 69 16 54 38 92 61 3C BA B2 48 42 60 9A F0 CE CF 43 B3 FD 49 95 09 45 6D BA 07 BD C2 1F 84 F3 F0 68 4A 59 93 5A 31 AF DB DE 42 77 68 34 BA 30 21 2E 27 A7 B0 1F 9E CC AA 8A 6D C2 CB FF 7A 20 87 C8 5F D9 FE 55 C0 DC 38 07 68 3C D6 29 60 6C A0 9C A8 89 FF BC 5B 64 28 E9 E0 0C 7F 64 70 8E 0B 7B 8E E0 8B 02 12 9B A1 E4 3B 11 CB E3 FC 80 6A 17 C0 0D 53 14 4A B3 44 03 59 83 35 9D 31 15 4E 55 2D 52 B7 6D 88 99 7F 71 CB 37 E6 9A A6 CD 77 3A 7E 99 2D EB 90 B5 F9 B3 88 D8 A0 41 97 F0 43 1D 22 71 37 D4 11 74 04 5B BF 1D 06 EC 13 7A FB 90 D7 C4 44 A8 4C E6 6C 34 C6 D0 9B F2 B1 EB B9 54 7A 4E C0 F7 F3 16 D0 E8 94 F7 D8 89 11 C3 0B 19 5C 17 B7 B6 8C F4 C8 E7 59 02 48 EF 9C 91 33 69 E2 7C BF B6 16 5C 1E B3 12 98 C6 1C C1 BB CA 54 26 1F 80 77 52 94 73 AA 20 B4 7A 35 03
//";
//       string string_raw_data =
//@"
//00 07 00 80 00 04 50 E1 79 0E B7 3F CF AD 00 00 00 00 00 70 2D 39 AC EE 23 D1 26 5C 55 6F 12 61 0C 77 3F 09 59 82 92 C1 47 E2 2F DA 8B 87 07 EC 0B FB B1 07 31 AF FA E8 C1 22 92 16 1C 44 CE 0D 46 38 AC FC B7 32 36 30 51 1F 23 C3 50 E2 AB 16 A1 72 CD 33 AC FE AF 72 5E F1 FB A3 B9 17 EB 17 EE 4F AE 0A 89 66 27 00 B3 54 44 29 A1 32 4A 0C 9C BD EB B8 58 E6 5A D6 E0 5C FC A6 B6 59 CA C6 F1 DB AB D6 00 0C 00 16 00 02 00 00 00 00 00 00 00 00 00 00 77 93 2D CE 1F 40 00 00 00 00 00 15 00 30 00 01 01 58 36 8F 1F 00 10 00 80 2F 32 EF E4 56 82 DA 0D 26 A4 24 AC D1 CC 02 62 47 AC 21 00 10 27 54 F4 7C 39 47 FC B9 F1 8A EA 52 C7 BE CC 41 00 18 00 16 00 01 00 00 04 13 00 00 00 01 00 00 12 F7 69 C7 56 E8 00 00 00 00 00 1F 00 22 00 01 CA E6 5F 96 A4 75 F0 B9 C5 98 7F F0 E5 76 9C 35 B8 F7 30 5E A6 90 B7 2D A9 3B B9 D1 8F 8F FF AB 01 05 00 88 00 01 01 02 00 40 02 01 03 3C 01 03 00 00 4E CF 6D D0 57 DB B9 B9 2A 83 91 BB 98 FE 26 E1 B8 AC F6 71 13 CF B0 DC B1 48 BA 54 BA 15 D2 F6 43 E4 89 75 E2 E1 E3 EE 60 E7 FC 25 96 35 D3 0A 74 5F 61 E4 31 F1 C4 E8 00 40 02 02 03 3C 01 03 00 00 C0 D1 A4 34 0D 89 A3 B4 B0 81 C8 8D E1 C4 CB 21 9B 4F E0 5D F7 2E F3 6C C3 AE 2F 16 ED BE B3 F6 00 46 91 CE D1 C9 6D F1 1D B8 0D 71 B6 59 05 E7 69 41 A4 22 F3 D1 C3 59 01 0B 00 38 00 01 B6 D0 EC 6C 19 10 49 2D 54 44 84 B1 74 69 6E 43 9C 10 00 00 00 00 00 00 00 02 00 18 0B 4A 93 36 AD B6 2E 42 22 37 A0 A0 C0 69 B5 E2 95 21 F7 99 70 A1 35 08 00 00 00 2D 00 06 00 01 C0 A8 58 01
//";
//=======================
        public byte[] raw_data;
        byte[] data;
        public byte[] bytes;

        public SP0X0828(MessageHelper msgHelper,int id)
        {
            QqnumInfo ins = new QQNUM().ISelect(string.Format("QQ='{0}'", msgHelper.QQ))[0];
            this.bytes = Tools.HexStringToBytes(this.string_bytes);
            this.raw_data = Tools.HexStringToBytes(ins.SP0X0828);
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
                return Tools.BytesToIPAddress(Tools.RB(raw_data, 456, 4));
            }
            set
            {
                Tools.WB(raw_data, 456, Tools.IPAddressToBytes(value));
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
            Array.Copy(qqbytes, 0, this.raw_data, 228, 4);
            //this.debugHelper("时间:" + Tools.BytesToDatetime(time).ToString("yyyy-MM-dd HH:mm:ss"));
            Array.Copy(loginTime, 0, raw_data, 6, 4);
            
            this.QQClentIP = qqclienpEP.Address;
            //this.QQClentPort = (short)qqclienpEP.Port;
            this.MachineIP = Tools.MachineIP;
            //Array.Copy(token_70, 0, raw_data, 20, 112);
            Array.Copy(new QQCrypt().QQ_Encrypt(raw_data, key0x0826),0,bytes, 84, 472);
            //Array.Copy(token_38,0,bytes,28,56);
        }
    }
}
