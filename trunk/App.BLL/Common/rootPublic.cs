//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NetCMS.DALFactory;
using NetCMS.Model;

namespace NetCMS.Content.Common
{
    public class rootPublic
    {
        private IrootPublic dal;
        public rootPublic()
        {
            dal = NetCMS.DALFactory.DataAccess.CreaterootPublic();
        }
        /// <summary>
        /// 得到站点ID是否存在
        /// </summary>
        /// <returns></returns>
        public int getSiteID(string SiteID)
        {
            return dal.getSiteID(SiteID);
        }

        /// <summary>
        /// 根据用户编号获取用户名
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public string getUserName(string strUserNum)
        {
            return dal.getUserName(strUserNum);
        }

        /// <summary>
        /// 根据用户编号获取用户自动编号
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public int getUserName_uid(string strUserNum)
        {
            return dal.getUserName_uid(strUserNum);
        }

        /// <summary>
        /// 根据用户ID获取用户编号
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public string getUidUserNum(int Uid)
        {
            return dal.getUidUserNum(Uid);
        }

        /// <summary>
        /// 根据用户名获取用户编号
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public string getUserNameUserNum(string UserName)
        {
            return dal.getUserNameUserNum(UserName);
        }

        public string getGidGroupNumber(int gid)
        {
            return dal.getGidGroupNumber(gid);
        }

        /// <summary>
        /// 根据会员组编号获取会员组名称
        /// </summary>
        /// <param name="strGroupNumber"></param>
        /// <returns></returns>
        public string getGroupName(string strGroupNumber)
        {
            return dal.getGroupName(strGroupNumber);
        }

        /// <summary>
        /// 获取会员组的标志
        /// </summary>
        /// <param name="strGroupNumber"></param>
        /// <returns></returns>
        public string getGroupNameFlag(string UserNum)
        {
            return dal.getGroupNameFlag(UserNum);
        }
        /// <summary>
        /// 根据会员编号获取会员组名称
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public string getUserGroupName(string strUserNum)
        {
            return dal.getUserGroupName(strUserNum);
        }

        /// <summary>
        /// 根据用户编号获得用户组编号
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public string getUserGroupNumber(string strUserNum)
        {
            return dal.getUserGroupNumber(strUserNum);
        }

        /// <summary>
        /// 根据会员组编号获取会员组ID
        /// </summary>
        /// <param name="strUserNum"></param>
        /// <returns></returns>
        public string getIDGroupNumber(string GroupNumber)
        {
            return dal.getIDGroupNumber(GroupNumber);
        }

        /// <summary>
        /// 获取G币名称
        /// </summary>
        /// <returns></returns>
        public string getgPointName()
        {
            return dal.getgPointName();
        }

        /// <summary>
        /// 获取站点名称
        /// </summary>
        /// <returns></returns>
        public string siteName()
        {
            return dal.siteName();
        }

        /// <summary>
        /// 获取版权信息
        /// </summary>
        /// <returns></returns>
        public string siteCopyRight()
        {
            return dal.siteCopyRight();
        }

        /// <summary>
        /// 获取站点域名
        /// </summary>
        /// <returns></returns>
        public string sitedomain()
        {
            return dal.sitedomain();
        }

        /// <summary>
        /// 获取站点首页模板,首页文件名
        /// </summary>
        /// <returns></returns>
        public string indexTempletfile()
        {
            return dal.indexTempletfile();
        }

        /// <summary>
        /// 获取默认的默认模板及扩展名
        /// </summary>
        /// <returns></returns>
        public string allTemplet()
        {
            return dal.allTemplet();
        }

        /// <summary>
        /// 前台页面显示方式，0为静态，1为动态
        /// </summary>
        /// <returns></returns>
        public int ReadType()
        {
            return dal.ReadType();
        }

        /// <summary>
        /// 获得站点电子邮件
        /// </summary>
        /// <returns></returns>
        public string SiteEmail()
        {
            return dal.SiteEmail();
        }

        /// <summary>
        /// 获取连接方式 0相对路径，1绝对路径
        /// </summary>
        /// <returns></returns>
        public int LinkType()
        {
            return dal.LinkType();
        }

        /// <summary>
        /// 栏目保存路径
        /// </summary>
        /// <returns></returns>
        public string SaveClassFilePath(string siteid)
        {
            return dal.SaveClassFilePath(siteid);
        }

        /// <summary>
        /// 生成索引页规则
        /// </summary>
        /// <returns></returns>
        public string SaveIndexPage()
        {
            return dal.SaveIndexPage();
        }

        /// <summary>
        /// 生成新闻的命名规则
        /// </summary>
        /// <returns></returns>
        public string SaveNewsFilePath()
        {
            return dal.SaveNewsFilePath();
        }

        /// <summary>
        /// 生成新闻的文件保存路径
        /// </summary>
        /// <returns></returns>
        public string SaveNewsDirPath()
        {
            return dal.SaveNewsDirPath();
        }

        public string GetRegGroupNumber()
        {
            return dal.GetRegGroupNumber();
        }

        /// <summary>
        /// 是否独立图片服务器域名
        /// </summary>
        /// <returns></returns>
        public int PicServerTF()
        {
            return dal.PicServerTF();
        }

        /// <summary>
        /// 图片服务器域名
        /// </summary>
        /// <returns></returns>
        public string PicServerDomain()
        {
            return dal.PicServerDomain();
        }

        /// <summary>
        /// 是否允许投稿
        /// </summary>
        /// <returns></returns>
        public int ConstrTF()
        {
            return dal.ConstrTF();
        }

        /// <summary>
        /// 获取审核机制
        /// </summary>
        /// <returns></returns>
        public int CheckInt()
        {
            return dal.CheckInt();
        }

        /// <summary>
        /// 新闻标题是否允许重复
        /// </summary>
        /// <returns></returns>
        public int CheckNewsTitle()
        {
            return dal.CheckNewsTitle();
        }

        /// <summary>
        /// 得到上传扩展名
        /// </summary>
        /// <returns></returns>
        public string upfileType()
        {
            return dal.upfileType();
        }
        /// <summary>
        /// 得到会员所在组的折扣率
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public double getDiscount(string UserNum)
        {
            return dal.getDiscount(UserNum);
        }

        /// <summary>
        /// 得到水印信息
        /// </summary>
        /// <returns></returns>
        public DataTable getWaterInfo()
        {
            return dal.getWaterInfo();
        }

        public string getUserChar(string UserNum)
        {
            return dal.getUserChar(UserNum);
        }
        /// <summary>
        /// 保存日志入库及日志文件
        /// </summary>
        /// <param name="num">标识，0表示写入数据库，1表示写入数据同时写入日志文件</param>
        /// <param name="_num">用户标志，0表示用户，1表示管理员</param>
        /// <param name="UserNum">传入的用户编号</param>
        /// <param name="Title">日志标题</param>
        /// <param name="Content">日志描述</param>
        public void SaveUserAdminLogs(int num, int _num, string UserNum, string Title, string Content)
        {
            dal.SaveUserAdminLogs(num, _num, UserNum, Title, Content);
        }
        /// <summary>
        /// 取得用户组列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetGroupList()
        {
            return dal.GetGroupList();
        }


        /// <summary>
        /// 得到Help信息
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public DataTable GetHelpId(string helpId)
        {
            DataTable dt = dal.GetHelpId(helpId);
            return dt;
        }


        /// <summary>
        /// 选择频道
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public DataTable GetselectNewsList()
        {
            DataTable dt = dal.GetselectNewsList();
            return dt;
        }

        /// <summary>
        /// 选择标签样式分类
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public DataTable GetselectLabelList()
        {
            DataTable dt = dal.GetselectLabelList();
            return dt;
        }

        /// <summary>
        /// 选择标签样式
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public DataTable GetselectLabelList1(string ClassID)
        {
            DataTable dt = dal.GetselectLabelList1(ClassID);
            return dt;
        }

        /// <summary>
        /// 使用ajax获取栏目
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public IDataReader GetajaxsNewsList(string ParentID)
        {
            return dal.GetajaxsNewsList(ParentID);
        }


        /// <summary>
        /// 得到新闻表
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public DataTable getNewsTableIndex()
        {
            DataTable dt = dal.getNewsTableIndex();
            return dt;
        }

        /// <summary>
        /// 使用ajax获取专题
        /// </summary>
        /// <param name="helpId"></param>
        /// <returns></returns>
        public IDataReader GetajaxsspecialList(string ParentID)
        {
            return dal.GetajaxsspecialList(ParentID);
        }

        /// <summary>
        /// 根据栏目得到SiteID
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getSiteIDFromClass(string ClassID)
        {
            return dal.getSiteIDFromClass(ClassID);
        }

        /// <summary>
        /// 得到栏目列表
        /// </summary>
        /// <returns></returns>
        public DataTable getClassListPublic(string ParentID)
        {
            DataTable dt = dal.getClassListPublic(ParentID);
            return dt;
        }

        /// <summary>
        /// 得到专题列表
        /// </summary>
        /// <returns></returns>
        public DataTable getSpecialListPublic(string ParentID)
        {
            DataTable dt = dal.getSpecialListPublic(ParentID);
            return dt;
        }

        public string getResultPage(string _Content, DateTime _DateTime, string ClassID, string EName)
        {
            return dal.getResultPage(_Content, _DateTime, ClassID, EName);
        }

        /// <summary>
        /// 得到某个栏目的英文名称
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public string getClassEName(string ClassID)
        {
            return dal.getClassEName(ClassID);
        }

        #region 会员登陆
        /// <summary>
        /// 判断登陆是否需要验证码.
        /// </summary>
        /// <returns></returns>
        public int getUserLoginCode()
        {
            return dal.getUserLoginCode();
        }

        /// <summary>
        /// 得到会员用户积分和G币
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public string getGIPoint(string UserNum)
        {
            return dal.getGIPoint(UserNum);
        }
        /// <summary>
        /// 得到用户魅力值
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public int getcPoint(string UserNum)
        {
            return dal.getcPoint(UserNum);
        }

        /// <summary>
        /// 得到会员的上传信息
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public DataTable getGroupUpInfo(string UserNum)
        {
            DataTable dt = dal.getGroupUpInfo(UserNum);
            return dt;
        }

        /// <summary>
        /// 得到用户签名
        /// </summary>
        /// <param name="UserNum"></param>
        /// <returns></returns>
        public int getUserUserInfo(string UserNum)
        {
            return dal.getUserUserInfo(UserNum);
        }

        #endregion 会员登陆

        /// <summary>
        /// 得到频道站点名称
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public string getChName(string SiteID)
        {
            return dal.getChName(SiteID);
        }

        #region 删除用户所有的信息

        public void delUserAllInfo(string UserNum)
        {
            dal.delUserAllInfo(UserNum);
        }
        #endregion  删除用户所有的信息

        #region 删除所有频道信息
        public void delSiteAllInfo(string SiteID)
        {
            dal.delSiteAllInfo(SiteID);
        }
        #endregion 删除所有频道信息

        #region 删除所有的新闻信息

        public void delNewsAllInfo(string NewsID)
        {
            dal.delNewsAllInfo(NewsID);
        }
        #endregion 删除所有的新闻信息
    }
}
