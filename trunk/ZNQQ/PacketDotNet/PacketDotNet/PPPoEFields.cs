namespace PacketDotNet
{
    using System;

    public class PPPoEFields
    {
        public static readonly int CodeLength = 1;
        public static readonly int CodePosition = (VersionTypePosition + VersionTypeLength);
        public static readonly int HeaderLength = (LengthPosition + LengthLength);
        public static readonly int LengthLength = 2;
        public static readonly int LengthPosition = (SessionIdPosition + SessionIdLength);
        public static readonly int SessionIdLength = 2;
        public static readonly int SessionIdPosition = (CodePosition + CodeLength);
        public static readonly int VersionTypeLength = 1;
        public static readonly int VersionTypePosition = 0;
    }
}

