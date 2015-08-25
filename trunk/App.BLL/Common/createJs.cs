//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Data;
using System.IO;

/// <summary>
/// createJs 的摘要说明
/// </summary>
public class createJs
{
    private static string str_SessionID= NetCMS.Global.Current.SiteID;
    private static string str_dirMana = NetCMS.Config.UIConfig.dirDumm;
    private static string str_rootpath= NetCMS.Common.ServerInfo.GetRootPath();
    /// <summary>
    /// 获取当前站点域名
    /// </summary>
    /// <param name="SiteID">SessinonID</param>
    /// <returns>返回域名字符串</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static string GetDomain(string SiteID)
    {
        string str_Domain = "";
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
            str_dirMana = "//" + str_dirMana;

        NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
        DataTable dt = ac.getAdsDomain();
        
        if (SiteID == "0")
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    str_Domain = "http://" + dt.Rows[0][0].ToString() + str_dirMana;
                dt.Clear();dt.Dispose();
            }
        }
        else
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == null || dt.Rows[0][0].ToString()==string.Empty)
                        str_Domain = GetDomain("0");
                    else
                        str_Domain = "http://" + dt.Rows[0][0].ToString() + str_dirMana;
                }
                dt.Clear();dt.Dispose();
            }
        }
        return str_Domain;
    }

    /// <summary>
    /// (生成JS文件公共部份)获取左图片
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>返回左图片字符串</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static string GetAdsLeftStr(string adsID)
    {
        string str_Temp = "";
        NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
        DataTable dt = ac.getAdsPicInfo("leftPic,leftSize", "ads", adsID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                string str_leftPic = ReplaceDirfile(dt.Rows[0][0].ToString());
                string str_leftSize = dt.Rows[0][1].ToString();

                str_leftPic = str_leftPic.ToLower();
                string[] arr_LeftSize = str_leftSize.Split('|');
                if (str_leftPic.IndexOf(".swf") != -1)
                {
                    if (str_leftPic.IndexOf("http://") != -1)
                        str_Temp = "<embed src=\"" + str_leftPic + "\" quality=\"high\" width=\"" + arr_LeftSize[0].ToString() + "\"" +
                                   " height=\"" + arr_LeftSize[1].ToString() + "\" type=\"application/x-shockwave-flash\" " +
                                   " pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                    else
                        str_Temp = "<embed src=\"" + GetDomain(str_SessionID) + str_leftPic + "\" quality=\"high\" " +
                                   " width=\"" + arr_LeftSize[0].ToString() + "\" height=\"" + arr_LeftSize[1].ToString() + "\" " +
                                   " type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                else
                {
                    if (str_leftPic.IndexOf("http://") == -1)
                        str_Temp = "<a href=\"" + GetDomain(str_SessionID) + "/jsfiles/ads/adsclick.aspx?adsID=" + adsID + "\" " +
                                   " target=_blank><img src=\"" + str_leftPic + "\" border=\"0\" width=\"" + arr_LeftSize[0].ToString() + "\" " +
                                   " height=\"" + arr_LeftSize[1].ToString() + "\" align=\"top\"></a>";
                    else
                        str_Temp = "<a href=\"" + GetDomain(str_SessionID) + "/jsfiles/ads/adsclick.aspx?adsID=" + adsID + "\" "+
                                   " target=_blank><img src=\"" + GetDomain(str_SessionID) + str_leftPic + "\" border=\"0\" "+
                                   "width=\"" + arr_LeftSize[0].ToString() + "\" height=\"" + arr_LeftSize[1].ToString() + "\" align=\"top\"></a>";
                }
            }
            dt.Clear();dt.Dispose();
        }
        return str_Temp;
    }

    /// <summary>
    /// (生成JS文件公共部份)获取右图片
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>返回右图片字符串</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static string GetAdsRightStr(string adsID)
    {
        NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
        DataTable dt = ac.getAdsPicInfo("rightPic,rightSize", "ads", adsID);
        
        string str_Temp = "";
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                string str_rightPic = ReplaceDirfile(dt.Rows[0][0].ToString());
                string str_rightSize = dt.Rows[0][1].ToString();

                str_rightPic = str_rightPic.ToLower();
                string[] arr_rightSize = str_rightSize.Split('|');
                if (str_rightPic.IndexOf(".swf") != -1)
                {
                    if (str_rightPic.IndexOf("http://") != -1)
                        str_Temp = "<embed src=\"" + str_rightPic + "\" quality=\"high\" width=\"" + arr_rightSize[0].ToString() + "\" " +
                                   " height=\"" + arr_rightSize[1].ToString() + "\" type=\"application/x-shockwave-flash\" " +
                                   " pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                    else
                        str_Temp = "<embed src=\"" + GetDomain(str_SessionID) + str_rightPic + "\" quality=\"high\" " +
                                   "width=\"" + arr_rightSize[0].ToString() + "\" height=\"" + arr_rightSize[1].ToString() + "\"" +
                                   "type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\"></embed>";
                }
                else
                {
                    if (str_rightPic.IndexOf("http://") != -1)
                        str_Temp = "<a href=\"" + GetDomain(str_SessionID) + "/jsfiles/ads/adsclick.aspx?adsID=" + adsID + "\" target=_blank>" +
                                   "<img src=\"" + str_rightPic + "\" border=\"0\" width=\"" + arr_rightSize[0].ToString() + "\" " +
                                   " height=\"" + arr_rightSize[1].ToString() + "\" align=\"top\"></a>";
                    else
                        str_Temp = "<a href=\"" + GetDomain(str_SessionID) + "/jsfiles/ads/adsclick.aspx?adsID=" + adsID + "\" " +
                                   " target=_blank><img src=\"" + GetDomain(str_SessionID) + str_rightPic + "\" border=\"0\" " +
                                   "width=\"" + arr_rightSize[0].ToString() + "\" height=\"" + arr_rightSize[1].ToString() + "\" align=\"top\"></a>";
                }
            }
            dt.Clear(); dt.Dispose();
        }
        return str_Temp;
    }

    /// <summary>
    /// 生成JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <param name="adsContent">广告内容</param>
    /// <returns>生成JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateJsFile(string adsID, string adsClassID, string adsContent)
    {
        if (str_dirMana != "" && str_dirMana != null && str_dirMana != string.Empty)
            str_dirMana = "\\" + str_dirMana;
        string str_jsdir = str_rootpath + str_dirMana + "\\jsfiles\\ads\\" + adsClassID;
        string str_jspath = str_rootpath + str_dirMana + "\\jsfiles\\ads\\" + adsClassID + "\\" + adsID + ".js";
        if (Directory.Exists(str_jsdir) == false)
        {
            try
            {
                Directory.CreateDirectory(str_jsdir);
            }
            catch (Exception ex) 
            {
                throw new Exception (ex.ToString());
            }
        }
        if (File.Exists(str_jspath) == true)
        {
            try
            {
                File.Delete(str_jspath);
            }
            catch (Exception ex1)
            {
                throw new Exception(ex1.ToString());
            }
        }
        try
        {
            File.AppendAllText(str_jspath, adsContent);
        }
        catch (Exception ex2) 
        {
            throw new Exception(ex2.ToString());
        }
    }

    /// <summary>
    /// 检测广告是否锁定与过期
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <returns>返回true或false</returns>
    /// 编写时间2007-04-11   Code By DengXi
    public static bool checkJs(string adsID)
    {
        bool tf = false;
        NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
        DataTable dt = ac.getAdsPicInfo("CondiTF,maxShowClick,TimeOutDay,maxClick,isLock,ClickNum,ShowNum", "ads", adsID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["isLock"].ToString() == "1")
                    tf = true;
                else
                {
                    if (dt.Rows[0]["CondiTF"].ToString() == "1")
                    {
                        int int_maxShowClick = int.Parse(dt.Rows[0]["maxShowClick"].ToString());
                        int int_maxClick = int.Parse(dt.Rows[0]["maxClick"].ToString());
                        int int_ClickNum = int.Parse(dt.Rows[0]["ClickNum"].ToString());
                        int int_ShowNum = int.Parse(dt.Rows[0]["ShowNum"].ToString());
                        if (int_ClickNum > int_maxClick || int_ShowNum > int_maxShowClick || Convert.ToDateTime(System.DateTime.Now.ToString()) > Convert.ToDateTime(dt.Rows[0]["TimeOutDay"].ToString()))
                            tf = true;
                    }
                    else
                        tf = false;
                }
            }
            dt.Clear();dt.Dispose();
        }
        return tf;
    }

    /// <summary>
    /// 生成显示广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成显示广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds0(string adsID, string adsClassID)
    {
        string str_Temp = "";
        string str_AdsJsstr = "";
        str_Temp = GetAdsLeftStr(adsID);
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
            str_AdsJsstr = "document.write('" + str_Temp + "');\r";
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成弹出新窗口广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成弹出新窗口广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds1(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_leftPic = "";
        string str_leftSize = "";

        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);
            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');
            str_AdsJsstr = "window.open('" + GetDomain(str_SessionID) + "/jsfiles/ads/pic.aspx?adsID=" + adsID + "','','width=" +
                           "" + arr_leftSize[0].ToString() + ",height=" + arr_leftSize[1].ToString() + ",scrollbars=1');\r";
            dt.Clear();dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成打开新窗口广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成打开新窗口广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds2(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
            str_AdsJsstr = "window.open('" + GetDomain(str_SessionID) + "/jsfiles/ads/pic.aspx?adsID=" + adsID + "','_blank');\r";
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成渐隐消失广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成渐隐消失广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds3(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_Temp = GetAdsLeftStr(adsID);
        string str_leftPic = "";
        string str_leftSize = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);
            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');
            str_AdsJsstr = "FilterAwayStr=(document.layers)?true:false;if(FilterAwayStr)\r" +
                           "{\r" +
                           "document.write('<layer id=FilterAwayT onLoad=\"moveToAbsolute(layer1.pageX-160,layer1.pageY);" +
                           "clip.height=" + str_leftSize[1].ToString() + ";clip.width=" + str_leftSize[0].ToString() + "; visibility=show;\">" +
                           "<layer id=FilterAwayF position:absolute; bottom:20; center:1>" + str_Temp + "</layer></layer>');\r" +
                           "}\r" +
                           "else\r" +
                           "{\r" +
                           "document.write('<div style=\"position:absolute;bottom:" + (int.Parse(str_leftSize[1].ToString()) + 20) + ";" +
                           "center:1;\"><div id=FilterAwayT style=\"position:absolute; width:" + str_leftSize[0].ToString() + ";" +
                           "height:" + str_leftSize[1].ToString() + "" +
                           "clip:rect(0," + str_leftSize[0].ToString() + "," + str_leftSize[1].ToString() + ",0)\">" +
                           "<div id=FilterAwayF style=\"position:absolute;bottom:20; center:1\">" + str_Temp + "</div></div></div>');\r" +
                           "}\r" +
                           "document.write('<script language=javascript src=" + GetDomain(str_SessionID) + "/jsfiles/ads/CreateJs/" +
                           "FilterAway.js></script>');\r";
            dt.Clear();
            dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成网页对话框广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成网页对话框广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi
    
    public static void CreateAds4(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_Temp = GetAdsLeftStr(adsID);
        string str_leftPic = "";
        string str_leftSize = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);
            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');
            str_AdsJsstr = "window.showModalDialog('" + GetDomain(str_SessionID) + "/jsfiles/ads/pic.aspx?adsID=" + adsID + "',''," +
                           "'dialogWidth:" + (int.Parse(arr_leftSize[0].ToString()) + 10) + "px;dialogHeight:" +
                           "" + (int.Parse(arr_leftSize[1].ToString()) + 30) + "px;center:0;status:no');\r";
            dt.Clear();
            dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成透明对话框广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成透明对话框广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds5(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_Temp = GetAdsLeftStr(adsID);
        string str_leftPic = "";
        string str_leftSize = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);
            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');
            str_AdsJsstr = "document.write('<script language=javascript src=" + GetDomain(str_SessionID) + "/jsfiles/" +
                           "ads/CreateJs/ClarityBox.js></script>');\r" +
                           "document.write('<div style=\"position:absolute;left:300px;top:150px;width:" + arr_leftSize[0].ToString() + ";" +
                           "height:" + arr_leftSize[1].ToString() + ";z-index:1;solid;filter:alpha(opacity=90)\" id=\"ClarityBoxID\" " +
                           "onmousedown=\"ClarityBox(this)\" onmousemove=\"ClarityBoxMove(this)\" " +
                           "onMouseOut=\"down=false\" onmouseup=\"down=false\" >" +
                           "<table cellpadding=0 border=0 cellspacing=1 width=" + arr_leftSize[0].ToString() + " " +
                           "height=" + (int.Parse(arr_leftSize[1].ToString()) + 20) + " bgcolor=#000000>" +
                           "<tr><td height=20 align=right style=\"cursor:move;\">" +
                           "<a href=\"#\" style=\"font-size: 9pt; color: white; text-decoration: none\" " +
                           "onClick=ClarityBoxclose(\"ClarityBoxID\") >>>关闭>></a></td></tr>" +
                           "<tr><td>" + str_Temp + "</td></tr></table></div>');\r";
            dt.Clear();
            dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成满屏浮动广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成满屏浮动广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds6(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_Temp = GetAdsLeftStr(adsID);
        string str_leftPic = "";
        string str_leftSize = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);
            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');
            str_AdsJsstr = "DriftBoxStr=(document.layers)?true:false;\r" +
                           "if(DriftBoxStr)\r" +
                           "{\r" +
                           "document.write('<layer id=DriftBox width=" + arr_leftSize[0].ToString() + " " +
                           "height=" + arr_leftSize[1].ToString() + " onmouseover=DriftBoxSM(\"DriftBox\") onmouseout=movechip(\"DriftBox\")>" +
                           "" + str_Temp + "</layer>');\r" +
                           "}\r" +
                           "else\r{\r" +
                           "document.write('<div id=DriftBox style=\"position:absolute; width:" + arr_leftSize[0].ToString() + "px; " +
                           "height:" + arr_leftSize[1].ToString() + "px; z-index:9; filter: Alpha(Opacity=90)\" " +
                           "onmouseover=DriftBoxSM(\"DriftBox\") onmouseout=movechip(\"DriftBox\")>" + str_Temp + "</div>');\r" +
                           "}\r" +
                           "document.write('<script language=javascript src=" + GetDomain(str_SessionID) + "/jsfiles/ads/CreateJs/" +
                           "DriftBox.js></script>');\r";
            dt.Clear();
            dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成底端广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <param name="Cy">0为左下底端广告,1为右下底端广告</param>
    /// <returns>生成底端广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds7(string adsID, string adsClassID,int Cy)
    {
        string str_AdsJsstr = "";
        string str_Temp = GetAdsLeftStr(adsID);
        string str_leftPic = "";
        string str_leftSize = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo(" leftPic,leftSize", "ads", adsID);

            str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
            str_leftSize = dt.Rows[0]["leftSize"].ToString();
            string[] arr_leftSize = str_leftSize.Split('|');

            if (Cy == 0)
            {
                str_AdsJsstr = "function BinitLeftBottomLoad()\r" +
                               "{\r" +
                               "    document.all.LeftBottom.style.visibility = 'visible'; \r" +
                               "    MoveLeftBottom('LeftBottom'); \r" +
                               "}\r" +
                               "function MoveLeftBottom(layerName) \r" +
                               "{ \r" +
                               "    var x = 5;\r" +
                               "    var y = document.body.scrollTop + document.body.offsetHeight -" + arr_leftSize[1].ToString() + ";\r" +
                               "    eval(\"document.all.\" + layerName + \".style.posTop = parseInt(y)\"); \r" +
                               "    eval(\"document.all.\" + layerName + \".style.posLeft = x\"); \r" +
                               "    setTimeout(\"MoveLeftBottom('LeftBottom');\", 20);\r" +
                               "} \r" +
                               "document.write(\"<div id=LeftBottom style='position: absolute;visibility:hidden;z-index:1'>" +
                               "" + str_Temp.Replace("\"", "'") + "</div>\");\r" +
                               "BinitLeftBottomLoad()\r";
            }
            else
            {
                str_AdsJsstr = "function BinitRightBottomLoad()\r" +
                               "{\r" +
                               "    document.all.RightBottom.style.visibility = 'visible'; \r" +
                               "    MoveRightBottom('RightBottom'); \r" +
                               "}\r" +
                               "function MoveRightBottom(layerName) \r" +
                               "{ \r" +
                               "	var x = 5;\r" +
                               "    var y = document.body.scrollTop + document.body.offsetHeight -" + arr_leftSize[1].ToString() + ";\r" +
                               "    eval('document.all.' + layerName + '.style.posTop = y');\r" +
                               "    eval('document.all.' + layerName + '.style.posRight = x');\r" +
                               "    setTimeout(\"MoveRightBottom('RightBottom');\", 20);\r" +
                               "} \r" +
                               "document.write(\"<div id=RightBottom " +
                               "style='position: absolute;visibility:hidden;z-index:1'>" + str_Temp.Replace("\"", "'") + "</div>" +
                               "\");\r" +
                               "BinitRightBottomLoad()\r";
            }
            dt.Clear();
            dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成对联广告(顶部)JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成对联广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds8(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        string str_TempLeft = GetAdsLeftStr(adsID);
        string str_TempRight = GetAdsRightStr(adsID);
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            str_AdsJsstr = "function TinitAdLoad()\r" +
                           "{\r" +
                           "    document.all.TAdLayer1.style.visibility = 'visible'; \r" +
                           "    document.all.TAdLayer2.style.visibility = 'visible'; \r" +
                           "    TMoveLeftLayer('TAdLayer1'); \r" +
                           "    TMoveRightLayer('TAdLayer2'); \r" +
                           "}\r" +
                           "function TMoveLeftLayer(layerName) \r" +
                           "{ \r" +
                           "    var x = 5;\r" +
                           "    var y = 5;\r" +
                           "    var diff = (document.body.scrollTop + y - document.all.TAdLayer1.style.posTop)*.40; \r" +
                           "    var y = document.body.scrollTop + y - diff; \r" +
                           "    eval(\"document.all.\" + layerName + \".style.posTop = parseInt(y)\"); \r" +
                           "    eval(\"document.all.\" + layerName + \".style.posLeft = x\"); \r" +
                           "    setTimeout(\"TMoveLeftLayer('TAdLayer1');\", 20);\r" +
                           "} \r" +
                           "function TMoveRightLayer(layerName) \r" +
                           "{ \r" +
                           "	var x = 5;\r" +
                           "    var y = 5;\r" +
                           "    var diff = (document.body.scrollTop + y - document.all.TAdLayer2.style.posTop)*.40;\r" +
                           "    var y = document.body.scrollTop + y - diff;\r" +
                           "    eval('document.all.' + layerName + '.style.posTop = y');\r" +
                           "    eval('document.all.' + layerName + '.style.posRight = x');\r" +
                           "   setTimeout(\"TMoveRightLayer('TAdLayer2');\", 20);\r" +
                           "} \r" +
                           "document.write(\"<div id=TAdLayer1 style='position: absolute;visibility:hidden;z-index:1'>" +
                           "" + str_TempLeft.Replace("\"", "'") + "<br /><div align='center' style='cursor:pointer;' " +
                           "onclick=" + "\\" + "\"javascript:document.getElementById(\'TAdLayer1\').style.display=\'none\';" +
                           "document.getElementById(\'TAdLayer2\').style.display=\'none\';" + "\\" + "\">>>关闭>></div></div>\");\r" +
                           "document.write(\"<div id=TAdLayer2 " +
                           "style='position: absolute;visibility:hidden;z-index:1'>" + str_TempRight.Replace("\"", "'") + "<br />" +
                           "<div align='center' style='cursor:pointer;' " +
                           "onclick=" + "\\" + "\"javascript:document.getElementById(\'TAdLayer2\').style.display=\'none\';" +
                           "document.getElementById(\'TAdLayer1\').style.display=\'none\';" + "\\" + "\">>>关闭>></div></div>" +
                           "\");\r" +
                           "TinitAdLoad()\r";
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成对联广告(底部)JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成对联广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi
    public static void CreateAds11(string adsID, string adsClassID)
    {
        string s_picH = "0";
        NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
        DataTable dt = ac.getAdsPicInfo("leftSize", "ads", adsID);
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                string str_leftSize = dt.Rows[0][0].ToString();
                string[] arr_LeftSize = str_leftSize.Split('|');
                s_picH = arr_LeftSize[1].ToString();
            }
            dt.Clear(); dt.Dispose();
        }

        string str_AdsJsstr = "";
        string str_TempLeft = GetAdsLeftStr(adsID);
        string str_TempRight = GetAdsRightStr(adsID);
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            str_AdsJsstr = "function BinitAdLoad()\r" +
                           "{\r" +
                           "    document.all.BAdLayer1.style.visibility = 'visible'; \r" +
                           "    document.all.BAdLayer2.style.visibility = 'visible'; \r" +
                           "    BMoveLeftLayer('BAdLayer1'); \r" +
                           "    BMoveRightLayer('BAdLayer2'); \r" +
                           "}\r" +
                           "function BMoveLeftLayer(layerName) \r" +
                           "{ \r" +
                           "    var x = 5;\r" +
                           "    var y = document.body.scrollTop + document.body.offsetHeight -" + s_picH + "-20;\r" +
                           "    eval(\"document.all.\" + layerName + \".style.posTop = parseInt(y)\"); \r" +
                           "    eval(\"document.all.\" + layerName + \".style.posLeft = x\"); \r" +
                           "    setTimeout(\"BMoveLeftLayer('BAdLayer1');\", 20);\r" +
                           "} \r" +
                           "function BMoveRightLayer(layerName) \r" +
                           "{ \r" +
                           "	var x = 5;\r" +
                           "    var y = document.body.scrollTop + document.body.offsetHeight -" + s_picH + "-20;\r" +
                           "    eval('document.all.' + layerName + '.style.posTop = y');\r" +
                           "    eval('document.all.' + layerName + '.style.posRight = x');\r" +
                           "    setTimeout(\"BMoveRightLayer('BAdLayer2');\", 20);\r" +
                           "} \r" +
                           "document.write(\"<div id=BAdLayer1 style='position: absolute;visibility:hidden;z-index:1'>" +
                           "" + str_TempLeft.Replace("\"", "'") + "</br><div align='center' style='cursor:pointer;' " +
                           "onclick=" + "\\" + "\"javascript:document.getElementById(\'BAdLayer1\').style.display=\'none\';" +
                           "document.getElementById(\'BAdLayer2\').style.display=\'none\';" + "\\" + "\">>>关闭>></div></div>\");\r" +
                           "document.write(\"<div id=BAdLayer2 " +
                           "style='position: absolute;visibility:hidden;z-index:1'>" + str_TempRight.Replace("\"", "'") + "<br />"+
                           "<div align='center' style='cursor:pointer;' " +
                           "onclick=" + "\\" + "\"javascript:document.getElementById(\'BAdLayer1\').style.display=\'none\';" +
                           "document.getElementById(\'BAdLayer2\').style.display=\'none\';" + "\\" + "\">>>关闭>></div></div>" +
                           "\");\r" +
                           "BinitAdLoad()\r";
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成循环广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成循环广告JS文件</returns>
    /// 编写时间2007-04-11   Code By DengXi

    public static void CreateAds9(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        if (checkJs(adsID) == true)
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo("CycAdID,CycSpeed,CycDic,leftPic,leftSize", "ads", adsID);

            if (checkJs(dt.Rows[0]["CycAdID"].ToString()) == true)
                str_AdsJsstr = "document.write('此广告已暂停或失效!')";
            else
            {
                string str_Temp1 = GetAdsLeftStr(adsID);
                string str_Temp2 = GetAdsLeftStr(dt.Rows[0]["CycAdID"].ToString());
                string str_Cycdic = "";
                switch (dt.Rows[0]["CycDic"].ToString())
                {
                    case "0":
                        str_Cycdic = "up";
                        break;
                    case "1":
                        str_Cycdic = "down";
                        break;
                    case "2":
                        str_Cycdic = "left";
                        break;
                    case "3":
                        str_Cycdic = "right";
                        break;
                }
                string str_leftPic = ReplaceDirfile(dt.Rows[0]["leftPic"].ToString());
                string str_leftSize = dt.Rows[0]["leftSize"].ToString();
                string str_CycSpeed = dt.Rows[0]["CycSpeed"].ToString();
                string[] arr_leftSize = str_leftSize.Split('|');

                str_AdsJsstr = "document.write('<marquee onmouseout=start() onmouseover=stop() width=" + arr_leftSize[0].ToString() + " " +
                               "height=" + arr_leftSize[1].ToString() + " direction=" + str_Cycdic + " " +
                               "scrollamount=" + str_CycSpeed + ">" +
                               "" + str_Temp1 + str_Temp2 + "</marquee>');\r";
            }
            dt.Clear();dt.Dispose();
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }

    /// <summary>
    /// 生成文字广告JS文件
    /// </summary>
    /// <param name="adsID">广告编号</param>
    /// <param name="adsClassID">广告栏目编号</param>
    /// <returns>生成文字广告JS文件</returns>
    /// 编写时间2007-04-12   Code By DengXi

    public static void CreateAds10(string adsID, string adsClassID)
    {
        string str_AdsJsstr = "";
        if (checkJs(adsID) == true)
        {
            str_AdsJsstr = "document.write('此广告已暂停或失效!')";
        }
        else
        {
            NetCMS.Content.Ads.Ads ac = new NetCMS.Content.Ads.Ads();
            DataTable dt = ac.getAdsPicInfo("AdTxtNum", "ads", adsID);
            int int_txtnum = 0;
            try
            {
                int_txtnum = int.Parse(dt.Rows[0][0].ToString());
            }
            catch
            {
                int_txtnum = 0;
            }
            dt.Clear();dt.Dispose();
            

            DataTable dv = ac.getAdsPicInfo("Id,AdTxt,AdCss", "adstxt", adsID);

            if (dv != null)
            {
                if (dv.Rows.Count > 0)
                {
                    str_AdsJsstr = "document.write('<table border=\"0\">";
                    str_AdsJsstr += "<tr>";
                    int j = 0;
                    for (int i = 0; i < dv.Rows.Count; i++)
                    {
                        string str_TxtID = dv.Rows[i]["Id"].ToString();
                        str_AdsJsstr += "<td>";
                        str_AdsJsstr += "<a href=\"" + GetDomain(str_SessionID) + "/jsfiles/ads/" +
                                        "adsclick.aspx?Type=Txt&adsID=" + adsID + "\" "+
                                        "target=_blank class=\"" + dv.Rows[i]["AdCss"].ToString() + "\">" +
                                        "" + dv.Rows[i]["AdTxt"].ToString() + "</a>";
                        str_AdsJsstr += "</td>";
                        j++;
                        if (j == int_txtnum)
                        {
                            str_AdsJsstr += "</tr><tr>";
                            j = 0;
                        }
                    }
                    str_AdsJsstr += "</table>');\r";
                }
            }
        }
        CreateJsFile(adsID, adsClassID, str_AdsJsstr);
    }


    /// <summary>
    /// 替换图片路径中的{@dirfile}为实际地址
    /// </summary>
    /// <param name="picpath">图片路径</param>
    /// <returns>返回处理过的图片路径</returns>
    ///Code By DengXi.

    public static string ReplaceDirfile(string picpath)
    {
        string str_Temppath = picpath.Replace("{@dirfile}", NetCMS.Config.UIConfig.dirFile);
        return str_Temppath;
    }
}
