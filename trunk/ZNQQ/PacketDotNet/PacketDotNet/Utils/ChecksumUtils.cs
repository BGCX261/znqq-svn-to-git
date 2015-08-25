namespace PacketDotNet.Utils
{
    using System;
    using System.IO;
    using System.Net;

    public sealed class ChecksumUtils
    {
        private ChecksumUtils()
        {
        }

        public static int OnesComplementSum(byte[] bytes)
        {
            return OnesComplementSum(bytes, 0, bytes.Length);
        }

        public static int OnesComplementSum(byte[] bytes, int start, int len)
        {
            return (~OnesSum(bytes, start, len) & 0xffff);
        }

        public static int OnesSum(byte[] bytes)
        {
            return OnesSum(bytes, 0, bytes.Length);
        }

        public static int OnesSum(byte[] bytes, int start, int len)
        {
            MemoryStream input = new MemoryStream(bytes, start, len);
            BinaryReader reader = new BinaryReader(input);
            int num = 0;
            while (input.Position < (input.Length - 1L))
            {
                ushort num2 = (ushort) IPAddress.NetworkToHostOrder(reader.ReadInt16());
                num += num2;
            }
            if (input.Position < len)
            {
                num += reader.ReadByte();
            }
            while ((num >> 0x10) != 0)
            {
                num = (num & 0xffff) + (num >> 0x10);
            }
            return num;
        }
    }
}

