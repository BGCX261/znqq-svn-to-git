namespace PacketDotNet
{
    using System;

    public enum IPProtocolType
    {
        AH = 0x33,
        COMP = 0x6c,
        DSTOPTS = 60,
        EGP = 8,
        ENCAP = 0x62,
        ESP = 50,
        FRAGMENT = 0x2c,
        GRE = 0x2f,
        HOPOPTS = 0,
        ICMP = 1,
        ICMPV6 = 0x3a,
        IDP = 0x16,
        IGMP = 2,
        INVALID = -1,
        IP = 0,
        IPIP = 4,
        IPV6 = 0x29,
        MASK = 0xff,
        MTP = 0x5c,
        NONE = 0x3b,
        PIM = 0x67,
        PUP = 12,
        RAW = 0xff,
        ROUTING = 0x2b,
        RSVP = 0x2e,
        TCP = 6,
        TP = 0x1d,
        UDP = 0x11
    }
}

