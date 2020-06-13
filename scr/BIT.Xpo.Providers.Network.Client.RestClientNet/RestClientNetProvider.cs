using BIT.Data.Helpers;
using BIT.Data.Transfer;
using BIT.Data.Transfer.RestClientNet;
using BIT.Data.Xpo.DataStores;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using System;

namespace BIT.Xpo.Providers.Network.Client.RestClientNet
{
    public class RestClientNetProvider : NetworkClientProviderBase
    {
        public RestClientNetProvider(IFunctionClient functionClient, IObjectSerializationHelper objectSerializationHelper, AutoCreateOption autoCreateOption) : base(functionClient, objectSerializationHelper, autoCreateOption)
        {
        }
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var EndPoint = Parser.GetPartByName("EndPoint");
            var Token = Parser.GetPartByName("Token");
            var DataStoreId = Parser.GetPartByName("DataStoreId");
            RestClientNetFunctionClient restClientNetFunctionClient = new RestClientNetFunctionClient(EndPoint);

            return new AsyncDataStoreWrapper(new RestClientNetProvider(restClientNetFunctionClient, new SimpleObjectSerializationHelper(), autoCreateOption));
        }
    }
}
