using models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace WEB_API_2NMCT1.Models
{
    public class CustomerDA
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

        public static List<Customer> GetCustomers(IEnumerable<Claim> claims)
        {
            //ConfigurationManager.ConnectionStrings.Add(CreateConnectionString(claims));

            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customer";

            try
            { DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);

                //ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;
                while (reader.Read())
                {
                    Customer c = new Customer();
                    c.Id = int.Parse(reader["ID"].ToString());
                    c.CustomerName = reader["CustomerName"].ToString();
                    c.Address = reader["Address"].ToString();
                    if (!DBNull.Value.Equals(reader["Picture"]))
                        c.Picture = (byte[])reader["Picture"];
                    else
                        c.Picture = new byte[0];
                    c.Balance = Double.Parse(reader["Balance"].ToString());
                    c.Barcode = Int64.Parse(reader["BarCode"].ToString());
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

       

        public static List<Customer> GetCustomers()
        {
            //ConfigurationManager.ConnectionStrings.Add(CreateConnectionString(claims));

            List<Customer> list = new List<Customer>();
            string sql = "SELECT * FROM Customer";

            try
            { DbDataReader reader = Database.GetData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT",DBNAME,DBLOGIN,DBPASS)), sql);

            //ConnectionStringSettingsCollection ConnectionStrings = ConfigurationManager.ConnectionStrings;
                while (reader.Read())
                {
                    Customer c = new Customer();
               
                    c.CustomerName = reader["CustomerName"].ToString();
                    c.Address = reader["Address"].ToString();
                    if (!DBNull.Value.Equals(reader["Picture"]))
                        c.Picture = (byte[])reader["Picture"];
                    else
                        c.Picture = new byte[0];
                    c.Balance = Double.Parse(reader["Balance"].ToString());
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

        //public static Customer GetCustomer(int id)
        //{
           
        //    Customer c = new Customer();
        //    string sql = "SELECT * FROM Customer WHERE Barcode=@Barcode";
            
        //    DbParameter par1 = Database.addParameter("AdminDB", "@Barcode", barcode );
        //    DbDataReader reader=Database.GetData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT",DBNAME,DBLOGIN,DBPASS)), sql,par1);
        //    reader.Read();
           

        //       c.Id = (int)reader["ID"];
        //        c.CustomerName = (string)reader["CustomerName"];
        //        c.Address = (string)reader["Address"];
        //        c.Balance = (double)reader["Balance"];
        //        c.Picture = (byte[])reader["Picture"];
        //        c.Barcode = (long)reader["Barcode"];

            
        //    reader.Close();
        //    return c;
        //}

        public static int InsertCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Customer VALUES(@CustomerName,@Address,@Picture,@Balance,@Barcode,@Active)";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            DbParameter par6=Database.addParameter("AdminDB","@Active",1);

            try
            {
                return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4,par5,par6);

            }
            catch (Exception)
            {

                return 0;
            }
            
        }
        public static int NotActiveToActive(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customer SET CustomerName=@CustomerName,Address=@Address,Picture=@Picture,Balance=@Balance,Active=@Active WHERE Barcode=@Barcode";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            DbParameter par6 = Database.addParameter("AdminDB", "@Active", 1);

            try
            {
                return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6);

            }
            catch (Exception)
            {

                return 0;
            }

        }

        public static int InsertCustomer(Customer c)
        {
            string sql = "INSERT INTO Customer VALUES(@CustomerName,@Address,@Picture,@Balance,@Barcode,@Active)";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
            DbParameter par5 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            DbParameter par6 = Database.addParameter("AdminDB", "@Active", 1);
            try
            { 
                return Database.InsertData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT",DBNAME,DBLOGIN,DBPASS)), sql, par1, par2, par3, par4, par5,par6);

            }
            catch (Exception)
            {

                return 0;
            }
           
        }

        public static int UpdateCustomer(Customer c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customer SET CustomerName=@CustomerName, Address=@Address, Picture=@Picture, Balance=@Balance WHERE Barcode=@Barcode";
            DbParameter par1 = Database.addParameter("AdminDB", "@CustomerName", c.CustomerName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Address", c.Address);
            DbParameter par3 = Database.addParameter("AdminDB", "@Picture", c.Picture);
            DbParameter par4 = Database.addParameter("AdminDB", "@Balance", c.Balance);
           
            DbParameter par6 = Database.addParameter("AdminDB", "@Barcode", c.Barcode);
            try
            { 
               return Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4,par6);

            }
            catch (Exception)
            {
                return 0;
               
            }
           
        }

        public static int DeleteCustomer(long id, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Customer SET Active=@Active WHERE Barcode=@Barcode";
            DbParameter par1 = Database.addParameter("AdminDB", "@Barcode", id);
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