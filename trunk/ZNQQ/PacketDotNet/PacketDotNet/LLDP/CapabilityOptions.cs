namespace PacketDotNet.LLDP
{
    using System;

    [Flags]
    public enum CapabilityOptions
    {
        Bridge = 4,
        DocsisCableDevice = 0x40,
        Other = 1,
        Repeater = 2,
        Router = 0x10,
        StationOnly = 0x80,
        Telephone = 0x20,
        WLanAP = 8
    }
}

