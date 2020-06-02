using DevExpress.Xpo.DB;
using Microsoft.Data.Sqlite;
//using Microsoft.Data.Sqlite;
using System;
using System.Data;

namespace BIT.Xpo.SqliteEncrypted
{
    public static class DbCommandExtensionMethods
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}
