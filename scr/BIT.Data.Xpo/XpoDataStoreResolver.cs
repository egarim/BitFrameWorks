using BIT.Data.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIT.Xpo
{
    public class XpoDataStoreResolver : ConfigurationResolverBase<DevExpress.Xpo.DB.IDataStore>
    {

        Dictionary<string, string> AutoCreateOptions;

        public XpoDataStoreResolver(string configuratioName, Func<IConfiguration, string, DevExpress.Xpo.DB.IDataStore> instaceBuilder) : base(configuratioName, instaceBuilder)
        {
           
        }
        public XpoDataStoreResolver(string configuratioName) : base(configuratioName, null)
        {
            this.InstaceBuilder = new Func<IConfiguration, string, IDataStore>(CreateNewDataStore);
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
                throw new ArgumentException($"Missin AutoCreateOptions:{Id}");


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




            //throw new Exception($"there is no configuration for the DataStore with the ID:{Id}");
        }


        //public UnitOfWork GetUnitOfWork(string DataStoreId)
        //{
        //    //TODO we might need to use a thread safe
        //    if (DataStores.ContainsKey(DataStoreId))
        //        return new UnitOfWork(new SimpleDataLayer(DataStores[DataStoreId]));
        //    else
        //    {
        //        LoadConfiguration();
        //        IDataStore DataStore = InitDataStore(DataStoreId, AutoCreateOptions[DataStoreId]);
        //        return new UnitOfWork(new SimpleDataLayer(DataStore));
        //    }
        //    throw new Exception($"there is no configuration for the DataStore with the ID:{DataStoreId}");
        //}
        //private IDataStore InitDataStore(string DataStoreId, string AutoCreateOptions)
        //{

        //    AutoCreateOption autoCreateOptionEnum = AutoCreateOption.None;

        //    switch (AutoCreateOptions)
        //    {
        //        case "DatabaseAndSchema":
        //            autoCreateOptionEnum = AutoCreateOption.DatabaseAndSchema;
        //            break;
        //        case "SchemaOnly":
        //            autoCreateOptionEnum = AutoCreateOption.SchemaOnly;
        //            break;
        //        case "None ":
        //            autoCreateOptionEnum = AutoCreateOption.None;
        //            break;
        //        case "SchemaAlreadyExists":
        //            autoCreateOptionEnum = AutoCreateOption.SchemaAlreadyExists;//HACK aqui esto estaba equivocado
        //            break;
        //    }

        //    var ConnectionString = configuration.GetConnectionString(DataStoreId);
        //    IDisposable[] DisposableObjects = null;
        //    //TODO read autocreate options from the config
        //    var DataStore = XpoDefault.GetConnectionProvider(ConnectionString, autoCreateOptionEnum, out DisposableObjects);
        //    if (DataStore != null)
        //    {
        //        DataStores.Add(DataStoreId, DataStore);
        //    }

        //    return DataStore;
        //}

        //public IDataStore GetById(string Id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
