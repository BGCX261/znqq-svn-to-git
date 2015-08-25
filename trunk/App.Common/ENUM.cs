using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common
{
    //SP  数据操作类型
    public enum DataOPerType
    {
        ZERO = 0,  //什么都不做
        INSERT,   //--增加
        UPDATE,   //--更新
        DELETE,   //--删除
        RESERVED4,//4
        RESERVED5,//5
        RESERVED6,//6
        RESERVED7,//7
        RESERVED8,//8
        RESERVED9,//9
        RESERVED10,//10
        doCheck,  //11
        doUnCheck,//12
        doCancel,//13
        doUnCancel,//14
        doReview,//15
        doUnReview, //16
        doGenerateDHSal,//17
        doUnGendrateDHsal,//18
        doReturn //19订货单退回
    }

    public enum OPerErrType //数据库操作错误类型
    {
        //一般数据库操作
        SUCCESS =0, //操作成功   -//used
        Error_KeyRepeat=5,//主键重复
        Error_NameRepeat=6,//名称重复
        Error_EXCEPTION=11,
        Error_NOTARGETWH=12,
        Error_INVALIDPARAM=13,//参数非法
        Forbid_ForChecked=14,//已经审核的记录不能执行该操作
        Forbid_ForUnchecked=15,//因为有记录没有审核，禁止后续操作
        Forbid_InvalidStatus = 16, //非可用状态
        Forbid_ExistsSubItems = 17,//存在子记录(比如分类中子分类)
        Forbid_ReferedBySystem=18,//被系统引用(比如该商品存在进货单)
        Forbid_InventoryShortage=19,//库存不足  
        Forbid_RepeatedOperation=20, //重复的操作
        Forbid_AlreadyChecked=21,  //无需审核   --表示即定状态无需更改
        Forbid_AlreadyUnChecked=31,//无需销审   31无需作状态改变
        Forbid_AlreadyCanceled = 41,//无需作费   41无需作状态改变
        Forbid_AlreadyUnCanceled = 51,//无需撤销作费  51无需作状态改变
        Forbid_AlreadyReviewed = 61, //无需复审  61无需作状态改变
        Forbid_AlreadyUnReviewed = 71, //无需撤销复审  71无需作状态改变
        Forbid_ForMonthEnded = 72, //已月结数据，拒绝操作
        Forbid_InvalidChannel =25, //该商品禁止从该渠道进货
        Error_GenerateSitePrdtFailed =26//生成站点产品失败
    }

}
