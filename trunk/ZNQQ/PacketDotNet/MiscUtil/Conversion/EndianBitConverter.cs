namespace MiscUtil.Conversion
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class EndianBitConverter
    {
        private static BigEndianBitConverter big = new BigEndianBitConverter();
        private static LittleEndianBitConverter little = new LittleEndianBitConverter();

        protected EndianBitConverter()
        {
        }

        private static void CheckByteArgument(byte[] value, int startIndex, int bytesRequired)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if ((startIndex < 0) || (startIndex > (value.Length - bytesRequired)))
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
        }

        private long CheckedFromBytes(byte[] value, int startIndex, int bytesToConvert)
        {
            CheckByteArgument(value, startIndex, bytesToConvert);
            return this.FromBytes(value, startIndex, bytesToConvert);
        }

        public void CopyBytes(bool value, byte[] buffer, int index)
        {
            this.CopyBytes(!value ? ((long) 0) : ((long) 1), 1, buffer, index);
        }

        public void CopyBytes(char value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 2, buffer, index);
        }

        public void CopyBytes(decimal value, byte[] buffer, int index)
        {
            int[] bits = decimal.GetBits(value);
            for (int i = 0; i < 4; i++)
            {
                this.CopyBytesImpl((long) bits[i], 4, buffer, (i * 4) + index);
            }
        }

        public void CopyBytes(double value, byte[] buffer, int index)
        {
            this.CopyBytes(this.DoubleToInt64Bits(value), 8, buffer, index);
        }

        public void CopyBytes(short value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 2, buffer, index);
        }

        public void CopyBytes(int value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 4, buffer, index);
        }

        public void CopyBytes(long value, byte[] buffer, int index)
        {
            this.CopyBytes(value, 8, buffer, index);
        }

        public void CopyBytes(float value, byte[] buffer, int index)
        {
            this.CopyBytes((long) this.SingleToInt32Bits(value), 4, buffer, index);
        }

        public void CopyBytes(ushort value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 2, buffer, index);
        }

        public void CopyBytes(uint value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 4, buffer, index);
        }

        public void CopyBytes(ulong value, byte[] buffer, int index)
        {
            this.CopyBytes((long) value, 8, buffer, index);
        }

        private void CopyBytes(long value, int bytes, byte[] buffer, int index)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer", "Byte array must not be null");
            }
            if (buffer.Length < (index + bytes))
            {
                throw new ArgumentOutOfRangeException("Buffer not big enough for value");
            }
            this.CopyBytesImpl(value, bytes, buffer, index);
        }

        protected abstract void CopyBytesImpl(long value, int bytes, byte[] buffer, int index);
        public long DoubleToInt64Bits(double value)
        {
            return BitConverter.DoubleToInt64Bits(value);
        }

        protected abstract long FromBytes(byte[] value, int startIndex, int bytesToConvert);
        public byte[] GetBytes(bool value)
        {
            return BitConverter.GetBytes(value);
        }

        public byte[] GetBytes(char value)
        {
            return this.GetBytes((long) value, 2);
        }

        public byte[] GetBytes(decimal value)
        {
            byte[] buffer = new byte[0x10];
            int[] bits = decimal.GetBits(value);
            for (int i = 0; i < 4; i++)
            {
                this.CopyBytesImpl((long) bits[i], 4, buffer, i * 4);
            }
            return buffer;
        }

        public byte[] GetBytes(double value)
        {
            return this.GetBytes(this.DoubleToInt64Bits(value), 8);
        }

        public byte[] GetBytes(short value)
        {
            return this.GetBytes((long) value, 2);
        }

        public byte[] GetBytes(int value)
        {
            return this.GetBytes((long) value, 4);
        }

        public byte[] GetBytes(long value)
        {
            return this.GetBytes(value, 8);
        }

        public byte[] GetBytes(float value)
        {
            return this.GetBytes((long) this.SingleToInt32Bits(value), 4);
        }

        public byte[] GetBytes(ushort value)
        {
            return this.GetBytes((long) value, 2);
        }

        public byte[] GetBytes(uint value)
        {
            return this.GetBytes((long) value, 4);
        }

        public byte[] GetBytes(ulong value)
        {
            return this.GetBytes((long) value, 8);
        }

        private byte[] GetBytes(long value, int bytes)
        {
            byte[] buffer = new byte[bytes];
            this.CopyBytes(value, bytes, buffer, 0);
            return buffer;
        }

        public float Int32BitsToSingle(int value)
        {
            Int32SingleUnion union = new Int32SingleUnion(value);
            return union.AsSingle;
        }

        public double Int64BitsToDouble(long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }

        public abstract bool IsLittleEndian();
        public int SingleToInt32Bits(float value)
        {
            Int32SingleUnion union = new Int32SingleUnion(value);
            return union.AsInt32;
        }

        public bool ToBoolean(byte[] value, int startIndex)
        {
            CheckByteArgument(value, startIndex, 1);
            return BitConverter.ToBoolean(value, startIndex);
        }

        public char ToChar(byte[] value, int startIndex)
        {
            return (char) ((ushort) this.CheckedFromBytes(value, startIndex, 2));
        }

        public decimal ToDecimal(byte[] value, int startIndex)
        {
            int[] bits = new int[4];
            for (int i = 0; i < 4; i++)
            {
                bits[i] = this.ToInt32(value, startIndex + (i * 4));
            }
            return new decimal(bits);
        }

        public double ToDouble(byte[] value, int startIndex)
        {
            return this.Int64BitsToDouble(this.ToInt64(value, startIndex));
        }

        public short ToInt16(byte[] value, int startIndex)
        {
            return (short) this.CheckedFromBytes(value, startIndex, 2);
        }

        public int ToInt32(byte[] value, int startIndex)
        {
            return (int) this.CheckedFromBytes(value, startIndex, 4);
        }

        public long ToInt64(byte[] value, int startIndex)
        {
            return this.CheckedFromBytes(value, startIndex, 8);
        }

        public float ToSingle(byte[] value, int startIndex)
        {
            return this.Int32BitsToSingle(this.ToInt32(value, startIndex));
        }

        public static string ToString(byte[] value)
        {
            return BitConverter.ToString(value);
        }

        public static string ToString(byte[] value, int startIndex)
        {
            return BitConverter.ToString(value, startIndex);
        }

        public static string ToString(byte[] value, int startIndex, int length)
        {
            return BitConverter.ToString(value, startIndex, length);
        }

        public ushort ToUInt16(byte[] value, int startIndex)
        {
            return (ushort) this.CheckedFromBytes(value, startIndex, 2);
        }

        public uint ToUInt32(byte[] value, int startIndex)
        {
            return (uint) this.CheckedFromBytes(value, startIndex, 4);
        }

        public ulong ToUInt64(byte[] value, int startIndex)
        {
            return (ulong) this.CheckedFromBytes(value, startIndex, 8);
        }

        public static BigEndianBitConverter Big
        {
            get
            {
                return big;
            }
        }

        public abstract MiscUtil.Conversion.Endianness Endianness { get; }

        public static LittleEndianBitConverter Little
        {
            get
            {
                return little;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Int32SingleUnion
        {
            [FieldOffset(0)]
            private float f;
            [FieldOffset(0)]
            private int i;

            internal Int32SingleUnion(int i)
            {
                this.f = 0f;
                this.i = i;
            }

            internal Int32SingleUnion(float f)
            {
                this.i = 0;
                this.f = f;
            }

            internal int AsInt32
            {
                get
                {
                    return this.i;
                }
            }

            internal float AsSingle
            {
                get
                {
                    return this.f;
                }
            }
        }
    }
}

