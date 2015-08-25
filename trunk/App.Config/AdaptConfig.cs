//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NetCMS.Config
{
    public class AdaptConfig
    {
        private bool _isAdapt;
        /// <summary>
        /// 是否开启整合
        /// </summary>
        public bool isAdapt
        {
            set { _isAdapt = value; }
            get { return _isAdapt; }
        }       
        private string _adaptKey;
        /// <summary>
        /// 整合密码key
        /// </summary>
        public string adaptKey
        {
            set { _adaptKey = value; }
            get { return _adaptKey; }
        }        
        private string _adaptPath;
        /// <summary>
        /// 请求页面地址
        /// </summary>
        public string adaptPath
        {
            set { _adaptPath = value; }
            get { return _adaptPath; }
        }
        /// <summary>
        /// 构造函数，为字段赋初值
        /// </summary>
        public AdaptConfig(string xmlName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();                
                xmlDoc.Load(xmlName);
                XmlNode xn = xmlDoc.SelectSingleNode("adapt");
                XmlElement xeIsAdapt = (XmlElement)xn.SelectSingleNode("isAdapt");
                XmlElement xeAdaptKey = (XmlElement)xn.SelectSingleNode("adaptKey");
                XmlElement xePagePath = (XmlElement)xn.SelectSingleNode("adaptPath");
                if (xeIsAdapt.InnerText.ToUpper() == "TRUE")
                {
                    _isAdapt = true;
                }
                else
                {
                    _isAdapt = false;
                }
                _adaptKey = xeAdaptKey.InnerText;
                _adaptPath = xePagePath.InnerText;                    
            }
            catch
            {
                //
            }
        }
        /// <summary>
        /// 更新AdaptConfig
        /// </summary>
        /// <returns></returns>
        public bool saveAdaptConfig(string xmlName)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();                
                xmlDoc.Load(xmlName);
                XmlNode xn = xmlDoc.SelectSingleNode("adapt");
                XmlElement xeIsAdapt = (XmlElement)xn.SelectSingleNode("isAdapt");
                XmlElement xeAdaptKey = (XmlElement)xn.SelectSingleNode("adaptKey");
                XmlElement xePagePath = (XmlElement)xn.SelectSingleNode("adaptPath");
                if (_isAdapt)
                {
                    xeIsAdapt.InnerText = "true";
                }
                else
                {
                    xeIsAdapt.InnerText = "false";
                }
                xeAdaptKey.InnerText = _adaptKey;
                xePagePath.InnerText = _adaptPath;
                xmlDoc.Save(xmlName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
