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
    public class RegisterClientDAcs
    {
         private static ConnectionStringSettings ConnectionString = new ConnectionStringSettings();

        public static void ChangeConnectionString(string provider, string server, string database, string username, string password)
        {
                ConnectionString = Database.CreateConnectionString(provider, server, database, username, password);
        }

        public static List<Register> GetRegisters()
        {
            List<Register> list = new List<Register>();

            string sql = "SELECT ID, RegisterName, Device FROM Register";
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        public static Register GetRegisters(int ID)
        {
            List<Register> list = new List<Register>();

            string sql = "SELECT ID, RegisterName, Device FROM Register WHERE ID=@ID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@ID", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

            reader.Read();
            Register register = Create(reader);
            reader.Close();

            return register;
        }

        public static void InsertRegister(Register r)
        {
            string sql = "INSERT INTO Registers VALUES(@RegisterName, @Device)";

            DbParameter par1 = Database.addParameter(ConnectionString, "@RegisterName", r.RegisterName);
            DbParameter par2 = Database.addParameter(ConnectionString, "@Device", r.DeviceName);
            Database.InsertData(Database.GetConnection(ConnectionString), sql, par1, par2);
        }

        public static void DeleteRegister(Register r)
        {
            string sql = "DELETE FROM Register WHERE ID=@ID";

            DbParameter par1 = Database.addParameter(ConnectionString, "@ID", r.Id);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1);
        }

        private static Register Create(IDataRecord record)
        {
            return new Register()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                DeviceName = record["Device"].ToString(),
            };
        }
    }
    }
