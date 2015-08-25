using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace App.Common.Web
{
    public class WebTips
    {
        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="p">页面</param>
        /// <param name="sMsg">信息内容</param>
        public static void Alert(Page p, string sMsg)
        {
            Alert(p, sMsg, string.Empty, false, false);
        }
        /// <summary>
        /// 弹出提示信息并跳转
        /// </summary>
        /// <param name="p">页面</param>
        /// <param name="sMsg">信息内容</param>
        /// <param name="goUrl">跳转地址</param>
        public static void Alert(Page p, string sMsg, string goUrl)
        {
            Alert(p, sMsg, goUrl, false, false);
        }
        /// <summary>
        /// 弹出提示信息返回
        /// </summary>
        /// <param name="p">页面</param>
        /// <param name="sMsg">信息内容</param>
        /// <param name="goBack">是否返回</param>
        public static void Alert(Page p, string sMsg, bool goBack)
        {
            Alert(p, sMsg, string.Empty, goBack, false);
        }
        public static void Alert(Page p, string sMsg, bool close, bool goBack)
        {
            Alert(p, sMsg, string.Empty, goBack, close);
        }
        /// <summary>
        /// 弹出提示信息
        /// </summary>
        /// <param name="p">页面</param>
        /// <param name="sMsg">信息内容</param>
        /// <param name="goUrl">跳转地址</param>
        /// <param name="goBack">是否返回</param>
        public static void Alert(Page p, string sMsg, string goUrl, bool goBack, bool close)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat("<script language=javascript>alert('{0}');", sMsg);
            if (close)
                sb.Append("window.close();");
            if (goUrl != string.Empty)
                sb.AppendFormat("window.location.href='{0}';", goUrl);
            if (goBack)
                sb.Append("history.go(-1);");
            sb.Append("</script>");
            p.ClientScript.RegisterClientScriptBlock(p.GetType(), "alert", sb.ToString());
        }

        public static void AnsyAlert(Page p, string sMsg)
        {

        }
    }

    public class Sec
    {
        public static void CheckRight()
        {
            if (System.Web.HttpContext.Current.Session[KName.K_USR_ISSUPER] != null && Convert.ToBoolean(System.Web.HttpContext.Current.Session[KName.K_USR_ISSUPER])) { return; }//超级用户不用判断
            App.BLL.SITE sbll = new App.BLL.SITE();
            App.Model.SiteInfo sbm = sbll.GetInfo(System.Web.HttpContext.Current.Session[KName.K_SIT_NO].ToString());
            if (string.IsNullOrEmpty(sbm.SHOPID) || sbm.SHOPID.ToLower() != System.Web.HttpContext.Current.Session[KName.K_USR_NO].ToString().ToLower()) //不是店长
            {
                System.Web.HttpContext.Current.Response.Write("没有权限！");
                System.Web.HttpContext.Current.Response.End();
            }
        }
    }
}
