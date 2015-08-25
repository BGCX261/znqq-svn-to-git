//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace App.Config
{
    public class UIConfig
    {
        //public static string WebDAL = ConfigurationManager.AppSettings["WebDAL"];
        //public static string dataRe = ConfigurationManager.AppSettings["dataRe"];
        //public static string mssql = ConfigurationManager.AppSettings["mssql"];
        //public static string CssPath = ConfigurationManager.AppSettings["manner"];
        //public static string CssPath()
        //{
        //    return BaseConfig.GetConfigValue("manner");
        //}
        public static string returnCopyRight = verConfig.isTryversion + verConfig.helpcenterStr + verConfig.ForumStr;
        //public static string HeadTitle = BaseConfig.GetConfigValue("headTitle");
        //public static string sHeight = BaseConfig.GetConfigValue("sHeight");
        //public static string sWidth = BaseConfig.GetConfigValue("sWidth");
        //public static string isLinkTF = BaseConfig.GetConfigValue("isLinkTF");
        //public static string dirMana = BaseConfig.GetConfigValue("dirMana");
        //public static string dirUser = BaseConfig.GetConfigValue("dirUser");
        //public static string dirDumm = BaseConfig.GetConfigValue("dirDumm");
        //public static string UserdirFile = BaseConfig.GetConfigValue("UserdirFile");
        //public static string protPass = BaseConfig.GetConfigValue("protPass");
        //public static string protRand() { return BaseConfig.GetConfigValue("protRand");}
        //public static string dirTemplet = BaseConfig.GetConfigValue("dirTemplet");
        //public static string dirSite = BaseConfig.GetConfigValue("dirSite");
        //public static string dirFile = BaseConfig.GetConfigValue("dirFile");
        //public static string dirHtml = BaseConfig.GetConfigValue("dirHtml");
        //public static string saveContent = BaseConfig.GetConfigValue("saveContent");
        //public static string publicType = NetCMS.Config.verConfig.PublicType;
        //public static string indeData = BaseConfig.GetConfigValue("indeData");
        //public static string Logfilename = BaseConfig.GetConfigValue("Logfilename");
        //public static string dirPige = BaseConfig.GetConfigValue("dirPige");
        //public static string dirPigeDate = BaseConfig.GetConfigValue("dirPigeDate");
        //public static string publicfreshinfo = BaseConfig.GetConfigValue("publicfreshinfo");
        //public static string constPass() { return BaseConfig.GetConfigValue("constPass"); }
        //public static string filePass() { return BaseConfig.GetConfigValue("filePass"); }
        //public static string filePath = BaseConfig.GetConfigValue("filePath");
        //public static string sqlConnData = BaseConfig.GetConfigValue("sqlConnData");
        //public static string smtpserver = BaseConfig.GetConfigValue("smtpserver");
        //public static string emailuserName = BaseConfig.GetConfigValue("emailuserName");
        //public static string emailuserpwd = BaseConfig.GetConfigValue("emailuserpwd");
        //public static string emailfrom = BaseConfig.GetConfigValue("emailfrom");
        //public static string copyright = BaseConfig.GetConfigValue("copyRight");
        /// <summary>
        /// 取得参数配置每页显示记录多少条
        /// </summary>
        /// <returns>返回数值型</returns>
        public static int GetPageSize()
        {

            //int n = Convert.ToInt32(BaseConfig.GetConfigValue("PageSize"));
            int n = Convert.ToInt32(10);  
            if (n < 1)
                throw new Exception("每页记录条数不能小于1!");
            return n;
        }
    }
}
