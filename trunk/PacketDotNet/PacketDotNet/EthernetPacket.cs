namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Net.NetworkInformation;
    using System.Text;

    public class EthernetPacket : InternetLinkLayerPacket
    {
        private static readonly ILogInactive log;

        public EthernetPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public EthernetPacket(PhysicalAddress SourceHwAddress, PhysicalAddress DestinationHwAddress, EthernetPacketType ethernetPacketType) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = EthernetFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.SourceHwAddress = SourceHwAddress;
            this.DestinationHwAddress = DestinationHwAddress;
            this.Type = ethernetPacketType;
        }

        public EthernetPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, EthernetFields.HeaderLength);
            base.payloadPacketOrData = ParseEncapsulatedBytes(base.header, this.Type, Timeval);
        }

        internal static PacketOrByteArraySegment ParseEncapsulatedBytes(ByteArraySegment Header, EthernetPacketType Type, PosixTimeval Timeval)
        {
            ByteArraySegment segment = Header.EncapsulatedBytes();
            PacketOrByteArraySegment segment2 = new PacketOrByteArraySegment();
            EthernetPacketType type = Type;
            if (type != EthernetPacketType.IpV4)
            {
                if (type == EthernetPacketType.Arp)
                {
                    segment2.ThePacket = new ARPPacket(segment.Bytes, segment.Offset, Timeval);
                    return segment2;
                }
                if (type == EthernetPacketType.IpV6)
                {
                    segment2.ThePacket = new IPv6Packet(segment.Bytes, segment.Offset, Timeval);
                    return segment2;
                }
                if (type == EthernetPacketType.PointToPointProtocolOverEthernetSessionStage)
                {
                    segment2.ThePacket = new PPPoEPacket(segment.Bytes, segment.Offset, Timeval);
                    return segment2;
                }
                if (type == EthernetPacketType.LLDP)
                {
                    segment2.ThePacket = new LLDPPacket(segment.Bytes, segment.Offset, Timeval);
                    return segment2;
                }
                segment2.TheByteArraySegment = segment;
                return segment2;
            }
            segment2.ThePacket = new IPv4Packet(segment.Bytes, segment.Offset, Timeval);
            return segment2;
        }

        public static EthernetPacket RandomPacket()
        {
            Random random = new Random();
            byte[] buffer = new byte[EthernetFields.MacAddressLength];
            byte[] buffer2 = new byte[EthernetFields.MacAddressLength];
            random.NextBytes(buffer);
            random.NextBytes(buffer2);
            return new EthernetPacket(new PhysicalAddress(buffer), new PhysicalAddress(buffer2), EthernetPacketType.None);
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("EthernetPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.SourceHwAddress + " -> " + this.DestinationHwAddress);
            builder.Append(" proto=" + this.Type.ToString() + " (0x" + Convert.ToString((int) this.Type, 0x10) + ")");
            builder.Append(" l=" + EthernetFields.HeaderLength);
            builder.Append(']');
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            return this.ToColoredString(colored);
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.DarkGray;
            }
        }

        public virtual PhysicalAddress DestinationHwAddress
        {
            get
            {
                byte[] destinationArray = new byte[EthernetFields.MacAddressLength];
                Array.Copy(base.header.Bytes, base.header.Offset + EthernetFields.DestinationMacPosition, destinationArray, 0, destinationArray.Length);
                return new PhysicalAddress(destinationArray);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                if (addressBytes.Length != EthernetFields.MacAddressLength)
                {
                    object[] objArray1 = new object[] { "address length ", addressBytes.Length, " not equal to the expected length of ", EthernetFields.MacAddressLength };
                    throw new InvalidOperationException(string.Concat(objArray1));
                }
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + EthernetFields.DestinationMacPosition, addressBytes.Length);
            }
        }

        public override Packet PayloadPacket
        {
            get
            {
                return base.PayloadPacket;
            }
            set
            {
                base.PayloadPacket = value;
                if (value is IPv4Packet)
                {
                    this.Type = EthernetPacketType.IpV4;
                }
                else if (value is IPv6Packet)
                {
                    this.Type = EthernetPacketType.IpV6;
                }
                else if (value is ARPPacket)
                {
                    this.Type = EthernetPacketType.Arp;
                }
                else if (value is LLDPPacket)
                {
                    this.Type = EthernetPacketType.LLDP;
                }
                else
                {
                    this.Type = EthernetPacketType.None;
                }
            }
        }

        public virtual PhysicalAddress SourceHwAddress
        {
            get
            {
                byte[] destinationArray = new byte[EthernetFields.MacAddressLength];
                Array.Copy(base.header.Bytes, base.header.Offset + EthernetFields.SourceMacPosition, destinationArray, 0, destinationArray.Length);
                return new PhysicalAddress(destinationArray);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                if (addressBytes.Length != EthernetFields.MacAddressLength)
                {
                    object[] objArray1 = new object[] { "address length ", addressBytes.Length, " not equal to the expected length of ", EthernetFields.MacAddressLength };
                    throw new InvalidOperationException(string.Concat(objArray1));
                }
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + EthernetFields.SourceMacPosition, addressBytes.Length);
            }
        }

        public virtual EthernetPacketType Type
        {
            get
            {
                return (EthernetPacketType) ((ushort) EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + EthernetFields.TypePosition));
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + EthernetFields.TypePosition);
            }
        }
    }
}

