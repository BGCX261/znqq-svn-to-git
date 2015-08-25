namespace PacketDotNet
{
    using System;

    public class EthernetFields
    {
        public static readonly int DestinationMacPosition = 0;
        public static readonly int HeaderLength = (TypePosition + TypeLength);
        public static readonly int MacAddressLength = 6;
        public static readonly int SourceMacPosition = MacAddressLength;
        public static readonly int TypeLength = 2;
        public static readonly int TypePosition = (MacAddressLength * 2);
    }
}

