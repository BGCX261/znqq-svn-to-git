namespace PacketDotNet
{
    using System;

    public enum PPPoECode : ushort
    {
        ActiveDiscoveryInitiation = 9,
        ActiveDiscoveryOffer = 7,
        ActiveDiscoveryTerminate = 0xa7,
        SessionStage = 0
    }
}

