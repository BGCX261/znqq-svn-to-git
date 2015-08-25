namespace PacketDotNet
{
    using System;

    public class InternetPacket : Packet
    {
        public InternetPacket(PosixTimeval Timeval) : base(Timeval)
        {
        }
    }
}

