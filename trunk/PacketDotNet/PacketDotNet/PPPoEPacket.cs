namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class PPPoEPacket : Packet
    {
        private static readonly ILogInactive log;

        public PPPoEPacket(PPPoECode Code, ushort SessionId) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = PPPoEFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.Code = Code;
            this.SessionId = SessionId;
            this.Version = 1;
            this.Type = 1;
            this.Length = 0;
        }

        public PPPoEPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public PPPoEPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, PPPoEFields.HeaderLength);
            base.payloadPacketOrData = ParseEncapsulatedBytes(base.header, Timeval);
        }

        internal static PacketOrByteArraySegment ParseEncapsulatedBytes(ByteArraySegment Header, PosixTimeval Timeval)
        {
            ByteArraySegment segment = Header.EncapsulatedBytes();
            return new PacketOrByteArraySegment { ThePacket = new PPPPacket(segment.Bytes, segment.Offset, Timeval) };
        }

        public static PPPoEPacket RandomPacket()
        {
            throw new NotImplementedException();
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            object[] args = new object[] { this.Version, this.Type, this.Code, this.SessionId, this.Length };
            builder.AppendFormat("[PPPoEPacket] Version {0}, Type {1}, Code {2}, SessionId {3}, Length {4}", args);
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            return this.ToColoredString(colored);
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public PPPoECode Code
        {
            get
            {
                return (PPPoECode) EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + PPPoEFields.CodePosition);
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + PPPoEFields.CodePosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.DarkGray;
            }
        }

        public ushort Length
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + PPPoEFields.LengthPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + PPPoEFields.LengthPosition);
            }
        }

        public ushort SessionId
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + PPPoEFields.SessionIdPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + PPPoEFields.SessionIdPosition);
            }
        }

        public byte Type
        {
            get
            {
                return (byte) (this.VersionType & 15);
            }
            set
            {
                byte num = (byte) ((this.VersionType & 240) | (value & 240));
                this.VersionType = num;
            }
        }

        public byte Version
        {
            get
            {
                return (byte) ((this.VersionType >> 4) & 240);
            }
            set
            {
                byte num = (byte) ((this.VersionType & 15) | ((value << 4) & 240));
                this.VersionType = num;
            }
        }

        private byte VersionType
        {
            get
            {
                return base.header.Bytes[base.header.Offset + PPPoEFields.VersionTypePosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + PPPoEFields.VersionTypePosition] = value;
            }
        }
    }
}

