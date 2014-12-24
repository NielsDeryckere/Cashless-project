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
namespace WEB_API_2NMCT1.Models
{
    public class ProductsDA
    {
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
    }
}