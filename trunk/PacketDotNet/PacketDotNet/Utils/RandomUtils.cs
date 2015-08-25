namespace PacketDotNet.Utils
{
    using PacketDotNet;
    using System;
    using System.Net;

    public class RandomUtils
    {
        public static IPAddress GetIPAddress(IpVersion version)
        {
            byte[] buffer;
            Random random = new Random();
            if (version == IpVersion.IPv4)
            {
                buffer = new byte[IPv4Fields.AddressLength];
                random.NextBytes(buffer);
            }
            else
            {
                if (version != IpVersion.IPv6)
                {
                    throw new InvalidOperationException("Unknown version of " + version);
                }
                buffer = new byte[IPv6Fields.AddressLength];
                random.NextBytes(buffer);
            }
            return new IPAddress(buffer);
        }
    }
}

