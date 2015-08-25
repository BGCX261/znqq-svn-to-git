//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.IO;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;//图片
using System.Xml;
using System.Text.RegularExpressions;

namespace NetCMS.Content.Common
{
  
    /// <summary>
    /// 用户操作类
    /// 编码时间2007年2月27日
    /// 编写人：杨晓彬
    /// </summary>
    public class CommStr
    {
        /// <summary>
        /// 查找XML配置文件
        /// </summary>
        /// <param name="xmlFilePath">XML配置文件的路径</param>
        /// <param name="xmlTargetElement">药查找的元素名称</param>
        /// <returns></returns>
        private static string getXmlElementValue(string xmlFilePath, string xmlTargetElement)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(HttpContext.Current.Server.MapPath(@"~\language\" + xmlFilePath));
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(xmlTargetElement);
            string[] reslt = new string[elemList.Count];
            for (int i = 0; i < elemList.Count; i++)
            {
                reslt[i] = elemList[i].InnerXml;
            }
            return reslt[0];
        }
        /// <summary>
        /// 字符串分页函数(一篇文章可以分多少页)
        /// </summary>
        /// <param name="str">待片理的字符串</param>
        /// <param name="ct">每页显示的个数</param>
        /// <param name="pagesize">分成多少页</param>
        /// <returns></returns>
        public string SubPage(string Input, int PageIndex, int Pagesize)
        {
            int strl = Input.Length;
            string s = null;
            if (strl == (strl / Pagesize) * Pagesize)//看看页面的总记录是否能被每页的记录数整除
            {
                for (int i = 1; i <= strl / Pagesize; i++)
                {
                    HttpContext.Current.Response.Write(" <a href=?page=" + i + ">" + (i) + "</" + "a> ");
                }
                s = Input.Substring(Pagesize * PageIndex - Pagesize, Pagesize);
            }
            else if (PageIndex * Pagesize > strl)
            //在不被整除的情况下,最后一页的设置,如字符长13,每页3,则处理最后那一页的显示
            {
                for (int i = 1; i <= (strl / Pagesize) + 1; i++)
                {
                    HttpContext.Current.Response.Write(" <a href=?page=" + i + ">" + (i) + "</" + "a> ");
                }
                s = Input.Substring((PageIndex - 1) * Pagesize, strl - (PageIndex - 1) * Pagesize);
            }
            else  //在不被整除的情况下其他页面的显示设置
            {
                for (int i = 1; i <= strl / Pagesize + 1; i++)
                {
                    HttpContext.Current.Response.Write(" <a href=?page=" + i + ">" + (i) + "</" + "a> ");
                }
                s = Input.Substring(Pagesize * PageIndex - Pagesize, Pagesize);
            }
            return s;
        }

        /// <summary>
        /// 组合生成文件/栏目名称
        /// </summary>
        /// <param name="str">相关参数</param>
        /// <returns>string</returns>
        /// 添加时间2007-03-01 17:37:00  随机字的长度不能超过10
        public static string FileRandName(string str)
        {
            CommStr cs = new CommStr();
            string[] DirStr = { "{@year02}", "{@year04}", "{@month}", "{@day}", "{@hour}", "{@minute}", "{@second}" };
            for (int i = 0; DirStr.Length > i; i++)
            {
                str = str.Replace(DirStr[i], "" + cs.Strch(i) + "");
            }

            if (str.IndexOf("{@Ram") != -1)
            {
                int Num = str.IndexOf("{@Ram");
                str = str.Replace(str.Substring(Num, 9), "" + cs.Strc(str.Substring(Num, 9)) + "");
            }
            return str;
        }

        protected string Strch(int code)
        {
            string str = "";
            switch (code)
            {
                case 0:
                    str = DateTime.Now.ToString("yy");
                    break;
                case 1:
                    str = DateTime.Now.ToString("yyyy");
                    break;
                case 2:
                    str = DateTime.Now.ToString("MM");
                    break;
                case 3:
                    str = DateTime.Now.ToString("dd");
                    break;
                case 4:
                    str = DateTime.Now.ToString("HH");
                    break;
                case 5:
                    str = DateTime.Now.ToString("mm");
                    break;
                case 6:
                    str = DateTime.Now.ToString("ss");
                    break;
            }
            return str;
        }

        protected string Strc(string StrChar)
        {
            string str = StrChar.Substring(5,1);
            int Str = int.Parse(StrChar.Substring(7,1));
            switch (Str)
            {
                case 0:
                    str = NetCMS.Common.Rand.Number(int.Parse(str));
                    break;
                case 1:
                    str = "abcdefae";
                    break;
                case 2:
                    str = NetCMS.Common.Rand.Str(int.Parse(str));
                    break;
            }
            return str;
        }

        
    }
}
