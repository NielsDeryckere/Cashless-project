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
    public class CustomerDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", dbname, dblogin, dbpass);
        }

        public static List<Customer> GetCustomers(IEnumerable<Claim> claims)
        {
            //ConfigurationManager.ConnectionStrings.Add(CreateConnectionString(claims));

            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customer";


            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

            //ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;
            while (reader.Read())
            {
                Customer c = new Customer();
                c.Id = Convert.ToInt32(reader["ID"]);
                c.CustomerName = reader["CustomerName"].ToString();
                c.Address = reader["Address"].ToString();
                if (!DBNull.Value.Equals(reader["Picture"]))
                    c.Picture = (byte[])reader["Picture"];
                else
                    c.Picture = new byte[0];
                c.Balance = Double.Parse(reader["Balance"].ToString());
                c.Barcode = reader["BarCode"].ToString();
                list.Add(c);
            }

            return list;
        }

        public static int InsertCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Customer VALUES(@CustomerName,@Address,@Picture,@Balance,@Barcode)";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4,par5);
        }

        public static void UpdateCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customer SET CustomerName=@CustomerName, Address=@Address, Picture=@Picture, Balance=@Balance,Barcode=@Barcode WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
            DbParameter par5 = Database.addParameter("AdminDB", "@ID", c.Id);
            DbParameter par6 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5,par6);
        }

        public static void DeleteCustomer(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Customer WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }
    }
}