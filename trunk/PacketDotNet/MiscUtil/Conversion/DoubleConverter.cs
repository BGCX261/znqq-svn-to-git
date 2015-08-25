namespace MiscUtil.Conversion
{
    using System;
    using System.Globalization;

    public class DoubleConverter
    {
        public static string ToExactString(double d)
        {
            if (double.IsPositiveInfinity(d))
            {
                return "+Infinity";
            }
            if (double.IsNegativeInfinity(d))
            {
                return "-Infinity";
            }
            if (double.IsNaN(d))
            {
                return "NaN";
            }
            long num = BitConverter.DoubleToInt64Bits(d);
            bool flag = num < 0L;
            int num2 = (int) ((num >> 0x34) & 0x7ffL);
            long x = num & 0xfffffffffffffL;
            if (num2 == 0)
            {
                num2++;
            }
            else
            {
                x |= 0x10000000000000L;
            }
            num2 -= 0x433;
            if (x == 0)
            {
                return "0";
            }
            while ((x & 1L) == 0)
            {
                x = x >> 1;
                num2++;
            }
            ArbitraryDecimal num4 = new ArbitraryDecimal(x);
            if (num2 < 0)
            {
                for (int i = 0; i < -num2; i++)
                {
                    num4.MultiplyBy(5);
                }
                num4.Shift(-num2);
            }
            else
            {
                for (int j = 0; j < num2; j++)
                {
                    num4.MultiplyBy(2);
                }
            }
            if (flag)
            {
                return ("-" + num4.ToString());
            }
            return num4.ToString();
        }

        private class ArbitraryDecimal
        {
            private int decimalPoint = 0;
            private byte[] digits;

            internal ArbitraryDecimal(long x)
            {
                string str = x.ToString(CultureInfo.InvariantCulture);
                this.digits = new byte[str.Length];
                for (int i = 0; i < str.Length; i++)
                {
                    this.digits[i] = (byte) (str[i] - '0');
                }
                this.Normalize();
            }

            internal void MultiplyBy(int amount)
            {
                byte[] sourceArray = new byte[this.digits.Length + 1];
                for (int i = this.digits.Length - 1; i >= 0; i--)
                {
                    int num2 = (this.digits[i] * amount) + sourceArray[i + 1];
                    sourceArray[i] = (byte) (num2 / 10);
                    sourceArray[i + 1] = (byte) (num2 % 10);
                }
                if (sourceArray[0] != 0)
                {
                    this.digits = sourceArray;
                }
                else
                {
                    Array.Copy(sourceArray, 1, this.digits, 0, this.digits.Length);
                }
                this.Normalize();
            }

            internal void Normalize()
            {
                int index = 0;
                while (index < this.digits.Length)
                {
                    if (this.digits[index] != 0)
                    {
                        break;
                    }
                    index++;
                }
                int num2 = this.digits.Length - 1;
                while (num2 >= 0)
                {
                    if (this.digits[num2] != 0)
                    {
                        break;
                    }
                    num2--;
                }
                if ((index != 0) || (num2 != (this.digits.Length - 1)))
                {
                    byte[] buffer = new byte[(num2 - index) + 1];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = this.digits[i + index];
                    }
                    this.decimalPoint -= this.digits.Length - (num2 + 1);
                    this.digits = buffer;
                }
            }

            internal void Shift(int amount)
            {
                this.decimalPoint += amount;
            }

            public override string ToString()
            {
                char[] chArray = new char[this.digits.Length];
                for (int i = 0; i < this.digits.Length; i++)
                {
                    chArray[i] = (char) (this.digits[i] + 0x30);
                }
                if (this.decimalPoint == 0)
                {
                    return new string(chArray);
                }
                if (this.decimalPoint < 0)
                {
                    return (new string(chArray) + new string('0', -this.decimalPoint));
                }
                if (this.decimalPoint >= chArray.Length)
                {
                    return ("0." + new string('0', this.decimalPoint - chArray.Length) + new string(chArray));
                }
                return (new string(chArray, 0, chArray.Length - this.decimalPoint) + "." + new string(chArray, chArray.Length - this.decimalPoint, this.decimalPoint));
            }
        }
    }
}

