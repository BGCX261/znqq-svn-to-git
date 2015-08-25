namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    using System.Text;

    public class IPv4Packet : IpPacket
    {
        public const int HeaderMinimumLength = 20;
        public static IpVersion ipVersion = IpVersion.IPv4;
        private static readonly ILogInactive log;

        public IPv4Packet(IPAddress SourceAddress, IPAddress DestinationAddress) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = IPv4Fields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.PayloadLength = 0;
            this.HeaderLength = 5;
            this.TimeToLive = base.DefaultTimeToLive;
            this.SourceAddress = SourceAddress;
            this.DestinationAddress = DestinationAddress;
            this.Version = ipVersion;
        }

        public IPv4Packet(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public IPv4Packet(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, Bytes.Length - Offset);
            if (this.TotalLength < 20)
            {
                object[] objArray1 = new object[] { "TotalLength ", this.TotalLength, " < HeaderMinimumLength ", 20 };
                throw new InvalidOperationException(string.Concat(objArray1));
            }
            base.header.Length = this.HeaderLength * 4;
            base.payloadPacketOrData = IpPacket.ParseEncapsulatedBytes(base.header, this.NextHeader, Timeval, this);
        }

        internal override byte[] AttachPseudoIPHeader(byte[] origHeader)
        {
            bool flag = (origHeader.Length % 2) != 0;
            int destinationIndex = 12;
            int num2 = destinationIndex + origHeader.Length;
            if (flag)
            {
                num2++;
            }
            byte[] destinationArray = new byte[num2];
            Array.Copy(base.header.Bytes, base.header.Offset + IPv4Fields.SourcePosition, destinationArray, 0, IPv4Fields.AddressLength * 2);
            destinationArray[8] = 0;
            destinationArray[9] = (byte) this.Protocol;
            short length = (short) origHeader.Length;
            EndianBitConverter.Big.CopyBytes(length, destinationArray, 10);
            Array.Copy(origHeader, 0, destinationArray, destinationIndex, origHeader.Length);
            if (flag)
            {
                destinationArray[destinationArray.Length - 1] = 0;
            }
            return destinationArray;
        }

        public int CalculateIPChecksum()
        {
            byte[] header = this.Header;
            byte[] destinationArray = new byte[header.Length];
            Array.Copy(header, destinationArray, header.Length);
            ushort num = 0;
            EndianBitConverter.Big.CopyBytes(num, destinationArray, IPv4Fields.ChecksumPosition);
            return ChecksumUtils.OnesComplementSum(destinationArray, 0, destinationArray.Length);
        }

        public static IPv4Packet RandomPacket()
        {
            IPAddress iPAddress = RandomUtils.GetIPAddress(ipVersion);
            return new IPv4Packet(iPAddress, RandomUtils.GetIPAddress(ipVersion));
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("IPv4Packet");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.SourceAddress + " -> " + this.DestinationAddress);
            builder.Append(" HeaderLength=" + this.HeaderLength);
            builder.Append(" Protocol=" + this.Protocol);
            builder.Append(" TimeToLive=" + this.TimeToLive);
            builder.Append(']');
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("IPv4Packet");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append("version=" + this.Version + ", ");
            builder.Append("hlen=" + this.HeaderLength + ", ");
            builder.Append("tos=" + this.TypeOfService + ", ");
            builder.Append("id=" + this.Id + ", ");
            builder.Append("flags=0x" + Convert.ToString(this.FragmentFlags, 0x10) + ", ");
            builder.Append("offset=" + this.FragmentOffset + ", ");
            builder.Append("ttl=" + this.TimeToLive + ", ");
            builder.Append("proto=" + this.Protocol + ", ");
            builder.Append("sum=0x" + Convert.ToString(this.Checksum, 0x10));
            builder.Append("src=" + this.SourceAddress + ", ");
            builder.Append("dest=" + this.DestinationAddress);
            builder.Append(']');
            builder.Append(base.ToColoredVerboseString(colored));
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public override void UpdateCalculatedValues()
        {
            this.TotalLength = base.TotalPacketLength;
        }

        public void UpdateIPChecksum()
        {
            this.Checksum = this.CalculateIPChecksum();
        }

        public virtual int Checksum
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IPv4Fields.ChecksumPosition);
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + IPv4Fields.ChecksumPosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.White;
            }
        }

        public override IPAddress DestinationAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetwork, base.header.Offset + IPv4Fields.DestinationPosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + IPv4Fields.DestinationPosition, addressBytes.Length);
            }
        }

        public int DifferentiatedServices
        {
            get
            {
                return base.header.Bytes[base.header.Offset + IPv4Fields.DifferentiatedServicesPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IPv4Fields.DifferentiatedServicesPosition] = (byte) value;
            }
        }

        public virtual int FragmentFlags
        {
            get
            {
                return (EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition) >> 13);
            }
            set
            {
                short num = (short) ((EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition) & 0x1fff) | ((value & 7) << 13));
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition);
            }
        }

        public virtual int FragmentOffset
        {
            get
            {
                return (EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition) & 0x1fff);
            }
            set
            {
                short num = (short) ((EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition) & 0xe000) | (value & 0x1fff));
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + IPv4Fields.FragmentOffsetAndFlagsPosition);
            }
        }

        public override int HeaderLength
        {
            get
            {
                return (base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition] & 15);
            }
            set
            {
                byte num = base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition];
                num = (byte) ((num & 240) | (((byte) value) & 15));
                base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition] = num;
            }
        }

        public virtual ushort Id
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + IPv4Fields.IdPosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + IPv4Fields.IdPosition);
            }
        }

        public override ushort PayloadLength
        {
            get
            {
                return (ushort) (this.TotalLength - (this.HeaderLength * 4));
            }
            set
            {
                this.TotalLength = value + (this.HeaderLength * 4);
            }
        }

        public override IPProtocolType Protocol
        {
            get
            {
                return (IPProtocolType) base.header.Bytes[base.header.Offset + IPv4Fields.ProtocolPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IPv4Fields.ProtocolPosition] = (byte) value;
            }
        }

        public override IPAddress SourceAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetwork, base.header.Offset + IPv4Fields.SourcePosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + IPv4Fields.SourcePosition, addressBytes.Length);
            }
        }

        public override int TimeToLive
        {
            get
            {
                return base.header.Bytes[base.header.Offset + IPv4Fields.TtlPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IPv4Fields.TtlPosition] = (byte) value;
            }
        }

        public override int TotalLength
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + IPv4Fields.TotalLengthPosition);
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + IPv4Fields.TotalLengthPosition);
            }
        }

        public int TypeOfService
        {
            get
            {
                return this.DifferentiatedServices;
            }
            set
            {
                this.DifferentiatedServices = value;
            }
        }

        public virtual bool ValidChecksum
        {
            get
            {
                return this.ValidIPChecksum;
            }
        }

        public bool ValidIPChecksum
        {
            get
            {
                if (this.Header.Length < IPv4Fields.HeaderLength)
                {
                    return false;
                }
                return (ChecksumUtils.OnesSum(this.Header) == 0xffff);
            }
        }

        public override IpVersion Version
        {
            get
            {
                return (((IpVersion) (base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition] >> 4)) & ((IpVersion) 15));
            }
            set
            {
                byte num = base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition];
                num = (byte) ((num & 15) | ((((byte) value) << 4) & 240));
                base.header.Bytes[base.header.Offset + IPv4Fields.VersionAndHeaderLengthPosition] = num;
            }
        }

        [StructLayout(LayoutKind.Sequential, Size=1)]
        public struct TypesOfService_Fields
        {
            public static readonly int MINIMIZE_DELAY;
            public static readonly int MAXIMIZE_THROUGHPUT;
            public static readonly int MAXIMIZE_RELIABILITY;
            public static readonly int MINIMIZE_MONETARY_COST;
            public static readonly int UNUSED;
            static TypesOfService_Fields()
            {
                MINIMIZE_DELAY = 0x10;
                MAXIMIZE_THROUGHPUT = 8;
                MAXIMIZE_RELIABILITY = 4;
                MINIMIZE_MONETARY_COST = 2;
                UNUSED = 1;
            }
        }
    }
}

