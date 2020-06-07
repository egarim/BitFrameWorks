using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.WebApi.Client
{
    public class XPOWebApiTemplate: IDataStore, ICommandChannel
    {
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            


            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var EndPoint = Parser.GetPartByName("EndPoint");
            var Token = Parser.GetPartByName("Token");
            var DataStoreId = Parser.GetPartByName("DataStoreId");


         

            return new XPOWebApiTemplate("Param1", autoCreateOption);
        }
        static XPOWebApiTemplate()
        {
             DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }

        public XPOWebApiTemplate(string param1, AutoCreateOption autoCreateOption)
        {
        
        }

        public static string GetConnectionString(string EndPoint, string param1, string Param2)
        {

            return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};EndPoint={EndPoint};Token={param1};DataStoreId={param1}";
        }
        public static void Register()
        {
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }

      
        public const string XpoProviderTypeString = nameof(XPOWebApi);
        private string server;
        private AutoCreateOption autoCreateOption;
        private string token;
        private string DataStoreId;
        public AutoCreateOption AutoCreateOption => autoCreateOption;

        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {

            throw new NotImplementedException();
        }

        public SelectedData SelectData(params SelectStatement[] selects)
        {
            throw new NotImplementedException();
        }

        public UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            throw new NotImplementedException();
        }

        object ICommandChannel.Do(string command, object args)
        {

            throw new NotImplementedException();

        }
    }
}
