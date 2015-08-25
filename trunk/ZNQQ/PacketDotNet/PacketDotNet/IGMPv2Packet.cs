namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    [Serializable]
    public class IGMPv2Packet : InternetPacket
    {
        public IGMPv2Packet(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public IGMPv2Packet(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            throw new NotImplementedException();
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("IGMPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.Type);
            builder.Append(", ");
            builder.Append(this.GroupAddress + ": ");
            builder.Append(" l=" + IGMPv2Fields.HeaderLength);
            builder.Append(']');
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public virtual int Checksum
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IGMPv2Fields.ChecksumPosition);
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + IGMPv2Fields.ChecksumPosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.Brown;
            }
        }

        public virtual IPAddress GroupAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetwork, base.header.Offset + IGMPv2Fields.GroupAddressPosition, base.header.Bytes);
            }
        }

        public virtual int MaxResponseTime
        {
            get
            {
                return base.header.Bytes[base.header.Offset + IGMPv2Fields.MaxResponseTimePosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IGMPv2Fields.MaxResponseTimePosition] = (byte) value;
            }
        }

        public virtual IGMPMessageType Type
        {
            get
            {
                return (IGMPMessageType) base.header.Bytes[base.header.Offset + IGMPv2Fields.TypePosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IGMPv2Fields.TypePosition] = (byte) value;
            }
        }
    }
}

