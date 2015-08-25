namespace PacketDotNet.LLDP
{
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class StringTLV : TLV
    {
        public StringTLV(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public StringTLV(TLVTypes tlvType, string StringValue)
        {
            byte[] bytes = new byte[2];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, bytes.Length);
            base.Type = tlvType;
            this.StringValue = StringValue;
        }

        public override string ToString()
        {
            return string.Format("[{0}: Description={0}]", base.Type, this.StringValue);
        }

        public string StringValue
        {
            get
            {
                return Encoding.ASCII.GetString(base.tlvData.Bytes, base.ValueOffset, base.Length);
            }
            set
            {
                byte[] bytes = Encoding.ASCII.GetBytes(value);
                int length = 2 + bytes.Length;
                if (base.tlvData.Length != length)
                {
                    byte[] destinationArray = new byte[length];
                    int offset = 0;
                    Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, 2);
                    base.tlvData = new ByteArraySegment(destinationArray, offset, length);
                }
                Array.Copy(bytes, 0, base.tlvData.Bytes, base.ValueOffset, bytes.Length);
            }
        }
    }
}

