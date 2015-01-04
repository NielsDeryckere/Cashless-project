using models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using WEB_API_2NMCT1.Helper;

namespace SSAPPmcv.DataAcces
{
    public class OrganisationDA
    {
        private const string ConnectionString = "DefaultConnection";
        public static List<Organisation> GetOrganisations()
        {
            List<Organisation> list = new List<Organisation>();

            string sql = "SELECT * FROM Organisation";
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        public static Organisation GetOrganisations(int ID)
        {
            List<Organisation> list = new List<Organisation>();

            string sql = "SELECT * FROM Organisation WHERE ID=@ID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@ID", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

            reader.Read();
            Organisation organisation = Create(reader);
            reader.Close();

            return organisation;
        }
        public static void DeleteOrganisation(int ID, string DbName,string DbLogin)
        {
            
            string sql = "DELETE FROM Organisation WHERE ID=@id";
            DbParameter par1 = Database.addParameter(ConnectionString, "@id", ID);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1);

            //string sql2= "DROP DATABASE [@NAME]";
            //DbParameter par2=Database.addParameter(ConnectionString,"@NAME",DbName);
            //Database.DeleteDatabase(Database.GetConnection(ConnectionString), sql2, par2);

            //string sql3 = "DROP USER @username";
            //DbParameter par3 = Database.addParameter(ConnectionString, "@username", DbLogin);
            //Database.DeleteDatabase(Database.GetConnection(ConnectionString), sql3, par3);
        }

        public static void EditOrganisation(Organisation o)
        {
            string sql = "UPDATE Organisation SET Login=@Login, Password=@Password, OrganisationName=@OrganisationName, Address=@Address, Email=@Email, Phone=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@Login", Cryptography.Encrypt(o.Login));
            DbParameter par2 = Database.addParameter(ConnectionString, "@Password", Cryptography.Encrypt(o.Password));
            DbParameter par3 = Database.addParameter(ConnectionString, "@OrganisationName", o.OrganisationName);
            DbParameter par4 = Database.addParameter(ConnectionString, "@Address", o.Address);
            DbParameter par5 = Database.addParameter(ConnectionString, "@Email", o.Email);
            DbParameter par6 = Database.addParameter(ConnectionString, "@Phone", o.Phone);
            DbParameter par7 = Database.addParameter(ConnectionString, "@ID", o.ID);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1, par2, par3, par4, par5, par6, par7);
        }

        private static Organisation Create(IDataRecord record)
        {
            return new Organisation()
            {
                ID = Int32.Parse(record["ID"].ToString()),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbLogin = record["DbLogin"].ToString(),
                DbPassword = record["DbPassword"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = record["Phone"].ToString()
            };
        }

        public static int InsertOrganisation(Organisation o)
        {
            string sql = "INSERT INTO Organisation VALUES(@Login,@Password,@DbName,@DbLogin,@DbPassword,@OrganisationName,@Address,@Email,@Phone)";
            DbParameter par1 = Database.addParameter(ConnectionString, "@Login", Cryptography.Encrypt(o.Login));
            DbParameter par2 = Database.addParameter(ConnectionString, "@Password", Cryptography.Encrypt(o.Password));
            DbParameter par3 = Database.addParameter(ConnectionString, "@DbName", Cryptography.Encrypt(o.DbName));
            DbParameter par4 = Database.addParameter(ConnectionString, "@DbLogin", Cryptography.Encrypt(o.DbLogin));
            DbParameter par5 = Database.addParameter(ConnectionString, "@DbPassword", Cryptography.Encrypt(o.DbPassword));
            DbParameter par6 = Database.addParameter(ConnectionString, "@OrganisationName", o.OrganisationName);
            DbParameter par7 = Database.addParameter(ConnectionString, "@Address", o.Address);
            DbParameter par8 = Database.addParameter(ConnectionString, "@Email", o.Email);
            DbParameter par9 = Database.addParameter(ConnectionString, "@Phone", o.Phone);
            int id = Database.InsertData(Database.GetConnection(ConnectionString), sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);

            CreateDatabase(o);

            return id;
        }

        private static void CreateDatabase(Organisation o)
        {
            // create the actual database
            string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt"));
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(Database.GetConnection(ConnectionString), commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction(ConnectionString);

                string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt"));
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n","GO" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }
    }
}