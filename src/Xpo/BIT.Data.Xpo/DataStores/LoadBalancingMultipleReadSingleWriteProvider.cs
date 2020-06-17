using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Data.Xpo.DataStores
{

    /// <summary>
    /// This provider will write only on one data store but ready to any datastore that is free
    /// </summary>
    public class LoadBalancingMultipleReadSingleWriteProvider : DataStoreForkMultipleReadersSingleWriter
    {
        public LoadBalancingMultipleReadSingleWriteProvider(IDataStore changesProvider, params IDataStore[] readProviders) : base(changesProvider, readProviders)
        {
        }

        public override IDataStore AcquireReadProvider()
        {
            IDataStore dataStore = base.AcquireReadProvider();
            DataStoreDiagnostics(dataStore);
            return dataStore;
            // your code goes here
        }

        private static void DataStoreDiagnostics(IDataStore dataStore, [System.Runtime.CompilerServices.CallerMemberName] string MethodName = "")
        {
            DevExpress.Xpo.DB.ConnectionProviderSql RealDataStore = (DevExpress.Xpo.DB.ConnectionProviderSql)dataStore;

            System.Diagnostics.Debug.WriteLine($"{MethodName} Data from connection:" + RealDataStore.ConnectionString.ToString().Split(';').FirstOrDefault(cs => cs.StartsWith("Initial Catalog=")).Replace("Initial Catalog=", ""));
        }

        public override IDataStore AcquireChangeProvider()
        {
            IDataStore dataStore = base.AcquireChangeProvider();
            DataStoreDiagnostics(dataStore);
            return dataStore;
        }
    }
}