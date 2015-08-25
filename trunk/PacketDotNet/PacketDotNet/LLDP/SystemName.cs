namespace PacketDotNet.LLDP
{
    using System;

    public class SystemName : StringTLV
    {
        public SystemName(string name) : base(TLVTypes.SystemName, name)
        {
        }

        public SystemName(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public string Name
        {
            get
            {
                return base.StringValue;
            }
            set
            {
                base.StringValue = value;
            }
        }
    }
}

