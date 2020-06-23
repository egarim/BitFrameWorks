using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.SingularOrm.Tests
{
    public static class Helpers
    {
        private const string ConnectionString = "XpoProvider=SQLite;Data Source={0}";

        public static string FormatConnectionString(string DatabaseName)
        {
            return string.Format(ConnectionString, DatabaseName);
        }
    }
}
