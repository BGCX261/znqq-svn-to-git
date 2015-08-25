﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZNQQ
{
    public class SP0x00622013
    {
        public byte[] bytes = 
        {
             0x02,0x30,0x37,0x00,0x62,0x33,0xB9,0x69,0xC7,0x56,0xE8,0x02,0x00,0x00,0x00,0x01
            ,0x01,0x01,0x00,0x00,0x65,0xB1,0xEE,0x62,0x9D,0x6B,0xF6,0x46,0xD5,0xE4,0xEF,0x9E
            ,0xB4,0x18,0xEE,0x43,0xCE,0x92,0x15,0x70,0x9A,0x82,0xBA,0x0D,0x2A,0xF3,0xBE,0x61
            ,0x2C,0xA4,0x3A,0x82,0x73,0xA3,0x03
        };
        byte[] data = { 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00 };
        public SP0x00622013()
        { }
        public void PrepareData(byte[] sessionKey, byte[] qqbytes)
        {
            Array.Copy(qqbytes, 0, this.bytes, 7, 4);
            Array.Copy(new QQCrypt().QQ_Encrypt(data, sessionKey), 0, bytes, 22, 32);
        }
    }
}