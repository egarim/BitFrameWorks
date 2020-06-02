
using BIT.Data.Helpers;
using BIT.Data.Models;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using RestSharp;
using System;
using System.Linq;
namespace BIT.Xpo.Providers.WebApi.Client
{



    //TODO review this article https://www.devexpress.com/Support/Center/Question/Details/K18167/troubleshooting-how-to-resolve-the-entering-the-getobjectsnonreenterant-state-error
    public class XPOWebApi : IDataStore, ICommandChannel
    {
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            //TODO the connection string should be read in any order and I should not assume that 
            //var ConnectionParams = connectionString.Split(';');

            //"XpoProvider=RestApiAgnosticDataStoreImp;EndPoint=http://192.168.1.64/MainServer/api/DataStoreAgnosticMultiDb;Token=Empty;DataStoreId=db1";


            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var EndPoint = Parser.GetPartByName("EndPoint");
            var Token = Parser.GetPartByName("Token");
            var DataStoreId = Parser.GetPartByName("DataStoreId");


            //ConnectionParams.Where(cs => cs.StartsWith("EndPoint")).FirstOrDefault();

            //var Server = ConnectionParams[0].Split(';')[0].Split('=')[1];
            //var Token = ConnectionParams[1].Split(';')[0].Split('=')[1];
            //var DataStoreId = ConnectionParams[2].Split(';')[0].Split('=')[1];

            return new XPOWebApi(EndPoint, autoCreateOption, Token, DataStoreId);
        }
        static XPOWebApi()
        {
           // DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }

        public XPOWebApi(string server, AutoCreateOption autoCreateOption, string token, string DataStoreId)
        {
            this.server = server;
            this.autoCreateOption = autoCreateOption;
            this.token = token;
            this.DataStoreId = DataStoreId;
            store = new BitFrameworksWebApi(this.server, this.autoCreateOption, this.token, DataStoreId);
            //store = new BitFrameworksWebApiHttp(this.server, this.autoCreateOption, this.token, DataStoreId);
        }

        public static string GetConnectionString(string EndPoint, string Token, string DataStoreId)
        {

            return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};EndPoint={EndPoint};Token={Token};DataStoreId={DataStoreId}";
        }
        public static void Register()
        {
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }

        public LoginResult Login(string UserName, string Password)
        {
            return store.Login(UserName, Password);
        }
        BitFrameworksWebApi store;
        //BitFrameworksWebApi store;
        public const string XpoProviderTypeString = nameof(XPOWebApi);
        private string server;
        private AutoCreateOption autoCreateOption;
        private string token;
        private string DataStoreId;
        public AutoCreateOption AutoCreateOption => autoCreateOption;

        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {

            //var Task=  store.ModifyData<ModificationResult>(BitFrameworksWebApi.ToByteArray(dmlStatements));
            //Task.Wait();
            //return Task.Result;
            return store.ModifyData<ModificationResult>(BitFrameworksWebApi.ToByteArray(dmlStatements));
        }

        public SelectedData SelectData(params SelectStatement[] selects)
        {
            //var task= store.SelectData<SelectedData>(BitFrameworksWebApi.ToByteArray(selects));
            //task.Wait();
            //return task.Result;
            return store.SelectData<SelectedData>(BitFrameworksWebApi.ToByteArray(selects));
        }

        public UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            //var task= store.UpdateSchema<UpdateSchemaResult>(dontCreateIfFirstTableNotExist, BitFrameworksWebApi.SerializeObject(tables));
            //task.Wait();
            //return task.Result;
            return store.UpdateSchema<UpdateSchemaResult>(dontCreateIfFirstTableNotExist, BitFrameworksWebApi.SerializeObject(tables));
        }

        object ICommandChannel.Do(string command, object args)
        {

            return store.Do(command, args);

        }
    }
}