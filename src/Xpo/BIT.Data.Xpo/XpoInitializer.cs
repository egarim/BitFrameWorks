using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Linq;

namespace BIT.Xpo
{
    public class XpoInitializer : IXpoInitializer
    {



        private IDataLayer UpdateDal;
        private IDataLayer WorkindDal;

        readonly DataLayerType dataLayerType;


        XPDictionary dictionary;
        Type[] entityTypes;

        public DataLayerType DataLayerType => dataLayerType;

        public XpoInitializer(string connectionString, DataLayerType DataLayerType, params Type[] entityTypes)
           : this(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema), DataLayerType, entityTypes)
        {

        }
        public XpoInitializer(IDataStore DataStore, DataLayerType DataLayerType, params Type[] entityTypes)
        {
            this.entityTypes = entityTypes;
            this.dataLayerType = DataLayerType;
            dictionary = this.PrepareDictionary(entityTypes);
            UpdateDal = new SimpleDataLayer(dictionary, DataStore);
            switch (DataLayerType)
            {
                case DataLayerType.Simple:
                    this.WorkindDal = new SimpleDataLayer(dictionary, DataStore);
                    break;
                case DataLayerType.ThreadSafe:
                    this.WorkindDal = new ThreadSafeDataLayer(dictionary, DataStore);
                    break;
            }
        }
        public XpoInitializer(string connectionString, params Type[] entityTypes)
              : this(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema), DataLayerType.Simple, entityTypes)
        {

        }
        public XpoInitializer(IDataStore DataStore, params Type[] entityTypes)
             : this(DataStore, DataLayerType.Simple, entityTypes)
        {

        }
        public void InitSchema()
        {




            if (XpoDefault.DataLayer == null)
            {
                this.UpdateDal.UpdateSchema(false, dictionary.CollectClassInfos(entityTypes));
            }




        }

        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(this.WorkindDal);
        }
        XPDictionary PrepareDictionary(Type[] entityTypes)
        {
            var dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(entityTypes);
            return dict;
        }
    }
}
