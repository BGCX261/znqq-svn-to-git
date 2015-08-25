namespace PacketDotNet.Utils
{
    using PacketDotNet;
    using System;
    using System.Runtime.CompilerServices;

    public class ByteArraySegment
    {
        private int length;
        private static readonly ILogInactive log;

        public ByteArraySegment(byte[] Bytes, int Offset, int Length)
        {
            this.Bytes = Bytes;
            this.Offset = Offset;
            this.Length = Length;
        }

        public byte[] ActualBytes()
        {
            if (this.NeedsCopyForActualBytes)
            {
                byte[] destinationArray = new byte[this.Length];
                Array.Copy(this.Bytes, this.Offset, destinationArray, 0, this.Length);
                return destinationArray;
            }
            return this.Bytes;
        }

        public ByteArraySegment EncapsulatedBytes()
        {
            int offset = this.Offset + this.Length;
            return new ByteArraySegment(this.Bytes, offset, this.Bytes.Length - offset);
        }

        public override string ToString()
        {
            object[] args = new object[] { this.Length, this.Bytes.Length, this.Offset, this.NeedsCopyForActualBytes };
            return string.Format("[ByteArraySegment: Length={0}, Bytes.Length={1}, Offset={2}, NeedsCopyForActualBytes={3}]", args);
        }

        public byte[] Bytes { get; private set; }

        public int Length
        {
            get
            {
                return this.length;
            }
            internal set
            {
                if (value < 0)
                {
                    throw new InvalidOperationException("attempting to set a negative length of " + value);
                }
                this.length = value;
            }
        }

        public bool NeedsCopyForActualBytes
        {
            get
            {
                bool flag = (this.Offset == 0) && (this.Length == this.Bytes.Length);
                return !flag;
            }
        }

        public int Offset { get; private set; }
    }
}

