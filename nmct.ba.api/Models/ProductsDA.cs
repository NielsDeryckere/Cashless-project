using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using models;
using System.Data.Common;
using WEB_API_2NMCT1.Helper;
using System.Data;
namespace WEB_API_2NMCT1.Models
{
    public class ProductsDA
    {
        public static List<Product>GetProducts()
        {
            List<Product> lijst = new List<Product>();
            string sql = "SELECT * FROM Products";
            DbDataReader reader = Database.GetData("ClientDB", sql);
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
    }
}