namespace PacketDotNet
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TcpFields
    {
        public static readonly int TCP_CWR_MASK;
        public static readonly int TCP_ECN_MASK;
        public static readonly int TCP_URG_MASK;
        public static readonly int TCP_ACK_MASK;
        public static readonly int TCP_PSH_MASK;
        public static readonly int TCP_RST_MASK;
        public static readonly int TCP_SYN_MASK;
        public static readonly int TCP_FIN_MASK;
        public static readonly int PortLength;
        public static readonly int SequenceNumberLength;
        public static readonly int AckNumberLength;
        public static readonly int DataOffsetLength;
        public static readonly int FlagsLength;
        public static readonly int WindowSizeLength;
        public static readonly int ChecksumLength;
        public static readonly int UrgentPointerLength;
        public static readonly int SourcePortPosition;
        public static readonly int DestinationPortPosition;
        public static readonly int SequenceNumberPosition;
        public static readonly int AckNumberPosition;
        public static readonly int DataOffsetPosition;
        public static readonly int FlagsPosition;
        public static readonly int WindowSizePosition;
        public static readonly int ChecksumPosition;
        public static readonly int UrgentPointerPosition;
        public static readonly int HeaderLength;
        static TcpFields()
        {
            TCP_CWR_MASK = 0x80;
            TCP_ECN_MASK = 0x40;
            TCP_URG_MASK = 0x20;
            TCP_ACK_MASK = 0x10;
            TCP_PSH_MASK = 8;
            TCP_RST_MASK = 4;
            TCP_SYN_MASK = 2;
            TCP_FIN_MASK = 1;
            PortLength = 2;
            SequenceNumberLength = 4;
            AckNumberLength = 4;
            DataOffsetLength = 1;
            FlagsLength = 1;
            WindowSizeLength = 2;
            ChecksumLength = 2;
            UrgentPointerLength = 2;
            SourcePortPosition = 0;
            DestinationPortPosition = PortLength;
            SequenceNumberPosition = DestinationPortPosition + PortLength;
            AckNumberPosition = SequenceNumberPosition + SequenceNumberLength;
            DataOffsetPosition = AckNumberPosition + AckNumberLength;
            FlagsPosition = DataOffsetPosition + DataOffsetLength;
            WindowSizePosition = FlagsPosition + FlagsLength;
            ChecksumPosition = WindowSizePosition + WindowSizeLength;
            UrgentPointerPosition = ChecksumPosition + ChecksumLength;
            HeaderLength = UrgentPointerPosition + UrgentPointerLength;
        }
    }
}

