namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;

    public class TLV
    {
        private ByteArraySegment _tlvData;
        private static readonly ILogInactive log;
        protected TLVTypeLength TypeLength;

        public TLV()
        {
        }

        public TLV(byte[] bytes, int offset)
        {
            ByteArraySegment byteArraySegment = new ByteArraySegment(bytes, offset, 2);
            this.TypeLength = new TLVTypeLength(byteArraySegment);
            this.tlvData = new ByteArraySegment(bytes, offset, this.TypeLength.Length + 2);
            this.tlvData.Length = this.TypeLength.Length + 2;
        }

        public virtual byte[] Bytes
        {
            get
            {
                return this.tlvData.ActualBytes();
            }
        }

        public int Length
        {
            get
            {
                return this.TypeLength.Length;
            }
            internal set
            {
                this.TypeLength.Length = value;
            }
        }

        internal ByteArraySegment tlvData
        {
            get
            {
                return this._tlvData;
            }
            set
            {
                this._tlvData = value;
                this.TypeLength = new TLVTypeLength(value);
                this.TypeLength.Length = value.Length - 2;
            }
        }

        public int TotalLength
        {
            get
            {
                return this.tlvData.Length;
            }
        }

        public TLVTypes Type
        {
            get
            {
                return this.TypeLength.Type;
            }
            set
            {
                this.TypeLength.Type = value;
            }
        }

        internal int ValueOffset
        {
            get
            {
                return (this.tlvData.Offset + 2);
            }
        }
    }
}

