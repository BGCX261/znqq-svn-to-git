using System;
using System.Reflection;
using System.Data;

namespace App.DALFactory
{
    public interface IDatabase
    {
        DataTable ExecuteSql(string sqlText);
        //int backSqlData(int type, string backpath);
        void Replace(string oldTxt, string newTxt, string Table, string FieldName);
    }
    public sealed partial class DataAccess
    {
        public static IDatabase CreateDatabase()
        {
            string className = path + ".Database";
            return (IDatabase)Assembly.Load(path).CreateInstance(className);
        }
    }

}
