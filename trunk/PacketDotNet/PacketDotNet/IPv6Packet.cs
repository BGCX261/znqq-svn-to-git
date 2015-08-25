namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class IPv6Packet : IpPacket
    {
        public const int HeaderMinimumLength = 40;
        public static IpVersion ipVersion = IpVersion.IPv6;
        private static readonly ILogInactive log;

        public IPv6Packet(IPAddress SourceAddress, IPAddress DestinationAddress) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = IPv6Fields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.PayloadLength = 0;
            this.TimeToLive = base.DefaultTimeToLive;
            this.SourceAddress = SourceAddress;
            this.DestinationAddress = DestinationAddress;
            this.Version = ipVersion;
        }

        public IPv6Packet(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public IPv6Packet(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, 40);
            base.header.Length = (Bytes.Length - Offset) - this.PayloadLength;
            base.payloadPacketOrData = IpPacket.ParseEncapsulatedBytes(base.header, this.NextHeader, Timeval, this);
        }

        internal override byte[] AttachPseudoIPHeader(byte[] origHeader)
        {
            MemoryStream output = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(output);
            writer.Write(base.header.Bytes, base.header.Offset + IPv6Fields.SourceAddressPosition, IPv6Fields.AddressLength);
            writer.Write(base.header.Bytes, base.header.Offset + IPv6Fields.DestinationAddressPosition, IPv6Fields.AddressLength);
            writer.Write((uint) IPAddress.HostToNetworkOrder(origHeader.Length));
            writer.Write((byte) 0);
            writer.Write((byte) 0);
            writer.Write((byte) 0);
            writer.Write((byte) this.NextHeader);
            byte[] sourceArray = output.ToArray();
            int num = sourceArray.Length + origHeader.Length;
            bool flag = (origHeader.Length % 2) != 0;
            if (flag)
            {
                num++;
            }
            byte[] destinationArray = new byte[num];
            Array.Copy(sourceArray, 0, destinationArray, 0, sourceArray.Length);
            Array.Copy(origHeader, 0, destinationArray, sourceArray.Length, origHeader.Length);
            if (flag)
            {
                destinationArray[destinationArray.Length - 1] = 0;
            }
            return destinationArray;
        }

        public static IPv6Packet RandomPacket()
        {
            IPAddress iPAddress = RandomUtils.GetIPAddress(ipVersion);
            return new IPv6Packet(iPAddress, RandomUtils.GetIPAddress(ipVersion));
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("IPv6Packet");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.SourceAddress + " -> " + this.DestinationAddress);
            builder.Append(" next header=" + this.NextHeader);
            builder.Append(']');
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string[] textArray1 = new string[] { base.ToString(), "\r\nIPv6 Packet [\r\n\tIPv6 Source Address: ", this.SourceAddress.ToString(), ", \r\n\tIPv6 Destination Address: ", this.DestinationAddress.ToString(), "\r\n]" };
            return string.Concat(textArray1);
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
                return IpPacket.GetIPAddress(AddressFamily.InterNetworkV6, base.header.Offset + IPv6Fields.DestinationAddressPosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + IPv6Fields.DestinationAddressPosition, addressBytes.Length);
            }
        }

        public virtual int FlowLabel
        {
            get
            {
                return (this.VersionTrafficClassFlowLabel & 0xfffff);
            }
            set
            {
                uint num = (uint) ((this.VersionTrafficClassFlowLabel & -1048576) | (value & 0xfffff));
                this.VersionTrafficClassFlowLabel = (int) num;
            }
        }

        public override int HeaderLength
        {
            get
            {
                return (IPv6Fields.HeaderLength / 4);
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int HopLimit
        {
            get
            {
                return base.header.Bytes[base.header.Offset + IPv6Fields.HopLimitPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IPv6Fields.HopLimitPosition] = (byte) value;
            }
        }

        public override IPProtocolType NextHeader
        {
            get
            {
                return (IPProtocolType) base.header.Bytes[base.header.Offset + IPv6Fields.NextHeaderPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + IPv6Fields.NextHeaderPosition] = (byte) value;
            }
        }

        public override ushort PayloadLength
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + IPv6Fields.PayloadLengthPosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + IPv6Fields.PayloadLengthPosition);
            }
        }

        public override IPProtocolType Protocol
        {
            get
            {
                return this.NextHeader;
            }
            set
            {
                this.NextHeader = value;
            }
        }

        public override IPAddress SourceAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetworkV6, base.header.Offset + IPv6Fields.SourceAddressPosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + IPv6Fields.SourceAddressPosition, addressBytes.Length);
            }
        }

        public override int TimeToLive
        {
            get
            {
                return this.HopLimit;
            }
            set
            {
                this.HopLimit = value;
            }
        }

        public override int TotalLength
        {
            get
            {
                return (this.PayloadLength + (this.HeaderLength * 4));
            }
            set
            {
                this.PayloadLength = (ushort) (value - (this.HeaderLength * 4));
            }
        }

        public virtual int TrafficClass
        {
            get
            {
                return ((this.VersionTrafficClassFlowLabel >> 20) & 0xff);
            }
            set
            {
                uint num = (uint) ((this.VersionTrafficClassFlowLabel & -267386881) | ((value << 20) & 0xff00000));
                this.VersionTrafficClassFlowLabel = (int) num;
            }
        }

        public override IpVersion Version
        {
            get
            {
                return (((IpVersion) (this.VersionTrafficClassFlowLabel >> 0x1c)) & ((IpVersion) 15));
            }
            set
            {
                int num = (int) value;
                uint num2 = (uint)(((ulong)(this.VersionTrafficClassFlowLabel & 0xfffffff)) | (ulong)((num << 0x1c) & 0xf0000000L));
                this.VersionTrafficClassFlowLabel = (int) num2;
            }
        }

        private int VersionTrafficClassFlowLabel
        {
            get
            {
                return EndianBitConverter.Big.ToInt32(base.header.Bytes, base.header.Offset + IPv6Fields.VersionTrafficClassFlowLabelPosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + IPv6Fields.VersionTrafficClassFlowLabelPosition);
            }
        }
    }
}

