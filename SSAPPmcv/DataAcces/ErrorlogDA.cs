using models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace SSAPPmcv.DataAcces
{
    public class ErrorlogDA
    {
        private static ConnectionStringSettings ConnectionString = new ConnectionStringSettings();

        public static void ChangeConnectionString(string provider, string server, string database, string username, string password)
        {
            ConnectionString = Database.CreateConnectionString(provider, server, database, username, password);
        }
        public static List<Errorlog> GetErrorlogs(int ID)
        {
            List<Errorlog> list = new List<Errorlog>();

            string sql = "SELECT * FROM Errorlog WHERE RegisterId=@ID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@ID", ID);
            try
            {  DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

                    while (reader.Read())
                    {
                        list.Add(Create(reader));

                    }
            
                    reader.Close();

                    return list;

            }
            catch (Exception)
            {

                return null;
            }
          
        }


        private static Errorlog Create(IDataRecord record)
        {
            return new Errorlog()
            {
                RegisterId = Int32.Parse(record["RegisterId"].ToString()),
                Message = record["Message"].ToString(),
                Stacktrace = record["Stacktrace"].ToString(),
                Timestamp = DateTime.ParseExact(record["Timestamp"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)
            };
        }
        private static Register CreateRegister(IDataRecord record)
        {
            return new Register()
            {

                Id = Int32.Parse(record["ID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                DeviceName = record["Device"].ToString(),
                PurchaseDate = DateTime.ParseExact(record["PurchaseDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                ExpiresDate = DateTime.ParseExact(record["ExpiresDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                Assigned = record["Assigned"].ToString()
            };
        }


    }
}