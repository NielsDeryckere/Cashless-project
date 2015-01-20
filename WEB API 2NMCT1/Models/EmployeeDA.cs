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
    public class EmployeeDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", dbname, dblogin, dbpass);
        }

        public static List<Employee> GetEmployees(IEnumerable<Claim> claims)
        {
            //ConfigurationManager.ConnectionStrings.Add(CreateConnectionString(claims));

            List<Employee> list = new List<Employee>();
            string sql = "SELECT * FROM Employee";

            try
            {  DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            //ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;
            while (reader.Read())
            {
                Employee c = new Employee();
               
                c.EmployeeName = reader["EmployeeName"].ToString();
                c.Address = reader["Address"].ToString();
                c.Email = reader["Email"].ToString();
                c.Phone = reader["Phone"].ToString();
                //if (!DBNull.Value.Equals(reader["Picture"]))
                //    c.Picture = (byte[])reader["Picture"];
                //else
                //    c.Picture = new byte[0];
               
                c.Barcode = Int64.Parse(reader["BarCode"].ToString());
                c.Active = bool.Parse(reader["Active"].ToString());
                list.Add(c);
            }
            reader.Close();
            return list;

            }
            catch (Exception)
            {

                return null;
            }

          
        }

        

        public static List<Employee> GetEmployees()
        {
            string DBNAME = Properties.Settings.Default.DBNAME;
            string DBLOGIN = Properties.Settings.Default.DBLOGIN;
            string DBPASS = Properties.Settings.Default.DBPASS;

            List<Employee> lijst = new List<Employee>();
            string sql = "SELECT * FROM Employee";
            try
            {
                    DbDataReader reader = Database.GetData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", DBNAME, DBLOGIN, DBPASS)), sql);
                while (reader.Read())
                {
                    Employee c = new Employee();
                    c.Id = c.Id = int.Parse(reader["ID"].ToString());
                    c.EmployeeName = reader["EmployeeName"].ToString();
                    c.Address = reader["Address"].ToString();
                    c.Email = reader["Email"].ToString();
                    c.Phone = reader["Phone"].ToString();
                    c.Active = bool.Parse(reader["Active"].ToString());
                    //if (!DBNull.Value.Equals(reader["Picture"]))
                    //    c.Picture = (byte[])reader["Picture"];
                    //else
                    //    c.Picture = new byte[0];

                    c.Barcode = Int64.Parse(reader["Barcode"].ToString());
                    lijst.Add(c);

               
                }
                reader.Close();




                return lijst;

            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public static int InsertEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employee VALUES(@EmployeeName,@Address,@Email,@Phone,@Barcode,@Active)";
            DbParameter par1 = Database.addParameter("AdminDB", "@EmployeeName", c.EmployeeName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Email", c.Email);
            DbParameter par4 = Database.addParameter("AdminDB", "@Phone", c.Phone);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            DbParameter par6 = Database.addParameter("AdminDB", "@Active", 1);
            try
            {  
                return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5,par6);

            }
            catch (Exception ex)
            {
              
                return 0;
            }
           
        }

        public static int UpdateEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employee SET EmployeeName=@EmployeeName, Address=@Address, Email=@Email, Phone=@Phone WHERE Barcode=@Barcode";
            DbParameter par1 = Database.addParameter("AdminDB", "@EmployeeName", c.EmployeeName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Email", c.Email);
            DbParameter par4 = Database.addParameter("AdminDB", "@Phone", c.Phone);
          
            DbParameter par6 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);

            try
            {
                return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par6);

            }
            catch (Exception)
            {

                return 0;
            }
          
        }

        public static int NotActiveToActive(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employee SET EmployeeName=@EmployeeName, Address=@Address, Email=@Email, Phone=@Phone, Active=@Active WHERE Barcode=@Barcode";
            DbParameter par1 = Database.addParameter("AdminDB", "@EmployeeName", c.EmployeeName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Email", c.Email);
            DbParameter par4 = Database.addParameter("AdminDB", "@Phone", c.Phone);
            DbParameter par5 = Database.addParameter("AdminDB", "@Active", 1);
            DbParameter par6 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);

            try
            {
                return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4,par5, par6);

            }
            catch (Exception)
            {

                return 0;
            }

        }

        public static int DeleteEmployee(long id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employee SET Active=@Active WHERE Barcode=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@ID", id);
            DbParameter par2 = Database.addParameter("AdminDB", "@Active", 0);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            try
            { 
                return Database.ModifyData(con, sql, par1,par2);
               

            }
            catch (Exception)
            {

                return 0;
            }
          
        }
    }
}