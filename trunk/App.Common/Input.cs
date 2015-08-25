//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Text;
using System.Text.RegularExpressions;
//using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Web;
using System.Configuration;

namespace App.Common
{
    public class Input
    {
        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Num">待检查数据</param>
        /// <returns></returns>
        public static bool IsInteger(string Input)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                return IsInteger(Input, true);
            }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static bool IsInteger(string Input, bool Plus)
        {
            if (Input == null)
            {
                return false;
            }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus)
                    pattern = "^[0-9]+$";
                if (Regex.Match(Input, pattern, RegexOptions.Compiled).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断输入是否为日期类型
        /// </summary>
        /// <param name="s">待检查数据</param>
        /// <returns></returns>
        public static bool IsDate(string s)
        {
            try
            {
                DateTime d = DateTime.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 过滤字符串中的html代码
        /// </summary>
        /// <param name="Str"></param>
        /// <returns>返回过滤之后的字符串</returns>
        public static string LostHTML(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "<\\/*[^<>]*>";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }

        public static string LostPage(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "\\[NT:PAGE\\/*[^<>]*\\$\\]";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return Re_Str;
        }

        public static string LostVoteStr(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "\\[NT:unLoop\\/*[^<>]*\\[\\/NT:unLoop\\]";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return Re_Str;
        }

        /// <summary>
        /// 根据新闻标题的属性设置返回设置后的标题
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="TitleColor">标题颜色</param>
        /// <param name="IsB">是否粗体</param>
        /// <param name="IsI">是否斜体</param>
        /// <param name="TitleNum">返回标题字数</param>
        /// <returns>返回设置后的标题</returns>
        public static string GetColorTitleSubStr(string Title, string TitleColor, int IsB, int IsI, int TitleNum)
        {
            string Return_title = "";
            string FormatTitle = LostHTML(Title);
            if (FormatTitle != null && FormatTitle != string.Empty)
            {
                FormatTitle = GetSubString(FormatTitle, TitleNum);
                if (IsB == 1)
                {
                    FormatTitle = "<b>" + FormatTitle + "</b>";
                }
                if (IsI == 1)
                {
                    FormatTitle = "<i>" + FormatTitle + "</i>";
                }
                if (TitleColor != null && TitleColor != string.Empty)
                {
                    FormatTitle = "<font style=\"color:" + TitleColor + ";\">" + FormatTitle + "</font>";
                }
                Return_title = FormatTitle;
            }
            return Return_title;
        }


        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num)
        {
            if (Str == null || Str == "")
                return "";
            string outstr = "";
            int n = 0;
            foreach (char ch in Str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > Num)
                    break;
                else
                    outstr += ch;
            }
            return outstr;
        }
        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <param name="Num">截取字符串后省略部分的字符串</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num, string LastStr)
        {
            return (Str.Length > Num) ? Str.Substring(0, Num) + LastStr : Str;
        }

        /// <summary>
        /// 验证字符串是否是图片路径
        /// </summary>
        /// <param name="Input">待检测的字符串</param>
        /// <returns>返回true 或 false</returns>
        public static bool IsImgString(string Input)
        {
            return IsImgString(Input, "/{@dirfile}/");
        }

        public static bool IsImgString(string Input, string checkStr)
        {
            bool re_Val = false;
            if (Input != string.Empty)
            {
                string s_input = Input.ToLower();
                if (s_input.IndexOf(checkStr.ToLower()) != -1 && s_input.IndexOf(".") != -1)
                {
                    string Ex_Name = s_input.Substring(s_input.LastIndexOf(".") + 1).ToString().ToLower();
                    if (Ex_Name == "jpg" || Ex_Name == "gif" || Ex_Name == "bmp" || Ex_Name == "png")
                    {
                        re_Val = true;
                    }
                }
            }
            return re_Val;
        }


        /// <summary>
        ///  将字符转化为HTML编码
        /// </summary>
        /// <param name="str">待处理的字符串</param>
        /// <returns></returns>
        //public static string HtmlEncode(string Input)
        //{
        //    return HttpContext.Current.Server.HtmlEncode(Input);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        //public static string HtmlDecode(string Input)
        //{
        //    return HttpContext.Current.Server.HtmlDecode(Input);
        //}

        /// <summary>
        /// URL地址编码
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        //public static string URLEncode(string Input)
        //{
        //    return HttpContext.Current.Server.UrlEncode(Input);
        //}

        /// <summary>
        /// URL地址解码
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        //public static string URLDecode(string Input)
        //{
        //    return HttpContext.Current.Server.UrlDecode(Input);
        //}

        /// <summary>
        /// 过滤字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary>
        /// 过滤特殊字符/前台会员
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToshowTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            return sb.ToString();
        }

        /// <summary>
        /// 把字符转化为文本格式
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ForTXT(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("<font", " ");
            sb.Replace("<span", " ");
            sb.Replace("<style", " ");
            sb.Replace("<div", " ");
            sb.Replace("<p", "");
            sb.Replace("</p>", "");
            sb.Replace("<label", " ");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "");
            sb.Replace("<br />", "");
            sb.Replace("<br />", "");
            sb.Replace("&lt;", "");
            sb.Replace("&gt;", "");
            sb.Replace("&amp;", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            return sb.ToString();
        }
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式

        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }



        /// <summary>
        /// 字符串加密  进行位移操作
        /// </summary>
        /// <param name="str">待加密数据</param>
        /// <returns>加密后的数据</returns>
        public static string EncryptString(string Input)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] + 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str">待解密数据</param>
        /// <returns>解密成功后的数据</returns>
        public static string NcyString(string Input)
        {
            string _temp = "";
            int _inttemp;
            char[] _chartemp = Input.ToCharArray();
            for (int i = 0; i < _chartemp.Length; i++)
            {
                _inttemp = _chartemp[i] - 1;
                _chartemp[i] = (char)_inttemp;
                _temp += _chartemp[i];
            }
            return _temp;
        }

        /// <summary>
        /// 检测含中文字符串实际长度
        /// </summary>
        /// <param name="str">待检测的字符串</param>
        /// <returns>返回正整数</returns>
        public static int NumChar(string Input)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(Input);
            int l = 0;
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63)//判断是否为汉字或全脚符号
                {
                    l++;
                }
                l++;
            }
            return l;
        }

        /// <summary>
        /// 检测是否合法日期
        /// </summary>
        /// <param name="str">待检测的字符串</param>
        /// <returns></returns>
        public static bool ChkDate(string Input)
        {
            try
            {
                DateTime t1 = DateTime.Parse(Input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 转换日期时间函数
        /// </summary>
        /// <returns></returns>        
        public static string ReDateTime()
        {
            return System.DateTime.Now.ToString("yyyyMMdd");
        }



        /// <summary>
        /// 去除字符串最后一个','号
        /// </summary>
        /// <param name="chr">:要做处理的字符串</param>
        /// <returns>返回已处理的字符串</returns>
        /// /// CreateTime:2007-03-26 Code By DengXi
        public static string CutComma(string Input)
        {
            return CutComma(Input, ",");
        }

        public static string CutComma(string Input, string indexStr)
        {
            if (Input.IndexOf(indexStr) >= 0)
                return Input.Remove(Input.LastIndexOf(indexStr));
            else
                return Input;
        }

        /// <summary>
        /// 去掉首尾P
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string RemovePor(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string TMPStr = Input;
                if (Input.ToLower().Substring(0, 3) == "<p>")
                {
                    TMPStr = TMPStr.Substring(3);
                }
                if (TMPStr.Substring(TMPStr.Length - 4) == "</p>")
                {
                    TMPStr = TMPStr.Remove(TMPStr.ToLower().LastIndexOf("</p>"));
                }
                return TMPStr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 判断参数是否合法
        /// </summary>
        /// <param name="ID">要判断的参数</param>
        /// <returns>返回已处理的字符串</returns>

        public static string checkID(string ID)
        {
            if (ID == null && ID == string.Empty)
                throw new Exception("参数传递错误!<li>参数不能为空</li>");
            return ID;
        }

        /// <summary>
        /// 去除编号字符串中的'-1'
        /// </summary>
        /// <param name="id"></param>
        /// <returns>如果为空则返回'IsNull'</returns>

        public static string Losestr(string id)
        {
            if (id == null || id == "" || id == string.Empty)
                return "IsNull";

            id = id.Replace("'-1',", "");

            if (id == null || id == "" || id == string.Empty)
                return "IsNull";
            else
                return id;
        }

        public static string FilterHTML(string html)
        {
            if (html == null)
                return "";
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
            html = regex4.Replace(html, ""); //过滤iframe
            html = regex5.Replace(html, ""); //过滤frameset
            html = regex6.Replace(html, ""); //过滤frameset
            html = regex7.Replace(html, ""); //过滤frameset
            html = regex8.Replace(html, ""); //过滤frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }
    }

    /// <summary>
    /// 加，解密相关 dxd
    /// </summary>
    public class EncryptString
    {
        private static byte[] Key64 = { 83, 11, 08, 156, 78, 4, 218, 32 };
        private static byte[] IV64 = { 50, 13, 246, 39, 20, 99, 167, 3 };
        private static byte[] Key192 = { 82, 16, 93, 156, 18, 4, 218, 32, 15, 167, 144, 80, 26, 250, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 };
        private static byte[] IV192 = { 55, 103, 246, 79, 26, 99, 167, 3, 42, 15, 162, 83, 184, 7, 209, 13, 145, 23, 200, 58, 173, 10, 121, 222 };
        public static String Encrypt(String valueString)
        {
            if (valueString != "")
            {   //定义DES的Provider
                DESCryptoServiceProvider desprovider =
                new DESCryptoServiceProvider();
                //定义内存流
                MemoryStream memoryStream = new MemoryStream();
                //定义加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                desprovider.CreateEncryptor(Key64, IV64),
                CryptoStreamMode.Write);
                //定义写IO流
                StreamWriter writerStream = new StreamWriter(cryptoStream);
                //写入加密后的字符流
                writerStream.Write(valueString);
                writerStream.Flush();
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();
                //返回加密后的字符串
                return (Convert.ToBase64String(memoryStream.GetBuffer(), 0,
                (int)memoryStream.Length));
            }
            return (null);
        }
        public static String Decrypt(String valueString)
        {
            if (valueString != "")
            {   //定义DES的Provider
                DESCryptoServiceProvider desprovider =
                new DESCryptoServiceProvider();
                //转换解密的字符串为二进制
                byte[] buffer = Convert.FromBase64String(valueString);
                //定义内存流
                MemoryStream memoryStream = new MemoryStream();
                //定义加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                desprovider.CreateEncryptor(Key64, IV64),
                CryptoStreamMode.Read);
                //定义读IO流
                StreamReader readerStream = new StreamReader(cryptoStream);
                //返回解密后的字符串
                return (readerStream.ReadToEnd());
            }
            return (null);
        }
        public static String EncryptTripleDES(String valueString)
        {
            if (valueString != "")
            {   //定义TripleDES的Provider
                TripleDESCryptoServiceProvider triprovider =
                new TripleDESCryptoServiceProvider();
                //定义内存流
                MemoryStream memoryStream = new MemoryStream();
                //定义加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                triprovider.CreateEncryptor(Key192, IV192),
                CryptoStreamMode.Write);
                //定义写IO流
                StreamWriter writerStream = new StreamWriter(cryptoStream);
                //写入加密后的字符流
                writerStream.Write(valueString);
                writerStream.Flush();
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();
                //返回加密后的字符串
                return (Convert.ToBase64String(memoryStream.GetBuffer(), 0,
                (int)memoryStream.Length));
            }
            return (null);
        }
        public static String DecryptTripleDES(String valueString)
        {
            if (valueString != "")
            {   //定义TripleDES的Provider
                TripleDESCryptoServiceProvider triprovider =
                new TripleDESCryptoServiceProvider();
                //转换解密的字符串为二进制
                byte[] buffer = Convert.FromBase64String(valueString);
                //定义内存流
                MemoryStream memoryStream = new MemoryStream();
                //定义加密流
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                  triprovider.CreateEncryptor(Key64, IV64),
                  CryptoStreamMode.Read);
                //定义读IO流
                StreamReader readerStream = new StreamReader(cryptoStream);
                //返回解密后的字符串
                return (readerStream.ReadToEnd());
            }
            return (null);
        }



        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        /// <param name="Input">待加密码字符串</param>
        /// <returns></returns>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)//16位MD5加密（取32位加密的9~25字符）
                output = output.Substring(8, 16);
            return output;
        }

        public static string MD5(string Input)
        {
            return MD5(Input, true);
        }

        /// <summary>
        /// MD5变种加密
        /// </summary>
        /// <param name="_Pw">原密钥</param>
        /// <returns>32位加密后的密钥</returns>
        public static string mutMd5(string _Pw)
        {
            string _password = "";

            MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(_Pw.ToLower()));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            string entry = sBuilder.ToString();

            for (int i = 0; i < sBuilder.Length; i++)
            {
                if (i % 2 == 0)
                {
                    _password += sBuilder[i];
                }
            }

            for (int i = 0; i < sBuilder.Length; i++)
            {
                if (i % 2 == 1)
                {
                    _password += sBuilder[i];
                }
            }

            return _password;
        }
    }

    
}
