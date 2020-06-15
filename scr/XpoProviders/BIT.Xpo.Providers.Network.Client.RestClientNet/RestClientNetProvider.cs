using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using BIT.Data.Xpo.DataStores;
using DevExpress.Data.Helpers;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace BIT.Xpo.Providers.Network.Client.RestClientNet
{
    public class RestClientNetProvider : FunctionDataStore
    {
        public const string TokenPart = "Token";
        public const string DataStoreIdPart = "DataStoreId";
        private const string UrlPart = "Url";
        private const string ControllerPart = "Controller";
        public RestClientNetProvider(IFunction functionClient, IObjectSerializationService objectSerializationService, AutoCreateOption autoCreateOption) : base(functionClient, objectSerializationService, autoCreateOption)
        {
        }
        public static string GetConnectionString(string Url, string Controller, string Token, string DataStoreId)
        {

            return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};{UrlPart}={Url};{ControllerPart}={Controller};{TokenPart}={Token};{DataStoreIdPart}={DataStoreId}";
        }
        public const string XpoProviderTypeString = nameof(RestClientNetProvider);
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var Url = Parser.GetPartByName(UrlPart);
            var Controller = Parser.GetPartByName(ControllerPart);
            var Token = Parser.GetPartByName(TokenPart);
            var DataStoreId = Parser.GetPartByName(DataStoreIdPart);
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(TokenPart, Token);
            Headers.Add(DataStoreIdPart, DataStoreId);
            RestClientNetFunctionClient restClientNetFunctionClient = new RestClientNetFunctionClient(new Uri(new Uri(Url), Controller).ToString(), Headers);

            return new AsyncDataStoreWrapper(new RestClientNetProvider(restClientNetFunctionClient, new SimpleObjectSerializationService(), autoCreateOption));
        }
        public static void Register()
        {
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }
    }
}
