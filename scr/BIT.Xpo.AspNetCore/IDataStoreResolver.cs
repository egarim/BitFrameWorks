using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;

namespace BIT.Xpo.AspNetCore
{
    public interface IDataStoreResolver
    {
        //TODO when the IDataStoreResolver, resolve 
        IDataStore GetDataStore(string DataStoreId);
        UnitOfWork GetUnitOfWork(string DataStoreId);
    }
}
