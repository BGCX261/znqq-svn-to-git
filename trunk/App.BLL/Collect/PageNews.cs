//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;

namespace NetCMS.Content.Collect
{
    public class PageNews : Page
    {
        #region 私有变量
        private string _title;
        private string _content;
        private string _author;
        private string _source;
        private string _titlerule = null;
        private string _contentrule = null;
        private string otherpgcon = "";
        private DateTime _addtime;
        #endregion 私有变量
        public PageNews(string url) : base(url) { }
        public PageNews(string url, string encode) : base(url, encode) { }
        public string Title
        { get { return _title; } }
        public string Content
        { get { return _content; } set { _content = value; } }
        public string Author { get { return _author; } }
        public string Source { get { return _source; } }
        public DateTime AddTime { get { return _addtime; } }
        public string RuleOfTitle
        {
            set { _titlerule = value; }
        }
        public string RuleOfContent
        {
            set { _contentrule = value; }
        }
        /// <summary>
        /// 过滤数据，提取作者
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="settled"></param>
        public void FigureAuthor(string pattern, bool settled)
        {
            if (!settled && pattern.IndexOf("[作者]") >= 0)
            {
                Match m = Utility.GetMatchRigid(_Doc, pattern, "[作者]");
                if (m.Success)
                {
                    _author = m.Groups["TARGET"].Value;
                }
            }
            else
                _author = pattern;
        }
        public void FigureSource(string pattern, bool settled)
        {
            if (!settled && pattern.IndexOf("[来源]") >= 0)
            {
                Match m = Utility.GetMatchRigid(_Doc, pattern, "[来源]");
                if (m.Success)
                {
                    _source = m.Groups["TARGET"].Value;
                }
            }
            else
            {
                _source = pattern;
            }
        }
        public void FigureAddTime(string pattern, bool settled)
        {
            string tm = "";
            if (!settled && pattern.IndexOf("[加入时间]") >= 0)
            {
                Match m = Utility.GetMatchRigid(_Doc, pattern, "[加入时间]");
                if (m.Success)
                {
                    tm = m.Groups["TARGET"].Value;
                }
            }
            else
            {
                tm = pattern;
            }
            try
            {
                _addtime = DateTime.Parse(tm);
            }
            catch
            {
                _addtime = DateTime.Now;
            }
        }
        public void FigureTitle()
        {
            if (_titlerule == null || _titlerule.IndexOf("[标题]") < 0)
                throw new Exception("采集新闻标题规则还没有设定!");
            Match m = Utility.GetMatchRigid(_Doc, _titlerule, "[标题]");
            if (m.Success)
            {
                _title = m.Groups["TARGET"].Value;
            }
        }
        public void FigureContent()
        {
            if (_contentrule == null || _contentrule.IndexOf("[内容]") < 0)
                throw new Exception("采集新闻内容规则还没有设定!");
            Match m = Utility.GetMatch(_Doc, _contentrule, "[内容]");
            if (m.Success)
            {
                _content = m.Groups["TARGET"].Value;
            }
        }
        private void FilterHtml(string element, int type)
        {
            string pattern = "";
            switch (type)
            {
                case 0:
                    pattern = element + "\\s?=\\s?(['\"][^'\"]*?['\"]|[^'\"]\\S*)";
                    break;
                case 1:
                    pattern = "<" + element + "[^>]*>|</" + element + ">";
                    break;
                case 2:
                    pattern = "<(?<tag>" + element + @")[^>]*>[\s\S]*</\k<tag>>";
                    break;
                default:
                    return;
            }
            try
            {
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                _content = reg.Replace(_content, "");
            }
            catch
            { }
        }
        public void Replace(string profile, string newstr, bool bIgnoreCase)
        {
            string pattern = Regex.Escape(profile);
            string instead = newstr.Replace("$", "$$");
            pattern = pattern.Replace(@"\[变量]", @"[\s\S]*?");
            string[] _pattern = pattern.Split(new char[] { '[', '过', '滤', '字', '符', '串', ']' }, StringSplitOptions.RemoveEmptyEntries);
            string p = "";
            string n = "";
            for (int i = 0; i < _pattern.Length; i++)
            {
                string s = _pattern[i];
                if (!s.Equals(""))
                {
                    p += "(?<ch" + i + ">" + s + @")[\s\S]+?";
                    n += "${ch" + i + "}" + instead;
                }
            }
            Regex reg;
            if (bIgnoreCase)
                reg = new Regex(p, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            else
                reg = new Regex(p, RegexOptions.Compiled);
            _content = reg.Replace(_content, n);
        }
        public void Filter(bool ridhtml, bool ridstyle, bool riddiv, bool rida, bool ridclass, bool ridfont, bool ridspan, bool ridobject, bool ridiframe, bool ridscript)
        {
            //if(ridhtml);
            if (ridstyle || ridhtml)
                FilterHtml("style", 0);
            if (riddiv || ridhtml)
                FilterHtml("div", 1);
            if (rida || ridhtml)
                FilterHtml("a", 1);
            if (ridclass || ridhtml)
                FilterHtml("class", 0);
            if (ridfont || ridhtml)
                FilterHtml("font", 1);
            if (ridspan || ridhtml)
                FilterHtml("span", 1);
            if (ridobject || ridhtml)
                FilterHtml("object", 2);
            if (ridiframe || ridhtml)
                FilterHtml("iframe", 2);
            if (ridscript || ridhtml)
                FilterHtml("script", 2);
        }
        public string GetOtherPagination(string profile)
        {
            otherpgcon = "";
            GetOtherPage(_Url, _Doc, profile);
            return otherpgcon;
        }
        private void GetOtherPage(string otherurl, string PageDoc, string pattern)
        {
            Match m = Utility.GetMatchUrl(PageDoc, pattern, "[分页新闻]");
            if (m.Success)
            {
                string obturl = Utility.StickUrl(otherurl, m.Groups["TARGET"].Value);
                if (!obturl.Trim().Equals(otherurl.Trim()))
                {
                    PageNews pgns = new PageNews(obturl, _Encode);
                    pgns.RuleOfContent = this._contentrule;
                    if (pgns.Fetch())
                    {
                        pgns.FigureContent();
                        otherpgcon += pgns.Content;
                        GetOtherPage(obturl, pgns._Doc, pattern);
                    }
                }
            }
        }
        public string GetIndexPagination(string profile)
        {
            string OtherContent = "";
            Match m = Utility.GetMatchUrl(_Doc, profile, "[分页新闻]");
            while (m.Success)
            {
                string otherurl = Utility.StickUrl(_Url, m.Groups["TARGET"].Value);
                if (!otherurl.Trim().Equals(this._Url))
                {
                    PageNews pgns = new PageNews(otherurl, _Encode);
                    if (pgns.Fetch())
                    {
                        pgns.FigureContent();
                        OtherContent += pgns.Content;
                    }
                }
                m = m.NextMatch();
            }
            return OtherContent;
        }
        public string AllDocument
        {
            get { return _Doc; }
        }
    }
}
