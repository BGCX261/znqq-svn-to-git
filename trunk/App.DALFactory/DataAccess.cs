using System.Reflection;
using System.Configuration;

namespace App.DALFactory
{
    public sealed partial class DataAccess
    {
        private static readonly string path = "App.DALSQLServer"; //App.Config.UIConfig.WebDAL;
       
        public  DataAccess() { }
    }
}
