//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Data;
using NetCMS.Model;
using NetCMS.DALFactory;
using System.IO;

namespace NetCMS.Content.Ads
{
    public class Ads
    {
        private string str_dirDumm = NetCMS.Config.UIConfig.dirDumm;
        private string str_rootpath = NetCMS.Common.ServerInfo.GetRootPath();
        private IAds ac;
        public Ads()
        {
            ac = DataAccess.CreateAds();
        }

        public DataTable list(NetCMS.Model.AdsListInfo ali)
        {
            DataTable dt = ac.list(ali);
            return dt;
        }
        public DataTable childlist(string classid)
        {
            DataTable dt = ac.childlist(classid);
            return dt;
        }
        public void Lock(string id)
        {
            ac.Lock(id);
        }

        public void UnLock(string id)
        {
            ac.UnLock(id);
        }
        public void DelAllAds()
        {
            DataTable dt = ac.AdsDt(null);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string classid = dt.Rows[i]["ClassID"].ToString();
                    string adsid = dt.Rows[i]["AdID"].ToString();
                    string adspath = str_rootpath + str_dirDumm + "\\jsfiles\\ads\\" + classid + "\\" + adsid + ".js";
                    NetCMS.Common.Public.DelFile("", adspath);
                }
                dt.Clear(); dt.Dispose();
            }
            ac.DelAllAds();
        }
        public void DelPAds(string id)
        {
            DataTable dt = ac.AdsDt(id);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string classid = dt.Rows[i]["ClassID"].ToString();
                    string adsid = dt.Rows[i]["AdID"].ToString();
                    string adspath = str_rootpath + str_dirDumm + "\\jsfiles\\ads\\" + classid + "\\" + adsid + ".js";
                    NetCMS.Common.Public.DelFile("", adspath);
                }
                dt.Clear(); dt.Dispose();
            }
            ac.DelPAds(id);
        }
        public void DelAllAdsClass()
        {
            DataTable dt = ac.adsClassDt(null);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string classid = dt.Rows[i]["AcID"].ToString();
                    string classpath = str_rootpath + str_dirDumm + "\\jsfiles\\ads\\" + classid;
                    NetCMS.Common.Public.DelFile(classpath, "");
                }
                dt.Clear(); dt.Dispose();
            }
            ac.DelAllAdsClass();
        }

        public void DelPAdsClass(string classid)
        {
            DataTable dt = ac.adsClassDt(classid);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str_classid = dt.Rows[i]["AcID"].ToString();
                    string classpath = str_rootpath + str_dirDumm + "\\jsfiles\\ads\\" + str_classid;
                    NetCMS.Common.Public.DelFile(classpath, "");
                }
                dt.Clear(); dt.Dispose();
            }
            ac.DelPAdsClass(classid);
        }
        public int AddClass(NetCMS.Model.AdsClassInfo aci)
        {
            int result = 0;
            result = ac.AddClass(aci);
            return result;
        }
        public int EditClass(NetCMS.Model.AdsClassInfo aci)
        {
            int result = 0;
            result = ac.EditClass(aci);
            return result;
        }

        public DataTable getAdsClassInfo(string classid)
        {
            DataTable dt = ac.getAdsClassInfo(classid);
            return dt;
        }
        public void statDelAll()
        {
            ac.statDelAll();
        }
        public void statDel(string idstr)
        {
            ac.statDel(idstr);
        }
        public DataTable getAdsClassList()
        {
            DataTable dt = ac.getAdsClassList();
            return dt;
        }
        public DataTable getAdsList(string id)
        {
            DataTable dt = ac.getAdsList(id);
            return dt;
        }

        public int adsAdd(NetCMS.Model.AdsInfo ai)
        {
            string AdID = ac.adsAdd(ai);
            createJS(ai.adType.ToString(),AdID,ai.ClassID);
            return 1;
        }
        public DataTable getAdsDomain()
        {
            DataTable dt = ac.getAdsDomain();
            return dt;
        }
        public DataTable getAdsPicInfo(string col, string tbname, string id)
        {
            DataTable dt = ac.getAdsPicInfo(col,tbname,id);
            return dt;
        }
        public DataTable getAdsInfo(string id)
        {
            DataTable dt = ac.getAdsInfo(id);
            return dt;
        }
        public int adsEdit(NetCMS.Model.AdsInfo ai)
        {
            int result = ac.adsEdit(ai);
            string str_jspath = str_rootpath + str_dirDumm + "\\jsfiles\\ads\\" + ai.OldClass + "\\" + ai.AdID + ".js";
            NetCMS.Common.Public.DelFile("", str_jspath);
            
            createJS(ai.adType.ToString(), ai.AdID, ai.ClassID);
            return result;
        }

        protected void createJS(string adType,string AdID,string classID)
        {
            switch (adType)
            {
                case "0":
                    createJs.CreateAds0(AdID, classID);
                    break;
                case "1":
                    createJs.CreateAds1(AdID, classID);
                    break;
                case "2":
                    createJs.CreateAds2(AdID, classID);
                    break;
                case "3":
                    createJs.CreateAds3(AdID, classID);
                    break;
                case "4":
                    createJs.CreateAds4(AdID, classID);
                    break;
                case "5":
                    createJs.CreateAds5(AdID, classID);
                    break;
                case "6":
                    createJs.CreateAds6(AdID, classID);
                    break;
                case "7":
                    createJs.CreateAds7(AdID, classID, 0);
                    break;
                case "8":
                    createJs.CreateAds7(AdID, classID, 1);
                    break;
                case "9":
                    createJs.CreateAds8(AdID, classID);
                    break;
                case "10":
                    createJs.CreateAds9(AdID, classID);
                    break;
                case "11":
                    createJs.CreateAds10(AdID, classID);
                    break;
                case "12":
                    createJs.CreateAds11(AdID, classID);
                    break;
            }
        }

        public DataTable get24HourStat(string type, string id)
        {
            DataTable dt = ac.get24HourStat(type, id);
            return dt;
        }
        public DataTable getDayStat(string type, string id, string mday)
        {
            DataTable dt = ac.getDayStat(type, id, mday);
            return dt;
        }
        public DataTable getMonthStat(string type, string id)
        {
            DataTable dt = ac.getMonthStat(type, id);
            return dt;
        }
        public DataTable getYearStat(string id)
        {
            DataTable dt = ac.getYearStat(id);
            return dt;
        }
        public DataTable getWeekStat(string type, string id)
        {
            DataTable dt = ac.getWeekStat(type, id);
            return dt;
        }
        public DataTable getSourceStat(string id)
        {
            DataTable dt = ac.getSourceStat(id);
            return dt;
        }
        public DataTable getDbNull()
        {
            DataTable dt = ac.getDbNull();
            return dt;
        }
        public void upStat(string adress, string id)
        {
            ac.upStat(adress, id);
        }
        public void upShowNum(string id)
        {
            ac.upShowNum(id);
        }
        public void upClickNum(string id, string type)
        {
            ac.upClickNum(id, type);
        }
        public void addStat(string id, string ip)
        {
            ac.addStat(id, ip);
        }
        public DataTable getClassAdprice(string classid)
        {
            DataTable dt = ac.getClassAdprice(classid);
            return dt;
        }
        public DataTable getuserG()
        {
            DataTable dt = ac.getuserG();
            return dt;
        }
        public void DelUserG(int Gnum)
        {
            ac.DelUserG(Gnum);
        }
    }
}
