namespace PacketDotNet
{
    using System;

    public class ICMPv4Fields
    {
        public static readonly int ChecksumLength = 2;
        public static readonly int ChecksumPosition;
        public static readonly int HeaderLength;
        public static readonly int IDLength = 2;
        public static readonly int IDPosition;
        public static readonly int SequenceLength = 2;
        public static readonly int SequencePosition;
        public static readonly int TypeCodeLength = 2;
        public static readonly int TypeCodePosition = 0;

        static ICMPv4Fields()
        {
            TypeCodePosition = 0;
            ChecksumPosition = TypeCodePosition + TypeCodeLength;
            IDPosition = ChecksumPosition + ChecksumLength;
            SequencePosition = IDPosition + IDLength;
            HeaderLength = SequencePosition + SequenceLength;
        }
    }
}

