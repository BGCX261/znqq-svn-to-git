namespace PacketDotNet
{
    using System;

    public abstract class DataLinkPacket : Packet
    {
        public DataLinkPacket(PosixTimeval Timeval) : base(Timeval)
        {
        }
    }
}

