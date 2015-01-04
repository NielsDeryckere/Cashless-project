using models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using WEB_API_2NMCT1.Helper;

namespace SSAPPmcv.DataAcces
{
    public class RegisterDA
    {
        private const string ConnectionString = "DefaultConnection";

        public static List<Register> GetRegisters()
        {
            List<Register> Registerlist = new List<Register>();

            string sql = "SELECT * FROM Registers WHERE Assigned='false'";
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql);

            while (reader.Read())
            {
                Registerlist.Add(Create(reader));
            }
            reader.Close();
            return Registerlist;
        }

        public static Register GetRegisters(int ID)
        {
           

            string sql = "SELECT * FROM Registers WHERE ID=@ID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@ID", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

            reader.Read();
            Register registers = Create(reader);
            reader.Close();

            return registers;
        }

        public static void InsertRegister(Register r)
        {
            string sql = "INSERT INTO Registers VALUES(@RegisterName, @Device, @PurchaseDate, @ExpiresDate,@Assigned)";
            string purchasedate = r.ExpiresDate.ToString("yyyy-MM-dd");
            string expiresdate=r.ExpiresDate.ToString("yyyy-MM-dd");
            DbParameter par1 = Database.addParameter(ConnectionString, "@RegisterName", r.RegisterName);
            DbParameter par2 = Database.addParameter(ConnectionString, "@Device", r.DeviceName);
            DbParameter par3 = Database.addParameter(ConnectionString, "@PurchaseDate", purchasedate);
            DbParameter par4 = Database.addParameter(ConnectionString, "@ExpiresDate",expiresdate );
            DbParameter par5 = Database.addParameter(ConnectionString, "@Assigned", "false");
            Database.InsertData(Database.GetConnection(ConnectionString), sql, par1, par2, par3, par4,par5);
        }

        private static Register Create(IDataRecord record)
        {
            return new Register()
            {
                
                Id = Int32.Parse(record["ID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                DeviceName = record["Device"].ToString(),
                PurchaseDate =DateTime.ParseExact(record["PurchaseDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                ExpiresDate = DateTime.ParseExact(record["ExpiresDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                Assigned=record["Assigned"].ToString()
            };
        }
    }
    
}