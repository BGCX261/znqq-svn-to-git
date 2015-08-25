//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Data;
using System.Configuration;
using System.IO;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text.RegularExpressions;
using App.Global;
using System.Net.Mail;

namespace App.Common
{
    /// <summary>
    /// Class1 的摘要说明
    /// </summary>
    public class Public
    {

        /// <summary>
        /// 得到站点用户IP, IpSTR = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString()
        /// </summary>
        /// <returns></returns>
        //public static string getUserIP()
        //{
        //    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        //}

        /// <summary>
        /// 去除字符串最后一个','号
        /// </summary>
        /// <param name="chr">:要做处理的字符串</param>
        /// <returns>返回已处理的字符串</returns>
        /// /// CreateTime:2007-03-26 Code By DengXi
        public static string Lost(string chr)
        {
            if (chr == null || chr == string.Empty)
            {
                return "";
            }
            else
            {
                chr = chr.Remove(chr.LastIndexOf(","));
                return chr;
            }
        }

        //  
        public static string lostfirst(string chr)
        {
            string flg = "";
            if (chr != string.Empty || chr != null)
            {
                if (chr.Substring(0, 1) == "/")
                    flg = chr.Replace(chr.Substring(0, 1), "");
                else
                    flg = chr;
            }
            return flg;
        }

        //public static void sendMail(string smtpserver,string userName,string pwd, string strfrom, string strto, string subj, string bodys)
        //{
        //    SmtpClient mail = new System.Net.Mail.SmtpClient();
        //    mail.Host = smtpserver;//smtp
        //    mail.Credentials = new System.Net.NetworkCredential(userName, pwd);
        //    //mail.Credentials.GetCredential = new System.Net.NetworkCredential(userName, pwd); ;
        //    //mail.EnableSsl = ssl;//发送连接套接层是否加密 例如用gmail发是加密的 
        //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(strfrom, strto);
        //    message.Body = bodys;
        //    message.Subject = subj;
        //    message.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    message.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    message.IsBodyHtml = true;
        //    //message.
        //    mail.Send(message);
        //}

        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subj"></param>
        /// <param name="bodys"></param>

        public static void sendMail(string smtpserver, string userName, string pwd, string strfrom, string strto, string subj, string bodys)
        {
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtpserver;//指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码

            MailMessage _mailMessage = new MailMessage(strfrom, strto);
            _mailMessage.Subject = subj;//主题
            _mailMessage.Body = bodys;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级
            _smtpClient.Send(_mailMessage);
        } 


        /// <summary>
        /// 读取web.config相关数据信息
        /// </summary>
        /// <param name="xmlTargetElement">相关字节</param>
        /// <returns></returns>
        /// 编写时间2007-03-08  y.xiaobin(著)
        public static string getXmlElementValue(string xmlTargetElement)
        {
            return System.Configuration.ConfigurationManager.AppSettings[xmlTargetElement];
        }

        /// <summary>
        /// web.config相关文件操作
        /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写
        /// 在此函数中自动去根目下寻找web.config
        /// </summary>
        /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
        /// 2007-5-9 y.xiaobin
        /// <returns></returns>
        public static bool constReadOnly(int num)
        {
            bool _readonly = false;
            string _config = "123";//HttpContext.Current.Server.MapPath(@"~/Web.config");
            FileInfo fi = new FileInfo(_config);
            switch (num)
            {
                case 0: _readonly = fi.IsReadOnly; break;
                case 1:
                    fi.IsReadOnly = true;
                    _readonly = true;
                    break;
                case 2:
                    {
                        fi.IsReadOnly = false;
                        _readonly = false;
                    }
                    break;
                default: throw new Exception("错误参数!");
            }

            return _readonly;

        }

        /// <summary>
        /// web.config相关文件操作
        /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把config改写为只读;2把web.config改写为可写
        /// 在此函数中自动去根目下寻找web.config
        /// </summary>
        /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
        /// 2007-5-9 y.xiaobin
        /// <returns></returns>
        public static bool constReadOnly(int num,string strSource)
        {
            bool _readonly = false;
            string _config = HttpContext.Current.Server.MapPath(@"~/" + strSource);
            FileInfo fi = new FileInfo(_config);
            switch (num)
            {
                case 0: _readonly = fi.IsReadOnly; break;
                case 1:
                    fi.IsReadOnly = true;
                    _readonly = true;
                    break;
                case 2:
                    {
                        fi.IsReadOnly = false;
                        _readonly = false;
                    }
                    break;
                default: throw new Exception("错误参数!");
            }

            return _readonly;

        }
        /// <summary>
        /// 保存web.config设置
        /// </summary>
        /// <param name="xmlTargetElement">关键字</param>
        /// <param name="xmlText">value</param>
        /// 2007.05.09 修改 y.xiaobin
        public static void SaveXmlElementValue(string xmlTargetElement, string xmlText)
        {
            string returnInt = null;
            string filename = "123";//HttpContext.Current.Server.MapPath("~") + @"/Web.config";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filename);
            XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
            foreach (XmlNode element in topM)
            {
                if (element.Name == "appSettings")
                {
                    XmlNodeList node = element.ChildNodes;
                    if (node.Count > 0)
                    {
                        foreach (XmlNode el in node)
                        {
                            if (el.Name == "add")
                            {
                                if (el.Attributes["key"].InnerXml == xmlTargetElement)
                                {
                                    //保存web.config数据
                                    el.Attributes["value"].Value = xmlText;
                                    //xmldoc.Save(HttpContext.Current.Server.MapPath(@"~/Web.config"));
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        returnInt = "Web.Config配置文件未配置";
                    }
                    break;
                }
                else
                {
                    returnInt = "Web.Config配置文件未配置";
                }
            }

            if (returnInt != null)
                throw new Exception(returnInt);
        }

        

        /// <summary>
        /// 删除文件夹,文件
        /// </summary>
        /// <param name="DirPath">文件夹路径</param>
        /// <param name="FilePath">文件路径</param>
        /// <returns>删除</returns>
        /// /// CreateTime:2007-03-28 Code By DengXi    
        public static void DelFile(string DirPath, string FilePath)
        {
            try
            {
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                if (System.IO.Directory.Exists(DirPath))
                {
                    System.IO.Directory.Delete(DirPath);
                }
            }
            catch {  }
        }

        /// <summary>
        /// 得到SQL语句的SiID;getstr = " and SiteID='" + NetCMS.Global.Current.SiteID + "'"
        /// </summary>
        /// <returns></returns>
        //public static string getSessionStr()
        //{
        //    string getstr = "";
        //    if (App.Global.Current.SiteID != "0") { getstr = " and SiteID='" + App.Global.Current.SiteID + "'"; }
        //    return getstr;
        //}

        /// <summary>
        /// 得到频道ID; and ChannelID='" + NetCMS.Global.Current.SiteID + "'
        /// </summary>
        /// <returns></returns>
        //public static string getCHStr()
        //{
        //    string getstr = "";
        //    if (App.Global.Current.SiteID != "0") { getstr = " and ChannelID='" + App.Global.Current.SiteID + "'"; }
        //    return getstr;
        //}

        /// <summary>
        /// 生成XML文件
        /// </summary>
        /// <param name="Ename"></param>
        //public static void saveClassXML(string Ename)
        //{
        //    StreamWriter sw = null;
        //    if (App.Global.Current.SiteID != "0")
        //    {

        //    }
        //    else
        //    {
        //        string FileName = HttpContext.Current.Server.MapPath("~/xml/Content/" + Ename + ".xml");
        //        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/Content")))
        //        {
        //            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/Content"));
        //        }
        //        sw = File.CreateText(FileName);
        //        sw.WriteLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>\r");
        //        sw.WriteLine("<rss version=\"2.0\">\r");
        //        sw.WriteLine("<channel>\r");
        //        sw.WriteLine("<item>\r");
        //        sw.WriteLine("</item>\r");
        //        sw.WriteLine("</channel>\r");
        //        sw.WriteLine("</rss>\r");
        //        sw.Flush();
        //        sw.Close(); sw.Dispose();
        //    }
        //}

        /// <summary>
        /// 保存系统参数
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="siteDomain"></param>
        /// <param name="linkTypeConfig"></param>
        /// <param name="ReviewType"></param>
        /// <param name="IndexTemplet"></param>
        /// <param name="IndexFileName"></param>
        //public static bool saveparamconfig(string siteName, string siteDomain, int linkTypeConfig, int ReviewType, string IndexTemplet, string IndexFileName, string LenSearch, string SaveIndexPage, string InsertPicPosition, string collectTF, string HistoryNum)
        //{
        //    bool sta = false;
        //    StreamWriter sw = null;
        //    if (App.Global.Current.SiteID != "0")
        //    {

        //    }
        //    else
        //    {
        //        try
        //        {
        //            string FileName = HttpContext.Current.Server.MapPath("~/xml/sys/base.config");
        //            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/sys")))
        //            {
        //                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/sys"));
        //            }
        //            sw = File.CreateText(FileName);
        //            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
        //            sw.WriteLine("<siteconfig>\r");
        //            sw.WriteLine("  <siteinfo>\r");
        //            sw.WriteLine("      <sitename>" + siteName + "</sitename>\r");
        //            sw.WriteLine("      <siteDomain>" + siteDomain + "</siteDomain>\r");
        //            sw.WriteLine("      <linkTypeConfig>" + linkTypeConfig + "</linkTypeConfig>\r");
        //            sw.WriteLine("      <ReviewType>" + ReviewType + "</ReviewType>\r");
        //            sw.WriteLine("      <IndexTemplet>" + IndexTemplet + "</IndexTemplet>\r");
        //            sw.WriteLine("      <IndexFileName>" + IndexFileName + "</IndexFileName>\r");
        //            sw.WriteLine("      <LenSearch>" + LenSearch + "</LenSearch>\r");
        //            sw.WriteLine("      <SaveIndexPage>" + SaveIndexPage + "</SaveIndexPage>\r");
        //            sw.WriteLine("      <InsertPicPosition>" + InsertPicPosition + "</InsertPicPosition>\r");
        //            sw.WriteLine("      <collectTF>" + collectTF + "</collectTF>\r");
        //            sw.WriteLine("      <HistoryNum>" + HistoryNum + "</HistoryNum>\r");
        //            sw.WriteLine("  </siteinfo>\r");
        //            sw.WriteLine("</siteconfig>\r");
        //            sw.Flush();
        //            sw.Close(); sw.Dispose();
        //            sta = true;
        //        }
        //        catch
        //        {
        //            sta = false;
        //        }
        //    }
        //    return sta;
        //}

        /// <summary>
        /// 保存分组刷新参数
        /// </summary>
        /// <param name="classlistNumber"></param>
        /// <param name="infoNumber"></param>
        /// <param name="delinfoNumber"></param>
        /// <param name="specialNumber"></param>
        /// <returns></returns>
        //public static bool saveRefreshConfig(string classlistNumber,string infoNumber,string delinfoNumber,string specialNumber)
        //{
        //    bool sta = false;
        //    StreamWriter sw = null;
        //    if (App.Global.Current.SiteID != "0")
        //    {

        //    }
        //    else
        //    {
        //        try
        //        {
        //            string FileName = HttpContext.Current.Server.MapPath("~/xml/sys/refresh.config");
        //            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/sys")))
        //            {
        //                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/sys"));
        //            }
        //            sw = File.CreateText(FileName);
        //            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
        //            sw.WriteLine("<siteconfig>\r");
        //            sw.WriteLine("  <siteinfo>\r");
        //            sw.WriteLine("      <classlistNumber>" + classlistNumber + "</classlistNumber>\r");
        //            sw.WriteLine("      <infoNumber>" + infoNumber + "</infoNumber>\r");
        //            sw.WriteLine("      <delinfoNumber>" + delinfoNumber + "</delinfoNumber>\r");
        //            sw.WriteLine("      <specialNumber>" + specialNumber + "</specialNumber>\r");
        //            sw.WriteLine("  </siteinfo>\r");
        //            sw.WriteLine("</siteconfig>\r");
        //            sw.Flush();
        //            sw.Close(); sw.Dispose();
        //            sta = true;
        //        }
        //        catch
        //        {
        //            sta = false;
        //        }
        //    }
        //    return sta;
        //}

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <returns></returns>
        public static string readparamConfig(string strTarget)
        {
            string rstr = "";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/base.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            rstr += elemList[0].InnerXml;
            return rstr;
        }

        /// <summary>
        /// 读取频道配置
        /// </summary>
        public static string readCHparamConfig(string strTarget,int ChID)
        {
            string rstr = "";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/Channel/ChParams/CH_" + ChID.ToString() + ".config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            rstr += elemList[0].InnerXml;
            return rstr;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <returns></returns>
        public static void SaveXmlConfig(string strTarget,string strValue,string strSource)
        {
            string xmlPath = HttpContext.Current.Server.MapPath("~/" + strSource);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            elemList[0].InnerXml = strValue;
            xdoc.Save(xmlPath);
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string readparamConfig(string strTarget,string type)
        {
            string rstr = "";
            if (type != null && type != string.Empty)
            {
                string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/" + type + ".config");
                FileInfo finfo = new FileInfo(xmlPath);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName(strTarget);
                rstr += elemList[0].InnerXml;
            }
            else
            {
                rstr = readparamConfig(strTarget);
            }
            return rstr;
        }

        public static void saveLogFiles(int _num, string UserNum, string Title, string Content)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            string FileName = date.Year + "-" + date.Month;
            try
            {
                FileName = HttpContext.Current.Server.MapPath("~/Logs/User-" + _num + "-") + FileName + "-" + App.Common.Input.MD5(FileName) + "-s.log";

                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
                }

                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                sw.WriteLine("IP                 :" + App.Common.Public.getUserIP() + "\r");
                sw.WriteLine("title              :" + Title + "\r");
                sw.WriteLine("content            :" + Content);
                sw.WriteLine("usernum&UserName   :" + UserNum + "\r");
                sw.WriteLine("Time               :" + System.DateTime.Now);
                sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        /// <summary>
        /// 发布日志
        /// </summary>
        /// <param name="item"></param>
        /// <param name="errorContent"></param>
        /// <param name="username"></param>
        //public static void savePublicLogFiles(string item,string errorContent,string username)
        //{
        //    StreamWriter sw = null;
        //    DateTime date = DateTime.Now;
        //    string FileName = App.Config.UIConfig.Logfilename;
        //    try
        //    {
        //        FileName = HttpContext.Current.Server.MapPath("~/Logs/public/" + FileName + "_" + date.Month + date.Day + ".log");

        //        #region 检测日志目录是否存在
        //        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
        //        {
        //            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
        //        }

        //        if (!File.Exists(FileName))
        //        {
        //            sw = File.CreateText(FileName);
        //        }
        //        else
        //        {
        //            sw = File.AppendText(FileName);
        //        }
        //        #endregion
        //        sw.WriteLine(item);
        //        sw.WriteLine(errorContent);
        //        sw.WriteLine("【UserName】" + username + "   【Time】" + System.DateTime.Now);
        //        sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
        //        sw.Flush();
        //    }
        //    finally
        //    {
        //        if (sw != null)
        //            sw.Close();
        //    }
        //}


        /// <summary> 
        /// 汉字转拼音缩写 
        /// 2004-11-30 
        /// </summary> 
        /// <param name="Input">要转换的汉字字符串</param> 
        /// <returns>拼音缩写</returns> 
        public static string GetPYString(string Input)
        {
            string ret = "";
            foreach (char c in Input)
            {
                if ((int)c >= 33 && (int)c <= 126)
                {//字母和符号原样保留 
                    ret += c.ToString();
                }
                else
                {//累加拼音声母 
                    ret += GetPYChar(c.ToString());
                }
            }
            return ret;
        }

        /// <summary> 
        /// 取单个字符的拼音声母 
        /// 2004-11-30 
        /// </summary> 
        /// <param name="c">要转换的单个汉字</param> 
        /// <returns>拼音声母</returns> 
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "A";
            if (i < 0xB2C1) return "B";
            if (i < 0xB4EE) return "C";
            if (i < 0xB6EA) return "D";
            if (i < 0xB7A2) return "E";
            if (i < 0xB8C1) return "F";
            if (i < 0xB9FE) return "G";
            if (i < 0xBBF7) return "H";
            if (i < 0xBFA6) return "G";
            if (i < 0xC0AC) return "K";
            if (i < 0xC2E8) return "L";
            if (i < 0xC4C3) return "M";
            if (i < 0xC5B6) return "N";
            if (i < 0xC5BE) return "O";
            if (i < 0xC6DA) return "P";
            if (i < 0xC8BB) return "Q";
            if (i < 0xC8F6) return "R";
            if (i < 0xCBFA) return "S";
            if (i < 0xCDDA) return "T";
            if (i < 0xCEF4) return "W";
            if (i < 0xD1B9) return "X";
            if (i < 0xD4D1) return "Y";
            if (i < 0xD7FA) return "Z";
            return "*";
        }

        /// <summary>
        /// 获得样式
        /// </summary>
        /// <param name="formName">表单名</param>
        /// <param name="Dir">xml源</param>
        /// <returns></returns>
        /// by Simplt.xie
        public static string getxmlstylelist(string formName, string Dir)
        {
            string _Str = "<select style=\"width:150px;\" class=\"form\" name=\"" + formName + "\" onchange=\"javascript:getValue(this.value);\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath(Dir);
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("stylename");
            XmlNodeList elemList1 = root.GetElementsByTagName("stylevalue");
            for (int i = 0; i < elemList.Count; i++)
            {
                string _i = (i + 1).ToString();
                if (_i.Length < 2) { _i = "0" + _i; }
                _Str += "<option value=\"" + elemList1[i].InnerXml + "\">" + (_i) + ".&nbsp;" + elemList[i].InnerXml + "</option>\r";
            }
            _Str += "</select>\r";
            return _Str;
        }

        /// <summary>
        /// 读取模型配置
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getModelContentType(string Number)
        {
            string disable = string.Empty;
            if (Number.Trim() != "999")
            {
                disable = "disabled=\"disabled\"";
            }
            string _Str = "<select " + disable + " style=\"width:310px;\" class=\"form\" name=\"channelType\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/channelbase.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("channelNumber");
            XmlNodeList elemList1 = root.GetElementsByTagName("channelName");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (Number == elemList[i].InnerXml)
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\" selected>" + elemList1[i].InnerXml + "</option>\r";
                }
                else
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\">" + elemList1[i].InnerXml + "</option>\r";
                }
            }
            _Str += "</select>\r";
            return _Str;
        }

        /// <summary>
        /// 读取模型类型默认的自定义字段
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getModelContentField(string Number)
        {
            string _Str = string.Empty;
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/channelbase.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("channelNumber");
            XmlNodeList elemList1 = root.GetElementsByTagName("channelvalue");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (Number.Trim() == elemList[i].InnerXml.Trim())
                {
                    _Str = elemList1[i].InnerXml.Trim();
                    break;
                }
                else
                {
                    continue;
                }
            }
            return _Str;
        }

        /// <summary>
        /// 读取字段配置
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static string getValueType(string Number)
        {
            string disable = string.Empty;
            if (Number.Trim() != "999")
            {
                disable = "disabled=\"disabled\"";
            }
            string _Str = "<select " + disable + " style=\"width:280px;\" class=\"form\" name=\"vType\" onchange=\"javascript:getValue(this.value);\">\r";
            string xmlPath = HttpContext.Current.Server.MapPath("~/xml/sys/channel/value.config");
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("valuetype");
            XmlNodeList elemList1 = root.GetElementsByTagName("valueName");
            string istr = string.Empty;
            for (int i = 0; i < elemList.Count; i++)
            {

                if (i < 9)
                {
                    istr = "0" + (i + 1).ToString()+".";
                }
                else
                {
                    istr = (i + 1).ToString()+".";
                }
                if (Number == elemList[i].InnerXml)
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\" selected>" + istr + elemList1[i].InnerXml + "</option>\r";
                }
                else
                {
                    _Str += "<option value=\"" + elemList[i].InnerXml + "\">" + istr + elemList1[i].InnerXml + "</option>\r";
                }
            }
            _Str += "</select>\r";
            return _Str;
        }


        /// <summary>
        /// 检查当前IP是否是受限IP
        /// </summary>
        /// <param name="LimitedIP">受限的IP，格式如:192.168.1.110|212.235.*.*|232.*.*.*</param>
        /// <returns>返回true表示IP未受到限制</returns>
        static public bool ValidateIP(string LimitedIP)
        {
            string CurrentIP = getUserIP();
            if (LimitedIP == null || LimitedIP.Trim() == string.Empty)
                return true;
            LimitedIP.Replace(".", @"\.");
            LimitedIP.Replace("*", @"[^\.]{1,3}");
            Regex reg = new Regex(LimitedIP, RegexOptions.Compiled);
            Match match = reg.Match(CurrentIP);
            return !match.Success;
        }

        /// <summary>
        /// 判断会员组
        /// </summary>
        /// <param name="uGroup"></param>
        /// <param name="nGroup"></param>
        /// <returns></returns>
        public static bool CommgetGroup(string uGroup, string nGroup)
        {
            bool gf = false;
            if (nGroup == string.Empty)
            {
                gf = true;
            }
            else
            {
                if (nGroup.IndexOf(".") > -1)
                {
                    string[] gARR = nGroup.Split(',');
                    for (int i = 0; i < gARR.Length; i++)
                    {
                        if (uGroup == gARR[i].ToString().Trim())
                        {
                            gf = true;
                        }
                    }
                }
                else
                {
                    if (uGroup == nGroup)
                    {
                        gf = true;
                    }
                }
            }
            return gf;
        }

        #region 把汉字转化成全拼音
        private static int[] pyValue = new int[]
        {
            -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
            -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
            -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
            -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
            -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
            -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
            -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
            -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
            -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
            -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
            -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
            -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
            -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
            -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
            -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
            -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
            -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
            -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
            -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
            -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
            -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
            -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
            -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
            -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
            -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
            -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
            -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
            -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
            -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
            -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
            -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
            -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
            -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
        };

            private static string[] pyName = new string[]
        {
        "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
        "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
        "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
        "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
        "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
        "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
        "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
        "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
        "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
        "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
        "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
        "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
        "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
        "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
        "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
        "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
        "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
        "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
        "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
        "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
        "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
        "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
        "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
        "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
        "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
        "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
        "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
        "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
        "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
        "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
        "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
        "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
        "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
        };

        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string ConvertE(string hzString)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    pyString += noWChar[j].ToString();
                }
            }
            return pyString;
        }
        #endregion
    }
}
