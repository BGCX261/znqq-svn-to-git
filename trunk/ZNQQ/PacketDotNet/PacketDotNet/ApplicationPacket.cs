namespace PacketDotNet
{
    using System;

    public abstract class ApplicationPacket : Packet
    {
        public ApplicationPacket(PosixTimeval Timeval) : base(Timeval)
        {
        }
    }
}

