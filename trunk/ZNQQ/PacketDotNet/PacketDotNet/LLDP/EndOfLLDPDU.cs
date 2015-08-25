namespace PacketDotNet.LLDP
{
    using PacketDotNet.Utils;
    using System;

    public class EndOfLLDPDU : TLV
    {
        public EndOfLLDPDU()
        {
            byte[] bytes = new byte[2];
            int offset = 0;
            int length = bytes.Length;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            base.Type = TLVTypes.EndOfLLDPU;
            base.Length = 0;
        }

        public EndOfLLDPDU(byte[] bytes, int offset) : base(bytes, offset)
        {
            base.Type = TLVTypes.EndOfLLDPU;
            base.Length = 0;
        }

        public override string ToString()
        {
            return string.Format("[EndOfLLDPDU]", new object[0]);
        }
    }
}

