namespace PacketDotNet.Utils
{
    using System;
    using System.Text;

    public class HexPrinter
    {
        public static string GetString(byte[] Byte, int Offset, int Length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = Offset; i < (Offset + Length); i++)
            {
                builder.AppendFormat("[{0:x2}]", Byte[i]);
            }
            return builder.ToString();
        }
    }
}

