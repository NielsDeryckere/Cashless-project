using models;
using modelsProject;
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
        public static List<RegisterClient> GetRegisters(IEnumerable<Claim> claims)
        {
            List<RegisterClient> lijst = new List<RegisterClient>();
           
          
                string sql2 = "SELECT * FROM Registers";
                try
                    { DbDataReader reader2 = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql2);
                    while (reader2.Read())
                    {
                        RegisterClient r= new RegisterClient();
                        r.RegisterID = int.Parse(reader2["ID"].ToString());
                        r.DeviceName = reader2["Device"].ToString();
                        r.RegisterName = reader2["RegisterName"].ToString();
                  

                       lijst.Add(r);

                    }
            



                return lijst;

                }
                catch (Exception)
                {

                    return null;
                }
               
        }

        public static int InsertEmployeeRegister(EmployeeRegister er)
        {

            ConnectionStringSettings con = Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", DBNAME, DBLOGIN, DBPASS);
            string sql = "INSERT INTO Register_Employee VALUES(@RegisterID,@EmployeeID,@From,@Untill)";
            DbParameter par1 = Database.addParameter("AdminDB", "@RegisterID", er.RegisterID);
            DbParameter par2 = Database.addParameter("AdminDB", "@From", er.From.ToString("yyyy-MM-dd HH:mm:ss"));
            DbParameter par3 = Database.addParameter("AdminDB", "@Untill", er.Untill.ToString("yyyy-MM-dd HH:mm:ss"));
            DbParameter par4 = Database.addParameter("AdminDB", "@EmployeeID", er.EmployeeID);
            try
            {
                return Database.InsertData(Database.GetConnection(con), sql, par1, par2,par3,par4);
            }
            catch (Exception)
            {

                return 0;
            }
            
        }

        public static List<EmployeeRegister> GetRegisterEmployee(int RegisterId,IEnumerable<Claim> claims)
        {
            List<EmployeeRegister> list = new List<EmployeeRegister>();
            string sql = "SELECT * FROM Register_Employee WHERE RegisterID=@id";
            DbParameter par1 = Database.addParameter("AdminDB", "@id", RegisterId);

            try
            {  DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql,par1);

                while(reader.Read())
                {
                    EmployeeRegister er = new EmployeeRegister()
                    {

                        EmployeeID = long.Parse(reader["EmployeeID"].ToString()),
                        RegisterID = int.Parse(reader["RegisterID"].ToString()),
                        From = DateTime.ParseExact(reader["Fromt"].ToString(), "yyyy-MM-dd HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture),

                    Untill=DateTime.ParseExact(reader["Untilt"].ToString(), "yyyy-MM-dd HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture)

                    };
                    list.Add(er);

            }

            return list;

            }
            catch (Exception)
            {

                return null;
            }
          

        }

    }
}