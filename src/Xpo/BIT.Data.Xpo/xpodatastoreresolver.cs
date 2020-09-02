using BIT.Data.Services;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIT.Xpo
{
    public class XpoDataStoreResolver : ConfigurationResolverBase<IDataStore>, IResolver<IDataStore>
    {

        Dictionary<string, string> AutoCreateOptions;

        public XpoDataStoreResolver(string configuratioName, Func<IConfiguration, string, DevExpress.Xpo.DB.IDataStore> instaceBuilder) : base(configuratioName, instaceBuilder)
        {

        }
        public XpoDataStoreResolver(string configuratioName) : base(configuratioName, null)
        {
            this.InstanceBuilder = new Func<IConfiguration, string, IDataStore>(CreateNewDataStore);
        }

        public XpoDataStoreResolver() : base("appsettings.json", null)
        {
            this.InstanceBuilder = new Func<IConfiguration, string, IDataStore>(CreateNewDataStore);
        }


        private DevExpress.Xpo.DB.IDataStore CreateNewDataStore(IConfiguration configuration, string Id)
        {

            var ConnectionStrings = configuration.GetSection("ConnectionStrings").AsEnumerable();
            List<KeyValuePair<string, string>> AutoCreateOptionsFromConfig = configuration.GetSection("DatabaseAutoCreateOptions").AsEnumerable().ToList();

            AutoCreateOptions = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> Value in AutoCreateOptionsFromConfig)
            {


                if (Value.Value == null)
                    continue;

                string[] Split = Value.Key?.Split(':');
                if (Split.Length > 1)
                    AutoCreateOptions.Add(Split[1], Value.Value);
            }

            if (!AutoCreateOptions.ContainsKey(Id))
                throw new ArgumentException($"Missing AutoCreateOptions:{Id}");


            AutoCreateOption autoCreateOptionEnum = AutoCreateOption.None;

            switch (AutoCreateOptions[Id])
            {
                case "DatabaseAndSchema":
                    autoCreateOptionEnum = AutoCreateOption.DatabaseAndSchema;
                    break;
                case "SchemaOnly":
                    autoCreateOptionEnum = AutoCreateOption.SchemaOnly;
                    break;
                case "None ":
                    autoCreateOptionEnum = AutoCreateOption.None;
                    break;
                case "SchemaAlreadyExists":
                    autoCreateOptionEnum = AutoCreateOption.SchemaAlreadyExists;
                    break;
            }

            var ConnectionString = configuration.GetConnectionString(Id);
            IDisposable[] DisposableObjects = null;
            //TODO read autocreate options from the config
            DevExpress.Xpo.DB.IDataStore DataStore = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOptionEnum, out DisposableObjects);
            return DataStore;


        }



    }
}
