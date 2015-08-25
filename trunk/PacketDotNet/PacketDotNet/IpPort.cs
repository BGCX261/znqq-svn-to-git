namespace PacketDotNet
{
    using System;

    public enum IpPort : ushort
    {
        Auth = 0x71,
        DayTime = 13,
        Echo = 7,
        Finger = 0x4f,
        Ftp = 0x15,
        FtpData = 20,
        Gopher = 70,
        Http = 80,
        Ident = 0x71,
        Imap = 0x8f,
        Kerberos = 0x58,
        Ntp = 0x7b,
        Pop3 = 110,
        PrivilegedPortLimit = 0x400,
        Sftp = 0x73,
        Smtp = 0x19,
        Snmp = 0xa1,
        Ssh = 0x16,
        Telnet = 0x17,
        Tftp = 0x45,
        Time = 0x25,
        Whois = 0x3f,
        Www = 80
    }
}

