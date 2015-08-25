using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;

namespace ZNQQ
{
    class MD5Helper
    {

        public static string ToMD5(string str)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "").ToLower();
        }
        public static byte[] ToMD5(byte[] bytes)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return hashmd5.ComputeHash(bytes);
        }
    }
}
