using System;
using System.Collections.Generic;
using System.Text;

namespace App.Global
{
    public class Current
    {
        private static string _siteID = "site";
        private static string _siteNAME = "siteNAME";
        private static string _userID = "admin";
        private static string _userName = "管理员";
        private static string _userCode = "0000";
        private static DateTime _lDate = System.DateTime.Now;
        public static string SiteID
        {
            get { return Current._siteID; }
            set { Current._siteID = value; }
        }

        public static string SiteName
        {
            get { return Current._siteNAME; }
            set { Current._siteNAME = value; }
        }

        public static string UserName
        {
            get { return Current._userName; }
            set { Current._userName = value; }
        }

        public static string UserID
        {
            get { return Current._userID; }
            set { Current._userID = value; }
        }

        public static string UserCode
        {
            get { return Current._userCode; }
            set { Current._userCode = value; }
        }

        public static string OPerId //只读
        {
            get { return Current._userID; }
            set { Current._userID = value; }
        }

        public static string OPerName//只读
        {
            get { return Current.UserName; }
            set { Current.UserName = value; }
        }

        public static DateTime LDate
        {
            get { return Current._lDate; }
            set { Current._lDate = value; }
        }

        //public static string ClientIP
        //{
        //    get
        //    {
        //        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //}
        //public static bool IsTimeout()
        //{
        //    try
        //    {
        //        GlobalUserInfo info = GetInfo();
        //        return false;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}
        //public static void Set(GlobalUserInfo info)
        //{
        //    HttpContext.Current.Session["SITEINFO"] = info;
        //}
        //private static GlobalUserInfo GetInfo()
        //{
        //    if (HttpContext.Current.Session["SITEINFO"] == null)
        //        throw new Exception("您没有登录系统或会话已过期,请重新登录");
        //    //return (GlobalUserInfo)HttpContext.Current.Session["SITEINFO"];
        //    else
        //        return (GlobalUserInfo)HttpContext.Current.Session["SITEINFO"];

        //}
    }
}
