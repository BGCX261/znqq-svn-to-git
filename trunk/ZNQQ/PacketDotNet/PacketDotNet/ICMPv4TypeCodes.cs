namespace PacketDotNet
{
    using System;

    public enum ICMPv4TypeCodes : ushort
    {
        CommunicationAdministrativelyProhibited = 780,
        DestinationHostUnknown = 0x307,
        DestinationHostUnreachable = 0x301,
        DestinationNetworkUnknown = 0x306,
        DestinationNetworkUnreachable = 0x300,
        DestinationPortUnreachable = 0x303,
        DestinationProtocolUnreachable = 770,
        EchoReply = 0,
        EchoRequest = 0x800,
        FragmentationRequiredAndDFFlagSet = 0x304,
        HostUnreachableForTos = 0x30b,
        NetworkAdministrativelyProhibited = 0x309,
        NetworkUnreachableForTos = 0x30a,
        SourceHostIsolated = 0x308,
        SourceRouteFailed = 0x305
    }
}

