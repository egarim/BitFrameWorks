
using DevExpress.Xpo.DB;
using System;

namespace BIT.Xpo.Models
{
    public class UpdateSchemaParameters
    {
        public bool dontCreateIfFirstTableNotExist { get; set; }

        public DBTable[] tables { get; set; }

        public UpdateSchemaParameters(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)

        {
            this.dontCreateIfFirstTableNotExist = dontCreateIfFirstTableNotExist;
            this.tables = tables;
        }

        public UpdateSchemaParameters()
        {
        }
    }
}