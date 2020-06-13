using BIT.Data.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;

namespace BIT.Data.Xpo.Extensions
{
    public static class XpoDataStoreResolverExtensions
    {
        public static UnitOfWork GetUnitOfWork(this IConfigResolver<IDataStore> Instance, string Id)
        {
            var DataStore = Instance.GetById(Id);
            if(DataStore!=null)
            {
                return new UnitOfWork(new SimpleDataLayer(DataStore));
            }
            else
            {
                throw new Exception($"there is no configuration for the DataStore with the ID:{Id}");
            }
        }
        public static IDataLayer GetSimpleDataLayer(this IConfigResolver<IDataStore> Instance, string Id)
        {
            var DataStore = Instance.GetById(Id);
            if (DataStore != null)
            {

                return new SimpleDataLayer(DataStore);
            }
            else
            {
                throw new Exception($"there is no configuration for the DataStore with the ID:{Id}");
            }
        }
    }

}
