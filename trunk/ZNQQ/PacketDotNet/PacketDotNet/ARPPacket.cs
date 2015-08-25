namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text;

    public class ARPPacket : InternetLinkLayerPacket
    {
        private static readonly ILogInactive log;

        public ARPPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public ARPPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, ARPFields.HeaderLength);
        }

        public ARPPacket(ARPOperation Operation, PhysicalAddress TargetHardwareAddress, IPAddress TargetProtocolAddress, PhysicalAddress SenderHardwareAddress, IPAddress SenderProtocolAddress) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = ARPFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.Operation = Operation;
            this.TargetHardwareAddress = TargetHardwareAddress;
            this.TargetProtocolAddress = TargetProtocolAddress;
            this.SenderHardwareAddress = SenderHardwareAddress;
            this.SenderProtocolAddress = SenderProtocolAddress;
            this.HardwareAddressType = LinkLayers.Ethernet;
            this.HardwareAddressLength = EthernetFields.MacAddressLength;
            this.ProtocolAddressType = EthernetPacketType.IpV4;
            this.ProtocolAddressLength = IPv4Fields.AddressLength;
        }

        public static ARPPacket GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is ARPPacket)
                {
                    return (ARPPacket) innerPayload;
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
            builder.Append("ARPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(this.Operation);
            builder.Append(' ');
            builder.Append(this.SenderHardwareAddress + " -> " + this.TargetHardwareAddress);
            builder.Append(", ");
            builder.Append(this.SenderProtocolAddress + " -> " + this.TargetProtocolAddress);
            builder.Append(']');
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.Purple;
            }
        }

        public virtual int HardwareAddressLength
        {
            get
            {
                return base.header.Bytes[base.header.Offset + ARPFields.HardwareAddressLengthPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + ARPFields.HardwareAddressLengthPosition] = (byte) value;
            }
        }

        public virtual LinkLayers HardwareAddressType
        {
            get
            {
                return (LinkLayers) EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ARPFields.HardwareAddressTypePosition);
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ARPFields.HardwareAddressTypePosition);
            }
        }

        public virtual ARPOperation Operation
        {
            get
            {
                return (ARPOperation) ((ushort) EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + ARPFields.OperationPosition));
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ARPFields.OperationPosition);
            }
        }

        public virtual int ProtocolAddressLength
        {
            get
            {
                return base.header.Bytes[base.header.Offset + ARPFields.ProtocolAddressLengthPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + ARPFields.ProtocolAddressLengthPosition] = (byte) value;
            }
        }

        public virtual EthernetPacketType ProtocolAddressType
        {
            get
            {
                return (EthernetPacketType) EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + ARPFields.ProtocolAddressTypePosition);
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + ARPFields.ProtocolAddressTypePosition);
            }
        }

        public virtual PhysicalAddress SenderHardwareAddress
        {
            get
            {
                byte[] destinationArray = new byte[this.HardwareAddressLength];
                Array.Copy(base.header.Bytes, base.header.Offset + ARPFields.SenderHardwareAddressPosition, destinationArray, 0, destinationArray.Length);
                return new PhysicalAddress(destinationArray);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                if (addressBytes.Length != EthernetFields.MacAddressLength)
                {
                    object[] objArray1 = new object[] { "expected physical address length of ", EthernetFields.MacAddressLength, " but it was ", addressBytes.Length };
                    throw new InvalidOperationException(string.Concat(objArray1));
                }
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + ARPFields.SenderHardwareAddressPosition, addressBytes.Length);
            }
        }

        public virtual IPAddress SenderProtocolAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetwork, base.header.Offset + ARPFields.SenderProtocolAddressPosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + ARPFields.SenderProtocolAddressPosition, addressBytes.Length);
            }
        }

        public virtual PhysicalAddress TargetHardwareAddress
        {
            get
            {
                byte[] destinationArray = new byte[this.HardwareAddressLength];
                Array.Copy(base.header.Bytes, base.header.Offset + ARPFields.TargetHardwareAddressPosition, destinationArray, 0, destinationArray.Length);
                return new PhysicalAddress(destinationArray);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                if (addressBytes.Length != EthernetFields.MacAddressLength)
                {
                    object[] objArray1 = new object[] { "expected physical address length of ", EthernetFields.MacAddressLength, " but it was ", addressBytes.Length };
                    throw new InvalidOperationException(string.Concat(objArray1));
                }
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + ARPFields.TargetHardwareAddressPosition, addressBytes.Length);
            }
        }

        public virtual IPAddress TargetProtocolAddress
        {
            get
            {
                return IpPacket.GetIPAddress(AddressFamily.InterNetwork, base.header.Offset + ARPFields.TargetProtocolAddressPosition, base.header.Bytes);
            }
            set
            {
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, base.header.Bytes, base.header.Offset + ARPFields.TargetProtocolAddressPosition, addressBytes.Length);
            }
        }
    }
}

