namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class PPPPacket : Packet
    {
        private static readonly ILogInactive log;

        public PPPPacket(PPPoECode Code, ushort SessionId) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = PPPFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.Protocol = PPPProtocol.Padding;
        }

        public PPPPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public PPPPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, PPPFields.HeaderLength);
            base.payloadPacketOrData = ParseEncapsulatedBytes(base.header, Timeval, this.Protocol);
        }

        internal static PacketOrByteArraySegment ParseEncapsulatedBytes(ByteArraySegment Header, PosixTimeval Timeval, PPPProtocol Protocol)
        {
            ByteArraySegment segment = Header.EncapsulatedBytes();
            PacketOrByteArraySegment segment2 = new PacketOrByteArraySegment();
            PPPProtocol protocol = Protocol;
            if (protocol != PPPProtocol.IPv4)
            {
                if (protocol != PPPProtocol.IPv6)
                {
                    throw new NotImplementedException("Protocol of " + Protocol + " is not implemented");
                }
            }
            else
            {
                segment2.ThePacket = new IPv4Packet(segment.Bytes, segment.Offset, Timeval);
                return segment2;
            }
            segment2.ThePacket = new IPv6Packet(segment.Bytes, segment.Offset, Timeval);
            return segment2;
        }

        public static PPPoEPacket RandomPacket()
        {
            throw new NotImplementedException();
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("[PPPPacket] Protocol {0}", this.Protocol);
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

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.DarkGray;
            }
        }

        public PPPProtocol Protocol
        {
            get
            {
                return (PPPProtocol) EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + PPPFields.ProtocolPosition);
            }
            set
            {
                ushort num = (ushort) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + PPPFields.ProtocolPosition);
            }
        }
    }
}

