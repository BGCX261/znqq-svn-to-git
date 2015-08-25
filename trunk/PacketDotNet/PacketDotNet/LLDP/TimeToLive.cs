namespace PacketDotNet.LLDP
{
    using MiscUtil.Conversion;
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;

    public class TimeToLive : TLV
    {
        private static readonly ILogInactive log;
        private const int ValueLength = 2;

        public TimeToLive(ushort seconds)
        {
            byte[] bytes = new byte[4];
            int offset = 0;
            int length = bytes.Length;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            base.Type = TLVTypes.TimeToLive;
            this.Seconds = seconds;
        }

        public TimeToLive(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public override string ToString()
        {
            return string.Format("[TimeToLive: Seconds={0}]", this.Seconds);
        }

        public ushort Seconds
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.tlvData.Bytes, base.tlvData.Offset + 2);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.tlvData.Bytes, base.tlvData.Offset + 2);
            }
        }
    }
}

