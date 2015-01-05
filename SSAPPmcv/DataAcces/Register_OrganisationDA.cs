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
    public class Register_OrganisationDA
    {
         private const string ConnectionString = "DefaultConnection";
        public static List<RegistersOrganisation> GetOrganisation_Registers()
        {
            List<RegistersOrganisation> list = new List<RegistersOrganisation>();

            string sql = "SELECT OrganisationID, RegisterID, FromDate, UntilDate FROM Organisation_Register";
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        public static List<RegistersOrganisation> GetOrganisation_Registers(int ID)
        {
            List<RegistersOrganisation> list = new List<RegistersOrganisation>();

            string sql = "SELECT OrganisationID, RegisterID, FromDate, UntilDate FROM Organisation_Register WHERE OrganisationID=@OrganisationID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@OrganisationID", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }

        public static RegistersOrganisation GetRegisters_Organisation(int ID)
        {
            RegistersOrganisation or = new RegistersOrganisation();

            string sql = "SELECT OrganisationID, RegisterID, FromDate, UntilDate FROM Organisation_Register WHERE RegisterID=@RegisterID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@RegisterID", ID);
            DbDataReader reader = Database.GetData(Database.GetConnection(ConnectionString), sql, par1);

            while (reader.Read())
            {
                or = Create(reader);
            }
            reader.Close();
            return or;
        }

        public static void EditRegistersOrganisation(RegistersOrganisation or)
        {
            string sql = "UPDATE Organisation_Register SET OrganisationID=@OrganisationID, RegisterID=@RegisterID, FromDate=@FromDate, UntilDate=@UntilDate WHERE OrganisationID=@OrganisationID";
            DbParameter par1 = Database.addParameter(ConnectionString, "@OrganisationID", or.Organisationid);
            DbParameter par2 = Database.addParameter(ConnectionString, "@RegisterID", or.RegisterId);
            DbParameter par3 = Database.addParameter(ConnectionString, "@FromDate", or.From);
            DbParameter par4 = Database.addParameter(ConnectionString, "@UntilDate", or.Untill);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1, par2, par3, par4);
        }

        private static RegistersOrganisation Create(IDataRecord record)
        {
            return new RegistersOrganisation()
            {
                Organisationid = Int32.Parse(record["OrganisationID"].ToString()),
                RegisterId = Int32.Parse(record["RegisterID"].ToString()),
                From = DateTime.ParseExact(record["FromDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture),
                Untill = DateTime.ParseExact(record["UntilDate"].ToString(), "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture)
        };
        }
        public static void InsertOrganisation_Register(RegistersOrganisation or)
        {
            string sql = "INSERT INTO Organisation_Register VALUES(@OrganisationID, @RegisterID, @FromDate, @UntilDate)";
            DbParameter par1 = Database.addParameter(ConnectionString, "@OrganisationID", or.Organisationid);
            DbParameter par2 = Database.addParameter(ConnectionString, "@RegisterID", or.RegisterId);
            DbParameter par3 = Database.addParameter(ConnectionString, "@FromDate", or.From.ToString("yyyy-MM-dd"));
            DbParameter par4 = Database.addParameter(ConnectionString, "@UntilDate", or.Untill.ToString("yyyy-MM-dd"));
            Database.InsertData(Database.GetConnection(ConnectionString), sql, par1, par2, par3, par4);
        }

        public static void DeleteRegistersOrganisation(int regid,int orgid)
        {
            string sql = "DELETE FROM Organisation_Register WHERE RegisterID=@RegisterID AND OrganisationID=@OrganisationID";

            DbParameter par1 = Database.addParameter(ConnectionString, "@RegisterID", regid);
            DbParameter par2= Database.addParameter(ConnectionString,"@OrganisationID",orgid);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1,par2);
        }

        public static void ModifyRegisterAssignValue(int id)
        {
            string sql = "UPDATE Registers SET Assigned=@Assigned WHERE ID=@id";
            DbParameter par1=Database.addParameter(ConnectionString,"@Assigned","true");
            DbParameter par2 = Database.addParameter(ConnectionString, "@id", id);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1, par2);


        }
        public static void ModifyRegisterAssignValueToFalse(int id)
        {
            string sql = "UPDATE Registers SET Assigned=@Assigned WHERE ID=@id";
            DbParameter par1 = Database.addParameter(ConnectionString, "@Assigned", "false");
            DbParameter par2 = Database.addParameter(ConnectionString, "@id", id);
            Database.ModifyData(Database.GetConnection(ConnectionString), sql, par1, par2);


        }


    }
    }
