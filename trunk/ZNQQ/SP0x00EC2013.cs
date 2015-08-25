﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZNQQ
{
    public class SP0x00EC2013
    {
        public byte[] bytes = 
        {
             0x02,0x30,0x37,0x00,0xEC,0x33,0xB9,0x69,0xC7,0x56,0xE8,0x02,0x00,0x00,0x00,0x01
            ,0x01,0x01,0x00,0x00,0x65,0xEF,0x80,0x64,0xE7,0x99,0xD1,0x7C,0xBE,0xCD,0x39,0x1E
            ,0x3E,0x26,0xC5,0xE2,0x81,0x49,0x03
        };
        byte[] data = { 0x01, 0x00, 0x0A };
        public SP0x00EC2013()
        { }
        public void PrepareData(byte[] sessionKey, byte[] qqbytes)
        {
            Array.Copy(qqbytes, 0, this.bytes, 7, 4);
            Array.Copy(new QQCrypt().QQ_Encrypt(data, sessionKey), 0, bytes, 22, 16);
        }
    }
}
