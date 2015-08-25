namespace PacketDotNet
{
    using System;

    public class LinuxSLLFields
    {
        public static readonly int EthernetProtocolTypeLength = 2;
        public static readonly int EthernetProtocolTypePosition = (LinkLayerAddressPosition + LinkLayerAddressMaximumLength);
        public static readonly int LinkLayerAddressLengthLength = 2;
        public static readonly int LinkLayerAddressLengthPosition = (LinkLayerAddressTypePosition + LinkLayerAddressTypeLength);
        public static readonly int LinkLayerAddressMaximumLength = 8;
        public static readonly int LinkLayerAddressPosition = (LinkLayerAddressLengthPosition + LinkLayerAddressLengthLength);
        public static readonly int LinkLayerAddressTypeLength = 2;
        public static readonly int LinkLayerAddressTypePosition = (PacketTypePosition + PacketTypeLength);
        public static readonly int PacketTypeLength = 2;
        public static readonly int PacketTypePosition = 0;
        public static readonly int SLLHeaderLength = 0x10;
    }
}

