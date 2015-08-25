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
using System.Drawing.Imaging;//ͼƬ
using System.Xml;
using System.Text.RegularExpressions;

namespace NetCMS.Content.Common
{
  
    /// <summary>
    /// �û�������
    /// ����ʱ��2007��2��27��
    /// ��д�ˣ�������
    /// </summary>
    public class CommStr
    {
        /// <summary>
        /// ����XML�����ļ�
        /// </summary>
        /// <param name="xmlFilePath">XML�����ļ���·��</param>
        /// <param name="xmlTargetElement">ҩ���ҵ�Ԫ������</param>
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
        /// �ַ�����ҳ����(һƪ���¿��Էֶ���ҳ)
        /// </summary>
        /// <param name="str">��Ƭ����ַ���</param>
        /// <param name="ct">ÿҳ��ʾ�ĸ���</param>
        /// <param name="pagesize">�ֳɶ���ҳ</param>
        /// <returns></returns>
        public string SubPage(string Input, int PageIndex, int Pagesize)
        {
            int strl = Input.Length;
            string s = null;
            if (strl == (strl / Pagesize) * Pagesize)//����ҳ����ܼ�¼�Ƿ��ܱ�ÿҳ�ļ�¼������
            {
                for (int i = 1; i <= strl / Pagesize; i++)
                {
                    HttpContext.Current.Response.Write(" <a href=?page=" + i + ">" + (i) + "</" + "a> ");
                }
                s = Input.Substring(Pagesize * PageIndex - Pagesize, Pagesize);
            }
            else if (PageIndex * Pagesize > strl)
            //�ڲ��������������,���һҳ������,���ַ���13,ÿҳ3,���������һҳ����ʾ
            {
                for (int i = 1; i <= (strl / Pagesize) + 1; i++)
                {
                    HttpContext.Current.Response.Write(" <a href=?page=" + i + ">" + (i) + "</" + "a> ");
                }
                s = Input.Substring((PageIndex - 1) * Pagesize, strl - (PageIndex - 1) * Pagesize);
            }
            else  //�ڲ������������������ҳ�����ʾ����
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
        /// ��������ļ�/��Ŀ����
        /// </summary>
        /// <param name="str">��ز���</param>
        /// <returns>string</returns>
        /// ���ʱ��2007-03-01 17:37:00  ����ֵĳ��Ȳ��ܳ���10
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
