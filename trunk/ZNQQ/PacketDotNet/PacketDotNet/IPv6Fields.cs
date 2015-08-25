namespace PacketDotNet
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct IPv6Fields
    {
        public static readonly int VersionTrafficClassFlowLabelLength;
        public static readonly int PayloadLengthLength;
        public static readonly int NextHeaderLength;
        public static readonly int HopLimitLength;
        public static readonly int AddressLength;
        public static readonly int VersionTrafficClassFlowLabelPosition;
        public static readonly int PayloadLengthPosition;
        public static readonly int NextHeaderPosition;
        public static readonly int HopLimitPosition;
        public static readonly int SourceAddressPosition;
        public static readonly int DestinationAddressPosition;
        public static readonly int HeaderLength;
        static IPv6Fields()
        {
            VersionTrafficClassFlowLabelLength = 4;
            PayloadLengthLength = 2;
            NextHeaderLength = 1;
            HopLimitLength = 1;
            AddressLength = 0x10;
            VersionTrafficClassFlowLabelPosition = 0;
            PayloadLengthPosition = VersionTrafficClassFlowLabelPosition + VersionTrafficClassFlowLabelLength;
            NextHeaderPosition = PayloadLengthPosition + PayloadLengthLength;
            HopLimitPosition = NextHeaderPosition + NextHeaderLength;
            SourceAddressPosition = HopLimitPosition + HopLimitLength;
            DestinationAddressPosition = SourceAddressPosition + AddressLength;
            HeaderLength = DestinationAddressPosition + AddressLength;
        }
    }
}

