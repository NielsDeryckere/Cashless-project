using models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace WEB_API_2NMCT1.Models
{
    public class OrganisationDA
    {
        public static Organisation CheckCredentials(string username, string password)
        {
            string sql = "SELECT * FROM Organisation WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.addParameter("AdminDB", "@Login", username);
            DbParameter par2 = Database.addParameter("AdminDB", "@Password",password);
            try
            {
                DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
                reader.Read();
                return new Organisation()
                {
                    ID = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
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
    }
}