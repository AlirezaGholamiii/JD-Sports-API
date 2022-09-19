
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

using System.Data;
using System.Globalization;


namespace JdRunner.Extentions
{
    public static class OracleManagedDataAccessrExtensions
    {
        public static string NullToEpmptyString(this Object obj)
        { 
            string rtn = obj.ToString();
            return rtn  == "null" ? null : rtn;
        }

        public static int GetIntOracleDecimalValue(this Object obj)
        {
            return (int)(OracleDecimal)obj;
        }

        public static OracleParameter Add(this OracleParameterCollection opc, string name, OracleDbType dbType, ParameterDirection dir, int size)
        {
            return opc.Add(new OracleParameter()
            {
                ParameterName = name,
                OracleDbType = dbType,
                Direction = dir,
                Size = size
            });
        }
        public static string GetStringByColumnName(this OracleDataReader dr, string columnName)
        {
            int i = dr.GetOrdinal(columnName.ToUpper());
            return dr.IsDBNull(i) ? null : dr.GetString(i);
        }
        public static DateTime? GetDateTimeByColumnName(this OracleDataReader dr, string columnName)
        {
            int i = dr.GetOrdinal(columnName.ToUpper());
            return dr.IsDBNull(i) ? null : dr.GetDateTime(i);
        }
        
        public static Int32? GetInt32ByColumnName(this OracleDataReader dr, string columnName)
        {
            int i = dr.GetOrdinal(columnName.ToUpper());
            return dr.IsDBNull(i) ? null : dr.GetInt32(i);
        }

        public static decimal? GetDecimalByColumnName(this OracleDataReader dr, string columnName)
        {
            int i = dr.GetOrdinal(columnName.ToUpper());
            return dr.IsDBNull(i) ? null : dr.GetDecimal(i);
        }

        public static T GetValueByColumnName<T>(this OracleDataReader dr, string columnName) where T : IConvertible
        {
            int i = dr.GetOrdinal(columnName.ToUpper());

            Type type = dr.GetFieldType(i);

            if (type.Name.Equals("String"))
            {
                return (T)Convert.ChangeType(dr.IsDBNull(i) ? null : dr.GetString(i), typeof(T), CultureInfo.InvariantCulture);
            }
            else if (type.Name.Equals("Decimal"))
            {
                return (T)Convert.ChangeType(dr.IsDBNull(i) ? default(T) : dr.GetOracleDecimal(i), typeof(T), CultureInfo.InvariantCulture);
            }

            return (T)Convert.ChangeType(null, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
