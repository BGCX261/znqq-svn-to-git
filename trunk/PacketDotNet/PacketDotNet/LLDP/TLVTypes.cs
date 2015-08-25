namespace PacketDotNet.LLDP
{
    using System;

    public enum TLVTypes
    {
        ChassisID = 1,
        EndOfLLDPU = 0,
        ManagementAddress = 8,
        OrganizationSpecific = 0x7f,
        PortDescription = 4,
        PortID = 2,
        SystemCapabilities = 7,
        SystemDescription = 6,
        SystemName = 5,
        TimeToLive = 3
    }
}

