namespace PacketDotNet
{
    using System;

    public class ARPFields
    {
        public static readonly int AddressLengthLength = 1;
        public static readonly int AddressTypeLength = 2;
        public static readonly int EthernetProtocolType = 1;
        public static readonly int HardwareAddressLengthPosition = (ProtocolAddressTypePosition + AddressTypeLength);
        public static readonly int HardwareAddressTypePosition = 0;
        public static readonly int HeaderLength = (TargetProtocolAddressPosition + IPv4Fields.AddressLength);
        public static readonly int IPv4ProtocolType = 0x800;
        public static readonly int OperationLength = 2;
        public static readonly int OperationPosition = (ProtocolAddressLengthPosition + AddressLengthLength);
        public static readonly int ProtocolAddressLengthPosition = (HardwareAddressLengthPosition + AddressLengthLength);
        public static readonly int ProtocolAddressTypePosition = (HardwareAddressTypePosition + AddressTypeLength);
        public static readonly int SenderHardwareAddressPosition = (OperationPosition + OperationLength);
        public static readonly int SenderProtocolAddressPosition = (SenderHardwareAddressPosition + EthernetFields.MacAddressLength);
        public static readonly int TargetHardwareAddressPosition = (SenderProtocolAddressPosition + IPv4Fields.AddressLength);
        public static readonly int TargetProtocolAddressPosition = (TargetHardwareAddressPosition + EthernetFields.MacAddressLength);
    }
}

