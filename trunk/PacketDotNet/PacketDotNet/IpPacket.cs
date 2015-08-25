namespace PacketDotNet
{
    using PacketDotNet.Utils;
    using System;
    using System.Net;
    using System.Net.Sockets;

    public abstract class IpPacket : InternetPacket
    {
        protected int DefaultTimeToLive;
        private static readonly ILogInactive log;

        public IpPacket(PosixTimeval Timeval) : base(Timeval)
        {
            this.DefaultTimeToLive = 0x40;
        }

        internal abstract byte[] AttachPseudoIPHeader(byte[] origHeader);
        public static IpPacket GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is IpPacket)
                {
                    return (IpPacket) innerPayload;
                }
            }
            return null;
        }

        public static IPAddress GetIPAddress(AddressFamily ipType, int fieldOffset, byte[] bytes)
        {
            byte[] buffer;
            if (ipType == AddressFamily.InterNetwork)
            {
                buffer = new byte[IPv4Fields.AddressLength];
            }
            else
            {
                if (ipType != AddressFamily.InterNetworkV6)
                {
                    throw new InvalidOperationException("ipType " + ipType + " unknown");
                }
                buffer = new byte[IPv6Fields.AddressLength];
            }
            Array.Copy(bytes, fieldOffset, buffer, 0, buffer.Length);
            return new IPAddress(buffer);
        }

        internal static PacketOrByteArraySegment ParseEncapsulatedBytes(ByteArraySegment Header, IPProtocolType ProtocolType, PosixTimeval Timeval, Packet ParentPacket)
        {
            ByteArraySegment segment = Header.EncapsulatedBytes();
            PacketOrByteArraySegment segment2 = new PacketOrByteArraySegment();
            IPProtocolType type = ProtocolType;
            if (type == IPProtocolType.ICMP)
            {
                segment2.ThePacket = new ICMPv4Packet(segment.Bytes, segment.Offset, Timeval);
                return segment2;
            }
            if (type != IPProtocolType.TCP)
            {
                if (type == IPProtocolType.UDP)
                {
                    segment2.ThePacket = new UdpPacket(segment.Bytes, segment.Offset, Timeval, ParentPacket);
                    return segment2;
                }
                if (type == IPProtocolType.ICMPV6)
                {
                    segment2.ThePacket = new ICMPv6Packet(segment.Bytes, segment.Offset, Timeval);
                    return segment2;
                }
                segment2.TheByteArraySegment = segment;
                return segment2;
            }
            segment2.ThePacket = new TcpPacket(segment.Bytes, segment.Offset, Timeval, ParentPacket);
            return segment2;
        }

        public static IpPacket RandomPacket(IpVersion version)
        {
            if (version == IpVersion.IPv4)
            {
                return IPv4Packet.RandomPacket();
            }
            if (version != IpVersion.IPv6)
            {
                throw new InvalidOperationException("Unknown version of " + version);
            }
            return IPv6Packet.RandomPacket();
        }

        public abstract IPAddress DestinationAddress { get; set; }

        public abstract int HeaderLength { get; set; }

        public virtual int HopLimit
        {
            get
            {
                return this.TimeToLive;
            }
            set
            {
                this.TimeToLive = value;
            }
        }

        public virtual IPProtocolType NextHeader
        {
            get
            {
                return this.Protocol;
            }
            set
            {
                this.Protocol = value;
            }
        }

        public abstract ushort PayloadLength { get; set; }

        public override Packet PayloadPacket
        {
            get
            {
                return base.PayloadPacket;
            }
            set
            {
                base.PayloadPacket = value;
                if (value is TcpPacket)
                {
                    this.NextHeader = IPProtocolType.TCP;
                }
                else if (value is UdpPacket)
                {
                    this.NextHeader = IPProtocolType.UDP;
                }
                else if (value is ICMPv6Packet)
                {
                    this.NextHeader = IPProtocolType.ICMPV6;
                }
                else if (value is ICMPv4Packet)
                {
                    this.NextHeader = IPProtocolType.ICMP;
                }
                else
                {
                    this.NextHeader = IPProtocolType.NONE;
                }
                ushort length = (ushort) base.PayloadPacket.Bytes.Length;
                this.PayloadLength = length;
            }
        }

        public abstract IPProtocolType Protocol { get; set; }

        public abstract IPAddress SourceAddress { get; set; }

        public abstract int TimeToLive { get; set; }

        public abstract int TotalLength { get; set; }

        public abstract IpVersion Version { get; set; }
    }
}

