namespace MiscUtil.Conversion
{
    using System;

    public sealed class BigEndianBitConverter : EndianBitConverter
    {
        protected override void CopyBytesImpl(long value, int bytes, byte[] buffer, int index)
        {
            int num = (index + bytes) - 1;
            for (int i = 0; i < bytes; i++)
            {
                buffer[num - i] = (byte) (value & 0xffL);
                value = value >> 8;
            }
        }

        protected override long FromBytes(byte[] buffer, int startIndex, int bytesToConvert)
        {
            long num = 0L;
            for (int i = 0; i < bytesToConvert; i++)
            {
                num = (num << 8) | buffer[startIndex + i];
            }
            return num;
        }

        public sealed override bool IsLittleEndian()
        {
            return false;
        }

        public sealed override MiscUtil.Conversion.Endianness Endianness
        {
            get
            {
                return MiscUtil.Conversion.Endianness.BigEndian;
            }
        }
    }
}

