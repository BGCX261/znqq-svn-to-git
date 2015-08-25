namespace PacketDotNet
{
    using MiscUtil.Conversion;
    using PacketDotNet.Utils;
    using System;
    using System.Text;

    public class TcpPacket : TransportPacket
    {
        public const int HeaderMinimumLength = 20;
        private static readonly ILogInactive log;

        public TcpPacket(ushort SourcePort, ushort DestinationPort) : base(new PosixTimeval())
        {
            int offset = 0;
            int headerLength = TcpFields.HeaderLength;
            byte[] bytes = new byte[headerLength];
            base.header = new ByteArraySegment(bytes, offset, headerLength);
            this.DataOffset = headerLength / 4;
            this.SourcePort = SourcePort;
            this.DestinationPort = DestinationPort;
        }

        public TcpPacket(byte[] Bytes, int Offset) : this(Bytes, Offset, new PosixTimeval())
        {
        }

        public TcpPacket(byte[] Bytes, int Offset, PosixTimeval Timeval) : base(Timeval)
        {
            base.header = new ByteArraySegment(Bytes, Offset, Bytes.Length - Offset);
            base.header.Length = this.DataOffset * 4;
            base.payloadPacketOrData = new PacketOrByteArraySegment();
            base.payloadPacketOrData.TheByteArraySegment = base.header.EncapsulatedBytes();
        }

        public TcpPacket(byte[] Bytes, int Offset, PosixTimeval Timeval, Packet ParentPacket) : this(Bytes, Offset, Timeval)
        {
            this.ParentPacket = ParentPacket;
            if (this.ParentPacket is IPv4Packet)
            {
                IPv4Packet parentPacket = (IPv4Packet) this.ParentPacket;
                int num = parentPacket.TotalLength - (parentPacket.HeaderLength * 4);
                int num2 = num - this.Header.Length;
                base.payloadPacketOrData.TheByteArraySegment.Length = num2;
            }
        }

        public int CalculateTCPChecksum()
        {
            return base.CalculateChecksum(TransportPacket.TransportChecksumOption.AttachPseudoIPHeader);
        }

        public static TcpPacket GetEncapsulated(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is IpPacket)
                {
                    Packet payloadPacket = innerPayload.PayloadPacket;
                    if (payloadPacket is TcpPacket)
                    {
                        return (TcpPacket) payloadPacket;
                    }
                }
            }
            return null;
        }

        public static TcpPacket RandomPacket()
        {
            Random random = new Random();
            ushort sourcePort = (ushort) random.Next(0, 0xffff);
            return new TcpPacket(sourcePort, (ushort) random.Next(0, 0xffff));
        }

        private void setFlag(bool on, int MASK)
        {
            if (on)
            {
                this.AllFlags |= MASK;
            }
            else
            {
                this.AllFlags &= ~MASK;
            }
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("TCPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append(" SourcePort: ");
            if (Enum.IsDefined(typeof(IpPort), this.SourcePort))
            {
                builder.Append((IpPort) this.SourcePort);
                builder.Append(" (" + this.SourcePort + ") ");
            }
            else
            {
                builder.Append(this.SourcePort);
            }
            builder.Append(" -> ");
            builder.Append(" DestinationPort: ");
            if (Enum.IsDefined(typeof(IpPort), this.DestinationPort))
            {
                builder.Append((IpPort) this.DestinationPort);
                builder.Append(" (" + this.DestinationPort + ") ");
            }
            else
            {
                builder.Append(this.DestinationPort);
            }
            if (this.Urg)
            {
                builder.Append(" urg[0x" + Convert.ToString(this.UrgentPointer, 0x10) + "]");
            }
            if (this.Ack)
            {
                builder.Append(string.Concat(new object[] { " ack[", this.AcknowledgmentNumber, " (0x", Convert.ToString((long) this.AcknowledgmentNumber, 0x10), ")]" }));
            }
            if (this.Psh)
            {
                builder.Append(" psh");
            }
            if (this.Rst)
            {
                builder.Append(" rst");
            }
            if (this.Syn)
            {
                builder.Append(string.Concat(new object[] { " syn[0x", Convert.ToString((long) this.SequenceNumber, 0x10), ",", this.SequenceNumber, "]" }));
            }
            if (this.Fin)
            {
                builder.Append(" fin");
            }
            builder.Append(']');
            builder.Append(base.ToColoredString(colored));
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(this.Color);
            }
            builder.Append("TCPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(": ");
            builder.Append("sport=" + this.SourcePort + ", ");
            builder.Append("dport=" + this.DestinationPort + ", ");
            builder.Append("seqn=0x" + Convert.ToString((long) this.SequenceNumber, 0x10) + ", ");
            builder.Append("ackn=0x" + Convert.ToString((long) this.AcknowledgmentNumber, 0x10) + ", ");
            builder.Append("urg=" + this.Urg + ", ");
            builder.Append("ack=" + this.Ack + ", ");
            builder.Append("psh=" + this.Psh + ", ");
            builder.Append("rst=" + this.Rst + ", ");
            builder.Append("syn=" + this.Syn + ", ");
            builder.Append("fin=" + this.Fin + ", ");
            builder.Append("wsize=" + this.WindowSize + ", ");
            builder.Append("uptr=0x" + Convert.ToString(this.UrgentPointer, 0x10));
            builder.Append(']');
            builder.Append(base.ToColoredVerboseString(colored));
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public void UpdateTCPChecksum()
        {
            this.Checksum = (ushort) this.CalculateTCPChecksum();
        }

        public virtual bool Ack
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_ACK_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_ACK_MASK);
            }
        }

        public uint AcknowledgmentNumber
        {
            get
            {
                return EndianBitConverter.Big.ToUInt32(base.header.Bytes, base.header.Offset + TcpFields.AckNumberPosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + TcpFields.AckNumberPosition);
            }
        }

        private int AllFlags
        {
            get
            {
                return base.header.Bytes[base.header.Offset + TcpFields.FlagsPosition];
            }
            set
            {
                base.header.Bytes[base.header.Offset + TcpFields.FlagsPosition] = (byte) value;
            }
        }

        public override ushort Checksum
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + TcpFields.ChecksumPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + TcpFields.ChecksumPosition);
            }
        }

        public override string Color
        {
            get
            {
                return AnsiEscapeSequences.Yellow;
            }
        }

        public virtual bool CWR
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_CWR_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_CWR_MASK);
            }
        }

        public virtual int DataOffset
        {
            get
            {
                byte num = base.header.Bytes[base.header.Offset + TcpFields.DataOffsetPosition];
                return ((num >> 4) & 15);
            }
            set
            {
                byte num = base.header.Bytes[base.header.Offset + TcpFields.DataOffsetPosition];
                num = (byte) ((num & 15) | ((value << 4) & 240));
                base.header.Bytes[base.header.Offset + TcpFields.DataOffsetPosition] = num;
            }
        }

        public virtual ushort DestinationPort
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + TcpFields.DestinationPortPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + TcpFields.DestinationPortPosition);
            }
        }

        public virtual bool ECN
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_ECN_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_ECN_MASK);
            }
        }

        public virtual bool Fin
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_FIN_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_FIN_MASK);
            }
        }

        public byte[] Options
        {
            get
            {
                if (this.Urg)
                {
                    throw new NotImplementedException("Urg == true not implemented yet");
                }
                int num = TcpFields.UrgentPointerPosition + TcpFields.UrgentPointerLength;
                int length = (this.DataOffset * 4) - num;
                byte[] destinationArray = new byte[length];
                Array.Copy(base.header.Bytes, base.header.Offset + num, destinationArray, 0, length);
                return destinationArray;
            }
        }

        public virtual bool Psh
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_PSH_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_PSH_MASK);
            }
        }

        public virtual bool Rst
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_RST_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_RST_MASK);
            }
        }

        public uint SequenceNumber
        {
            get
            {
                return EndianBitConverter.Big.ToUInt32(base.header.Bytes, base.header.Offset + TcpFields.SequenceNumberPosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + TcpFields.SequenceNumberPosition);
            }
        }

        public virtual ushort SourcePort
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + TcpFields.SourcePortPosition);
            }
            set
            {
                ushort num = value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + TcpFields.SourcePortPosition);
            }
        }

        public virtual bool Syn
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_SYN_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_SYN_MASK);
            }
        }

        public virtual bool Urg
        {
            get
            {
                return ((this.AllFlags & TcpFields.TCP_URG_MASK) != 0);
            }
            set
            {
                this.setFlag(value, TcpFields.TCP_URG_MASK);
            }
        }

        public int UrgentPointer
        {
            get
            {
                return EndianBitConverter.Big.ToInt16(base.header.Bytes, base.header.Offset + TcpFields.UrgentPointerPosition);
            }
            set
            {
                short num = (short) value;
                EndianBitConverter.Big.CopyBytes(num, base.header.Bytes, base.header.Offset + TcpFields.UrgentPointerPosition);
            }
        }

        public bool ValidChecksum
        {
            get
            {
                if (base.parentPacket.GetType() == typeof(IPv6Packet))
                {
                    return this.ValidTCPChecksum;
                }
                return (((IPv4Packet) this.ParentPacket).ValidIPChecksum && this.ValidTCPChecksum);
            }
        }

        public virtual bool ValidTCPChecksum
        {
            get
            {
                return this.IsValidChecksum(TransportPacket.TransportChecksumOption.AttachPseudoIPHeader);
            }
        }

        public virtual ushort WindowSize
        {
            get
            {
                return EndianBitConverter.Big.ToUInt16(base.header.Bytes, base.header.Offset + TcpFields.WindowSizePosition);
            }
            set
            {
                EndianBitConverter.Big.CopyBytes(value, base.header.Bytes, base.header.Offset + TcpFields.WindowSizePosition);
            }
        }

        public enum OptionTypes
        {
            EndOfList,
            Nop,
            MaximumSegmentSize,
            WindowScale,
            SelectiveAckSupported,
            Unknown5,
            Unknown6,
            Unknown7,
            Timestamp
        }
    }
}

