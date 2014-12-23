using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace WEB_API_2NMCT1.Helper
{
    public class Database
    {
        private static DbConnection GetConnection(string ConnectionString)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConnectionString];
            DbConnection connection = DbProviderFactories.GetFactory(settings.ProviderName).CreateConnection();

            connection.ConnectionString = settings.ConnectionString;
            connection.Open();

            return connection;



        }

        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }



        }

        private static DbCommand BuildCommand(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = GetConnection(constring).CreateCommand();

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);

            }

            return command;
        }

        public static DbDataReader GetData(string constring, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(constring, sql, parameters);
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (reader != null)
                {
                    reader.Close();


                }
                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }


        }

        public static int ModifyData(string constring, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                return command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }
        }
        private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = trans.Connection.CreateCommand();
            command.Transaction = trans;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);

            }

            return command;
        }

        public static DbDataReader GetData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (reader != null)
                {
                    reader.Close();


                }
                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }


        }

        public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);
                return command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }
        }

        public static int InsertData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;

            try
            {
                command = BuildCommand(trans, sql, parameters);


                command.Parameters.Clear();
                command.CommandText = "select @@IDENTITY";

                int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }
        }

        public static int InsertData(string constring, string sql, params DbParameter[] parameters)
        {

            DbCommand command = null;

            try
            {
                command = BuildCommand(constring, sql, parameters);
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = "select @@IDENTITY";

                int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();

                return id;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);


                if (command != null)
                {
                    ReleaseConnection(command.Connection);


                }
                throw;
            }
        }

        public static DbParameter addParameter(string constring, string name, object value)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[constring];
            DbParameter par = DbProviderFactories.GetFactory(settings.ProviderName).CreateParameter();
            par.ParameterName = name;
            par.Value = value;
            return par;


        }

        public static DbTransaction BeginTransaction(string constring)
        {
            DbConnection con = null;

            try
            {
                con = GetConnection(constring);
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReleaseConnection(con);
                throw;
            }


        }
    }
}