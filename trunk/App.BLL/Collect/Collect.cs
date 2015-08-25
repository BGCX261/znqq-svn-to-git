//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.IO;
using System.Data;
using System.Net;
using System.Text;
using NetCMS.Model;
using NetCMS.Control;

namespace NetCMS.Content.Collect
{
    /// <summary>
    /// 采集类
    /// </summary>
    public class Collect
    {
        private NetCMS.DALFactory.ICollect dal;
        private string ErrorMsg = "";
        private bool _ShowProGressBar;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Collect()
        {
            _ShowProGressBar = true;
            dal = NetCMS.DALFactory.DataAccess.CreateCollect();
        }
        #region 采集入库
        /// <summary>
        /// 是否保存远程图片
        /// </summary>
        private bool bSaveRemotePic = false;
        private string PicSavePath = "";
        private string PicSaveUrl = "";
        /// <summary>
        /// 是否在采集时显示进度条，默认为true
        /// </summary>
        public bool ShowProGressBar
        {
            set { _ShowProGressBar = value; }
            get { return _ShowProGressBar; }
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="folderid">目录名称</param>
        /// <param name="num">采集数量</param>
        public void Collecting(int folderid, int num, bool bnorepeat)
        {
            if (ShowProGressBar) HProgressBar.Start("正在读取列表数据");
            DataTable tb = GetSite(folderid);
            #region 检查数据是否完整
            if (tb == null || tb.Rows.Count < 1)
            {
                if (ShowProGressBar) HProgressBar.Roll("没有找到该站点的相关记录!", 0);
                return;
            }
            DataRow r = tb.Rows[0];
            if (r.IsNull("LinkSetting") || r.IsNull("PageTitleSetting") || r.IsNull("PagebodySetting"))
            {
                if (ShowProGressBar) HProgressBar.Roll("相关的参数没有设置,无法取得新闻列表!", 0);
                return;
            }
            if (bool.Parse(r["SaveRemotePic"].ToString()))
            {
                #region 远程图片
                string rtpath = NetCMS.Config.UIConfig.dirFile;
                if (rtpath == null || rtpath.Trim().Equals(""))
                {
                    if (ShowProGressBar) HProgressBar.Roll("没有找到管理员附件目录!", 0);
                    return;
                }
                string dtpath = DateTime.Now.ToString("yyyyMMdd");
                PicSavePath = NetCMS.Common.ServerInfo.GetRootPath().TrimEnd('\\') + @"\" + rtpath + @"\RemoteFiles\" + dtpath;
                if (!Directory.Exists(PicSavePath))
                    Directory.CreateDirectory(PicSavePath);
                PicSaveUrl = NetCMS.Publish.CommonData.getUrl() + "/" + rtpath + "/RemoteFiles/" + dtpath;
                bSaveRemotePic = true;
                #endregion
            }
            #endregion 检查数据是否完整
            if (ShowProGressBar) HProgressBar.Roll("正在获取新闻列表页", 0);

            string sListUrl = r["objURL"].ToString();
            string sEncode = r["Encode"].ToString();
            bool bReverse = bool.Parse(r["IsReverse"].ToString());
            string listset = @"<body[^>]*>(?<list>[\s\S]+?)</body>";
            if (!r.IsNull("ListSetting"))
                listset = r["ListSetting"].ToString();
            PageList PL = new PageList(r["objURL"].ToString(), r["Encode"].ToString());
            PL.RuleOfList = listset;
            PL.RuleOfLink = r["LinkSetting"].ToString();
            string[] NewsUrl = GetNewsList(PL);
            if (NewsUrl == null)
            {
                if (ShowProGressBar) HProgressBar.Roll("没有找到相关新闻链接地址!", 0);
                return;
            }
            int len = NewsUrl.Length;
            if (len < num)
            {
                int pagetype = int.Parse(r["OtherType"].ToString());
                string[] otherurl = null;
                switch (pagetype)
                {
                    case 0:
                        break;
                    case 1://递归
                        otherurl = PL.Pagination(r["OtherPageSetting"].ToString(), num - len);
                        break;
                    case 2://其他页
                        otherurl = PL.SinglePagination(r["OtherPageSetting"].ToString(), num - len);
                        break;
                    case 3://索引页
                        otherurl = PL.IndexPagination(r["OtherPageSetting"].ToString(), int.Parse(r["StartPageNum"].ToString()), int.Parse(r["EndPageNum"].ToString()), num - len);
                        break;
                    default:
                        break;
                }
                if (otherurl != null && otherurl.Length > 0)
                {
                    Array.Resize(ref NewsUrl, len + otherurl.Length);
                    otherurl.CopyTo(NewsUrl, len);
                }
            }
            if (NewsUrl.Length < 1)
            {
                if (ShowProGressBar) HProgressBar.Roll("从列表内容中没有找到任何新闻的相关链接!", 0);
                return;
            }
            if (bReverse)
                Array.Reverse(NewsUrl);
            if (ShowProGressBar) HProgressBar.Roll("开始采集新闻", 0);
            int nSucceed = 0, nFailed = 0, nRepeat = 0;
            for (int i = 0; i < NewsUrl.Length; i++)
            {
                if (i >= num)
                    break;
                try
                {
                    int flag = CollectPage(NewsUrl[i], r, bnorepeat);
                    if (flag != 1)
                    {
                        nSucceed++;
                        if (flag == -1)
                            nRepeat++;
                    }
                    else
                        nFailed++;
                }
                catch
                {
                    nFailed++;
                }
                string prompt = "正在采集新闻,终止<a href=\"Collect_List.aspx\">返回</a>.成功:" + nSucceed * 100 / num + "% ";
                if (nRepeat > 0)
                    prompt += "(其中重复:" + nRepeat * 100 / num + "%) ";
                prompt += "失败:" + nFailed * 100 / num + "%";
                if (ShowProGressBar) HProgressBar.Roll(prompt, (i + 1) * 100 / num);
            }
        }
        /// <summary>
        /// 处理采集单条新闻
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="r"></param>
        /// <param name="norepeat"></param>
        /// <returns>0为成功,-1为重复,1,为失败</returns>
        private int CollectPage(string Url, DataRow r, bool norepeat)
        {
            try
            {
                if (Url == null || Url.Trim().Equals(""))
                    return 1;
                PageNews pn = new PageNews(Url, r["Encode"].ToString());
                if (!pn.Fetch())
                    return 1;
                pn.RuleOfTitle = r["PageTitleSetting"].ToString();
                pn.RuleOfContent = r["PagebodySetting"].ToString();
                pn.FigureTitle();
                if (norepeat)
                {
                    if (pn.Title == null)
                        return 1;
                    if (dal.TitleExist(pn.Title))
                        return -1;
                }
                pn.FigureContent();
                if (r.IsNull("HandSetAuthor"))
                {
                    pn.FigureAuthor(r["AuthorSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureAuthor(r["HandSetAuthor"].ToString(), true);
                }
                if (r.IsNull("HandSetSource"))
                {
                    pn.FigureSource(r["SourceSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureSource(r["HandSetSource"].ToString(), true);
                }
                if (r.IsNull("HandSetAddDate"))
                {
                    pn.FigureAddTime(r["AddDateSetting"].ToString(), false);
                }
                else
                {
                    pn.FigureAddTime(r["HandSetAddDate"].ToString(), true);
                }
                int pgtp = int.Parse(r["OtherNewsType"].ToString());
                if (pgtp == 1)
                {
                    pn.Content += pn.GetOtherPagination(r["OtherNewsPageSetting"].ToString());
                }
                else if (pgtp == 2)
                {
                    pn.Content += pn.GetIndexPagination(r["OtherNewsPageSetting"].ToString());
                }
                pn.Filter(bool.Parse(r["TextTF"].ToString()),
                    bool.Parse(r["IsStyle"].ToString()), bool.Parse(r["IsDIV"].ToString()), bool.Parse(r["IsA"].ToString()),
                    bool.Parse(r["IsClass"].ToString()), bool.Parse(r["IsFont"].ToString()), bool.Parse(r["IsSpan"].ToString()),
                    bool.Parse(r["IsObject"].ToString()), bool.Parse(r["IsIFrame"].ToString()), bool.Parse(r["IsScript"].ToString()));
                if (!r.IsNull("OldContent") && !r.IsNull("ReContent") && !r.IsNull("IgnoreCase"))
                    pn.Replace(r["OldContent"].ToString(), r["ReContent"].ToString(), bool.Parse(r["IgnoreCase"].ToString()));
                if (pn.Content != null && !pn.Content.Trim().Equals("") && !pn.Title.Trim().Equals(""))
                {
                    NetCMS.Model.CollectNewsInfo ninf = new NetCMS.Model.CollectNewsInfo();
                    ninf.Author = pn.Author;
                    ninf.Source = pn.Source;
                    ninf.AddDate = pn.AddTime;
                    ninf.Title = pn.Title;
                    ninf.SiteID = int.Parse(r["ID"].ToString());
                    ninf.Links = Url;
                    ninf.ClassID = r["ClassID"].ToString();
                    string Content = pn.Content;
                    if (bSaveRemotePic)
                    {
                        RemoteResource rs = new RemoteResource(Content, PicSaveUrl, PicSavePath, Url, true);
                        rs.FetchResource();
                        Content = rs.Content;
                    }
                    ninf.Content = Content;
                    NewsAdd(ninf);
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 1;
            }
        }
        private string[] GetNewsList(PageList pagelist)
        {
            if (!pagelist.Fetch())
            {
                HProgressBar.Roll(pagelist.LastError, 0);
            }
            pagelist.FigureList();
            pagelist.FigureNewsUrls();
            return pagelist.NewsUrl;
        }
        #endregion

        /// <summary>
        /// 获取目录信息和采集站点分页
        /// </summary>
        /// <param name="FolderID">目录ＩＤ，如果小于１则为根目录，否则只获取该目录下的站点</param>
        /// <param name="PageIndex">当前页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">返回记录总条数</param>
        /// <param name="PageCount">返回总页数</param>
        /// <returns>返回当前页的数据集</returns>
        public DataTable GetFolderSitePage(int FolderID, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return dal.GetFolderSitePage(FolderID, PageIndex, PageSize, out RecordCount, out PageCount);
        }
        /// <summary>
        /// 复制目录
        /// </summary>
        /// <param name="id">要复制的目录ＩＤ</param>
        public void FolderCopy(int id)
        {
            dal.FolderCopy(id);
        }
        /// <summary>
        /// 复制采集站点
        /// </summary>
        /// <param name="id">要复制的站点的ＩＤ</param>
        public void SiteCopy(int id)
        {
            dal.SiteCopy(id);
        }
        /// <summary>
        /// 删除采集目录
        /// </summary>
        /// <param name="id">要删除的目录ＩＤ</param>
        public void FolderDelete(int id)
        {
            dal.FolderDelete(id);
        }
        /// <summary>
        /// 删除采集站点
        /// </summary>
        /// <param name="id">要删除的站点ＩＤ</param>
        public void SiteDelete(int id)
        {
            dal.SiteDelete(id);
        }
        /// <summary>
        /// 获取指定的目录信息（用于目录修改）
        /// </summary>
        /// <param name="id">要获取的目录ＩＤ</param>
        /// <returns>指定的数据</returns>
        public DataTable GetFolder(int id)
        {
            return dal.GetFolder(id, false);
        }
        /// <summary>
        /// 获取所有的目录的信息
        /// </summary>
        /// <returns>返回所有的目录信息</returns>
        public DataTable GetFolder()
        {
            return dal.GetFolder(0, true);
        }
        /// <summary>
        /// 获取指定的采集站点信息（主要用于站点信息修改和设置）
        /// </summary>
        /// <param name="id">站点的ＩＤ</param>
        /// <returns>数据集</returns>
        public DataTable GetSite(int id)
        {
            return dal.GetSite(id);
        }
        /// <summary>
        /// 新增一个采集站点
        /// </summary>
        /// <param name="st">新增的采集站点信息</param>
        /// <returns>返回新增的站点的自动编号</returns>
        public int SiteAdd(CollectSiteInfo st)
        {
            Encoding end = Encoding.GetEncoding(st.Encode);
            if (!this.ValidateUrl(st.objURL))
                throw new Exception(ErrorMsg);
            return dal.SiteAdd(st);
        }
        public void SiteUpdate(CollectSiteInfo st, int step)
        {
            if (step.Equals(1))
            {
                Encoding end = Encoding.GetEncoding(st.Encode);
                if (!this.ValidateUrl(st.objURL))
                    throw new Exception(ErrorMsg);
            }
            else if (step.Equals(2))
            {
                switch (st.OtherType)
                {
                    case 0:
                        st.OtherPageSetting = "";
                        st.StartPageNum = -1;
                        st.EndPageNum = -1;
                        break;
                    case 1:
                    case 2:
                        st.StartPageNum = -1;
                        st.EndPageNum = -1;
                        break;
                }
            }
            dal.SiteUpdate(st, step);
        }
        public int FolderAdd(string Name, string Description)
        {
            return dal.FolderAdd(Name, Description);
        }
        public void FolderUpdate(int id, string Name, string Description)
        {
            dal.FolderUpdate(id, Name, Description);
        }
        private bool ValidateUrl(string sUrl)
        {
            bool flag = false;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sUrl);
                req.KeepAlive = false;
                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                rsp.Close();
                flag = true;
            }
            catch (WebException e)
            {
                HttpWebResponse response = (HttpWebResponse)e.Response;
                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        string challenge = null;
                        challenge = response.GetResponseHeader("WWW-Authenticate");
                        if (challenge != null)
                            ErrorMsg = challenge;
                    }
                    else
                        ErrorMsg = e.Message;
                }
                else
                    ErrorMsg = "请检查采集对象页地址,不能从服务器取得任何信息!";

            }
            catch (Exception e)
            {
                ErrorMsg = e.Message;
            }
            return flag;
        }
        public DataTable GetRulePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return dal.GetRulePage(PageIndex, PageSize, out  RecordCount, out PageCount);
        }
        public void RuleDelete(int id)
        {
            dal.RuleDelete(id);
        }
        public int RuleAdd(string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase)
        {
            return dal.RuleAdd(Name, OldStr, NewStr, AppSites, IgnoreCase);
        }
        public void RuleUpdate(int RuleID, string Name, string OldStr, string NewStr, int[] AppSites, bool IgnoreCase)
        {
            dal.RuleUpdate(RuleID, Name, OldStr, NewStr, AppSites, IgnoreCase);
        }
        public DataTable GetRule(int id)
        {
            return dal.GetRule(id);
        }
        public DataTable SiteList()
        {
            return dal.SiteList();
        }
        public void NewsAdd(CollectNewsInfo newsinfo)
        {
            dal.NewsAdd(newsinfo);
        }
        public DataTable GetNewsPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            return dal.GetNewsPage(PageIndex, PageSize, out  RecordCount, out  PageCount);
        }
        public void NewsDelete(string id)
        {
            dal.NewsDelete(id);
        }
        public CollectNewsInfo GetNews(int id)
        {
            return dal.GetNews(id);
        }
        public void NewsUpdate(int id, CollectNewsInfo info)
        {
            dal.NewsUpdate(id, info);
        }

        #region 新闻入库
        /// <summary>
        /// 新闻入库
        /// </summary>
        /// <param name="id">如果为0表示入库所有未入库的新闻，否则为要入库的新闻的编号，以,分隔</param>
        public void StorageNews(string id)
        {
            string s = "请点击<a href=\"Collect_News.aspx\">这里返回</a>";
            try
            {
                HTextProgressBar.Start("正在统计数据");
                int[] nid = null;

                bool bUnAll = false;
                if (id == "0")
                {
                    bUnAll = true;
                }
                else
                {
                    if (id.IndexOf(",") > 0)
                    {
                        string[] _id = id.Split(',');
                        int num = _id.Length;
                        nid = new int[num];
                        for (int i = 0; i < num; i++)
                            nid[i] = int.Parse(_id[i]);
                    }
                    else
                    {
                        nid = new int[] { int.Parse(id) };
                    }
                    if (nid.Length < 1)
                        HTextProgressBar.EndProgress("没有选择要入库的采集新闻!" + s);
                }
                int ns = 0;
                int nf = 0;
                HTextProgressBar.ShowText("开始入库采集新闻,请稍候。要终止," + s);
                dal.StoreNews(bUnAll, nid, out ns, out nf);
                HTextProgressBar.EndProgress("采集新闻入库已完成。共成功:" + ns + "条,失败:" + nf + "条新闻。" + s);
            }
            catch (Exception ex)
            {
                HTextProgressBar.EndProgress("采集新闻入库异常终止。异常信息:" + ex.Message + "<br/>" + s);
            }
        }
        #endregion 新闻入库
    }
}
