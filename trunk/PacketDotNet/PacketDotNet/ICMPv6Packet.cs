namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    [Serializable]
    public class ICMPv6Packet : InternetPacket
    {
        private static readonly ILogInactive log;

        public ICMPv6Packet(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public ICMPv6Packet(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, Bytes.Length - Offset);
        }

        public static ICMPv6Packet GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is IpPacket)
                {
                    Packet payloadPacket = innerPayload.PayloadPacket;
                    if (payloadPacket is ICMPv6Packet)
                    {
                        return (ICMPv6Packet) payloadPacket;
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
            builder.Append(this.Type);
            builder.Append(this.Code);
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
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ICMPv6Fields.ChecksumPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ICMPv6Fields.ChecksumPosition);
            }
        }

        public virtual byte Code
        {
            get
            {
                return base.header.Bytes[base.header.Offset + ICMPv6Fields.CodePosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + ICMPv6Fields.CodePosition] = value;
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.LightBlue;
            }
        }

        public virtual ICMPv6Types Type
        {
            get
            {
                byte num = base.header.Bytes[base.header.Offset + ICMPv6Fields.TypePosition];
                if (!Enum.IsDefined(typeof(ICMPv6Types), num))
                {
                    throw new NotImplementedException("Type of " + num + " is not defined in ICMPv6Types");
                }
                return (ICMPv6Types) num;
            }
            set
            {
                base.header.Bytes[base.header.Offset + ICMPv6Fields.TypePosition] = (byte) value;
            }
        }
    }
}

