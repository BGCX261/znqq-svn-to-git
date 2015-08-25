namespace PacketDotNet
{
    using System;

    public enum LinkLayers
    {
        AmateurRadioAX25 = 3,
        ArcNet = 7,
        AtmClip = 0x13,
        AtmRfc1483 = 11,
        Chaos = 5,
        CiscoHDLC = 0x68,
        Ethernet = 1,
        ExperimentalEthernet3MB = 2,
        Fddi = 10,
        Ieee802 = 6,
        Ieee80211 = 0x69,
        LinuxSLL = 0x71,
        Loop = 0x6c,
        Null = 0,
        Ppp = 9,
        PppBSD = 0x10,
        PppSerial = 50,
        ProteonProNetTokenRing = 4,
        Raw = 12,
        Slip = 8,
        SlipBSD = 15,
        Unknown = -1
    }
}

