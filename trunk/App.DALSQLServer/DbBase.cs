//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using App.Config;
using App.DALProfile;

namespace App.DALSQLServer
{
    public class DbBase : IDbBase
    {
        DbCommand IDbBase.CreateCommand()
        {
            return new SqlCommand();
        }
        DbConnection IDbBase.CreateConnection()
        {
            return new SqlConnection();
        }
        DbDataAdapter IDbBase.CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }
        DbParameter IDbBase.CreateParameter()
        {
            return new SqlParameter();
        }
        protected string Pre;
        public DbBase()
        {
            string Pre = DBConfig.TableNamePrefix;
            DbHelper.Provider = this;
        }
    }
}
