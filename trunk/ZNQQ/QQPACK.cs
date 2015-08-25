using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ZNQQ
{
    #region QQPACK
    /// <summary>
    /// This object represents the properties and methods of a QQPACK.
    /// </summary>
    public class QQPACK
    {
        private int _id;
        private string _qQ = String.Empty;
        private string _pASS = String.Empty;
        private string _mD5PASS = String.Empty;
        private string _sP0X0825_BYTES = String.Empty;
        private string _sP0X0825 = String.Empty;
        private string _rP0X0825_BYTES = String.Empty;
        private string _rP0X0825 = String.Empty;
        private string _sP0X0826_BYTES = String.Empty;
        private string _sP0X0826 = String.Empty;
        private string _vERIFYKEY = String.Empty;
        private string _rP0X0826_BYTES = String.Empty;
        private string _rP0X0826 = String.Empty;
        private string _sP0X0828_BYTES = String.Empty;
        private string _sP0X0828 = String.Empty;
        private string _rP0X0828_BYTES = String.Empty;
        private string _rP0X0828 = String.Empty;

        public QQPACK()
        {
        }

        public QQPACK(int id)
        {
            // NOTE: If this reference doesn't exist then add SqlService.cs from the template directory to your solution.
            SqlService sql = new SqlService();
            sql.AddParameter("@ID", SqlDbType.Int, id);
            SqlDataReader reader = sql.ExecuteSqlReader("SELECT * FROM QQPACK WHERE ID = @ID");

            if (reader.Read())
            {
                this.LoadFromReader(reader);
                reader.Close();
            }
            else
            {
                if (!reader.IsClosed) reader.Close();
                throw new ApplicationException("QQPACK does not exist.");
            }
        }

        public QQPACK(SqlDataReader reader)
        {
            this.LoadFromReader(reader);
        }

        protected void LoadFromReader(SqlDataReader reader)
        {
            if (reader != null && !reader.IsClosed)
            {
                _id = reader.GetInt32(0);
                if (!reader.IsDBNull(1)) _qQ = reader.GetString(1);
                if (!reader.IsDBNull(2)) _pASS = reader.GetString(2);
                if (!reader.IsDBNull(3)) _mD5PASS = reader.GetString(3);
                if (!reader.IsDBNull(4)) _sP0X0825_BYTES = reader.GetString(4);
                if (!reader.IsDBNull(5)) _sP0X0825 = reader.GetString(5);
                if (!reader.IsDBNull(6)) _rP0X0825_BYTES = reader.GetString(6);
                if (!reader.IsDBNull(7)) _rP0X0825 = reader.GetString(7);
                if (!reader.IsDBNull(8)) _sP0X0826_BYTES = reader.GetString(8);
                if (!reader.IsDBNull(9)) _sP0X0826 = reader.GetString(9);
                if (!reader.IsDBNull(10)) _vERIFYKEY = reader.GetString(10);
                if (!reader.IsDBNull(11)) _rP0X0826_BYTES = reader.GetString(11);
                if (!reader.IsDBNull(12)) _rP0X0826 = reader.GetString(12);
                if (!reader.IsDBNull(13)) _sP0X0828_BYTES = reader.GetString(13);
                if (!reader.IsDBNull(14)) _sP0X0828 = reader.GetString(14);
                if (!reader.IsDBNull(15)) _rP0X0828_BYTES = reader.GetString(15);
                if (!reader.IsDBNull(16)) _rP0X0828 = reader.GetString(16);
            }
        }

        public void Delete()
        {
            QQPACK.Delete(_id);
        }

        public void Update()
        {
            SqlService sql = new SqlService();
            StringBuilder queryParameters = new StringBuilder();

            sql.AddParameter("@ID", SqlDbType.Int, Id);
            queryParameters.Append("ID = @ID");

            sql.AddParameter("@QQ", SqlDbType.VarChar, QQ);
            queryParameters.Append(", QQ = @QQ");
            sql.AddParameter("@PASS", SqlDbType.VarChar, PASS);
            queryParameters.Append(", PASS = @PASS");
            sql.AddParameter("@MD5PASS", SqlDbType.VarChar, MD5PASS);
            queryParameters.Append(", MD5PASS = @MD5PASS");
            sql.AddParameter("@SP0X0825_BYTES", SqlDbType.VarChar, SP0X0825_BYTES);
            queryParameters.Append(", SP0X0825_BYTES = @SP0X0825_BYTES");
            sql.AddParameter("@SP0X0825", SqlDbType.VarChar, SP0X0825);
            queryParameters.Append(", SP0X0825 = @SP0X0825");
            sql.AddParameter("@RP0X0825_BYTES", SqlDbType.VarChar, RP0X0825_BYTES);
            queryParameters.Append(", RP0X0825_BYTES = @RP0X0825_BYTES");
            sql.AddParameter("@RP0X0825", SqlDbType.VarChar, RP0X0825);
            queryParameters.Append(", RP0X0825 = @RP0X0825");
            sql.AddParameter("@SP0X0826_BYTES", SqlDbType.VarChar, SP0X0826_BYTES);
            queryParameters.Append(", SP0X0826_BYTES = @SP0X0826_BYTES");
            sql.AddParameter("@SP0X0826", SqlDbType.VarChar, SP0X0826);
            queryParameters.Append(", SP0X0826 = @SP0X0826");
            sql.AddParameter("@VERIFYKEY", SqlDbType.VarChar, VERIFYKEY);
            queryParameters.Append(", VERIFYKEY = @VERIFYKEY");
            sql.AddParameter("@RP0X0826_BYTES", SqlDbType.VarChar, RP0X0826_BYTES);
            queryParameters.Append(", RP0X0826_BYTES = @RP0X0826_BYTES");
            sql.AddParameter("@RP0X0826", SqlDbType.VarChar, RP0X0826);
            queryParameters.Append(", RP0X0826 = @RP0X0826");
            sql.AddParameter("@SP0X0828_BYTES", SqlDbType.VarChar, SP0X0828_BYTES);
            queryParameters.Append(", SP0X0828_BYTES = @SP0X0828_BYTES");
            sql.AddParameter("@SP0X0828", SqlDbType.VarChar, SP0X0828);
            queryParameters.Append(", SP0X0828 = @SP0X0828");
            sql.AddParameter("@RP0X0828_BYTES", SqlDbType.VarChar, RP0X0828_BYTES);
            queryParameters.Append(", RP0X0828_BYTES = @RP0X0828_BYTES");
            sql.AddParameter("@RP0X0828", SqlDbType.VarChar, RP0X0828);
            queryParameters.Append(", RP0X0828 = @RP0X0828");

            string query = String.Format("Update QQPACK Set {0} Where ID = @ID", queryParameters.ToString());
            SqlDataReader reader = sql.ExecuteSqlReader(query);
        }

        public void Create()
        {
            SqlService sql = new SqlService();
            StringBuilder queryParameters = new StringBuilder();

            //sql.AddParameter("@ID", SqlDbType.Int, Id);
            //queryParameters.Append("@ID");

            sql.AddParameter("@QQ", SqlDbType.VarChar, QQ);
            queryParameters.Append("@QQ");
            sql.AddParameter("@PASS", SqlDbType.VarChar, PASS);
            queryParameters.Append(", @PASS");
            sql.AddParameter("@MD5PASS", SqlDbType.VarChar, MD5PASS);
            queryParameters.Append(", @MD5PASS");
            sql.AddParameter("@SP0X0825_BYTES", SqlDbType.VarChar, SP0X0825_BYTES);
            queryParameters.Append(", @SP0X0825_BYTES");
            sql.AddParameter("@SP0X0825", SqlDbType.VarChar, SP0X0825);
            queryParameters.Append(", @SP0X0825");
            sql.AddParameter("@RP0X0825_BYTES", SqlDbType.VarChar, RP0X0825_BYTES);
            queryParameters.Append(", @RP0X0825_BYTES");
            sql.AddParameter("@RP0X0825", SqlDbType.VarChar, RP0X0825);
            queryParameters.Append(", @RP0X0825");
            sql.AddParameter("@SP0X0826_BYTES", SqlDbType.VarChar, SP0X0826_BYTES);
            queryParameters.Append(", @SP0X0826_BYTES");
            sql.AddParameter("@SP0X0826", SqlDbType.VarChar, SP0X0826);
            queryParameters.Append(", @SP0X0826");
            sql.AddParameter("@VERIFYKEY", SqlDbType.VarChar, VERIFYKEY);
            queryParameters.Append(", @VERIFYKEY");
            sql.AddParameter("@RP0X0826_BYTES", SqlDbType.VarChar, RP0X0826_BYTES);
            queryParameters.Append(", @RP0X0826_BYTES");
            sql.AddParameter("@RP0X0826", SqlDbType.VarChar, RP0X0826);
            queryParameters.Append(", @RP0X0826");
            sql.AddParameter("@SP0X0828_BYTES", SqlDbType.VarChar, SP0X0828_BYTES);
            queryParameters.Append(", @SP0X0828_BYTES");
            sql.AddParameter("@SP0X0828", SqlDbType.VarChar, SP0X0828);
            queryParameters.Append(", @SP0X0828");
            sql.AddParameter("@RP0X0828_BYTES", SqlDbType.VarChar, RP0X0828_BYTES);
            queryParameters.Append(", @RP0X0828_BYTES");
            sql.AddParameter("@RP0X0828", SqlDbType.VarChar, RP0X0828);
            queryParameters.Append(", @RP0X0828");

            string query = String.Format("Insert Into QQPACK ({0}) Values ({1})", queryParameters.ToString().Replace("@", ""), queryParameters.ToString());
            SqlDataReader reader = sql.ExecuteSqlReader(query);
        }

        public static QQPACK NewQQPACK(int id)
        {
            QQPACK newEntity = new QQPACK();
            newEntity._id = id;

            return newEntity;
        }

        #region Public Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string QQ
        {
            get { return _qQ; }
            set { _qQ = value; }
        }

        public string PASS
        {
            get { return _pASS; }
            set { _pASS = value; }
        }

        public string MD5PASS
        {
            get { return _mD5PASS; }
            set { _mD5PASS = value; }
        }

        public string SP0X0825_BYTES
        {
            get { return _sP0X0825_BYTES; }
            set { _sP0X0825_BYTES = value; }
        }

        public string SP0X0825
        {
            get { return _sP0X0825; }
            set { _sP0X0825 = value; }
        }

        public string RP0X0825_BYTES
        {
            get { return _rP0X0825_BYTES; }
            set { _rP0X0825_BYTES = value; }
        }

        public string RP0X0825
        {
            get { return _rP0X0825; }
            set { _rP0X0825 = value; }
        }

        public string SP0X0826_BYTES
        {
            get { return _sP0X0826_BYTES; }
            set { _sP0X0826_BYTES = value; }
        }

        public string SP0X0826
        {
            get { return _sP0X0826; }
            set { _sP0X0826 = value; }
        }

        public string VERIFYKEY
        {
            get { return _vERIFYKEY; }
            set { _vERIFYKEY = value; }
        }

        public string RP0X0826_BYTES
        {
            get { return _rP0X0826_BYTES; }
            set { _rP0X0826_BYTES = value; }
        }

        public string RP0X0826
        {
            get { return _rP0X0826; }
            set { _rP0X0826 = value; }
        }

        public string SP0X0828_BYTES
        {
            get { return _sP0X0828_BYTES; }
            set { _sP0X0828_BYTES = value; }
        }

        public string SP0X0828
        {
            get { return _sP0X0828; }
            set { _sP0X0828 = value; }
        }

        public string RP0X0828_BYTES
        {
            get { return _rP0X0828_BYTES; }
            set { _rP0X0828_BYTES = value; }
        }

        public string RP0X0828
        {
            get { return _rP0X0828; }
            set { _rP0X0828 = value; }
        }
        #endregion

        public static QQPACK GetQQPACK(int id)
        {
            return new QQPACK(id);
        }

        public static void Delete(int id)
        {
            SqlService sql = new SqlService();
            sql.AddParameter("@ID", SqlDbType.Int, id);

            SqlDataReader reader = sql.ExecuteSqlReader("Delete QQPACK Where ID = @ID");
        }
    }
    #endregion
}

