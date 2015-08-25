namespace PacketDotNet
{
    using PacketDotNet.LLDP;
    using PacketDotNet.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    public class LLDPPacket : InternetLinkLayerPacket, IEnumerable
    {
        private int _Length;
        private static readonly ILogInactive log;
        public TLVCollection TlvCollection;

        public LLDPPacket() : base(new PosixTimeval())
        {
            this.TlvCollection = new TLVCollection();
            this.TlvCollection.Add(new EndOfLLDPDU());
        }

        public LLDPPacket(byte[] bytes, int offset) : this(bytes, offset, new PosixTimeval())
        {
        }

        public LLDPPacket(byte[] bytes, int offset, PosixTimeval timeval) : base(timeval)
        {
            this.TlvCollection = new TLVCollection();
            base.header = new ByteArraySegment(bytes, offset, bytes.Length - offset);
            this.ParseByteArrayIntoTlvs(base.header.Bytes, base.header.Offset);
        }

        public IEnumerator GetEnumerator()
        {
            return this.TlvCollection.GetEnumerator();
        }

        public static LLDPPacket GetType(Packet p)
        {
            if (p is InternetLinkLayerPacket)
            {
                Packet innerPayload = InternetLinkLayerPacket.GetInnerPayload((InternetLinkLayerPacket) p);
                if (innerPayload is LLDPPacket)
                {
                    return (LLDPPacket) innerPayload;
                }
            }
            return null;
        }

        public void ParseByteArrayIntoTlvs(byte[] bytes, int offset)
        {
            int num = 0;
            this.TlvCollection.Clear();
            while (num < bytes.Length)
            {
                ByteArraySegment byteArraySegment = new ByteArraySegment(bytes, offset + num, 2);
                TLVTypeLength length = new TLVTypeLength(byteArraySegment);
                TLV item = TLVFactory(bytes, offset + num, length.Type);
                if (item == null)
                {
                    break;
                }
                this.TlvCollection.Add(item);
                if (item is EndOfLLDPDU)
                {
                    break;
                }
                num += item.TotalLength;
            }
        }

        public static LLDPPacket RandomPacket()
        {
            Random random = new Random();
            LLDPPacket packet = new LLDPPacket();
            byte[] buffer = new byte[EthernetFields.MacAddressLength];
            random.NextBytes(buffer);
            PhysicalAddress mACAddress = new PhysicalAddress(buffer);
            packet.TlvCollection.Add(new ChassisID(mACAddress));
            byte[] buffer2 = new byte[IPv4Fields.AddressLength];
            random.NextBytes(buffer2);
            packet.TlvCollection.Add(new PortID(new NetworkAddress(new IPAddress(buffer2))));
            ushort seconds = (ushort) random.Next(0, 120);
            packet.TlvCollection.Add(new TimeToLive(seconds));
            packet.TlvCollection.Add(new EndOfLLDPDU());
            return packet;
        }

        private static TLV TLVFactory(byte[] Bytes, int offset, TLVTypes type)
        {
            switch (type)
            {
                case TLVTypes.EndOfLLDPU:
                    return new EndOfLLDPDU(Bytes, offset);

                case TLVTypes.ChassisID:
                    return new ChassisID(Bytes, offset);

                case TLVTypes.PortID:
                    return new PortID(Bytes, offset);

                case TLVTypes.TimeToLive:
                    return new TimeToLive(Bytes, offset);

                case TLVTypes.PortDescription:
                    return new PortDescription(Bytes, offset);

                case TLVTypes.SystemName:
                    return new SystemName(Bytes, offset);

                case TLVTypes.SystemDescription:
                    return new SystemDescription(Bytes, offset);

                case TLVTypes.SystemCapabilities:
                    return new SystemCapabilities(Bytes, offset);

                case TLVTypes.ManagementAddress:
                    return new ManagementAddress(Bytes, offset);

                case TLVTypes.OrganizationSpecific:
                    return new OrganizationSpecific(Bytes, offset);
            }
            throw new ArgumentOutOfRangeException();
        }

        public override string ToColoredString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Blue);
            }
            builder.Append("LLDPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(":");
            IEnumerator<TLV> enumerator = this.TlvCollection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    TLV current = enumerator.Current;
                    Match match = new Regex(@"[^(\.)]([^\.]*)$").Match(current.GetType().ToString());
                    builder.Append(string.Concat(new object[] { " [", match.Groups[0].Value, " length:", current.Length, "]" }));
                }
            }
            finally
            {
                if (enumerator == null)
                {
                }
                enumerator.Dispose();
            }
            builder.Append(']');
            return builder.ToString();
        }

        public override string ToColoredVerboseString(bool colored)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Blue);
            }
            builder.Append("LLDPPacket");
            if (colored)
            {
                builder.Append(AnsiEscapeSequences.Reset);
            }
            builder.Append(":");
            IEnumerator<TLV> enumerator = this.TlvCollection.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    builder.Append(" " + enumerator.Current.ToString());
                }
            }
            finally
            {
                if (enumerator == null)
                {
                }
                enumerator.Dispose();
            }
            builder.Append(']');
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToColoredString(false);
        }

        public override ByteArraySegment BytesHighPerformance
        {
            get
            {
                MemoryStream stream = new MemoryStream();
                IEnumerator<TLV> enumerator = this.TlvCollection.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        TLV current = enumerator.Current;
                        byte[] buffer = current.Bytes;
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
                finally
                {
                    if (enumerator == null)
                    {
                    }
                    enumerator.Dispose();
                }
                int offset = 0;
                byte[] bytes = stream.ToArray();
                return new ByteArraySegment(bytes, offset, bytes.Length);
            }
        }

        public TLV this[int index]
        {
            get
            {
                return this.TlvCollection[index];
            }
            set
            {
                this.TlvCollection[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return this._Length;
            }
            set
            {
                this._Length = value;
            }
        }
    }
}

