using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

using App.Config;

namespace App.DALProfile
{
    public abstract class DbHelper
    {
        public static void SetTimeoutDefault()
        {
            Timeout = 30;
        }
        public static int Timeout = 30;

        public static IDbBase Provider = null;

        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteNonQuery(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection conn = Provider.CreateConnection())
            {
                conn.ConnectionString = connectionString;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        public static int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteReader(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }
        public static DbDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">连接字符串
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DbDataReader ExecuteReaderPage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string GroupClause, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            DbConnection conn = Provider.CreateConnection();
            conn.ConnectionString = connectionString;
            try
            {
                conn.Open();
                DbCommand cmd = Provider.CreateCommand();
                PrepareCommand(cmd, conn, null, CommandType.Text, "", commandParameters);
                string Sql = GetPageSql(conn, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount);
                if (GroupClause != null && GroupClause.Trim() != "")
                {
                    int n = Sql.ToLower().LastIndexOf(" order by ");
                    Sql = Sql.Substring(0, n) + " " + GroupClause +" "+ Sql.Substring(n);
                }
                cmd.CommandText = Sql;
                DbDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                throw;
            }
        }

        public static DbDataReader ExecuteReader(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteScalar(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            return ExecuteTable(DBConfig.CmsConString, cmdType, cmdText, commandParameters);
        }

        public static DataTable ExecuteTable(string connectionString, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();

            using (DbConnection connection = Provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                DbDataAdapter ap = Provider.CreateDataAdapter();
                ap.SelectCommand = cmd;
                DataSet st = new DataSet();
                ap.Fill(st, "Result");
                cmd.Parameters.Clear();
                return st.Tables["Result"];
            }
        }

        public static DataTable ExecuteTable(DbConnection connection, CommandType cmdType, string cmdText, params DbParameter[] commandParameters)
        {

            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "Result");
            cmd.Parameters.Clear();
            return st.Tables["Result"];
        }

        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            return ExecutePage(DBConfig.CmsConString, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount, commandParameters);
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connectionString">SQL数据库连接字符串</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(string connectionString, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            using (DbConnection connection = Provider.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                return ExecutePage(connection, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, commandParameters);
            }
        }

        /// <summary>
        /// 执行有自定义排序的分页的查询
        /// </summary>
        /// <param name="connection">SQL数据库连接对象</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">查询的表如果包含查询条件，也将条件带上，但不要包含order by子句，也不要包含"from"关键字，如:students a inner join achievement b on a.... where ....</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        public static DataTable ExecutePage(DbConnection connection,string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params DbParameter[] commandParameters)
        {
            DbCommand cmd = Provider.CreateCommand();
            PrepareCommand(cmd, connection, null, CommandType.Text, "", commandParameters);
            string Sql = GetPageSql(connection, cmd, SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out  RecordCount, out  PageCount);
            cmd.CommandText = Sql;
            DbDataAdapter ap = Provider.CreateDataAdapter();
            ap.SelectCommand = cmd;
            DataSet st = new DataSet();
            ap.Fill(st, "PageResult");
            cmd.Parameters.Clear();
            return st.Tables["PageResult"];
        }
        /// <summary>
        /// 取得分页的SQL语句
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <param name="SqlAllFields"></param>
        /// <param name="SqlTablesAndWhere"></param>
        /// <param name="IndexField"></param>
        /// <param name="OrderFields"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="RecordCount"></param>
        /// <param name="PageCount"></param>
        /// <returns></returns>
        private static string GetPageSql(DbConnection connection, DbCommand cmd, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            RecordCount = 0;
            PageCount = 0;
            if (PageSize <= 0)
            {
                PageSize = 10;
            }
            string SqlCount = "select count(" + IndexField + ") from " + SqlTablesAndWhere;
            cmd.CommandText = SqlCount;
            RecordCount = (int)cmd.ExecuteScalar();
            if (RecordCount % PageSize == 0)
            {
                PageCount = RecordCount / PageSize;
            }
            else
            {
                PageCount = RecordCount / PageSize + 1;
            }
            if (PageIndex > PageCount)
                PageIndex = PageCount;
            if (PageIndex < 1)
                PageIndex = 1;
            string Sql = null;
            if (PageIndex == 1)
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from " + SqlTablesAndWhere + " " + OrderFields;
            }
            else
            {
                Sql = "select top " + PageSize + " " + SqlAllFields + " from ";
                if (SqlTablesAndWhere.ToLower().IndexOf(" where ") > 0)
                {
                    string _where = Regex.Replace(SqlTablesAndWhere, @"\ where\ ", " where (", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    Sql += _where + ") and (";
                }
                else
                {
                    Sql += SqlTablesAndWhere + " where (";
                }
                Sql += IndexField + " not in (select top " + (PageIndex - 1) * PageSize + " " + IndexField + " from " + SqlTablesAndWhere + " " + OrderFields;
                Sql += ")) " + OrderFields;
            }
            return Sql;
        }
        private static void PrepareCommand(DbCommand cmd, DbConnection conn, DbTransaction trans, CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;
            cmd.CommandTimeout = Timeout;
            if (cmdParms != null)
            {
                foreach (DbParameter parm in cmdParms)
                {
                    if (parm != null && parm.Value != null)
                    {
                        parm.Value = PrepareParameterValue(parm.Value);
                    }
                    if (parm != null)
                    {
                        cmd.Parameters.Add(parm);
                    }
                }
            }
        }
        //----
        public static DbConnection NewConnection
        {
            get { return new SqlConnection(DBConfig.CmsConString); }
        }
        //准备参数--------------
        #region CreateParameter
        public static IDbDataParameter CreateParameter(string name, DbType type, object value)
        {
            IDbDataParameter prm = Provider.CreateParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.DbType = type;
            prm.Value = DbHelper.PrepareParameterValue(value,true);

            return prm;
        }

        public static IDbDataParameter CreateParameter(string name, DbType type, object value, int size)
        {
            IDbDataParameter prm = Provider.CreateParameter();
            prm.Direction = ParameterDirection.Input;
            prm.ParameterName = name;
            prm.DbType = type;
            prm.Size = size;
            prm.Value = DbHelper.PrepareParameterValue(value,true);

            return prm;
        }

        public static IDbDataParameter CreateParameter(string name, DbType type, object value, ParameterDirection direction)
        {
            IDbDataParameter prm = Provider.CreateParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.DbType = type;
            prm.Value = DbHelper.PrepareParameterValue(value,true);

            return prm;
        }

        public static IDbDataParameter CreateParameter(string name, DbType type, object value, int size, ParameterDirection direction)
        {
            IDbDataParameter prm = Provider.CreateParameter();
            prm.Direction = direction;
            prm.ParameterName = name;
            prm.DbType = type;
            prm.Size = size;
            prm.Value = DbHelper.PrepareParameterValue(value,true);

            return prm;
        }

        private static object PrepareParameterValue(object value)
        {
            return PrepareParameterValue(value, true);
        }

        private static object PrepareParameterValue(object value, bool convertZeroToDBNull)
        {
            if (!convertZeroToDBNull) return value;

            switch (value.GetType().ToString())
            {
                case "System.String":
                    if (Convert.ToString(value) == String.Empty)
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        return value;
                    }
                case "System.Guid":
                    if (new Guid(Convert.ToString(value)) == Guid.Empty)
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        return value;
                    }
                case "System.DateTime":
                    if (Convert.ToDateTime(value) == DateTime.MinValue)
                    {
                        return DBNull.Value;
                    }
                    else
                    {
                        return value;
                    }
                //case "System.Int16":
                //    if (Convert.ToInt16(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                //case "System.Int32":
                //    if (Convert.ToInt32(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                //case "System.Int64":
                //    if (Convert.ToInt64(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                //case "System.Single":
                //    if (Convert.ToSingle(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                //case "System.Double":
                //    if (Convert.ToDouble(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                //case "System.Decimal":
                //    if (Convert.ToDecimal(value) == 0)
                //    {
                //        if (convertZeroToDBNull)
                //        {
                //            return DBNull.Value;
                //        }
                //        else
                //        {
                //            return value;
                //        }
                //    }
                //    else
                //    {
                //        return value;
                //    }
                default:
                    return value;
            }
        }
        #endregion AddParameter
    }
}