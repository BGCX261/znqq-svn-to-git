namespace PacketDotNet
{
    using System;

    public abstract class SessionPacket : Packet
    {
        public SessionPacket(PosixTimeval Timeval) : base(Timeval)
        {
        }
    }
}

