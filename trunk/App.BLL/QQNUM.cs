using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using App.Model;
using App.DALFactory;
using App.Common;

namespace App.BLL
{

    public class QQNUM
    {
        private IQQNUM dal;
        public QQNUM()
        {
            dal = DataAccess.CreateQQNUM();
        }
        public bool Delete(string QQ, XResult rst)  //???????
		{
            return dal.Delete(QQ,rst);
        }
        public bool Update(QqnumInfo ins, XResult rst)
		{
            return dal.Update(ins,rst);
        }
        public bool Insert(QqnumInfo ins, XResult rst)
        {
            return dal.Insert(ins,rst);
        }
        public IList <QqnumInfo> ISelect()
        {
            return dal.ISelect();
        }
        public IList <QqnumInfo> ISelect(string strFilter)
        {
            return dal.ISelect(strFilter);
        }
        public DataTable Select()
        {
            return dal.Select();
        }
        public DataTable Select(string strFilter)
        {
            return dal.Select(strFilter);
        }
    }
}



