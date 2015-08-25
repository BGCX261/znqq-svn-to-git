namespace PacketDotNet
{
    using System;

    public class IGMPv2Fields
    {
        public static readonly int ChecksumLength = 2;
        public static readonly int ChecksumPosition = (MaxResponseTimePosition + MaxResponseTimeLength);
        public static readonly int GroupAddressLength = IPv4Fields.AddressLength;
        public static readonly int GroupAddressPosition = (ChecksumPosition + ChecksumLength);
        public static readonly int HeaderLength = (GroupAddressPosition + GroupAddressLength);
        public static readonly int MaxResponseTimeLength = 1;
        public static readonly int MaxResponseTimePosition = (TypePosition + TypeLength);
        public static readonly int TypeLength = 1;
        public static readonly int TypePosition = 0;
    }
}

