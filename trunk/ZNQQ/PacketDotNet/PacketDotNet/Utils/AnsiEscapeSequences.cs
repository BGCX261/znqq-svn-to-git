namespace PacketDotNet.Utils
{
    using System;

    public class AnsiEscapeSequences
    {
        public static readonly string Black = BuildValue("0;30");
        public static readonly string Blue = BuildValue("0;34");
        public static readonly string BlueBackground = BuildValue("0;44");
        public static readonly string Bold = BuildValue("0;1");
        public static readonly string Brown = BuildValue("0;33");
        public static readonly string Cyan = BuildValue("0;36");
        public static readonly string CyanBackground = BuildValue("0;46");
        public static readonly string DarkGray = BuildValue("1;30");
        public static readonly string EscapeBegin = ("" + '\x001b' + "[");
        public static readonly string EscapeEnd = "m";
        public static readonly string Green = BuildValue("0;32");
        public static readonly string GreenBackground = BuildValue("0;42");
        public static readonly string Inverse = BuildValue("0;7");
        public static readonly string LightBlue = BuildValue("1;34");
        public static readonly string LightCyan = BuildValue("1;36");
        public static readonly string LightGray = BuildValue("0;37");
        public static readonly string LightGrayBackground = BuildValue("0;47");
        public static readonly string LightGreen = BuildValue("1;32");
        public static readonly string LightPurple = BuildValue("1;35");
        public static readonly string LightRed = BuildValue("1;31");
        public static readonly string Purple = BuildValue("0;35");
        public static readonly string PurpleBackground = BuildValue("0;45");
        public static readonly string Red = BuildValue("0;31");
        public static readonly string RedBackground = BuildValue("0;41");
        public static readonly string Reset = BuildValue("0");
        public static readonly string Underline = BuildValue("0;4");
        public static readonly string White = BuildValue("1;37");
        public static readonly string Yellow = BuildValue("1;33");
        public static readonly string YellowBackground = BuildValue("0;43");

        private static string BuildValue(string ColorCode)
        {
            return (EscapeBegin + ColorCode + EscapeEnd);
        }
    }
}

