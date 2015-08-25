namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class LinuxSLLPacket : InternetLinkLayerPacket
    {
        public LinuxSLLPacket(byte[] bytes, int offset) : this(bytes, offset, new PosixTimeval())
        {
        }

        public LinuxSLLPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, LinuxSLLFields.SLLHeaderLength);
            base.payloadPacketOrData = EthernetPacket.ParseEncapsulatedBytes(base.header, this.EthernetProtocolType, Timeval);
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            object[] args = new object[] { this.Type, this.LinkLayerAddressType, this.LinkLayerAddressLength, this.LinkLayerAddress, this.EthernetProtocolType };
            builder.AppendFormat("[LinuxSLLPacket: Type={0}, LinkLayerAddressType={1}, LinkLayerAddressLength={2}, LinkLayerHeader={3}, EthernetProtocolType={4}]", args);
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

        public EthernetPacketType EthernetProtocolType
        {
            get
            {
                return (EthernetPacketType) ((ushort) EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + LinuxSLLFields.EthernetProtocolTypePosition));
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + LinuxSLLFields.EthernetProtocolTypePosition);
            }
        }

        public byte[] LinkLayerAddress
        {
            get
            {
                int linkLayerAddressLength = this.LinkLayerAddressLength;
                byte[] destinationArray = new byte[linkLayerAddressLength];
                Array.Copy(base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressPosition, destinationArray, 0, linkLayerAddressLength);
                return destinationArray;
            }
            set
            {
                this.LinkLayerAddressLength = value.Length;
                Array.Copy(value, 0, base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressPosition, value.Length);
            }
        }

        public int LinkLayerAddressLength
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressLengthPosition);
            }
            set
            {
                if ((value < 0) || (value > 8))
                {
                    throw new InvalidOperationException("value of " + value + " out of range of 0 to 8");
                }
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressLengthPosition);
            }
        }

        public int LinkLayerAddressType
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressTypePosition);
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + LinuxSLLFields.LinkLayerAddressTypePosition);
            }
        }

        public LinuxSLLType Type
        {
            get
            {
                return (LinuxSLLType) EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + LinuxSLLFields.PacketTypePosition);
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + LinuxSLLFields.PacketTypePosition);
            }
        }
    }
}

