namespace PacketDotNet
{
    using PacketDotNet.Utils;
    using System;

    public abstract class TransportPacket : Packet
    {
        private static readonly ILogInactive log;

        public TransportPacket(PosixTimeval Timeval) : base(Timeval)
        {
        }

        internal int CalculateChecksum(TransportChecksumOption option)
        {
            this.Checksum = 0;
            byte[] bytes = ((IpPacket) this.ParentPacket).PayloadPacket.Bytes;
            if (option == TransportChecksumOption.AttachPseudoIPHeader)
            {
                bytes = ((IpPacket) this.ParentPacket).AttachPseudoIPHeader(bytes);
            }
            return ChecksumUtils.OnesComplementSum(bytes);
        }

        public virtual bool IsValidChecksum(TransportChecksumOption option)
        {
            byte[] bytes = ((IpPacket) this.ParentPacket).PayloadPacket.Bytes;
            if (option == TransportChecksumOption.AttachPseudoIPHeader)
            {
                bytes = ((IpPacket) this.ParentPacket).AttachPseudoIPHeader(bytes);
            }
            return (ChecksumUtils.OnesSum(bytes) == 0xffff);
        }

        public abstract ushort Checksum { get; set; }

        public enum TransportChecksumOption
        {
            None,
            AttachPseudoIPHeader
        }
    }
}

