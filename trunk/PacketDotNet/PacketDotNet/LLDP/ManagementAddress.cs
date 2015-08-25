namespace PacketDotNet.LLDP
{
    using MiscUtil.Conversion;
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class ManagementAddress : TLV
    {
        private const int InterfaceNumberLength = 4;
        private const int InterfaceNumberSubTypeLength = 1;
        private static readonly ILogInactive log;
        private const int maxObjectIdentifierLength = 0x80;
        private const int MgmtAddressLengthLength = 1;
        private const int ObjectIdentifierLengthLength = 1;

        public ManagementAddress(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public ManagementAddress(NetworkAddress managementAddress, InterfaceNumbering interfaceSubType, uint ifNumber, string oid)
        {
            int length = 9;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            this.AddressLength = 0;
            this.ObjIdLength = 0;
            base.Type = TLVTypes.ManagementAddress;
            this.MgmtAddress = managementAddress;
            this.InterfaceSubType = interfaceSubType;
            this.InterfaceNumber = ifNumber;
            this.ObjectIdentifier = oid;
        }

        public override string ToString()
        {
            object[] args = new object[] { this.AddressLength, this.AddressSubType, this.MgmtAddress, this.InterfaceSubType, this.InterfaceNumber, this.ObjIdLength, this.ObjectIdentifier };
            return string.Format("[ManagementAddress: AddressLength={0}, AddressSubType={1}, MgmtAddress={2}, InterfaceSubType={3}, InterfaceNumber={4}, ObjIdLength={5}, ObjectIdentifier={6}]", args);
        }

        public int AddressLength
        {
            get
            {
                return base.tlvData.Bytes[base.ValueOffset];
            }
            internal set
            {
                base.tlvData.Bytes[base.ValueOffset] = (byte) value;
            }
        }

        public AddressFamily AddressSubType
        {
            get
            {
                return this.MgmtAddress.AddressFamily;
            }
        }

        public uint InterfaceNumber
        {
            get
            {
                return EndianBitConverter.Big.ToUInt32(base.tlvData.Bytes, this.InterfaceNumberOffset);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.tlvData.Bytes, this.InterfaceNumberOffset);
            }
        }

        private int InterfaceNumberOffset
        {
            get
            {
                return (((base.ValueOffset + 1) + this.AddressLength) + 1);
            }
        }

        public InterfaceNumbering InterfaceSubType
        {
            get
            {
                return (InterfaceNumbering) base.tlvData.Bytes[(base.ValueOffset + 1) + this.MgmtAddress.Length];
            }
            set
            {
                base.tlvData.Bytes[(base.ValueOffset + 1) + this.MgmtAddress.Length] = (byte) value;
            }
        }

        public NetworkAddress MgmtAddress
        {
            get
            {
                return new NetworkAddress(base.tlvData.Bytes, base.ValueOffset + 1, this.AddressLength);
            }
            set
            {
                int length = value.Length;
                byte[] bytes = value.Bytes;
                if (this.AddressLength != length)
                {
                    int num2 = ((((3 + length) + 1) + 4) + 1) + this.ObjIdLength;
                    byte[] destinationArray = new byte[num2];
                    int num3 = 3;
                    int sourceIndex = (base.ValueOffset + 1) + this.AddressLength;
                    int destinationIndex = 3 + value.Length;
                    int num6 = 6 + this.ObjIdLength;
                    Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, num3);
                    Array.Copy(base.tlvData.Bytes, sourceIndex, destinationArray, destinationIndex, num6);
                    int offset = 0;
                    base.tlvData = new ByteArraySegment(destinationArray, offset, num2);
                    this.AddressLength = length;
                }
                Array.Copy(bytes, 0, base.tlvData.Bytes, base.ValueOffset + 1, length);
            }
        }

        public string ObjectIdentifier
        {
            get
            {
                return Encoding.UTF8.GetString(base.tlvData.Bytes, this.ObjectIdentifierOffset, this.ObjIdLength);
            }
            set
            {
                byte[] bytes = Encoding.UTF8.GetBytes(value);
                if (bytes.Length > 0x80)
                {
                    throw new ArgumentOutOfRangeException("ObjectIdentifier", "length > maxObjectIdentifierLength of " + 0x80);
                }
                if (this.ObjIdLength != bytes.Length)
                {
                    int length = (((3 + this.AddressLength) + 1) + 4) + 1;
                    int num2 = length + bytes.Length;
                    byte[] destinationArray = new byte[num2];
                    Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, length);
                    int offset = 0;
                    base.tlvData = new ByteArraySegment(destinationArray, offset, num2);
                    this.ObjIdLength = (byte) value.Length;
                }
                Array.Copy(bytes, 0, base.tlvData.Bytes, this.ObjectIdentifierOffset, bytes.Length);
            }
        }

        private int ObjectIdentifierOffset
        {
            get
            {
                return (this.ObjIdLengthOffset + 1);
            }
        }

        public byte ObjIdLength
        {
            get
            {
                return base.tlvData.Bytes[this.ObjIdLengthOffset];
            }
            internal set
            {
                base.tlvData.Bytes[this.ObjIdLengthOffset] = value;
            }
        }

        private int ObjIdLengthOffset
        {
            get
            {
                return (this.InterfaceNumberOffset + 4);
            }
        }
    }
}

