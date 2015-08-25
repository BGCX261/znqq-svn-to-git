namespace PacketDotNet
{
    using System;

    public class InternetLinkLayerPacket : Packet
    {
        private static readonly ILogInactive log;

        public InternetLinkLayerPacket(PosixTimeval timeval) : base(timeval)
        {
        }

        public static Packet GetInnerPayload(InternetLinkLayerPacket packet)
        {
            if (!(packet is EthernetPacket))
            {
                return packet.PayloadPacket;
            }
            Packet payloadPacket = packet.PayloadPacket;
            if (payloadPacket is PPPoEPacket)
            {
                return payloadPacket.PayloadPacket.PayloadPacket;
            }
            return payloadPacket;
        }
    }
}

