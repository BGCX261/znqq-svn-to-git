namespace MiscUtil.IO
{
    using MiscUtil.Conversion;
    using System;
    using System.IO;
    using System.Text;

    public class EndianBinaryWriter : IDisposable
    {
        private EndianBitConverter bitConverter;
        private byte[] buffer;
        private char[] charBuffer;
        private bool disposed;
        private System.Text.Encoding encoding;
        private Stream stream;

        public EndianBinaryWriter(EndianBitConverter bitConverter, Stream stream) : this(bitConverter, stream, System.Text.Encoding.UTF8)
        {
        }

        public EndianBinaryWriter(EndianBitConverter bitConverter, Stream stream, System.Text.Encoding encoding)
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
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Stream isn't writable", "stream");
            }
            this.stream = stream;
            this.bitConverter = bitConverter;
            this.encoding = encoding;
        }

        private void CheckDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("EndianBinaryWriter");
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
                this.Flush();
                this.disposed = true;
                this.stream.Dispose();
            }
        }

        public void Flush()
        {
            this.CheckDisposed();
            this.stream.Flush();
        }

        public void Seek(int offset, SeekOrigin origin)
        {
            this.CheckDisposed();
            this.stream.Seek((long) offset, origin);
        }

        public void Write(bool value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 1);
        }

        public void Write(byte value)
        {
            this.buffer[0] = value;
            this.WriteInternal(this.buffer, 1);
        }

        public void Write(decimal value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 0x10);
        }

        public void Write(double value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 8);
        }

        public void Write(int value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 4);
        }

        public void Write(long value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 8);
        }

        public void Write(sbyte value)
        {
            this.buffer[0] = (byte) value;
            this.WriteInternal(this.buffer, 1);
        }

        public void Write(float value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 4);
        }

        public void Write(uint value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 4);
        }

        public void Write(ulong value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 8);
        }

        public void Write(byte[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.WriteInternal(value, value.Length);
        }

        public void Write(char value)
        {
            this.charBuffer[0] = value;
            this.Write(this.charBuffer);
        }

        public void Write(short value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 2);
        }

        public void Write(char[] value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.CheckDisposed();
            byte[] bytes = this.Encoding.GetBytes(value, 0, value.Length);
            this.WriteInternal(bytes, bytes.Length);
        }

        public void Write(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.CheckDisposed();
            byte[] bytes = this.Encoding.GetBytes(value);
            this.Write7BitEncodedInt(bytes.Length);
            this.WriteInternal(bytes, bytes.Length);
        }

        public void Write(ushort value)
        {
            this.bitConverter.CopyBytes(value, this.buffer, 0);
            this.WriteInternal(this.buffer, 2);
        }

        public void Write(byte[] value, int offset, int count)
        {
            this.CheckDisposed();
            this.stream.Write(value, offset, count);
        }

        public void Write7BitEncodedInt(int value)
        {
            this.CheckDisposed();
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", "Value must be greater than or equal to 0.");
            }
            int count = 0;
            while (value >= 0x80)
            {
                this.buffer[count++] = (byte) ((value & 0x7f) | 0x80);
                value = value >> 7;
                count++;
            }
            this.buffer[count++] = (byte) value;
            this.stream.Write(this.buffer, 0, count);
        }

        private void WriteInternal(byte[] bytes, int length)
        {
            this.CheckDisposed();
            this.stream.Write(bytes, 0, length);
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

