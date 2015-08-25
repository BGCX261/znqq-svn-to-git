//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
//using System.Web;

namespace App.BLL.Common
{
    public class SystemInfo
    {
        public static string GetRootURI()
        {
            string AppPath = "";
            //string UrlAuthority = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            //if (HttpContext.Current.Request.ApplicationPath == "/")
            //    //直接安装在   Web   站点   
            //    AppPath = UrlAuthority;
            //else
            //    //安装在虚拟子目录下   
            //    AppPath = UrlAuthority + HttpContext.Current.Request.ApplicationPath;
            return AppPath;
        }
    }
}
