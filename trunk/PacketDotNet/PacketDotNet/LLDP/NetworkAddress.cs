namespace PacketDotNet.LLDP
{
    using PacketDotNet;
    using PacketDotNet.Utils;
    using System;
    using System.Net;
    using System.Net.Sockets;

    public class NetworkAddress
    {
        internal const int AddressFamilyLength = 1;
        internal ByteArraySegment data;

        public NetworkAddress(IPAddress address)
        {
            this.Address = address;
        }

        public NetworkAddress(byte[] bytes, int offset, int length)
        {
            this.data = new ByteArraySegment(bytes, offset, length);
        }

        private static PacketDotNet.LLDP.AddressFamily AddressFamilyFromSocketAddress(IPAddress address)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return PacketDotNet.LLDP.AddressFamily.IPv4;
            }
            return PacketDotNet.LLDP.AddressFamily.IPv6;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (base.GetType() != obj.GetType()))
            {
                return false;
            }
            NetworkAddress address = (NetworkAddress) obj;
            return (this.AddressFamily.Equals(address.AddressFamily) && this.Address.Equals(address.Address));
        }

        public override int GetHashCode()
        {
            return (this.AddressFamily.GetHashCode() + this.Address.GetHashCode());
        }

        private static int LengthFromAddressFamily(PacketDotNet.LLDP.AddressFamily addressFamily)
        {
            if (addressFamily == PacketDotNet.LLDP.AddressFamily.IPv4)
            {
                return IPv4Fields.AddressLength;
            }
            if (addressFamily != PacketDotNet.LLDP.AddressFamily.IPv6)
            {
                throw new NotImplementedException("Unknown addressFamily of " + addressFamily);
            }
            return IPv6Fields.AddressLength;
        }

        public override string ToString()
        {
            return string.Format("[NetworkAddress: AddressFamily={0}, Address={1}]", this.AddressFamily, this.Address);
        }

        public IPAddress Address
        {
            get
            {
                byte[] destinationArray = new byte[LengthFromAddressFamily(this.AddressFamily)];
                Array.Copy(this.data.Bytes, this.data.Offset + 1, destinationArray, 0, destinationArray.Length);
                return new IPAddress(destinationArray);
            }
            set
            {
                int length = LengthFromAddressFamily(AddressFamilyFromSocketAddress(value)) + 1;
                if ((this.data == null) || (this.data.Length != length))
                {
                    byte[] bytes = new byte[length];
                    int offset = 0;
                    this.data = new ByteArraySegment(bytes, offset, length);
                }
                this.AddressFamily = AddressFamilyFromSocketAddress(value);
                byte[] addressBytes = value.GetAddressBytes();
                Array.Copy(addressBytes, 0, this.data.Bytes, this.data.Offset + 1, addressBytes.Length);
            }
        }

        public PacketDotNet.LLDP.AddressFamily AddressFamily
        {
            get
            {
                return (PacketDotNet.LLDP.AddressFamily) this.data.Bytes[this.data.Offset];
            }
            set
            {
                this.data.Bytes[this.data.Offset] = (byte) value;
            }
        }

        internal byte[] Bytes
        {
            get
            {
                byte[] addressBytes = this.Address.GetAddressBytes();
                byte[] destinationArray = new byte[1 + addressBytes.Length];
                destinationArray[0] = (byte) this.AddressFamily;
                Array.Copy(addressBytes, 0, destinationArray, 1, addressBytes.Length);
                return destinationArray;
            }
        }

        internal int Length
        {
            get
            {
                return (1 + this.Address.GetAddressBytes().Length);
            }
        }
    }
}

