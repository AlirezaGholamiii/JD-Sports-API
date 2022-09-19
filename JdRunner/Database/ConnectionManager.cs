using System;
using System.Data;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace JdRunner.Database
{
    public static class ConnectionManager
    {

        private static string Schema = "";
        private static string Username = "";
        private static string Password = "";
        private static string Server = "";
        private static string Port = "";
        private static string ServiceName = "";
        private static string MinPoolSize = "";
        private static string MaxPoolSize = "";
        private static string IncrPoolSize = "";
        private static string DecrPoolSize = "";
        private static string ConnectionLifetime = "";
        private static string ConnectionTimeout = "";

        public enum DatabaseChoice
        {
            OMNI,
            POS
        }

        //public ConnectionManager(DatabaseChoice databaseChoice)
        //{
        //    if (DatabaseChoice.POS == databaseChoice)
        //    {
        //        InitPosSettings();
        //    }
        //    else
        //    {
        //        InitOmniSettings();
        //    }
        //}

        public static OracleConnection GetOmniConnection()
        {
            InitOmniSettings();
            return CreateConnection();
        }

        public static OracleConnection GetPosConnection()
        {
            InitPosSettings();
            return CreateConnection();
        }

        public static async Task<OracleConnection> GetPosConnectionAsync()
        {
            InitPosSettings();
            return await CreateConnectionAsync();
        }

        private static string ConnectionDescriptor
        {
            get
            {
                return "user id=" + Username +
                ";password=" + Password +
                ";data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=" + Server + ")" +
                "(PORT=" + Port + "))" +
                "(CONNECT_DATA=(SERVICE_NAME=" + ServiceName + ")))";
            }
        }

        private static string PoolDescriptor
        {
            get
            {
                return ";Min Pool Size=" + MinPoolSize + ";Connection Lifetime=" + ConnectionLifetime + ";Connection Timeout=" + ConnectionTimeout + ";" +
               "Incr Pool Size=" + IncrPoolSize + "; Decr Pool Size=" + DecrPoolSize + "; Max Pool Size=" + MaxPoolSize + "";
            }
        }
        private static OracleConnection CreateConnection()
        {
            OracleConnection conn = new OracleConnection(ConnectionDescriptor + PoolDescriptor);
            conn.Open();

            return conn;
        }
        private static async Task<OracleConnection> CreateConnectionAsync()
        {
            OracleConnection conn = new OracleConnection(ConnectionDescriptor + PoolDescriptor);
            await conn.OpenAsync();

            return conn;
        }

        public static void CloseConnection(OracleConnection connection)
        {
            ((OracleConnection)connection).Close();
        }

        private static void InitOmniSettings()
        {
            Schema = Settings.GetOmniDbConfigParameter("Schema");
            Username = Settings.GetOmniDbConfigParameter("User");
            Password = Settings.GetOmniDbConfigParameter("Password");
            Server = Settings.GetOmniDbConfigParameter("Host");
            Port = Settings.GetOmniDbConfigParameter("Port");
            ServiceName = Settings.GetOmniDbConfigParameter("Service");
            MinPoolSize = Settings.GetOmniDbConfigParameter("MinPoolSize");
            MaxPoolSize = Settings.GetOmniDbConfigParameter("MaxPoolSize");
            IncrPoolSize = Settings.GetOmniDbConfigParameter("IncrPoolSize");
            DecrPoolSize = Settings.GetOmniDbConfigParameter("DecrPoolSize");
            ConnectionLifetime = Settings.GetOmniDbConfigParameter("ConnectionLifetime");
            ConnectionTimeout = Settings.GetOmniDbConfigParameter("ConnectionTimeout");
        }

        private static void InitPosSettings()
        {
            Schema = Settings.GetPosDbConfigParameter("Schema");
            Username = Settings.GetPosDbConfigParameter("User");
            Password = Settings.GetPosDbConfigParameter("Password");
            Server = Settings.GetPosDbConfigParameter("Host");
            Port = Settings.GetPosDbConfigParameter("Port");
            ServiceName = Settings.GetPosDbConfigParameter("Service");
            MinPoolSize = Settings.GetPosDbConfigParameter("MinPoolSize");
            MaxPoolSize = Settings.GetPosDbConfigParameter("MaxPoolSize");
            IncrPoolSize = Settings.GetPosDbConfigParameter("IncrPoolSize");
            DecrPoolSize = Settings.GetPosDbConfigParameter("DecrPoolSize");
            ConnectionLifetime = Settings.GetPosDbConfigParameter("ConnectionLifetime");
            ConnectionTimeout = Settings.GetPosDbConfigParameter("ConnectionTimeout");
        }

        public static string GetOmniSchemaName()
        {
            //if(Schema == null)
            //  Schema = Settings.GetOmniDbConfigParameter("Schema");

            //return Schema;

            return Settings.GetOmniDbConfigParameter("Schema");
        }

        public static string GetPosSchemaName()
        {
            //if (Schema == null)
            //    Schema = Settings.GetPosDbConfigParameter("Schema");

            //return Schema;
            return Settings.GetPosDbConfigParameter("Schema");
        }

        //Creates an Oracle Parameter for the specified OracleCommand object.
        public static OracleParameter CreateParameter(OracleCommand cmd, string ParamName, OracleDbType ParamDataType, ParameterDirection ParamDirection)
        {
            return CreateParameter(cmd, ParamName, ParamDataType, ParamDirection, null, 0);
        }

        //Creates an Oracle Parameter for the specified OracleCommand object.
        public static OracleParameter CreateParameter(OracleCommand cmd, string ParamName, OracleDbType ParamDataType, ParameterDirection ParamDirection, Nullable<int> ParamSize)
        {
            return CreateParameter(cmd, ParamName, ParamDataType, ParamDirection, ParamSize, 0);
        }

        //Creates an Oracle Parameter for the specified OracleCommand object.
        public static OracleParameter CreateParameter(OracleCommand cmd, string ParamName, OracleDbType ParamDataType, ParameterDirection ParamDirection, object ParamValue, int ParamSize = 0)
        {
            OracleParameter cmdParam = null;
            OracleParameter cmdParamReturned = null;

            try
            {
                cmdParam = new OracleParameter
                {
                    ParameterName = ParamName,
                    OracleDbType = ParamDataType,
                    Direction = ParamDirection
                };
                if (ParamSize != 0)
                    cmdParam.Size = ParamSize;
                if (ParamValue != null)
                    cmdParam.Value = ParamValue;
                cmd.Parameters.Add(cmdParam);
                cmdParamReturned = cmdParam;
                cmdParam = null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cmdParam != null)
                    cmdParam.Dispose();
            }

            return cmdParamReturned;
        }
    }
}
