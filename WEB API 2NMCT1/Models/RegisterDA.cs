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
    public class RegisterDA
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
        public static List<RegistersOrganisation> GetRegisters(int id,IEnumerable<Claim> claims)
        {
            List<RegistersOrganisation> lijst = new List<RegistersOrganisation>();
            List<int> registerid=new List<int>();
            string sql = "SELECT RegisterID FROM Organisation_Register WHERE OrganisationID=@OrganisationID";
         
            DbParameter par1 = Database.addParameter("AdminDB", "@OrganisationID", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql,par1);
            while (reader.Read())
            {

                registerid.Add(int.Parse(reader["OrganisationID"].ToString()));

            }
            reader.Close();
            foreach(int i in registerid)
            {
                string sql2 = "SELECT * FROM Registers WHERE ID=@ID";
                DbParameter par2 = Database.addParameter("AdminDB", "@ID", i);
                DbDataReader reader2 = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql2,par2);
                while (reader2.Read())
                {
                    RegistersOrganisation ro = new RegistersOrganisation();
                    ro.Id = int.Parse(reader2["ID"].ToString());
                    ro.Device = reader2["Device"].ToString();
                    ro.RegisterName = reader2["RegisterName"].ToString();

                   lijst.Add(ro);

                }
            }



            return lijst;
        }

        public static int InsertEmployeeRegister(int RegisterId,long EmployeeId,DateTime from,DateTime untill)
        {

            ConnectionStringSettings con = Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", DBNAME, DBLOGIN, DBPASS);
            string sql = "INSERT INTO Register_Employee VALUES(@RegisterID,@EmployeeID,@From,@Untill)";
            DbParameter par1 = Database.addParameter("AdminDB", "@RegisterID", RegisterId);
            DbParameter par2 = Database.addParameter("AdminDB", "@From", from.ToString("yyyy-MM-dd HH:mm:ss"));
            DbParameter par3 = Database.addParameter("AdminDB", "@Untill", untill.ToString("yyyy-MM-dd HH:mm:ss"));
            DbParameter par4 = Database.addParameter("AdminDB", "@EmployeeID", EmployeeId);

            return Database.InsertData(Database.GetConnection(con), sql, par1, par2,par3,par4);
        }

    }
}