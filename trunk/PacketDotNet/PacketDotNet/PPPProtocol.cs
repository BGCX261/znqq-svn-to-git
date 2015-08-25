namespace PacketDotNet
{
    using System;

    public enum PPPProtocol : ushort
    {
        IPv4 = 0x21,
        IPv6 = 0x57,
        Padding = 1
    }
}

