namespace PacketDotNet
{
    using PacketDotNet.Utils;
    using System;
    using System.IO;

    internal class PacketOrByteArraySegment
    {
        private ByteArraySegment theByteArraySegment;
        private Packet thePacket;

        public void AppendToMemoryStream(MemoryStream ms)
        {
            if (this.ThePacket != null)
            {
                byte[] bytes = this.ThePacket.Bytes;
                ms.Write(bytes, 0, bytes.Length);
            }
            else if (this.TheByteArraySegment != null)
            {
                byte[] buffer = this.TheByteArraySegment.ActualBytes();
                ms.Write(buffer, 0, buffer.Length);
            }
        }

        public ByteArraySegment TheByteArraySegment
        {
            get
            {
                return this.theByteArraySegment;
            }
            set
            {
                this.thePacket = null;
                this.theByteArraySegment = value;
            }
        }

        public Packet ThePacket
        {
            get
            {
                return this.thePacket;
            }
            set
            {
                this.theByteArraySegment = null;
                this.thePacket = value;
            }
        }

        public PayloadType Type
        {
            get
            {
                if (this.ThePacket != null)
                {
                    return PayloadType.Packet;
                }
                if (this.TheByteArraySegment != null)
                {
                    return PayloadType.Bytes;
                }
                return PayloadType.None;
            }
        }
    }
}

