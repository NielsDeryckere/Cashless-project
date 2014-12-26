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


            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            //ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;
            while (reader.Read())
            {
                Employee c = new Employee();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.EmployeeName = reader["EmployeeName"].ToString();
                c.Address = reader["Address"].ToString();
                c.Email = reader["Email"].ToString();
                c.Phone = reader["Phone"].ToString();
                //if (!DBNull.Value.Equals(reader["Picture"]))
                //    c.Picture = (byte[])reader["Picture"];
                //else
                //    c.Picture = new byte[0];
               
                c.Barcode = reader["BarCode"].ToString();
                list.Add(c);
            }

            return list;
        }

        public static int InsertEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Employee VALUES(@EmployeeName,@Address,@Email,@Phone,@Barcode)";
            DbParameter par1 = Database.addParameter("AdminDB", "@EmployeeName", c.EmployeeName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Email", c.Email);
            DbParameter par4 = Database.addParameter("AdminDB", "@Phone", c.Phone);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);
        }

        public static void UpdateEmployee(Employee c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Employee SET EmployeeName=@EmployeeName, Address=@Address, Email=@Email, Phone=@Phone,Barcode=@Barcode WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@EmployeeName", c.EmployeeName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Email", c.Email);
            DbParameter par4 = Database.addParameter("AdminDB", "@Phone", c.Phone);
            DbParameter par5 = Database.addParameter("AdminDB", "@ID", c.Id);
            DbParameter par6 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6);
        }

        public static void DeleteEmployee(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Employee WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}