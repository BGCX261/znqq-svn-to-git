namespace PacketDotNet.LLDP
{
    using MiscUtil.Conversion;
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;

    public class TLVTypeLength
    {
        private ByteArraySegment byteArraySegment;
        private const int LengthBits = 9;
        private const int LengthMask = 0x1ff;
        private static readonly ILogInactive log;
        private const int MaximumTLVLength = 0x1ff;
        private const int TypeBits = 7;
        public const int TypeLengthLength = 2;
        private const int TypeMask = 0xfe00;

        public TLVTypeLength(ByteArraySegment byteArraySegment)
        {
            this.byteArraySegment = byteArraySegment;
        }

        public int Length
        {
            get
            {
                ushort typeAndLength = this.TypeAndLength;
                return (0x1ff & typeAndLength);
            }
            internal set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Length", "Length must be a positive value");
                }
                if (value > 0x1ff)
                {
                    throw new ArgumentOutOfRangeException("Length", "The maximum value for a TLV length is 511");
                }
                ushort num = (ushort) (0xfe00 & this.TypeAndLength);
                this.TypeAndLength = (ushort) (num | value);
            }
        }

        public TLVTypes Type
        {
            get
            {
                return (TLVTypes) (this.TypeAndLength >> 9);
            }
            set
            {
                ushort num = (ushort) (((ushort) value) << 9);
                ushort num2 = (ushort) (0x1ff & this.TypeAndLength);
                this.TypeAndLength = (ushort) (num | num2);
            }
        }

        private ushort TypeAndLength
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(this.byteArraySegment.Bytes, this.byteArraySegment.Offset);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, this.byteArraySegment.Bytes, this.byteArraySegment.Offset);
            }
        }
    }
}

