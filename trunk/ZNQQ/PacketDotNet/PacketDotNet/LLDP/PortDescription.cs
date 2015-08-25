namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using System;

    public class PortDescription : StringTLV
    {
        private static readonly ILogInactive log;

        public PortDescription(string description) : base(TLVTypes.PortDescription, description)
        {
        }

        public PortDescription(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public string Description
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

