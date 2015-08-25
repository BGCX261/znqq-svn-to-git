using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.BLL;
using App.Model;

namespace ZNQQ
{
    public class SP0X0825
    {
        string string_bytes =
@"
02 30 37 08 25 0F 60 69 C7 56 E8 03 00 00 00 01 01 01 00 00 65 EF 00 00 00 00 FB 0C 4F D0 60 A8 5A 57 12 FB 67 5C BD 0E 2D 4E EB 7E 74 02 07 50 13 EC C6 6C E9 2A CA 59 A8 B1 ED FE D9 A2 6B 0D DC E2 57 31 4E 24 C2 31 BA A2 0A 7E AC 0B 0F 87 4C DD 2E D2 89 A3 9F 30 5D 1D 39 FE 63 7B 9F F7 CF 41 E3 60 92 01 57 B6 9B E3 95 FE 8D 17 30 99 B4 DC 03
";
        string string_raw_data =
@"
00 18 00 16 00 01 00 00 04 13 00 00 00 01 00 00 12 F7 69 C7 56 E8 00 00 00 00 01 14 00 19 01 01 00 15 02 64 D2 2C 8E 09 FC 67 68 B7 2E 56 D1 1F BC 59 85 EF 19 0B 92
";

        public byte[] bytes;
        public byte[] raw_data;

        public SP0X0825(MessageHelper msgHelper,int id)
        {
            QqnumInfo ins = new QQNUM().ISelect(string.Format("QQ='{0}'", msgHelper.QQ))[0];
            this.bytes = Tools.HexStringToBytes(this.string_bytes);
            this.raw_data = Tools.HexStringToBytes(ins.SP0X0825);

            this.InitSp(msgHelper);
        }
        public void InitSp(MessageHelper msgHelper)
        {
           // msgHelper.
        }
        public short SEQ
        {
            get 
            {
                return Tools.BytesToPort(Tools.RB(bytes, 5, 2));
            }
            set 
            {
                Tools.WB(bytes,5,Tools.PortToBytes(value));
            }
        }

        public string QQ
        {
            get
            {
                if (Tools.BytesToQQ(Tools.RB(bytes, 7, 4)) != Tools.BytesToQQ(Tools.RB(raw_data, 18, 4))) throw new Exception("SP0X0825");
                return Tools.BytesToQQ(Tools.RB(bytes, 7, 4));
                //return Tools.BytesToQQ(Tools.RB(raw_data, 18, 4));
            }
            set 
            {
                Tools.WB(bytes, 7, Tools.QQtoBytes(value));
                Tools.WB(raw_data, 18, Tools.QQtoBytes(value));
            }
        }
        public byte[] key
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
        public byte[] para001
        {
            get
            {
                return Tools.RB(raw_data, 35, 20);
            }
            set
            {
                Tools.WB(raw_data, 35, value);
            }
        }

        public void PrepareData(byte[] qqbytes)
        {
            Array.Copy(qqbytes, 0, this.bytes, 7, 4);
            Array.Copy(qqbytes,0,this.raw_data,18,4);
            Array.Copy(new QQCrypt().QQ_Encrypt(raw_data, key), 0, bytes, 42, 72);
        }
    }
}
