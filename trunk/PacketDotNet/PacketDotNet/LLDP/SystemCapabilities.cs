namespace PacketDotNet.LLDP
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;

    public class SystemCapabilities : TLV
    {
        private const int EnabledCapabilitiesLength = 2;
        private const int SystemCapabilitiesLength = 2;

        public SystemCapabilities(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public SystemCapabilities(ushort capabilities, ushort enabled)
        {
            int length = 6;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            base.Type = TLVTypes.SystemCapabilities;
            this.Capabilities = capabilities;
            this.Enabled = enabled;
        }

        public bool IsCapable(CapabilityOptions capability)
        {
            ushort num = (ushort) capability;
            return ((this.Capabilities & num) != 0);
        }

        public bool IsEnabled(CapabilityOptions capability)
        {
            ushort num = (ushort) capability;
            return ((this.Enabled & num) != 0);
        }

        public override string ToString()
        {
            return string.Format("[SystemCapabilities: Capabilities={0}, Enabled={1}]", this.Capabilities, this.Enabled);
        }

        public ushort Capabilities
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

        public ushort Enabled
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.tlvData.Bytes, (base.tlvData.Offset + 2) + 2);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.tlvData.Bytes, base.ValueOffset + 2);
            }
        }
    }
}

