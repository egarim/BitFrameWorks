using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
namespace BIT.Xpo.AspNetCore
{
    public class DataStoreResolver : IDataStoreResolver
    {
        Dictionary<string, IDataStore> DataStores = new Dictionary<string, IDataStore>();
        IConfigurationRoot configuration;
        IConfigurationBuilder builder;
        Dictionary<string, string> AutoCreateOptions;
        public DataStoreResolver()
        {
            //TODO ask javier if we should init the datastore when the resolver is constructed or when is access for first time
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            configuration = builder.Build();
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





            foreach (KeyValuePair<string, string> Connection in ConnectionStrings)
            {
                //Parsing connection strings
                var IdAndConnectionString = Connection.Key.Split(':');
                if (IdAndConnectionString.Length > 1)
                {
                    if (IdAndConnectionString[1] != null)
                    {

                        string autoCreateOptions;
                        AutoCreateOptions.TryGetValue(IdAndConnectionString[1], out autoCreateOptions);

                        if (string.IsNullOrEmpty(autoCreateOptions))
                            continue;

                        string dataStoreId = IdAndConnectionString[1];
                        InitDataStore(dataStoreId, autoCreateOptions);
                    }
                }

            }
        }

        public IDataStore GetDataStore(string DataStoreId)
        {
            
            if (DataStores.ContainsKey(DataStoreId))
                return DataStores[DataStoreId];
            else
            {
                if (AutoCreateOptions.ContainsKey(DataStoreId))
                {
                    LoadConfiguration();
                    IDataStore DataStore = InitDataStore(DataStoreId, AutoCreateOptions[DataStoreId]);
                    return DataStore;
                }
                else
                {
                    throw new Exception($"Autocreate options missing for the store with ID:{DataStoreId} please check your DatabaseAutoCreateOptions on your appsettings.json");
                }
              
            }
            throw new Exception($"there is no configuration for the DataStore with the ID:{DataStoreId}");
        }
        public UnitOfWork GetUnitOfWork(string DataStoreId)
        {
            //TODO we might need to use a thread safe
            if (DataStores.ContainsKey(DataStoreId))
                return new UnitOfWork(new SimpleDataLayer(DataStores[DataStoreId]));
            else
            {
                LoadConfiguration();
                IDataStore DataStore = InitDataStore(DataStoreId, AutoCreateOptions[DataStoreId]);
                return new UnitOfWork(new SimpleDataLayer(DataStore));
            }
            throw new Exception($"there is no configuration for the DataStore with the ID:{DataStoreId}");
        }
        private IDataStore InitDataStore(string DataStoreId, string AutoCreateOptions)
        {

            AutoCreateOption autoCreateOptionEnum = AutoCreateOption.None;

            switch (AutoCreateOptions)
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
                    autoCreateOptionEnum = AutoCreateOption.SchemaAlreadyExists;//HACK aqui esto estaba equivocado
                    break;
            }

            var ConnectionString = configuration.GetConnectionString(DataStoreId);
            IDisposable[] DisposableObjects = null;
            //TODO read autocreate options from the config
            var DataStore = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOptionEnum, out DisposableObjects);
            if (DataStore != null)
            {
                DataStores.Add(DataStoreId, DataStore);
            }

            return DataStore;
        }

    }
}
