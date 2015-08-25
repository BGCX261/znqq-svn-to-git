namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class UdpPacket : TransportPacket
    {
        private static readonly ILogInactive log;

        public UdpPacket(ushort SourcePort, ushort DestinationPort) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = UdpFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.SourcePort = SourcePort;
            this.DestinationPort = DestinationPort;
        }

        public UdpPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public UdpPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, UdpFields.HeaderLength);
            base.payloadPacketOrData = new PacketOrByteArraySegment();
            base.payloadPacketOrData.TheByteArraySegment = base.header.EncapsulatedBytes();
        }

        public UdpPacket(byte[] Bytes, int Offset, PosixTimeval Timeval, Packet ParentPacket) : this(Bytes, Offset, Timeval)
        {
            this.ParentPacket = ParentPacket;
        }

        public int CalculateUDPChecksum()
        {
            return base.CalculateChecksum(TransportPacket.TransportChecksumOption.AttachPseudoIPHeader);
        }

        public static UdpPacket GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is IpPacket)
                {
                    Packet payloadPacket = innerPayload.PayloadPacket;
                    if (payloadPacket is UdpPacket)
                    {
                        return (UdpPacket) payloadPacket;
                    }
                }
            }
            return null;
        }

        public static UdpPacket RandomPacket()
        {
            Random random = new Random();
            ushort sourcePort = (ushort) random.Next(0, 0xffff);
            return new UdpPacket(sourcePort, (ushort) random.Next(0, 0xffff));
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("UDPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            if (Enum.IsDefined(typeof(IpPort), this.SourcePort))
            {
                builder.Append((IpPort) this.SourcePort);
            }
            else
            {
                builder.Append(this.SourcePort);
            }
            builder.Append(" -> ");
            if (Enum.IsDefined(typeof(IpPort), this.DestinationPort))
            {
                builder.Append((IpPort) this.DestinationPort);
            }
            else
            {
                builder.Append(this.DestinationPort);
            }
            builder.Append(string.Concat(new object[] { " l=", UdpFields.HeaderLengthLength, ",", this.Length - UdpFields.HeaderLengthLength }));
            builder.Append(']');
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public override void UpdateCalculatedValues()
        {
            this.Length = base.TotalPacketLength;
        }

        public void UpdateUDPChecksum()
        {
            this.Checksum = (ushort) this.CalculateUDPChecksum();
        }

        public override ushort Checksum
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + UdpFields.ChecksumPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + UdpFields.ChecksumPosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.LightGreen;
            }
        }

        public virtual ushort DestinationPort
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + UdpFields.DestinationPortPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + UdpFields.DestinationPortPosition);
            }
        }

        public virtual int Length
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + UdpFields.HeaderLengthPosition);
            }
            internal set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + UdpFields.HeaderLengthPosition);
            }
        }

        public virtual ushort SourcePort
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + UdpFields.SourcePortPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + UdpFields.SourcePortPosition);
            }
        }

        public bool ValidChecksum
        {
            get
            {
                if (base.parentPacket.GetType() == typeof(IPv6Packet))
                {
                    return this.ValidUDPChecksum;
                }
                return (((IPv4Packet) this.ParentPacket).ValidIPChecksum && this.ValidUDPChecksum);
            }
        }

        public virtual bool ValidUDPChecksum
        {
            get
            {
                return this.IsValidChecksum(TransportPacket.TransportChecksumOption.AttachPseudoIPHeader);
            }
        }
    }
}

