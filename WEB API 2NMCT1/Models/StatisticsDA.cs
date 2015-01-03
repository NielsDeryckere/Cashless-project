using models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace WEB_API_2NMCT1.Models
{
    public class StatisticsDA
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;

            return Database.CreateConnectionString("System.Data.SqlClient", @"MCT-NIELS\DATAMANAGEMENT", dbname, dblogin, dbpass);
        }
        public static List<Sales> GetSales(IEnumerable<Claim> claims)
        {
            List<Sales> lijst = new List<Sales>();
            string sql = "SELECT * FROM Sales";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {

                lijst.Add(Create(reader));
            }
            reader.Close();




            return lijst;
        }

        private static Sales Create(IDataRecord record)
        {
            DateTime myDate = DateTime.ParseExact(record["Timestamp"].ToString(), "yyyy-MM-dd HH:mm:ss",
                                       System.Globalization.CultureInfo.InvariantCulture);
            return new Sales()
            {

                 Timestamp =myDate,
                Amount = int.Parse(record["Amount"].ToString()),
                Id = int.Parse(record["ID"].ToString()),
                CustomerId=long.Parse((record["CustomerID"]).ToString()),
                ProductId = int.Parse((record["ProductID"]).ToString()),
                 RegisterId = int.Parse((record["RegisterID"]).ToString()),
                 TotalPrice = double.Parse((record["TotalPrice"]).ToString())



            };

        }
    }
}