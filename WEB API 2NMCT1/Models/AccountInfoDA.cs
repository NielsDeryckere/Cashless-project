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
    public class AccountInfoDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", dbname, dblogin, dbpass);
        }
        public static Organisation GetAccountInfo(IEnumerable<Claim>claims)
        {
            string login = claims.FirstOrDefault(c => c.Type == "login").Value;
            string pass = claims.FirstOrDefault(c => c.Type == "pass").Value;
            string sql = "SELECT * FROM Organisation WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.addParameter("AdminDB", "@Login",Cryptography.Encrypt( login));
            DbParameter par2 = Database.addParameter("AdminDB", "@Password",Cryptography.Encrypt( pass));
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
                reader.Read();
                return new Organisation()
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Login = Cryptography.Decrypt( reader["Login"].ToString()),
                    Password =Cryptography.Decrypt( reader["Password"].ToString()),
                    DbName = Cryptography.Decrypt( reader["DbName"].ToString()),
                    DbLogin =Cryptography.Decrypt( reader["DbLogin"].ToString()),
                    DbPassword =Cryptography.Decrypt( reader["DbPassword"].ToString()),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        
        
        }
        public static void UpdateOrganisation(Organisation o,string pass,IEnumerable<Claim> claims)
        {
             string sql = "UPDATE Organisation SET Password=@pass WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@pass",pass);
            DbParameter par2 = Database.addParameter("AdminDB", "@ID",o.ID);
          
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);

        }

    }
}