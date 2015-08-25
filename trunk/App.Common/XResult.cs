using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common
{

    //系统SP设计为退回值大于0，如果返回0则应设置错误，供程序反应给用户

    public class XResult
    {
        public OPerErrType ErrCode = 0;
        public string ErrMsg = string.Empty;
    }
}
