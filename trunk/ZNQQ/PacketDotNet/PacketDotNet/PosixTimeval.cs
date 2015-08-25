namespace PacketDotNet
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public class PosixTimeval
    {
        private static readonly DateTime epochDateTime = new DateTime(0x7b2, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        internal long microsecondsPerMillisecond;

        public PosixTimeval()
        {
            ulong num;
            ulong num2;
            this.microsecondsPerMillisecond = 0x3e8L;
            DateTimeToUnixTimeVal(DateTime.UtcNow, out num, out num2);
            this.Seconds = num;
            this.MicroSeconds = num2;
        }

        public PosixTimeval(ulong Seconds, ulong MicroSeconds)
        {
            this.microsecondsPerMillisecond = 0x3e8L;
            this.Seconds = Seconds;
            this.MicroSeconds = MicroSeconds;
        }

        private static void DateTimeToUnixTimeVal(DateTime dateTime, out ulong tvSec, out ulong tvUsec)
        {
            TimeSpan span = dateTime.ToUniversalTime().Subtract(epochDateTime);
            tvSec = (ulong) (span.TotalMilliseconds / 1000.0);
            tvUsec = (ulong) ((span.TotalMilliseconds - (tvSec * 0x3e8L)) * 1000.0);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (base.GetType() != obj.GetType()))
            {
                return false;
            }
            PosixTimeval timeval = (PosixTimeval) obj;
            return ((this.Seconds == timeval.Seconds) && (this.MicroSeconds == timeval.MicroSeconds));
        }

        public override int GetHashCode()
        {
            return (this.Seconds.GetHashCode() + this.MicroSeconds.GetHashCode());
        }

        public static bool operator ==(PosixTimeval a, PosixTimeval b)
        {
            return ((a.Seconds == b.Seconds) && (a.MicroSeconds == b.MicroSeconds));
        }

        public static bool operator >(PosixTimeval a, PosixTimeval b)
        {
            return (b < a);
        }

        public static bool operator >=(PosixTimeval a, PosixTimeval b)
        {
            return (b <= a);
        }

        public static bool operator !=(PosixTimeval a, PosixTimeval b)
        {
            return !(a == b);
        }

        public static bool operator <(PosixTimeval a, PosixTimeval b)
        {
            return ((a.Seconds < b.Seconds) || ((a.Seconds == b.Seconds) && (a.MicroSeconds < b.MicroSeconds)));
        }

        public static bool operator <=(PosixTimeval a, PosixTimeval b)
        {
            return ((a < b) || ((a.Seconds == b.Seconds) && (a.MicroSeconds <= b.MicroSeconds)));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.Seconds);
            builder.Append('.');
            builder.Append(this.MicroSeconds);
            builder.Append('s');
            return builder.ToString();
        }

        private static DateTime UnixTimeValToDateTime(ulong tvSec, ulong tvUsec)
        {
            return epochDateTime.AddSeconds((double) tvSec).AddMilliseconds((double) (tvUsec / ((ulong) 0x3e8L)));
        }

        public virtual DateTime Date
        {
            get
            {
                return UnixTimeValToDateTime(this.Seconds, this.MicroSeconds);
            }
        }

        public virtual ulong MicroSeconds { get; set; }

        public virtual ulong Seconds { get; set; }
    }
}

