using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZNQQ
{
    public class QQUser
    {
        public QQUser(string qq,string pws)
        {
            this._QQID = qq;
            this._password = pws;
        }

        private string _QQID;

        public string QQID
        {
            get { return _QQID; }
            set { _QQID = value; }
        }

        private  string _password;

        public  string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private byte[] _rawmsg;
        public byte[] RawMsg
        {
            get { return _rawmsg; }
            set { _rawmsg = value; }
        }

        //private static List<ChatForming> chatFormings = new List<ChatForming>();//保存正在聊天的窗口

        //internal static List<ChatForming> ChatFormings
        //{
        //    get { return QQUser.chatFormings; }
        //    set { QQUser.chatFormings = value; }
        //}
    }
}
