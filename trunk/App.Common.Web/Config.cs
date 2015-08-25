using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace App.Common.Web
{
    public class Config
    {

    }

    public class KName
    {
        //key
        public static readonly string K_NAME = ConfigurationManager.AppSettings["KeyName"];
        public static readonly string K_USR_NO = ConfigurationManager.AppSettings["KeyUno"];
        public static readonly string K_USR_NAME = ConfigurationManager.AppSettings["KeyUname"];
        public static readonly string K_USR_CODE = ConfigurationManager.AppSettings["KeyUcode"];
        public static readonly string K_SIT_NO = ConfigurationManager.AppSettings["KeySno"];
        public static readonly string K_SIT_NAME = ConfigurationManager.AppSettings["KeySname"];
        public static readonly string K_MECH_ID = ConfigurationManager.AppSettings["KeyMechId"];
        public static readonly string K_MECH_NAME = ConfigurationManager.AppSettings["KeyMechName"];
        public static readonly string K_DEPT_ID = ConfigurationManager.AppSettings["KeyDeptId"];
        public static readonly string K_DEPT_NAME = ConfigurationManager.AppSettings["KeyDeptName"];
        public static readonly string K_WH_NO = "WH_NO";
        public static readonly string K_WH_NAME = "WH_NAME";
        public static readonly string K_USR_ISSUPER = "ISSUPER";
        public static readonly string K_MECH_TYPE = "MECH_TYPE";

        //model
        public static readonly string M_ORDER = ConfigurationManager.AppSettings["KeyOrderCode"];//模块名称 订货
        public static readonly string M_RET = ConfigurationManager.AppSettings["KeyRetCode"];//模块名称 零售
        public static readonly string M_PT = ConfigurationManager.AppSettings["KeyPtCode"];//模块名称 盘点
        public static readonly string M_IND = ConfigurationManager.AppSettings["KeyIndCode"];//模块名称 其它入库单
        public static readonly string M_OUTD = ConfigurationManager.AppSettings["KeyOutdCode"];//模块名称 其它入库单
        public static readonly string M_MEB_QUERY = ConfigurationManager.AppSettings["KeyMebQuery"];//会员档案
        public static readonly string M_MEB_REVISIT = ConfigurationManager.AppSettings["KeyMebRevisit"];//会员消费情况 回访
        public static readonly string M_ALO = ConfigurationManager.AppSettings["KeyAloCode"];//模块名称 调拔单
        public static readonly string M_REDEEM = ConfigurationManager.AppSettings["KeyRedeemCode"];//模块名称 积分兑换单
        public static readonly string M_CSMT = ConfigurationManager.AppSettings["KeyCsmtCode"];//模块名称 代销单
        public static readonly string M_RCSMT = ConfigurationManager.AppSettings["KeyRcsmtCode"];//模块名称 代销退回单
        public static readonly string M_RRET = ConfigurationManager.AppSettings["KeyRretCode"];//模块名称 零售退回单
        public static readonly string M_RORDER = ConfigurationManager.AppSettings["KeyRorderCode"];//模块名称 订货退回单
        public static readonly string M_PUR = ConfigurationManager.AppSettings["KeyPurCode"];//模块名称 采购单
        public static readonly string M_RPUR = ConfigurationManager.AppSettings["KeyRpurCode"];//模块名称 采购退回单

        //cache 明细
        public static readonly string C_TF_ORDER = "TF_ORDER"; //订货
        public static readonly string C_TF_RET = "TF_RET";//零售
        public static readonly string C_TF_IND = "TF_IND"; //其他入库
        public static readonly string C_TF_OUTD = "TF_OUTD";//其他出库
        public static readonly string C_TF_ALO = "TF_ALO";//调拔 
        public static readonly string C_TF_REDEEM = "TF_REDEEM";//积分兑换
        public static readonly string C_TF_CSMT = "TF_CSMT";//代销
        public static readonly string C_TF_RCSMT = "TF_RCSMT";//代销退回
        public static readonly string C_TF_RRET = "TF_RRET";//零售退回
        public static readonly string C_TF_RORDER = "TF_RORDER";//订货退回
        public static readonly string C_TF_PUR = "TF_PUR";//采购单
        public static readonly string C_TF_RPUR = "TF_RPUR";//采购退回单

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

    public class CookieEncrypt
    {
        public static void SetCookie(HttpCookie cookie)
        {   //设置Cookie 
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        public static void SetCookie(String key, String valueString)
        {   //设置加密后的Cookie 
            key = HttpContext.Current.Server.UrlEncode(key);
            valueString = HttpContext.Current.Server.UrlEncode(valueString);
            HttpCookie cookie = new HttpCookie(key, valueString);
            SetCookie(cookie);
        }
        public static void SetCookie(String key, String valueString, DateTime expires)
        {   //设置加密后的Cookie，并设置Cookie的有效时间 
            key = HttpContext.Current.Server.UrlEncode(key);
            valueString = HttpContext.Current.Server.UrlEncode(valueString);
            HttpCookie cookie = new HttpCookie(key, valueString);
            cookie.Expires = expires;
            SetCookie(cookie);
        }
        public static void SetTripleDESEncryptedCookie(String key, String valueString)
        {   //设置使用TripleDES加密后的Cookie 
            key = EncryptString.EncryptTripleDES(key);
            valueString = EncryptString.EncryptTripleDES(valueString);
            SetCookie(key, valueString);
        }
        public static void SetTripleDESEncryptedCookie(String key, String valueString, DateTime expires)
        {   //设置使用TripleDES加密后的Cookie，并设置Cookie的有效时间 
            key = EncryptString.EncryptTripleDES(key);
            valueString = EncryptString.EncryptTripleDES(valueString);
            SetCookie(key, valueString, expires);
        }

        public static void SetEncryptedCookie(String key, String valueString)
        {   //设置使用DES加密后的Cookie 
            key = EncryptString.Encrypt(key);
            valueString = EncryptString.Encrypt(valueString);
            SetCookie(key, valueString);
        }
        public static void SetEncryptedCookie(String key, String valueString, DateTime expires)
        {   //设置使用DES加密后的Cookie，并设置Cookie的有效时间 
            key = EncryptString.Encrypt(key);
            valueString = EncryptString.Encrypt(valueString);
            SetCookie(key, valueString, expires);
        }
        public static String GetTripleDESEncryptedCookieValue(String key)
        {   //获取使用TripleDES解密后的Cookie 
            key = EncryptString.EncryptTripleDES(key);
            String valueString = GetCookieValue(key);
            valueString = EncryptString.DecryptTripleDES(valueString);
            return (valueString);
        }
        public static String GetEncryptedCookieValue(String key)
        {   //获取使用DES解密后的Cookie 
            key = EncryptString.Encrypt(key);
            String valueString = GetCookieValue(key);
            valueString = EncryptString.Decrypt(valueString);
            return (valueString);
        }
        public static HttpCookie GetCookie(String key)
        { //通过关键字获取Cookie 
            key = HttpContext.Current.Server.UrlEncode(key);
            return (HttpContext.Current.Request.Cookies.Get(key));
        }
        public static String GetCookieValue(String key)
        {   //通过关键字获取Cookie的value 
            String valueString = GetCookie(key).Value;
            valueString = HttpContext.Current.Server.UrlDecode(valueString);
            return (valueString);
        }
    }

    public class Input
    {
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
    }
}
