namespace PacketDotNet
{
    using System;
    using System.Runtime.CompilerServices;

    public class RawPacket
    {
        public RawPacket(LinkLayers LinkLayerType, PosixTimeval Timeval, byte[] Data)
        {
            this.LinkLayerType = LinkLayerType;
            this.Timeval = Timeval;
            this.Data = Data;
        }

        public override string ToString()
        {
            return string.Format("[RawPacket: LinkLayerType={0}, Timeval={1}, Data={2}]", this.LinkLayerType, this.Timeval, this.Data);
        }

        public virtual byte[] Data { get; set; }

        public LinkLayers LinkLayerType { get; set; }

        public PosixTimeval Timeval { get; set; }
    }
}

