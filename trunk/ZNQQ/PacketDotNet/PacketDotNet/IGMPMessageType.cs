namespace PacketDotNet
{
    using System;

    public enum IGMPMessageType : byte
    {
        LeaveGroup = 0x17,
        MembershipQuery = 0x11,
        MembershipReportIGMPv1 = 0x12,
        MembershipReportIGMPv2 = 0x16,
        MembershipReportIGMPv3 = 0x22
    }
}

