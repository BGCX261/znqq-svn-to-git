namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;
    using System.Net.NetworkInformation;
    using System.Text;

    public class ChassisID : TLV
    {
        private static readonly ILogInactive log;
        private const int SubTypeLength = 1;

        public ChassisID(PhysicalAddress MACAddress)
        {
            this.EmptyTLVDataInit();
            base.Type = TLVTypes.ChassisID;
            this.SubType = ChassisSubTypes.MACAddress;
            this.SubTypeValue = MACAddress;
        }

        public ChassisID(string InterfaceName)
        {
            this.EmptyTLVDataInit();
            base.Type = TLVTypes.ChassisID;
            this.SubType = ChassisSubTypes.InterfaceName;
            this.SetSubTypeValue(InterfaceName);
        }

        public ChassisID(ChassisSubTypes subType, object subTypeValue)
        {
            this.EmptyTLVDataInit();
            base.Type = TLVTypes.ChassisID;
            this.SubType = subType;
            this.SubTypeValue = subTypeValue;
        }

        public ChassisID(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        private void EmptyTLVDataInit()
        {
            int length = 3;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
        }

        private object GetSubTypeValue()
        {
            byte[] buffer;
            int sourceIndex = base.ValueOffset + 1;
            int length = base.Length - 1;
            switch (this.SubType)
            {
                case ChassisSubTypes.ChassisComponent:
                case ChassisSubTypes.InterfaceAlias:
                case ChassisSubTypes.PortComponent:
                case ChassisSubTypes.LocallyAssigned:
                    buffer = new byte[length];
                    Array.Copy(base.tlvData.Bytes, sourceIndex, buffer, 0, length);
                    return buffer;

                case ChassisSubTypes.MACAddress:
                    buffer = new byte[length];
                    Array.Copy(base.tlvData.Bytes, sourceIndex, buffer, 0, length);
                    return new PhysicalAddress(buffer);

                case ChassisSubTypes.NetworkAddress:
                    return new PacketDotNet.LLDP.NetworkAddress(base.tlvData.Bytes, sourceIndex, length);

                case ChassisSubTypes.InterfaceName:
                    return Encoding.ASCII.GetString(base.tlvData.Bytes, sourceIndex, length);
            }
            throw new ArgumentOutOfRangeException();
        }

        private void SetSubTypeValue(object val)
        {
            byte[] bytes;
            switch (this.SubType)
            {
                case ChassisSubTypes.ChassisComponent:
                case ChassisSubTypes.InterfaceAlias:
                case ChassisSubTypes.PortComponent:
                case ChassisSubTypes.LocallyAssigned:
                    if (!(val is byte[]))
                    {
                        throw new ArgumentOutOfRangeException("expected byte[] for type");
                    }
                    bytes = (byte[]) val;
                    this.SetSubTypeValue(bytes);
                    break;

                case ChassisSubTypes.MACAddress:
                    if (!(val is PhysicalAddress))
                    {
                        throw new ArgumentOutOfRangeException("expected PhysicalAddress for MACAddress");
                    }
                    this.SetSubTypeValue(((PhysicalAddress) val).GetAddressBytes());
                    break;

                case ChassisSubTypes.NetworkAddress:
                    if (!(val is PacketDotNet.LLDP.NetworkAddress))
                    {
                        throw new ArgumentOutOfRangeException("expected NetworkAddress instance for NetworkAddress");
                    }
                    bytes = ((PacketDotNet.LLDP.NetworkAddress) val).Bytes;
                    this.SetSubTypeValue(bytes);
                    break;

                case ChassisSubTypes.InterfaceName:
                {
                    if (!(val is string))
                    {
                        throw new ArgumentOutOfRangeException("expected string for InterfaceName");
                    }
                    string s = (string) val;
                    bytes = Encoding.ASCII.GetBytes(s);
                    this.SetSubTypeValue(bytes);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSubTypeValue(byte[] subTypeValue)
        {
            if (subTypeValue.Length != base.Length)
            {
                int length = 3;
                byte[] destinationArray = new byte[length + subTypeValue.Length];
                Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, length);
                base.tlvData = new ByteArraySegment(destinationArray, 0, destinationArray.Length);
            }
            Array.Copy(subTypeValue, 0, base.tlvData.Bytes, base.ValueOffset + 1, subTypeValue.Length);
        }

        public override string ToString()
        {
            return string.Format("[ChassisID: SubType={0}, SubTypeValue={1}]", this.SubType, this.SubTypeValue);
        }

        public byte[] ChassisComponent
        {
            get
            {
                return (byte[]) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.ChassisComponent;
                this.SetSubTypeValue(value);
            }
        }

        public byte[] InterfaceAlias
        {
            get
            {
                return (byte[]) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.InterfaceAlias;
                this.SetSubTypeValue(value);
            }
        }

        public string InterfaceName
        {
            get
            {
                return (string) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.InterfaceName;
                this.SetSubTypeValue(value);
            }
        }

        public PhysicalAddress MACAddress
        {
            get
            {
                return (PhysicalAddress) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.MACAddress;
                this.SetSubTypeValue(value);
            }
        }

        public PacketDotNet.LLDP.NetworkAddress NetworkAddress
        {
            get
            {
                return (PacketDotNet.LLDP.NetworkAddress) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.NetworkAddress;
                this.SetSubTypeValue(value);
            }
        }

        public byte[] PortComponent
        {
            get
            {
                return (byte[]) this.GetSubTypeValue();
            }
            set
            {
                this.SubType = ChassisSubTypes.PortComponent;
                this.SetSubTypeValue(value);
            }
        }

        public ChassisSubTypes SubType
        {
            get
            {
                return (ChassisSubTypes) base.tlvData.Bytes[base.ValueOffset];
            }
            set
            {
                base.tlvData.Bytes[base.ValueOffset] = (byte) value;
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

