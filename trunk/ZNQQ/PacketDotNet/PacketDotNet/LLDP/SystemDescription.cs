namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using System;

    public class SystemDescription : StringTLV
    {
        private static readonly ILogInactive log;

        public SystemDescription(string description) : base(TLVTypes.SystemDescription, description)
        {
        }

        public SystemDescription(byte[] bytes, int offset) : base(bytes, offset)
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

