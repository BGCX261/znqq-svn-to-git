//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace App.Config
{
     //因为没有配置应该，所以读到这里的时候数据会出错
    public class DBConfig
    {
        public static readonly string CmsConString = ConfigurationManager.AppSettings["ConnectionString"];
        //public static readonly string CmsConString = "server=192.168.128.251;uid=sa;pwd=server;database=PB_Data;";
        //public static readonly string CmsConString = "server=Lenovo-pc;uid=sa;pwd=123;database=QQDATA;";
        //public static readonly string HelpConString =ConfigurationManager.ConnectionStrings["HelpKey"].ConnectionString; 
        //public static readonly string CollectConString = ConfigurationManager.ConnectionStrings["Collect"].ConnectionString;
        public static readonly string TableNamePrefix = "NT_"; //App.Config.UIConfig.dataRe;
    }
}
