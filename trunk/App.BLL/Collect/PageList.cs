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
    public class PageList : Page
    {
        private string _List;
        private ArrayList _NewsUrlList;
        private string listrule = "";
        private string linkrule = "";
        public string RuleOfList
        {
            set { listrule = value; }
        }
        public string RuleOfLink
        {
            set { linkrule = value; }
        }
        public PageList(string url) : base(url) { }
        public PageList(string url, string encode) : base(url, encode) { }
        public void FigureList()
        {
            if (listrule == null || listrule.IndexOf("[列表内容]") < 0)
                throw new Exception("列表内容规则没有设定");
            _List = "";
            if (_Doc.Equals(""))
            {
                if (!this.Fetch())
                    throw new Exception(_Error);
            }
            Match m = Utility.GetMatch(_Doc, listrule, "[列表内容]");
            while (m.Success)
            {
                _List += m.Groups["TARGET"].Value;
                m = m.NextMatch();
            }
        }
        public string List
        {
            get
            {
                return _List;
            }
            set
            {
                _List = value;
            }
        }
        public void FigureNewsUrls()
        {
            if (linkrule == null || linkrule.IndexOf("[列表URL]") < 0)
                throw new Exception("列表URL规则没有设定");
            _NewsUrlList = new ArrayList();
            _NewsUrlList.Clear();
            Match m = Utility.GetMatchUrl(_List, linkrule, "[列表URL]");
            while (m.Success)
            {
                _NewsUrlList.Add(Utility.StickUrl(_Url, m.Groups["TARGET"].Value));
                m = m.NextMatch();
            }
        }
        public string[] NewsUrl
        {
            get
            {
                if (_NewsUrlList == null || _NewsUrlList.Count < 1)
                    return null;
                else
                    return (string[])_NewsUrlList.ToArray(typeof(string));
            }
        }
        public string[] Pagination(string profile, int total)
        {
            string[] result = new string[total];
            GetOtherPage(this._Url, _Doc, profile, ref result, total, 0);
            return result;
        }
        private void GetOtherPage(string otherurl, string PageDoc, string pattern, ref string[] r, int total, int n)
        {
            Match m = Utility.GetMatchUrl(PageDoc, pattern, "[其他页面]");
            if (m.Success)
            {
                string obturl = Utility.StickUrl(otherurl, m.Groups["TARGET"].Value);
                if (!obturl.Trim().Equals(otherurl.Trim()))
                {
                    PageList pglst = new PageList(obturl, _Encode);
                    ArrayList arraylist = GetListUrl(pglst);
                    if (arraylist != null && arraylist.Count > 0)
                    {
                        int len = arraylist.Count;
                        int j = len + n;
                        if (j < total)
                            arraylist.CopyTo(0, r, n, len);
                        else
                        {
                            arraylist.CopyTo(0, r, n, total - n);
                            return;
                        }
                        n = j;
                    }
                    if (n < total)
                    {
                        GetOtherPage(obturl, pglst._Doc, pattern, ref r, total, n);
                    }
                }
            }
        }
        public string[] SinglePagination(string profile, int total)
        {
            string[] result = new string[total];
            int n = 0;
            Match m = Utility.GetMatchUrl(_Doc, profile, "[其他页面]");
            while (m.Success)
            {
                if (n >= total)
                    break;
                string otherurl = Utility.StickUrl(_Url, m.Groups["TARGET"].Value);
                if (!otherurl.Trim().Equals(this._Url.Trim()))
                {
                    PageList pglst = new PageList(otherurl, _Encode);
                    ArrayList arraylist = GetListUrl(pglst);
                    if (arraylist != null && arraylist.Count > 0)
                    {
                        int len = arraylist.Count;
                        int j = len + n;
                        if (j < total)
                            arraylist.CopyTo(0, result, n, len);
                        else
                        {
                            arraylist.CopyTo(0, result, n, total - n);
                            break;
                        }
                        n = j;
                    }
                }
                m = m.NextMatch();
            }
            return result;
        }
        public string[] IndexPagination(string profile, int min, int max, int total)
        {
            string[] result = new string[total];
            int n = 0;
            for (int i = min; i <= max; i++)
            {
                if (n >= total)
                    break;
                string otherurl = profile.Replace("[页码]", i.ToString());
                if (!otherurl.Trim().Equals(this._Url.Trim()))
                {
                    PageList pglst = new PageList(otherurl, _Encode);
                    ArrayList arraylist = GetListUrl(pglst);
                    if (arraylist != null && arraylist.Count > 0)
                    {
                        int len = arraylist.Count;
                        int j = len + n;
                        if (j < total)
                            arraylist.CopyTo(0, result, n, len);
                        else
                        {
                            arraylist.CopyTo(0, result, n, total - n);
                            break;
                        }
                        n = j;
                    }
                }
            }
            return result;
        }

        private ArrayList GetListUrl(PageList pl)
        {
            if (pl == null)
                return null;
            pl.linkrule = this.linkrule;
            pl.listrule = this.listrule;
            if (!pl.Fetch())
            {
                return null;
            }
            pl.FigureList();
            pl.FigureNewsUrls();
            return pl._NewsUrlList;
        }
    }
}
