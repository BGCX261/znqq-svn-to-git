namespace PacketDotNet
{
    using System;

    public enum ICMPv6Types : byte
    {
        DestinationUnreachable = 1,
        EchoReply = 0x81,
        EchoRequest = 0x80,
        PacketTooBig = 2,
        ParameterProblem = 4,
        PrivateExperimentation1 = 100,
        PrivateExperimentation2 = 0x65,
        PrivateExperimentation3 = 200,
        PrivateExperimentation4 = 0xc9,
        ReservedForExpansion1 = 0x7f,
        ReservedForExpansion2 = 0xff,
        RouterSolicitation = 0x85,
        TimeExceeded = 3
    }
}

