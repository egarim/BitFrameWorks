using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Linq;

namespace BIT.Xpo
{
    public class XpoInitializer
    {

        readonly Type[] entityTypes;

        private IDataLayer UpdateDal;
        public XpoInitializer(string connectionString, params Type[] entityTypes)
        {
            this.entityTypes = entityTypes;
            UpdateDal = XpoDefault.GetDataLayer(connectionString,this.PrepareDictionary(),AutoCreateOption.DatabaseAndSchema);
        }
        public XpoInitializer(IDataStore DataStore, params Type[] entityTypes)
        {
            this.entityTypes = entityTypes;
            UpdateDal = new SimpleDataLayer(this.PrepareDictionary(), DataStore);
        }
        public void InitSchema()
        {
            var dictionary = PrepareDictionary();



            if (XpoDefault.DataLayer == null)
            {
                this.UpdateDal.UpdateSchema(false, dictionary.CollectClassInfos(entityTypes));
            }

          


        }
   
        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(this.UpdateDal);
        }
        XPDictionary PrepareDictionary()
        {
            var dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(entityTypes);
            return dict;
        }
    }
}
