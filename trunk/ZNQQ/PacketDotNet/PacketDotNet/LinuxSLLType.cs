namespace PacketDotNet
{
    using System;

    public enum LinuxSLLType
    {
        PacketSentToUs,
        PacketBroadCast,
        PacketMulticast,
        PacketSentToSomeoneElse,
        PacketSentByUs
    }
}

