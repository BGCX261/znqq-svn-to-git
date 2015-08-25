namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    [Serializable]
    public class ICMPv4Packet : InternetPacket
    {
        private static readonly ILogInactive log;

        public ICMPv4Packet(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public ICMPv4Packet(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, ICMPv4Fields.HeaderLength);
            base.payloadPacketOrData = new PacketOrByteArraySegment();
            base.payloadPacketOrData.TheByteArraySegment = base.header.EncapsulatedBytes();
        }

        public static ICMPv4Packet GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is IpPacket)
                {
                    Packet payloadPacket = innerPayload.PayloadPacket;
                    if (payloadPacket is ICMPv4Packet)
                    {
                        return (ICMPv4Packet) payloadPacket;
                    }
                }
            }
            return null;
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("ICMPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.TypeCode);
            builder.Append(", ");
            builder.Append(" l=" + base.header.Length);
            builder.Append(']');
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public ushort Checksum
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ICMPv4Fields.ChecksumPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ICMPv4Fields.ChecksumPosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.LightBlue;
            }
        }

        public byte[] Data
        {
            get
            {
                return base.payloadPacketOrData.TheByteArraySegment.ActualBytes();
            }
            set
            {
                base.payloadPacketOrData.TheByteArraySegment = new ByteArraySegment(value, 0, value.Length);
            }
        }

        public ushort ID
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ICMPv4Fields.IDPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ICMPv4Fields.IDPosition);
            }
        }

        public ushort Sequence
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ICMPv4Fields.SequencePosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + ICMPv4Fields.SequencePosition);
            }
        }

        public virtual ICMPv4TypeCodes TypeCode
        {
            get
            {
                ushort num = EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ICMPv4Fields.TypeCodePosition);
                if (!Enum.IsDefined(typeof(ICMPv4TypeCodes), num))
                {
                    throw new NotImplementedException("TypeCode of " + num + " is not defined in ICMPv4TypeCode");
                }
                return (ICMPv4TypeCodes) num;
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ICMPv4Fields.TypeCodePosition);
            }
        }
    }
}

