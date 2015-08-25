//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Text;

namespace NetCMS.Content.Common
{
    public class FsLog
    {
        /// <summary>
        /// 系统日志处理
        /// </summary>
        /// <param name="logt">日志类别  0常规日志  1异常日志</param>
        /// <param name="logip">用户ip地址</param>
        /// <param name="url">错误地址</param>
        /// <param name="detail">日志内容</param>
        public static void logSave(int logt, string logip, string url, string detail)
        {
            StreamWriter sw = null;
            DateTime date = DateTime.Now;
            CommStr str = new CommStr();
            string FileName = date.Year + "-" + date.Month;
            try
            {
                #region 检测日志目录是否存在
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Logs")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Logs"));
                #endregion
                switch (logt)
                {
                    case 0:
                        FileName = HttpContext.Current.Server.MapPath("~/Logs/") + FileName + "-" + NetCMS.Common.Input.MD5(FileName + "~") + "-s.log";
                        break;
                    case 1:
                        FileName = HttpContext.Current.Server.MapPath("~/Logs/") + FileName + "-" + NetCMS.Common.Input.MD5(FileName + "~") + "-e.log";
                        break;
                    default:
                        FileName = HttpContext.Current.Server.MapPath("~/Logs/") + FileName + "-" + NetCMS.Common.Input.MD5(FileName + "~") + "-s.log";
                        break;
                }
                #region 检测日志文件是否存在
                if (!File.Exists(FileName))
                    sw = File.CreateText(FileName);
                else
                {
                    sw = File.AppendText(FileName);
                }
                #endregion

                #region 向log文件中写数相关日志
                sw.WriteLine("IP        :" + logip);
                sw.WriteLine("Time      :" + date);
                sw.WriteLine("URL       :" + url);
                sw.WriteLine("");
                sw.WriteLine("Details   :" + detail);
                sw.WriteLine("------------------------------------------------------------");
                sw.WriteLine("");
                sw.Flush();
                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }


        /// <summary>
        /// SaveUserLogs 写入日志
        /// </summary>
        /// <param name="intnum">类型，0为会员日志，1为管理员日志</param>
        /// <param name="intSaveFiles">保存类型，0为只保存到数据库中，1保存到数据库和日志文件中</param>
        /// <param name="titlestr">传入的日志标题</param>
        /// <param name="ContentStr">传入的日志详细描述</param>

        public static void SaveUserLogs(int intnum, int intSaveFiles, string titlestr, string ContentStr)
        {
            NetCMS.DALFactory.INTLog fslog = NetCMS.DALFactory.DataAccess.CreateNTLog();
            int flag = fslog.Add(intnum, titlestr, ContentStr, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(), HttpContext.Current.Session["UserNum"].ToString(), HttpContext.Current.Session["SiteID"].ToString());
            if (flag < 1)
            {
              throw new Exception("意外错误：未知错误!");
            }
            else
            {
                if (intSaveFiles == 1)
                {
                    StreamWriter sw = null;
                    DateTime date = DateTime.Now;
                    CommStr str = new CommStr();
                    string FileName = date.Year + "-" + date.Month;
                    try
                    {
                        FileName = HttpContext.Current.Server.MapPath("~/Logs/User-" + intnum + "-") + FileName + "-" + NetCMS.Common.Input.MD5(FileName + "~") + "-s.log";

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

                        sw.WriteLine("IP        :" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + "\r");
                        sw.WriteLine("title     :" + titlestr + "\r");
                        sw.WriteLine("content   :" + ContentStr);
                        sw.WriteLine("usernum   :" + HttpContext.Current.Session["UserNum"] + "|||SiteID:" + HttpContext.Current.Session["SiteID"]);
                        sw.WriteLine("Time      :" + System.DateTime.Now);
                        sw.WriteLine("≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡≡\r");
                        sw.Flush();
                    }
                    finally
                    {
                        if (sw != null)
                            sw.Close();
                    }
                }
            }
        }
    }
}
