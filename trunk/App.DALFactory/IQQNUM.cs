using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.IO;
using App.Model;
using App.Common;

namespace App.DALFactory
{
    public interface IQQNUM
    {
        bool Delete(string QQ,XResult rst);  //???????

        bool Update(QqnumInfo ins, XResult rst);

        bool Insert(QqnumInfo ins, XResult rst);

        IList<QqnumInfo> ISelect();

        IList<QqnumInfo> ISelect(string strFilter);

        DataTable Select();

        DataTable Select(string strFilter);
    }
    public sealed partial class DataAccess
    {
        public static IQQNUM CreateQQNUM()
        {
            string className = path + ".QQNUM";
            return (IQQNUM)Assembly.Load(path).CreateInstance(className);
        }
    }
}
