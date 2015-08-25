namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;
    using System.Net.NetworkInformation;

    public class PortID : TLV
    {
        private static readonly ILogInactive log;
        private const int SubTypeLength = 1;

        public PortID(NetworkAddress networkAddress)
        {
            int length = 3;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            base.Type = TLVTypes.PortID;
            this.SubType = PortSubTypes.NetworkAddress;
            this.SubTypeValue = networkAddress;
        }

        public PortID(PortSubTypes subType, object subTypeValue)
        {
            this.EmptyTLVDataInit();
            base.Type = TLVTypes.PortID;
            this.SubType = subType;
            this.SubTypeValue = subTypeValue;
        }

        public PortID(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        private void EmptyTLVDataInit()
        {
            int length = 3;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
        }

        private NetworkAddress GetNetworkAddress(AddressFamily addressFamily)
        {
            if (this.SubType != PortSubTypes.NetworkAddress)
            {
                throw new ArgumentOutOfRangeException("SubType != PortSubTypes.NetworkAddress");
            }
            return new NetworkAddress(base.tlvData.Bytes, this.DataOffset, this.DataLength);
        }

        private object GetSubTypeValue()
        {
            byte[] buffer;
            switch (this.SubType)
            {
                case PortSubTypes.InterfaceAlias:
                case PortSubTypes.PortComponent:
                case PortSubTypes.InterfaceName:
                case PortSubTypes.AgentCircuitID:
                case PortSubTypes.LocallyAssigned:
                    buffer = new byte[this.DataLength];
                    Array.Copy(base.tlvData.Bytes, this.DataOffset, buffer, 0, this.DataLength);
                    return buffer;

                case PortSubTypes.MACAddress:
                    buffer = new byte[this.DataLength];
                    Array.Copy(base.tlvData.Bytes, this.DataOffset, buffer, 0, this.DataLength);
                    return new PhysicalAddress(buffer);

                case PortSubTypes.NetworkAddress:
                {
                    AddressFamily addressFamily = (AddressFamily) base.tlvData.Bytes[this.DataLength];
                    return this.GetNetworkAddress(addressFamily);
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        private void SetSubTypeValue(object subTypeValue)
        {
            switch (this.SubType)
            {
                case PortSubTypes.InterfaceAlias:
                case PortSubTypes.PortComponent:
                case PortSubTypes.InterfaceName:
                case PortSubTypes.AgentCircuitID:
                case PortSubTypes.LocallyAssigned:
                    this.SetSubTypeValue((byte[]) subTypeValue);
                    break;

                case PortSubTypes.MACAddress:
                    this.SetSubTypeValue(((PhysicalAddress) subTypeValue).GetAddressBytes());
                    break;

                case PortSubTypes.NetworkAddress:
                    this.SetSubTypeValue(((NetworkAddress) subTypeValue).Bytes);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSubTypeValue(byte[] val)
        {
            int num = base.Length - 1;
            if (num != val.Length)
            {
                int length = 3;
                int num3 = length + val.Length;
                byte[] destinationArray = new byte[num3];
                Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, length);
                int offset = 0;
                base.tlvData = new ByteArraySegment(destinationArray, offset, num3);
            }
            Array.Copy(val, 0, base.tlvData.Bytes, base.ValueOffset + 1, val.Length);
        }

        public override string ToString()
        {
            return string.Format("[PortID: SubType={0}, SubTypeValue={1}]", this.SubType, this.SubTypeValue);
        }

        private int DataLength
        {
            get
            {
                return (base.Length - 1);
            }
        }

        private int DataOffset
        {
            get
            {
                return (base.ValueOffset + 1);
            }
        }

        public PortSubTypes SubType
        {
            get
            {
                return (PortSubTypes) base.tlvData.Bytes[base.tlvData.Offset + 2];
            }
            set
            {
                base.tlvData.Bytes[base.tlvData.Offset + 2] = (byte) value;
            }
        }

        public object SubTypeValue
        {
            get
            {
                return this.GetSubTypeValue();
            }
            set
            {
                this.SetSubTypeValue(value);
            }
        }
    }
}

