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
    public class SP0X0826
    {
        string string_verifybytes =
@"
4A 0E 94 C0 00 01 69 C7 56 E8 00 00 04 13 00 00 00 01 00 00 12 F7 00 00 01 68 46 86 06 84 F0 50 29 AB CC C0 9A 53 CD 66 F1 50 E1 75 18 00 00 00 00 00 00 00 00 00 00 00 00 00 B7 3F CF AD 00 00 00 00 00 00 00 00 00 10 AE A5 23 FF 67 3C BB 46 A3 0E C5 5D F8 C9 1A 28 C4 D2 20 01 D7 B8 73 58 81 6D B7 A1 86 A1 AE 78
";
       
        string string_raw_data =
@"
01 12 00 38 E0 45 01 36 2A 9A 14 F7 F5 55 2C AF D3 D1 CC B2 F3 57 3C C5 65 94 ED DD 68 2F 2C 96 65 4E AD 84 9E C9 69 43 E8 41 9F AB 7D 6F D6 30 67 EC C3 31 97 6B 8E 6C 2A 3C 75 EE 00 05 00 06 00 02 69 C7 56 E8 00 06 00 78 33 63 EE BD 1C 61 E5 5D 58 55 AC 6F DB DB C6 B4 CE 3D 39 C7 14 AC 7B EE 7C 8F 4A 17 2A EC DF BC 23 37 49 8C 98 25 83 E6 55 DB 55 AC E9 DC 7F 5C F0 2E CB D0 20 0A 1D 59 1B 5C 0F 5D 80 BE 4F B6 27 A7 87 FA F9 0B 2F A8 CF 9E F2 4F E0 F9 60 EE 26 D8 EE 71 3E E6 5B 40 51 5E AB 6A FD D6 5C 29 D0 C0 A8 EB 48 FC 9E 93 2B B7 50 4C 88 32 2C 51 14 40 0F 09 F3 9D 12 33 00 18 00 16 00 01 00 00 04 13 00 00 00 01 00 00 12 F7 69 C7 56 E8 00 00 00 00 01 03 00 14 00 01 00 10 49 79 B8 19 5A 40 5F 90 AA A3 1C 68 D1 B9 86 B3 01 14 00 19 01 01 00 15 02 64 D2 2C 8E 09 FC 67 68 B7 2E 56 D1 1F BC 59 85 EF 19 0B 92 01 02 00 62 00 01 F7 3A 0D AF AB 7F 59 B6 14 0F 8B BE 63 24 82 74 00 38 A8 63 E1 48 9F 61 78 14 CD 65 FF FE CC 70 0A 9F E4 7A 39 65 DB FF AF 35 0D BA C6 55 BF B1 01 8A B0 71 43 7D F1 9B 3E 63 F7 9F 2D DD EF C8 C6 1D 51 79 6A 4B B8 B6 A0 27 00 14 90 AF F3 A3 D3 43 EB F9 B2 AB 52 A5 0F D4 92 9F C1 F1 0D 37 00 1A 00 40 78 46 58 0C D8 54 B1 89 98 13 FD 99 DC 47 DA 9A 18 40 76 01 08 F1 A6 3B A3 2B 8B 15 BA 94 A8 27 5E 63 57 9D 71 61 64 EF C1 17 81 F1 A3 3D 55 74 DE 55 3B FA 82 98 57 74 03 67 92 1E E5 CC EF 43
";
        string string_last_data =
@"
00 15 00 30 00 00 01 58 36 8F 1F 00 10 00 80 2F 32 EF E4 56 82 DA 0D 26 A4 24 AC D1 CC 02 62 47 AC 21 00 10 27 54 F4 7C 39 47 FC B9 F1 8A EA 52 C7 BE CC 41
";
        string string_bytes =
@"
02 30 37 08 26 26 9F 69 C7 56 E8 03 00 00 00 01 01 01 00 00 65 EF 00 00 00 00 F1 E5 C1 EF 51 D6 F5 83 4C 02 5D C7 44 10 92 49 B3 48 EF D5 C4 9E 96 73 AD 01 63 26 0C 18 00 D7 E7 A8 B2 7B 82 C5 FD 3F 3F 84 B2 A5 63 02 DA 8B 68 FE A5 1D 7A 0B 3A 1E B7 8D 82 2C CB 40 7C 4F 70 0D E5 81 16 B3 C2 7C A0 6B E8 5F 36 6C D6 B5 E8 EF 27 D9 73 41 EC 5B FC B1 30 87 F1 B2 BD 02 52 49 9E FD 41 01 EF E5 73 83 71 ED E6 2A E6 F8 76 67 92 B8 0C 9F 3A 80 15 9C B7 BC BB F7 A7 B7 01 40 80 44 FB 76 78 EC 79 5E 2E 0C E9 AF ED A7 42 9A E9 AD 2C DC 4D 22 D4 CC 57 CD D4 1F C5 5D 7A 5F 41 20 23 52 5A 63 12 5A 62 EE 1F A2 91 86 C5 B9 6F D4 AF 38 DE 20 D9 83 BA 69 B6 D6 B9 5B E8 F0 31 E0 BC 8E 4C 91 27 84 FD 67 51 CA 29 DA 59 04 A7 45 D2 9B CC E8 AC 86 95 28 50 67 B4 22 2B FD 89 B9 75 25 74 7F 28 57 06 97 68 0F 03 FC 61 C8 B9 D8 3D 09 2C BE 4B FC 32 13 82 E7 75 BD 97 7D 4A A0 61 FD 71 FD EE 0C 84 E3 4E C7 EA 9D A0 78 3F CE 06 09 3A C6 AC BA 59 3C 79 8B 25 C5 0B E7 48 C6 3E 6F 20 03 74 61 D2 94 01 5D 29 32 6C A0 CE 74 6E 79 04 11 86 ED 0D D2 E3 D7 E6 F6 BE 49 3B 70 EA 52 C1 BC 8E 57 2B D8 68 A7 A4 91 B1 47 EC A6 D0 BC CA C5 D7 11 FC D8 B3 E8 E5 68 D5 1B 24 77 D9 43 07 0A D9 29 AD 5D A8 F2 54 BA 83 F4 75 54 CC 19 4D ED 61 00 CA 81 FB 3D BF 29 4B EB 27 F4 59 3E 1A E8 6D 98 F8 75 D3 1D AE 2D 4F 1E E5 3E 5B 30 DB 13 42 12 2F 53 FC 16 CB 24 B4 64 4F 68 61 B0 AE DB 6B 2B 40 DA E7 BB 88 28 A9 7A 7A CA 0C 2D 99 8B AE 86 10 40 DE 67 81 E7 77 C1 D2 29 59 4B DC CA BE F1 BC 48 5F D7 A8 EB EC FC 20 37 7F 7E 97 68 03
";


        public byte[] verify_data;
        public byte[] raw_data;
        public byte[] last_data;
        byte[] data = new byte[499];
        public byte[] bytes;

        public SP0X0826(MessageHelper msgHelper,int id)
        {
            QqnumInfo ins = new QQNUM().ISelect(string.Format("QQ='{0}'", msgHelper.QQ))[0];
            //this.raw_data2 = Tools.HexStringToBytes(this.string_verifybytes);
            //this.raw_data = Tools.HexStringToBytes(this.string_raw_data);
            this.verify_data = Tools.HexStringToBytes(ins.VERIFYKEY);
            this.raw_data = Tools.HexStringToBytes(ins.SP0X0826);
            this.last_data = Tools.HexStringToBytes(this.string_last_data);
            this.bytes = Tools.HexStringToBytes(this.string_bytes);

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
                return Tools.BytesToIPAddress(Tools.RB(verify_data, 58, 4));
            }
            set 
            {
                Tools.WB(verify_data, 58, Tools.IPAddressToBytes(value));
            }
        }

        byte[] _keyforverify;
        public byte[] keyForVerify //密码验证
        {
            get { return this._keyforverify; }
            set { this._keyforverify = value; }
        }

        //byte[] _md5pass;
        public byte[] md5Pass
        {
            get { return Tools.RB(verify_data, 25, 16); }
            set { Tools.WB(verify_data, 25, value); }
        }

        public byte[] key0x0826 //0x0826key
        {
            get
            {
                return Tools.RB(bytes, 26, 16);
            }
            set
            {
                Tools.WB(bytes, 26, value);
            }
        }

        public byte[] keyfor0x0826recv //keyfor0x0826recv
        {
            get
            {
                return Tools.RB(verify_data, 88, 16);
            }
            set
            {
                Tools.WB(verify_data, 88, value);
            }
        }

        public void PrepareData(byte[] token0x0825, IPAddress qqclientip, byte[] loginTime,byte[] qqbytes)
        {
            Array.Copy(qqbytes, 0, this.bytes, 7, 4);
            Array.Copy(qqbytes,0,this.raw_data,66,4);
            Array.Copy(qqbytes, 0, this.raw_data, 212, 4);//这个地方是和0x0825一样
            Array.Copy(qqbytes, 0, this.verify_data, 6, 4);

            Array.Copy(loginTime, 0, verify_data, 41, 4);
            this.QQClentIP = qqclientip;

            //debugHelper(string.Format("data2:{0}", Tools.BytesToHexString(data2)));
            Array.Copy(token0x0825, 0, raw_data, 4, 56); //复制令牌
            Array.Copy(new QQCrypt().QQ_Encrypt(verify_data, keyForVerify), 0, raw_data, 74, 120);//复制120字节密文
            Array.Copy(new QQCrypt().QQ_Encrypt(last_data, this.keyfor0x0826recv),0, raw_data,raw_data.Length-64,64);
            data = new QQCrypt().QQ_Encrypt(raw_data, key0x0826);
            //debugHelper(Tools.BytesToHexString(data));
            Array.Copy(data, 0, bytes, 42, data.Length);
        }
    }
}
