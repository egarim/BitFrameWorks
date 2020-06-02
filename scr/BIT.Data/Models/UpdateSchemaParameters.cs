
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;

namespace BIT.Data.Models
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