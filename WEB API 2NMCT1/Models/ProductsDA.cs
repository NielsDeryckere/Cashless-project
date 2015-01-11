using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models;
using System.Data.Common;
using WEB_API_2NMCT1.Helper;
using System.Data;
using System.Configuration;
using System.Security.Claims;
using modelsProject;
namespace WEB_API_2NMCT1.Models
{
    public class ProductsDA
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
        public static List<Product> GetProducts(IEnumerable<Claim> claims)
        {
            List<Product> lijst = new List<Product>();
            string sql = "SELECT * FROM Product";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                
                lijst.Add(Create(reader));
            }
            reader.Close();




            return lijst;
        }

        public static List<Product> GetProducts()
        {
         

            List<Product> lijst = new List<Product>();
            string sql = "SELECT * FROM Product";
            DbDataReader reader = Database.GetData(Database.GetConnection(Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT",DBNAME,DBLOGIN,DBPASS)), sql);
            while (reader.Read())
            {

                lijst.Add(Create(reader));
            }
            reader.Close();




            return lijst;
        }

        public static Product GetProduct(int productid, IEnumerable<Claim> claims)
        {
            Product p = new Product();
            string sql = "SELECT * FROM Product WHERE ID=@ID";

            DbParameter par1 = Database.addParameter("AdminDB", "@ID", productid);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql,par1);
            reader.Read();
            p.Id = int.Parse(reader["ID"].ToString());
            p.ProductName = reader["ProductName"].ToString();
            p.Price=double.Parse(reader["Price"].ToString());

            reader.Close();
            return p;

        }


        private static Product Create(IDataRecord record )
        {
            return new Product()
            {

                ProductName = record["ProductName"].ToString(),
                Price = double.Parse(record["Price"].ToString()),
                Id=int.Parse(record["ID"].ToString())


            };

        }

        public static int InsertProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "INSERT INTO Product VALUES(@ProductName,@Price)";
            DbParameter par1 = Database.addParameter("AdminDB", "@ProductName", c.ProductName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Price", c.Price);
           
            return Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2);
        }

        public static void UpdateProduct(Product c, IEnumerable<Claim> claims)
        {
            string sql = "UPDATE Product SET ProductName=@ProductName, Price=@Price WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@ProductName", c.ProductName);
            DbParameter par2 = Database.addParameter("AdminDB", "@Price", c.Price);
            DbParameter par3 = Database.addParameter("AdminDB", "@ID", c.Id);
            Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3);
        }

        public static void DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM Product WHERE ID=@ID";
            DbParameter par1 = Database.addParameter("AdminDB", "@ID", id);
            DbConnection con = Database.GetConnection(CreateConnectionString(claims));
            Database.ModifyData(con, sql, par1);
        }

        public static int UpdateAccounts(Transfer t)
        {
            int rowsaffected = 0;
            DbTransaction trans = null;
           
            ConnectionStringSettings con = Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", DBNAME, DBLOGIN, DBPASS);
            try
            {
                trans = Database.BeginTransaction(con);
                
                string sql = "UPDATE Customer SET Balance=Balance-@Amount WHERE Barcode=@ID";
                DbParameter par1 = Database.addParameter(con, "@Amount", t.Receiver.TotalPrice);
                DbParameter par2 = Database.addParameter(con, "@ID", t.Sender.Barcode);
                rowsaffected += Database.ModifyData(trans, sql, par1, par2);

               
                string sql2 = "INSERT INTO Sales VALUES(@Timestamp,@CustomerID,@RegisterID,@ProductID,@Amount,@TotalPrice)";
                DbParameter par3 = Database.addParameter(con, "@Amount", t.Receiver.Amount);
                DbParameter par4 = Database.addParameter(con, "@Timestamp",DateConverter.DateTimeToUnixTimestamp(DateTime.Now));
                DbParameter par5 = Database.addParameter(con, "@CustomerID", t.Sender.Barcode);
                DbParameter par6 = Database.addParameter(con, "@RegisterID", t.Receiver.RegisterId);
                DbParameter par7 = Database.addParameter(con, "@ProductID", t.Receiver.ProductId);
                DbParameter par8 = Database.addParameter(con, "@TotalPrice", t.Receiver.TotalPrice);
                rowsaffected += Database.InsertData(trans, sql2, par3, par4,par5,par6,par7,par8);
                trans.Commit();
}
               
            
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                Errorlog er = new Errorlog()
                {

                    Message = "Could not complete transaction",
                    RegisterId = 5,
                    Stacktrace = ex.StackTrace,
                    Timestamp = DateTime.Now,

                };

                ErrorlogDA.InsertErrorlog(er);
            }
            finally
            {
                if (trans != null)
                    Database.ReleaseConnection(trans.Connection);
            }

            return rowsaffected;
        }
    }
}