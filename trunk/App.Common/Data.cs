using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Drawing;

namespace App.Common
{
    public class Data
    {

      //  一. 二进制转换成图片
//MemoryStream ms = new MemoryStream(bytes);
//ms.Position = 0;
//Image img = Image.FromStream(ms);
//ms.Close();
//this.pictureBox1.Image
        public static Image ImageFromBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            Image img= Image.FromStream(ms);
            ms.Close();
            return img;
        }
        
//二. C#中byte[]与string的转换代码

//1、System.Text.UnicodeEncoding converter = new System.Text.UnicodeEncoding();
//  byte[] inputBytes =converter.GetBytes(inputString);
//  string inputString = converter.GetString(inputBytes);

//2、string inputString = System.Convert.ToBase64String(inputBytes);
//  byte[] inputBytes = System.Convert.FromBase64String(inputString);

//FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);



        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// 将 byte[] 转成 Stream

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


    }
}
