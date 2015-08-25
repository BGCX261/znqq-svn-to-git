namespace PacketDotNet
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct UdpFields
    {
        public static readonly int PortLength;
        public static readonly int HeaderLengthLength;
        public static readonly int ChecksumLength;
        public static readonly int SourcePortPosition;
        public static readonly int DestinationPortPosition;
        public static readonly int HeaderLengthPosition;
        public static readonly int ChecksumPosition;
        public static readonly int HeaderLength;
        static UdpFields()
        {
            PortLength = 2;
            HeaderLengthLength = 2;
            ChecksumLength = 2;
            SourcePortPosition = 0;
            DestinationPortPosition = PortLength;
            HeaderLengthPosition = DestinationPortPosition + PortLength;
            ChecksumPosition = HeaderLengthPosition + HeaderLengthLength;
            HeaderLength = ChecksumPosition + ChecksumLength;
        }
    }
}

