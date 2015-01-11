using models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace WEB_API_2NMCT1.Models
{
    public class ErrorlogDA
    {
        private static string DBNAME = Properties.Settings.Default.DBNAME;
        private static string DBLOGIN = Properties.Settings.Default.DBLOGIN;
        private static string DBPASS = Properties.Settings.Default.DBPASS;
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", dbname, dblogin, dbpass);
        }
        public static int InsertErrorlog(Errorlog e)
        {

            string sql = "INSERT INTO Errorlog VALUES(@RegisterID,@Timestamp,@Message,@Stacktrace)";
            DbParameter par1 = Database.addParameter("AdminDB", "@RegisterID", e.RegisterId);
            DbParameter par2 = Database.addParameter("AdminDB", "@Timestamp", e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            DbParameter par3 = Database.addParameter("AdminDB", "@Message",e.Message);
            DbParameter par4 = Database.addParameter("AdminDB", "@Stacktrace", e.Stacktrace);
         
            try
            {
                return Database.InsertData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", DBNAME, DBLOGIN, DBPASS)), sql, par1, par2, par3, par4);
               
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}