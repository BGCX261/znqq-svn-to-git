namespace PacketDotNet
{
    using System;

    public class ICMPv6Fields
    {
        public static readonly int ChecksumLength = 2;
        public static readonly int ChecksumPosition = (CodePosition + CodeLength);
        public static readonly int CodeLength = 1;
        public static readonly int CodePosition = (TypePosition + TypeLength);
        public static readonly int HeaderLength = (ChecksumPosition + ChecksumLength);
        public static readonly int TypeLength = 1;
        public static readonly int TypePosition = 0;
    }
}

