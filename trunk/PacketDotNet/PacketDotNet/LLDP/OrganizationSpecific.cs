namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;

    public class OrganizationSpecific : TLV
    {
        private static readonly ILogInactive log;
        private const int OUILength = 3;
        private const int OUISubTypeLength = 1;

        public OrganizationSpecific(byte[] bytes, int offset) : base(bytes, offset)
        {
        }

        public OrganizationSpecific(byte[] oui, int subType, byte[] infoString)
        {
            int length = 6;
            byte[] bytes = new byte[length];
            int offset = 0;
            base.tlvData = new ByteArraySegment(bytes, offset, length);
            base.Type = TLVTypes.OrganizationSpecific;
            this.OrganizationUniqueID = oui;
            this.OrganizationDefinedSubType = subType;
            this.OrganizationDefinedInfoString = infoString;
        }

        public override string ToString()
        {
            return string.Format("[OrganizationSpecific: OrganizationUniqueID={0}, OrganizationDefinedSubType={1}, OrganizationDefinedInfoString={2}]", this.OrganizationUniqueID, this.OrganizationDefinedSubType, this.OrganizationDefinedInfoString);
        }

        public byte[] OrganizationDefinedInfoString
        {
            get
            {
                int length = base.Length - 4;
                byte[] destinationArray = new byte[length];
                Array.Copy(base.tlvData.Bytes, (base.ValueOffset + 3) + 1, destinationArray, 0, length);
                return destinationArray;
            }
            set
            {
                int num = base.Length - 4;
                if (value.Length != num)
                {
                    int length = 6;
                    int num3 = length + value.Length;
                    byte[] destinationArray = new byte[num3];
                    Array.Copy(base.tlvData.Bytes, base.tlvData.Offset, destinationArray, 0, length);
                    int offset = 0;
                    base.tlvData = new ByteArraySegment(destinationArray, offset, num3);
                }
                Array.Copy(value, 0, base.tlvData.Bytes, (base.ValueOffset + 3) + 1, value.Length);
            }
        }

        public int OrganizationDefinedSubType
        {
            get
            {
                return base.tlvData.Bytes[base.ValueOffset + 3];
            }
            set
            {
                base.tlvData.Bytes[base.ValueOffset + 3] = (byte) value;
            }
        }

        public byte[] OrganizationUniqueID
        {
            get
            {
                byte[] destinationArray = new byte[3];
                Array.Copy(base.tlvData.Bytes, base.ValueOffset, destinationArray, 0, 3);
                return destinationArray;
            }
            set
            {
                Array.Copy(value, 0, base.tlvData.Bytes, base.ValueOffset, 3);
            }
        }
    }
}

