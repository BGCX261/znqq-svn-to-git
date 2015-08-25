namespace MiscUtil.IO
{
    using MiscUtil.Conversion;
    using System;
    using System.IO;
    using System.Text;

    public class EndianBinaryReader : IDisposable
    {
        private EndianBitConverter bitConverter;
        private byte[] buffer;
        private char[] charBuffer;
        private System.Text.Decoder decoder;
        private bool disposed;
        private System.Text.Encoding encoding;
        private int minBytesPerChar;
        private Stream stream;

        public EndianBinaryReader(EndianBitConverter bitConverter, Stream stream) : this(bitConverter, stream, System.Text.Encoding.UTF8)
        {
        }

        public EndianBinaryReader(EndianBitConverter bitConverter, Stream stream, System.Text.Encoding encoding)
        {
            this.disposed = false;
            this.buffer = new byte[0x10];
            this.charBuffer = new char[1];
            if (bitConverter == null)
            {
                throw new ArgumentNullException("bitConverter");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream isn't writable", "stream");
            }
            this.stream = stream;
            this.bitConverter = bitConverter;
            this.encoding = encoding;
            this.decoder = encoding.GetDecoder();
            this.minBytesPerChar = 1;
            if (encoding is UnicodeEncoding)
            {
                this.minBytesPerChar = 2;
            }
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("EndianBinaryReader");
            }
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.stream.Dispose();
            }
        }

        public int Read()
        {
            if (this.Read(this.charBuffer, 0, 1) == 0)
            {
                return -1;
            }
            return this.charBuffer[0];
        }

        public int Read(byte[] buffer, int index, int count)
        {
            this.CheckDisposed();
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if ((count + index) > buffer.Length)
            {
                throw new ArgumentException("Not enough space in buffer for specified number of bytes starting at specified index");
            }
            int num = 0;
            while (count > 0)
            {
                int num2 = this.stream.Read(buffer, index, count);
                if (num2 == 0)
                {
                    return num;
                }
                index += num2;
                num += num2;
                count -= num2;
            }
            return num;
        }

        public int Read(char[] data, int index, int count)
        {
            this.CheckDisposed();
            if (this.buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            if ((count + index) > data.Length)
            {
                throw new ArgumentException("Not enough space in buffer for specified number of characters starting at specified index");
            }
            int num = 0;
            bool flag = true;
            byte[] buffer = this.buffer;
            if (buffer.Length < (count * this.minBytesPerChar))
            {
                buffer = new byte[0x1000];
            }
            while (num < count)
            {
                int length;
                if (flag)
                {
                    length = count * this.minBytesPerChar;
                    flag = false;
                }
                else
                {
                    length = (((count - num) - 1) * this.minBytesPerChar) + 1;
                }
                if (length > buffer.Length)
                {
                    length = buffer.Length;
                }
                int byteCount = this.TryReadInternal(buffer, length);
                if (byteCount == 0)
                {
                    return num;
                }
                int num4 = this.decoder.GetChars(buffer, 0, byteCount, data, index);
                num += num4;
                index += num4;
            }
            return num;
        }

        public int Read7BitEncodedInt()
        {
            this.CheckDisposed();
            int num = 0;
            for (int i = 0; i < 0x23; i += 7)
            {
                int num3 = this.stream.ReadByte();
                if (num3 == -1)
                {
                    throw new EndOfStreamException();
                }
                num |= (num3 & 0x7f) << i;
                if ((num3 & 0x80) == 0)
                {
                    return num;
                }
            }
            throw new IOException("Invalid 7-bit encoded integer in stream.");
        }

        public int ReadBigEndian7BitEncodedInt()
        {
            this.CheckDisposed();
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                int num3 = this.stream.ReadByte();
                if (num3 == -1)
                {
                    throw new EndOfStreamException();
                }
                num = (num << 7) | (num3 & 0x7f);
                if ((num3 & 0x80) == 0)
                {
                    return num;
                }
            }
            throw new IOException("Invalid 7-bit encoded integer in stream.");
        }

        public bool ReadBoolean()
        {
            this.ReadInternal(this.buffer, 1);
            return this.bitConverter.ToBoolean(this.buffer, 0);
        }

        public byte ReadByte()
        {
            this.ReadInternal(this.buffer, 1);
            return this.buffer[0];
        }

        public byte[] ReadBytes(int count)
        {
            int num2;
            this.CheckDisposed();
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }
            byte[] buffer = new byte[count];
            for (int i = 0; i < count; i += num2)
            {
                num2 = this.stream.Read(buffer, i, count - i);
                if (num2 == 0)
                {
                    byte[] dst = new byte[i];
                    Buffer.BlockCopy(buffer, 0, dst, 0, i);
                    return dst;
                }
            }
            return buffer;
        }

        public byte[] ReadBytesOrThrow(int count)
        {
            byte[] data = new byte[count];
            this.ReadInternal(data, count);
            return data;
        }

        public decimal ReadDecimal()
        {
            this.ReadInternal(this.buffer, 0x10);
            return this.bitConverter.ToDecimal(this.buffer, 0);
        }

        public double ReadDouble()
        {
            this.ReadInternal(this.buffer, 8);
            return this.bitConverter.ToDouble(this.buffer, 0);
        }

        public short ReadInt16()
        {
            this.ReadInternal(this.buffer, 2);
            return this.bitConverter.ToInt16(this.buffer, 0);
        }

        public int ReadInt32()
        {
            this.ReadInternal(this.buffer, 4);
            return this.bitConverter.ToInt32(this.buffer, 0);
        }

        public long ReadInt64()
        {
            this.ReadInternal(this.buffer, 8);
            return this.bitConverter.ToInt64(this.buffer, 0);
        }

        private void ReadInternal(byte[] data, int size)
        {
            int num2;
            this.CheckDisposed();
            for (int i = 0; i < size; i += num2)
            {
                num2 = this.stream.Read(data, i, size - i);
                if (num2 == 0)
                {
                    throw new EndOfStreamException(string.Format("End of stream reached with {0} byte{1} left to read.", size - i, ((size - i) != 1) ? "" : "s"));
                }
            }
        }

        public sbyte ReadSByte()
        {
            this.ReadInternal(this.buffer, 1);
            return (sbyte) this.buffer[0];
        }

        public float ReadSingle()
        {
            this.ReadInternal(this.buffer, 4);
            return this.bitConverter.ToSingle(this.buffer, 0);
        }

        public string ReadString()
        {
            int size = this.Read7BitEncodedInt();
            byte[] data = new byte[size];
            this.ReadInternal(data, size);
            return this.encoding.GetString(data, 0, data.Length);
        }

        public ushort ReadUInt16()
        {
            this.ReadInternal(this.buffer, 2);
            return this.bitConverter.ToUInt16(this.buffer, 0);
        }

        public uint ReadUInt32()
        {
            this.ReadInternal(this.buffer, 4);
            return this.bitConverter.ToUInt32(this.buffer, 0);
        }

        public ulong ReadUInt64()
        {
            this.ReadInternal(this.buffer, 8);
            return this.bitConverter.ToUInt64(this.buffer, 0);
        }

        public void Seek(int offset, SeekOrigin origin)
        {
            this.CheckDisposed();
            this.stream.Seek((long) offset, origin);
        }

        private int TryReadInternal(byte[] data, int size)
        {
            this.CheckDisposed();
            int offset = 0;
            while (offset < size)
            {
                int num2 = this.stream.Read(data, offset, size - offset);
                if (num2 == 0)
                {
                    return offset;
                }
                offset += num2;
            }
            return offset;
        }

        public Stream BaseStream
        {
            get
            {
                return this.stream;
            }
        }

        public EndianBitConverter BitConverter
        {
            get
            {
                return this.bitConverter;
            }
        }

        public System.Text.Encoding Encoding
        {
            get
            {
                return this.encoding;
            }
        }
    }
}

