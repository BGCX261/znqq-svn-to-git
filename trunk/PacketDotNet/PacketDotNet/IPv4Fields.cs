namespace PacketDotNet
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct IPv4Fields
    {
        public static readonly int VersionAndHeaderLengthLength;
        public static readonly int DifferentiatedServicesLength;
        public static readonly int TotalLengthLength;
        public static readonly int IdLength;
        public static readonly int FragmentOffsetAndFlagsLength;
        public static readonly int TtlLength;
        public static readonly int ProtocolLength;
        public static readonly int ChecksumLength;
        public static readonly int VersionAndHeaderLengthPosition;
        public static readonly int DifferentiatedServicesPosition;
        public static readonly int TotalLengthPosition;
        public static readonly int IdPosition;
        public static readonly int FragmentOffsetAndFlagsPosition;
        public static readonly int TtlPosition;
        public static readonly int ProtocolPosition;
        public static readonly int ChecksumPosition;
        public static readonly int SourcePosition;
        public static readonly int DestinationPosition;
        public static readonly int HeaderLength;
        public static readonly int AddressLength;
        static IPv4Fields()
        {
            VersionAndHeaderLengthLength = 1;
            DifferentiatedServicesLength = 1;
            TotalLengthLength = 2;
            IdLength = 2;
            FragmentOffsetAndFlagsLength = 2;
            TtlLength = 1;
            ProtocolLength = 1;
            ChecksumLength = 2;
            VersionAndHeaderLengthPosition = 0;
            AddressLength = 4;
            DifferentiatedServicesPosition = VersionAndHeaderLengthPosition + VersionAndHeaderLengthLength;
            TotalLengthPosition = DifferentiatedServicesPosition + DifferentiatedServicesLength;
            IdPosition = TotalLengthPosition + TotalLengthLength;
            FragmentOffsetAndFlagsPosition = IdPosition + IdLength;
            TtlPosition = FragmentOffsetAndFlagsPosition + FragmentOffsetAndFlagsLength;
            ProtocolPosition = TtlPosition + TtlLength;
            ChecksumPosition = ProtocolPosition + ProtocolLength;
            SourcePosition = ChecksumPosition + ChecksumLength;
            DestinationPosition = SourcePosition + AddressLength;
            HeaderLength = DestinationPosition + AddressLength;
        }
    }
}

