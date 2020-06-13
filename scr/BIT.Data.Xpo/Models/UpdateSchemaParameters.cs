
using DevExpress.Xpo.DB;
using System;

namespace BIT.Data.Xpo.Models
{
    public class CommandChannelDoParams
    {
        public string Command { get; set; }

        public object Args { get; set; }
        public CommandChannelDoParams(string command, object args)
        {
            Command = command;
            Args = args;
        }
    }
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