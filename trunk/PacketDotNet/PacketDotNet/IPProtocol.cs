namespace PacketDotNet
{
    using System;
    using System.Collections;

    public class IPProtocol
    {
        private static Hashtable messages = new Hashtable();

        static IPProtocol()
        {
            messages[0] = "Dummy protocol for TCP";
            messages[0] = "IPv6 Hop-by-Hop options";
            messages[1] = "Internet Control Message Protocol";
            messages[2] = "Internet Group Management Protocol";
            messages[4] = "IPIP tunnels";
            messages[6] = "Transmission Control Protocol";
            messages[8] = "Exterior Gateway Protocol";
            messages[12] = "PUP protocol";
            messages[0x11] = "User Datagram Protocol";
            messages[0x16] = "XNS IDP protocol";
            messages[0x1d] = "SO Transport Protocol Class 4";
            messages[0x29] = "IPv6 header";
            messages[0x2b] = "IPv6 routing header";
            messages[0x2c] = "IPv6 fragmentation header";
            messages[0x2e] = "Reservation Protocol";
            messages[0x2f] = "General Routing Encapsulation";
            messages[50] = "encapsulating security payload";
            messages[0x33] = "authentication header";
            messages[0x3a] = "ICMPv6";
            messages[0x3b] = "IPv6 no next header";
            messages[60] = "IPv6 destination options";
            messages[0x5c] = "Multicast Transport Protocol";
            messages[0x62] = "Encapsulation Header";
            messages[0x67] = "Protocol Independent Multicast";
            messages[0x6c] = "Compression Header Protocol";
            messages[0xff] = "Raw IP Packet";
        }

        public static string getDescription(int code)
        {
            int key = code;
            if (messages.ContainsKey(key))
            {
                return (string) messages[key];
            }
            return "unknown";
        }
    }
}

