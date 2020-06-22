using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Linq;

namespace BIT.Xpo
{
    public class XpoInitializer
    {

       

        private IDataLayer UpdateDal;
        XPDictionary dictionary;
        Type[] entityTypes;
        public XpoInitializer(string connectionString, params Type[] entityTypes)
           : this(XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema), entityTypes)
        {
            
        }
        public XpoInitializer(IDataStore DataStore, params Type[] entityTypes)
        {
            this.entityTypes = entityTypes;
            dictionary = this.PrepareDictionary(entityTypes);
            UpdateDal = new SimpleDataLayer(this.PrepareDictionary(entityTypes), DataStore);
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
            return new UnitOfWork(this.UpdateDal);
        }
        XPDictionary PrepareDictionary(Type[] entityTypes)
        {
            var dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(entityTypes);
            return dict;
        }
    }
}
