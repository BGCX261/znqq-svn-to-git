namespace PacketDotNet
{
    using PacketDotNet.Utils;
    using System;
    using System.IO;

    public abstract class Packet
    {
        internal ByteArraySegment header;
        private static readonly ILogInactive log;
        internal Packet parentPacket;
        internal PacketOrByteArraySegment payloadPacketOrData = new PacketOrByteArraySegment();
        internal PosixTimeval timeval;

        public Packet(PosixTimeval timeval)
        {
            this.timeval = timeval;
        }

        public static Packet Parse(byte[] data)
        {
            return new EthernetPacket(data, 0);
        }

        public static Packet ParsePacket(RawPacket rawPacket)
        {
            return ParsePacket(rawPacket.LinkLayerType, rawPacket.Timeval, rawPacket.Data);
        }

        public static Packet ParsePacket(LinkLayers LinkLayer, PosixTimeval Timeval, byte[] PacketData)
        {
            LinkLayers layers = LinkLayer;
            if (layers != LinkLayers.Ethernet)
            {
                if (layers != LinkLayers.LinuxSLL)
                {
                    throw new NotImplementedException("LinkLayer of " + LinkLayer + " is not implemented");
                }
                return new LinuxSLLPacket(PacketData, 0, Timeval);
            }
            return new EthernetPacket(PacketData, 0, Timeval);
        }

        protected void RecursivelyUpdateCalculatedValues()
        {
            this.UpdateCalculatedValues();
            if (this.payloadPacketOrData.Type == PayloadType.Packet)
            {
                this.payloadPacketOrData.ThePacket.RecursivelyUpdateCalculatedValues();
            }
        }

        public virtual string ToColoredString(bool colored)
        {
            if (this.payloadPacketOrData.Type == PayloadType.Packet)
            {
                return this.payloadPacketOrData.ThePacket.ToColoredString(colored);
            }
            return string.Empty;
        }

        public virtual string ToColoredVerboseString(bool colored)
        {
            if (this.payloadPacketOrData.Type == PayloadType.Packet)
            {
                return this.payloadPacketOrData.ThePacket.ToColoredVerboseString(colored);
            }
            return string.Empty;
        }

        public virtual void UpdateCalculatedValues()
        {
        }

        public virtual byte[] Bytes
        {
            get
            {
                return this.BytesHighPerformance.ActualBytes();
            }
        }

        public virtual ByteArraySegment BytesHighPerformance
        {
            get
            {
                this.RecursivelyUpdateCalculatedValues();
                if (this.SharesMemoryWithSubPackets)
                {
                    return new ByteArraySegment(this.header.Bytes, this.header.Offset, this.header.Bytes.Length - this.header.Offset);
                }
                MemoryStream ms = new MemoryStream();
                byte[] header = this.Header;
                ms.Write(header, 0, header.Length);
                this.payloadPacketOrData.AppendToMemoryStream(ms);
                byte[] bytes = ms.ToArray();
                return new ByteArraySegment(bytes, 0, bytes.Length);
            }
        }

        public virtual string Color
        {
            get
            {
                return AnsiEscapeSequences.Black;
            }
        }

        public virtual byte[] Header
        {
            get
            {
                return this.header.ActualBytes();
            }
        }

        public virtual Packet ParentPacket
        {
            get
            {
                return this.parentPacket;
            }
            set
            {
                this.parentPacket = value;
            }
        }

        public byte[] PayloadData
        {
            get
            {
                if (this.payloadPacketOrData.TheByteArraySegment == null)
                {
                    return null;
                }
                return this.payloadPacketOrData.TheByteArraySegment.ActualBytes();
            }
            set
            {
                this.payloadPacketOrData.TheByteArraySegment = new ByteArraySegment(value, 0, value.Length);
            }
        }

        public virtual Packet PayloadPacket
        {
            get
            {
                return this.payloadPacketOrData.ThePacket;
            }
            set
            {
                if (this.payloadPacketOrData.ThePacket == value)
                {
                    throw new InvalidOperationException("A packet cannot have itself as its payload.");
                }
                this.payloadPacketOrData.ThePacket = value;
                this.payloadPacketOrData.ThePacket.ParentPacket = this;
            }
        }

        internal bool SharesMemoryWithSubPackets
        {
            get
            {
                switch (this.payloadPacketOrData.Type)
                {
                    case PayloadType.Packet:
                        if ((this.header.Bytes != this.payloadPacketOrData.ThePacket.header.Bytes) || ((this.header.Offset + this.header.Length) != this.payloadPacketOrData.ThePacket.header.Offset))
                        {
                            return false;
                        }
                        return this.payloadPacketOrData.ThePacket.SharesMemoryWithSubPackets;

                    case PayloadType.Bytes:
                        if ((this.header.Bytes != this.payloadPacketOrData.TheByteArraySegment.Bytes) || ((this.header.Offset + this.header.Length) != this.payloadPacketOrData.TheByteArraySegment.Offset))
                        {
                            return false;
                        }
                        return true;

                    case PayloadType.None:
                        return true;
                }
                throw new NotImplementedException();
            }
        }

        public virtual PosixTimeval Timeval
        {
            get
            {
                return this.timeval;
            }
        }

        internal int TotalPacketLength
        {
            get
            {
                int num = 0;
                num += this.header.Length;
                if (this.payloadPacketOrData.Type == PayloadType.Bytes)
                {
                    return (num + this.payloadPacketOrData.TheByteArraySegment.Length);
                }
                if (this.payloadPacketOrData.Type == PayloadType.Packet)
                {
                    num += this.payloadPacketOrData.ThePacket.TotalPacketLength;
                }
                return num;
            }
        }
    }
}

