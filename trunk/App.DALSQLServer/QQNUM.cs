using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using App.Model;
using App.DALFactory;
using App.DALProfile;
using App.Config;
using App.Common;

namespace App.DALSQLServer
{
	#region QQNUM
	/// <summary>
	/// This object represents the properties and methods of a QQNUM.
	/// </summary>
	public class QQNUM :DbBase,IQQNUM
	{
        
        private QqnumInfo inner;
        
        public QqnumInfo INNER { get { return inner; } }
        	
		public QQNUM()
		{
		}
		
		public QQNUM(string QQ)
		{
            		// NOTE: If this reference doesn't exist then add SqlService.cs from the template directory to your solution.
			SqlParameter param = new SqlParameter("@QQ", SqlDbType.VarChar);
			param.Value = QQ;
            IDataReader reader =DbHelper.ExecuteReader(CommandType.Text,"SELECT * FROM QQNUM WHERE QQ = @QQ",param);
			
			if (reader.Read()) 
			{
				this.LoadFromReader(reader);
				reader.Close();
			}
			else
			{
				if (!reader.IsClosed) reader.Close();
				//throw new ApplicationException("QQNUM does not exist.");
			}
		}
		
		public QQNUM(IDataReader reader)
		{
			this.LoadFromReader(reader);
		}
		
		protected void LoadFromReader(IDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
			{
                inner = new QqnumInfo();
				if (!reader.IsDBNull(0)) inner.ID = reader.GetInt32(0);
				if (!reader.IsDBNull(1)) inner.QQ = reader.GetString(1);
				if (!reader.IsDBNull(2)) inner.PASS = reader.GetString(2);
				if (!reader.IsDBNull(3)) inner.MD5PASS = reader.GetString(3);
				if (!reader.IsDBNull(4)) inner.SP0X0825_BYTES = reader.GetString(4);
				if (!reader.IsDBNull(5)) inner.SP0X0825 = reader.GetString(5);
				if (!reader.IsDBNull(6)) inner.RP0X0825_BYTES = reader.GetString(6);
				if (!reader.IsDBNull(7)) inner.RP0X0825 = reader.GetString(7);
				if (!reader.IsDBNull(8)) inner.SP0X0826_BYTES = reader.GetString(8);
				if (!reader.IsDBNull(9)) inner.SP0X0826 = reader.GetString(9);
				if (!reader.IsDBNull(10)) inner.VERIFYKEY = reader.GetString(10);
				if (!reader.IsDBNull(11)) inner.RP0X0826_BYTES = reader.GetString(11);
				if (!reader.IsDBNull(12)) inner.RP0X0826 = reader.GetString(12);
				if (!reader.IsDBNull(13)) inner.SP0X0828_BYTES = reader.GetString(13);
				if (!reader.IsDBNull(14)) inner.SP0X0828 = reader.GetString(14);
				if (!reader.IsDBNull(15)) inner.RP0X0828_BYTES = reader.GetString(15);
				if (!reader.IsDBNull(16)) inner.RP0X0828 = reader.GetString(16);
			}
            else
            {
                inner = null;
            }
		}
		
		public bool Delete(string QQ, XResult rst)  //???????
		{
            QqnumInfo ins = new QQNUM(QQ).INNER;
            this.usp_QQNUM(ins, DataOPerType.DELETE, rst);
			return true;
        }
		
		public bool Update(QqnumInfo ins, XResult rst)
		{
            this.usp_QQNUM(ins, DataOPerType.UPDATE, rst);
		    return true;
        }
		
		public bool Insert(QqnumInfo ins, XResult rst)
		{
            this.usp_QQNUM(ins, DataOPerType.INSERT, rst);
		    return true;
        }	
        
        public IList <QqnumInfo> ISelect()
		{
			    		// NOTE: If this reference doesn't exist then add SqlService.cs from the template directory to your solution.
			//SqlParameter param = new SqlParameter("@QQ", SqlDbType.VarChar);
			//param.Value = QQ;
            IDataReader reader =DbHelper.ExecuteReader(CommandType.Text,"SELECT * FROM QQNUM",null);
			
            IList <QqnumInfo> list =new List <QqnumInfo>();

			while(reader.Read()) 
			{
				this.LoadFromReader(reader);
                list.Add(this.inner);
			}
            reader.Close();
            return list;
        }
        
        public IList <QqnumInfo> ISelect(string strFilter)
		{
			// NOTE: If this reference doesn't exist then add SqlService.cs from the template directory to your solution.
			//SqlParameter param = new SqlParameter("@QQ", SqlDbType.VarChar);
			//param.Value = QQ;
            IDataReader reader =DbHelper.ExecuteReader(CommandType.Text,"SELECT * FROM QQNUM WHERE " + strFilter,null);
			
            IList <QqnumInfo> list =new List <QqnumInfo>();

			while(reader.Read()) 
			{
				this.LoadFromReader(reader);
                list.Add(this.inner);
			}
            reader.Close();
            return list;
        }
        
        public DataTable Select()
		{
			//SqlParameter param = new SqlParameter("@QQ", SqlDbType.VarChar);
	        //param.Value = QQ;
			return DbHelper.ExecuteTable(CommandType.Text,"SELECT * FROM QQNUM",null);
		}
        
        public DataTable Select(string strFilter)
		{
			//SqlParameter param = new SqlParameter("@QQ", SqlDbType.VarChar);
	        //param.Value = QQ;
			return DbHelper.ExecuteTable(CommandType.Text,"SELECT * FROM QQNUM WHERE " + strFilter,null);
		}
        
        //public DataTable SelectLike(string strLike)
		//{
        //    SqlParameter param = new SqlParameter("@LIKE",SqlDbType.VarChar);
        //    param.Value = strLike;
		//	return DbHelper.ExecuteTable(CommandType.Text,"SELECT * FROM QQNUM WHERE AAA LIKE @LIKE OR BBB LIKE @LIKE",param);
		//}
        
        public QqnumInfo GetInfo(string QQ)
        {
            try
            {
                return new QQNUM(QQ).INNER;
            }
            catch
            {
                return null;
            }
        }
        
        //UpdateDB
        #region Update Methods
        public bool UpdateDB(DataTable dt, XResult rst)
        {
            SqlCommand _comm = DbHelper.Provider.CreateCommand() as SqlCommand;
            SqlConnection _conn = DbHelper.NewConnection as SqlConnection;
            _conn.Open();
            _comm.Connection = _conn;
            _comm.Transaction = _conn.BeginTransaction();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr.RowState)
                    {
                        case DataRowState.Added:
                            {
                                this.usp_QQNUM(dr, DataOPerType.INSERT, rst);
                                if (rst.ErrCode != OPerErrType.SUCCESS) return false;
                                break;
                            }
                        case DataRowState.Modified:
                            {
                                this.usp_QQNUM(dr, DataOPerType.UPDATE, rst);
                                if (rst.ErrCode != OPerErrType.SUCCESS) return false;
                                break;
                            }
                        case DataRowState.Deleted:
                            {
                                this.usp_QQNUM(dr, DataOPerType.DELETE, rst);
                                if (rst.ErrCode != OPerErrType.SUCCESS) return false;
                                break;
                            }
                    }
                }
                _comm.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _comm.Transaction.Rollback();
                return false;
            }

        }
        #endregion
    
        public bool usp_QQNUM(DataRow dr, DataOPerType dot, XResult rst)
        {
            SqlCommand _comm = DbHelper.Provider.CreateCommand() as SqlCommand;
            SqlConnection _conn = DbHelper.NewConnection as SqlConnection;
            _comm.Connection = _conn;
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.CommandText = "usp_QQNUM";
            _conn.Open();
            _comm.Parameters.Add(DbHelper.CreateParameter("@ID", DbType.Int32, dr.RowState == DataRowState.Deleted ? (object)dr["ID", DataRowVersion.Original]:(object)dr["ID"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@QQ", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["QQ", DataRowVersion.Original]:(object)dr["QQ"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@PASS", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["PASS", DataRowVersion.Original]:(object)dr["PASS"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@MD5PASS", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["MD5PASS", DataRowVersion.Original]:(object)dr["MD5PASS"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0825_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0825_BYTES", DataRowVersion.Original]:(object)dr["SP0X0825_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0825", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0825", DataRowVersion.Original]:(object)dr["SP0X0825"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0825_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0825_BYTES", DataRowVersion.Original]:(object)dr["RP0X0825_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0825", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0825", DataRowVersion.Original]:(object)dr["RP0X0825"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0826_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0826_BYTES", DataRowVersion.Original]:(object)dr["SP0X0826_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0826", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0826", DataRowVersion.Original]:(object)dr["SP0X0826"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@VERIFYKEY", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["VERIFYKEY", DataRowVersion.Original]:(object)dr["VERIFYKEY"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0826_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0826_BYTES", DataRowVersion.Original]:(object)dr["RP0X0826_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0826", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0826", DataRowVersion.Original]:(object)dr["RP0X0826"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0828_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0828_BYTES", DataRowVersion.Original]:(object)dr["SP0X0828_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0828", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["SP0X0828", DataRowVersion.Original]:(object)dr["SP0X0828"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0828_BYTES", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0828_BYTES", DataRowVersion.Original]:(object)dr["RP0X0828_BYTES"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0828", DbType.AnsiString, dr.RowState == DataRowState.Deleted ? (object)dr["RP0X0828", DataRowVersion.Original]:(object)dr["RP0X0828"]));
            _comm.Parameters.Add(DbHelper.CreateParameter("@DataOPerType", DbType.Int16, dot));
            _comm.Parameters.Add(DbHelper.CreateParameter("@ErrCode", DbType.Int16, rst.ErrCode, 2, ParameterDirection.Output));
            _comm.Parameters.Add(DbHelper.CreateParameter("@ErrMsg", DbType.AnsiString, rst.ErrMsg, 1000, ParameterDirection.Output));
            _comm.ExecuteNonQuery();
            rst.ErrCode = (OPerErrType)Enum.ToObject(typeof(OPerErrType), _comm.Parameters["@ErrCode"].Value);
            rst.ErrMsg = _comm.Parameters["@ErrMsg"].Value.ToString();
            return true; //return
        }
        
        public bool usp_QQNUM(QqnumInfo ins, DataOPerType dot, XResult rst)
        {
            SqlCommand _comm = DbHelper.Provider.CreateCommand() as SqlCommand;
            SqlConnection _conn = DbHelper.NewConnection as SqlConnection;
            _comm.Connection = _conn;
            _comm.CommandType = CommandType.StoredProcedure;
            _comm.CommandText = "usp_QQNUM";
            _conn.Open();
            _comm.Parameters.Add(DbHelper.CreateParameter("@ID", DbType.Int32, ins.ID));
            _comm.Parameters.Add(DbHelper.CreateParameter("@QQ", DbType.AnsiString, ins.QQ));
            _comm.Parameters.Add(DbHelper.CreateParameter("@PASS", DbType.AnsiString, ins.PASS));
            _comm.Parameters.Add(DbHelper.CreateParameter("@MD5PASS", DbType.AnsiString, ins.MD5PASS));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0825_BYTES", DbType.AnsiString, ins.SP0X0825_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0825", DbType.AnsiString, ins.SP0X0825));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0825_BYTES", DbType.AnsiString, ins.RP0X0825_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0825", DbType.AnsiString, ins.RP0X0825));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0826_BYTES", DbType.AnsiString, ins.SP0X0826_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0826", DbType.AnsiString, ins.SP0X0826));
            _comm.Parameters.Add(DbHelper.CreateParameter("@VERIFYKEY", DbType.AnsiString, ins.VERIFYKEY));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0826_BYTES", DbType.AnsiString, ins.RP0X0826_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0826", DbType.AnsiString, ins.RP0X0826));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0828_BYTES", DbType.AnsiString, ins.SP0X0828_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@SP0X0828", DbType.AnsiString, ins.SP0X0828));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0828_BYTES", DbType.AnsiString, ins.RP0X0828_BYTES));
            _comm.Parameters.Add(DbHelper.CreateParameter("@RP0X0828", DbType.AnsiString, ins.RP0X0828));
            _comm.Parameters.Add(DbHelper.CreateParameter("@DataOPerType", DbType.Int16, dot));
            _comm.Parameters.Add(DbHelper.CreateParameter("@ErrCode", DbType.Int16, rst.ErrCode, 2, ParameterDirection.Output));
            _comm.Parameters.Add(DbHelper.CreateParameter("@ErrMsg", DbType.AnsiString, rst.ErrMsg, 1000, ParameterDirection.Output));
            _comm.ExecuteNonQuery();
            rst.ErrCode = (OPerErrType)Enum.ToObject(typeof(OPerErrType), _comm.Parameters["@ErrCode"].Value);
            rst.ErrMsg = _comm.Parameters["@ErrMsg"].Value.ToString();
            return true; //return
        }

        public bool Delete(string QQ)
        {
            throw new NotImplementedException();
        }

        public bool Update(QqnumInfo ins)
        {
            throw new NotImplementedException();
        }

        public bool Insert(QqnumInfo ins)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDB(DataTable dtMaster, DataTable dtDetail)
        {
            throw new NotImplementedException();
        }

        public bool CheckNUM(string strNUM)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDB(DataTable dt)
        {
            throw new NotImplementedException();
        }
    }
	#endregion
}












