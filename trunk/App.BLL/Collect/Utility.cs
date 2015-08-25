//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace NetCMS.Content.Collect
{
    public class Utility
    {
        /// <summary>
        /// 取得网页的内容
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="sEncode">编码名称</param>
        /// <param name="sDocument">返回的网页内容或者是异常</param>
        /// <returns>有异常返回false</returns>
        public static string GetPageContent(Uri Url, string sEncode)
        {
            try
            {

                Encoding encoding = System.Text.Encoding.GetEncoding(sEncode);
                return GetPageContent(Url, encoding);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 取得网页的内容
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="sDocument">返回的网页内容或者是异常</param>
        /// <returns>有异常返回false</returns>
        public static string GetPageContent(Uri Url, Encoding encoding)
        {
            WebClient webclient = new WebClient();
            try
            {

                webclient.Encoding = encoding;
                return webclient.DownloadString(Url);
            }
            catch (WebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                webclient.Dispose();
            }
        }
        /// <summary>
        /// 处理URL地址，当BranchUrl为一个全名的URL时则返回本身，否则恰当的衔接到BaseUrl后面
        /// </summary>
        /// <param name="BaseUrl">完整的URL</param>
        /// <param name="BranchUrl">分支URL</param>
        /// <returns></returns>
        public static string StickUrl(string BaseUrl, string BranchUrl)
        {
            if (Regex.Match(BranchUrl, @"^(http|https|ftp|rtsp|mms)://", RegexOptions.IgnoreCase | RegexOptions.Compiled).Success)
            {
                return BranchUrl;
            }
            else
            {
                BaseUrl = BaseUrl.Replace("\\", "/");
                BranchUrl = BranchUrl.Replace("\\", "/");
                //2007-09-27 ken暂时修改
                if (NetCMS.Common.Input.GetSubString(BranchUrl, 1).ToString() == "/")
                {
                    return GetLastUrl(BaseUrl, BranchUrl);
                }
                //--------------------------
                BranchUrl = BranchUrl.TrimStart('/');
                if (BranchUrl.IndexOf("../") != 0)
                {
                    return UrlPlus(BaseUrl, BranchUrl);
                }
                else
                {
                    if (Regex.Match(BaseUrl, @"/$", RegexOptions.Compiled).Success)
                    {
                        BaseUrl = BaseUrl.TrimEnd('/');
                    }
                    else if (Regex.Match(BaseUrl, @"/[^\./]+\.[^/]+$", RegexOptions.Compiled).Success)
                    {
                        BaseUrl = Regex.Replace(BaseUrl, @"/[^\./]+\.[^/]+$", "", RegexOptions.Compiled);
                    }
                    while (BranchUrl.IndexOf("../") >= 0)
                    {
                        BranchUrl = Regex.Replace(BranchUrl, @"^\.\./", "", RegexOptions.Compiled);
                        BaseUrl = Regex.Replace(BaseUrl, @"/[^/]*$", "", RegexOptions.Compiled);
                    }
                    return BaseUrl + "/" + BranchUrl;
                }
            }
        }
        //--------------------------------
        private static string GetLastUrl(string BaseUrl, string BranchUrl)
        {
            BranchUrl = BranchUrl.TrimStart('/');
            string Star_url = "";
            string End_Url = BaseUrl;
            if (BaseUrl.IndexOf("//") > 0)
            {
                BaseUrl = BaseUrl.Replace("//", "|");
                string[] Url_Arr = BaseUrl.Split('|');
                Star_url = Url_Arr[0].ToString();
                End_Url = Url_Arr[1].ToString();
            }
            if (End_Url.IndexOf("/") > 0)
            {
                string[] End_Arr = End_Url.Split('/');
                End_Url = End_Arr[0].ToString();
                if (Star_url != string.Empty)
                {
                    return Star_url + "//" + End_Url + "/" + BranchUrl;
                }
                else
                {
                    return End_Url + "/" + BranchUrl;
                }
            }
            else
            {
                if (Star_url != string.Empty)
                {
                    return Star_url + "//" + End_Url + "/" + BranchUrl;
                }
                else
                {
                    return End_Url + "/" + BranchUrl;
                }
            }
        }



        private static string UrlPlus(string front, string tail)
        {
            if (Regex.Match(front, "(http|https|ftp|rtsp|mms)://[^/]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                return front + "/" + tail;
            }
            else if (Regex.Match(front, "(http|https|ftp|rtsp|mms)://[^/]+/$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                return front + tail;
            }
            else if (Regex.Match(front, "(http|https|ftp|rtsp|mms)://.+/$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                return front + tail;
            }
            else if (Regex.Match(front, @"/[^/\.]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                return front + "/" + tail;
            }
            else if (Regex.Match(front, @"/[^/\.]+\.[^/]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                return Regex.Replace(front, @"/[^/\.]+\.[^/]+$", "", RegexOptions.IgnoreCase | RegexOptions.Compiled) + "/" + tail;
            }
            else
            {
                return front + "/" + tail;
            }
        }
        /// <summary>
        /// 获取一个目标的匹配结果
        /// </summary>
        /// <param name="input">要匹配的字符串</param>
        /// <param name="pattern"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static Match GetMatch(string input, string pattern, string find)
        {
            string _pattn = Regex.Escape(pattern);
            _pattn = _pattn.Replace(@"\[变量]", @"[\s\S]*?");
            _pattn = Regex.Replace(_pattn, @"((\\r\\n)|(\\ ))+", @"\s*", RegexOptions.Compiled);
            if (Regex.Match(pattern.TrimEnd(), Regex.Escape(find) + "$", RegexOptions.Compiled).Success)
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+)");
            else
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+?)");
            Regex r = new Regex(_pattn, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = r.Match(input);
            return m;
        }
        /// <summary>
        /// 按严格的匹配方式获取一个目标的匹配结果
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static Match GetMatchRigid(string input, string pattern, string find)
        {
            string _pattn = Regex.Escape(pattern);
            _pattn = _pattn.Replace(@"\[变量]", @"[\s\S]*?");
            if (Regex.Match(pattern.TrimEnd(), Regex.Escape(find) + "$", RegexOptions.Compiled).Success)
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+)");
            else
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[\s\S]+?)");
            Regex r = new Regex(_pattn, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = r.Match(input);
            return m;
        }
        /// <summary>
        /// 匹配超级链接地址
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static Match GetMatchUrl(string input, string pattern, string find)
        {
            string _pattn = Regex.Escape(pattern);
            _pattn = _pattn.Replace(@"\[变量]", @"[\s\S]*?");
            if (Regex.Match(pattern.TrimEnd(), Regex.Escape(find) + "$", RegexOptions.Compiled).Success)
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[^'""\ >]+)");
            else
                _pattn = _pattn.Replace(@"\" + find, @"(?<TARGET>[^'""\ >]+?)");
            Regex r = new Regex(_pattn, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = r.Match(input);
            return m;
        }
    }
    /*
   // The RequestState class passes data across async calls.
   public class RequestState
   {
       const int BufferSize = 1024;
       public StringBuilder RequestData;
       public byte[] BufferRead;
       public WebRequest Request;
       public Stream ResponseStream;
       // Create Decoder for appropriate enconding type.
       public Decoder StreamDecode = Encoding.UTF8.GetDecoder();

       public RequestState()
       {
           BufferRead = new byte[BufferSize];
           RequestData = new StringBuilder(String.Empty);
           Request = null;
           ResponseStream = null;
       }
   }

   // ClientGetAsync issues the async request.
   class ClientGetAsync
   {
       public static ManualResetEvent allDone = new ManualResetEvent(false);
       const int BUFFER_SIZE = 1024;
       public static void BeginReq(string[] HttpURL)
       {
           foreach (string singleurl in HttpURL)
           {
               try
               {
                   Uri httpSite = new Uri(singleurl);
                   HttpWebRequest wreq = (HttpWebRequest)WebRequest.Create(httpSite);
                   RequestState rs = new RequestState();
                   rs.Request = wreq;
                   IAsyncResult r = (IAsyncResult)wreq.BeginGetResponse(new AsyncCallback(RespCallback), rs);
                   allDone.WaitOne();
                   wreq.EndGetResponse(r);
               }
               catch (WebException e)
               { }
               catch (Exception e)
               { }
           }
       }
       private static void RespCallback(IAsyncResult ar)
       {
           RequestState rs = (RequestState)ar.AsyncState;
           WebRequest req = rs.Request;
           WebResponse resp = req.EndGetResponse(ar);
           Stream ResponseStream = resp.GetResponseStream();
           rs.ResponseStream = ResponseStream;
           IAsyncResult iarRead = ResponseStream.BeginRead(rs.BufferRead, 0,
              BUFFER_SIZE, new AsyncCallback(ReadCallBack), rs);
       }


       private static void ReadCallBack(IAsyncResult asyncResult)
       {
           RequestState rs = (RequestState)asyncResult.AsyncState;
           Stream responseStream = rs.ResponseStream;
           int read = responseStream.EndRead(asyncResult);
           if (read > 0)
           {
               Char[] charBuffer = new Char[BUFFER_SIZE];
               int len =
                  rs.StreamDecode.GetChars(rs.BufferRead, 0, read, charBuffer, 0);
               String str = new String(charBuffer, 0, len);
               rs.RequestData.Append(
                  Encoding.ASCII.GetString(rs.BufferRead, 0, read));
               IAsyncResult ar = responseStream.BeginRead(
                  rs.BufferRead, 0, BUFFER_SIZE,
                  new AsyncCallback(ReadCallBack), rs);
           }
           else
           {
               if (rs.RequestData.Length > 0)
               {
                   string strContent;
                   strContent = rs.RequestData.ToString();
               }
               responseStream.Close();
               allDone.Set();
           }
           return;
       }
   }*/

}
