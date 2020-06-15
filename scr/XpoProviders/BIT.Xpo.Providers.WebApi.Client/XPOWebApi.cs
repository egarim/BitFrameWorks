
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
        private const string UrlPart = "Url";
        private const string TokenPart = "Token";
        private const string DataStoreIdPart = "DataStoreId";
        private const string ControllerPart = "Controller";

        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var Url = Parser.GetPartByName(UrlPart);
            var Token = Parser.GetPartByName(TokenPart);
            var DataStoreId = Parser.GetPartByName(DataStoreIdPart);
            var Controller = Parser.GetPartByName(ControllerPart);
            return new XPOWebApi(Url, Controller, autoCreateOption, Token, DataStoreId);
        }
        static XPOWebApi()
        {
           // DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }

        public XPOWebApi(string server,string Controller, AutoCreateOption autoCreateOption, string token, string DataStoreId)
        {
            this.server = server;
            this.controller = Controller;
            this.autoCreateOption = autoCreateOption;
            this.token = token;
            this.DataStoreId = DataStoreId;
            store = new BitFrameworksWebApi(this.server, this.controller,"LoginController", this.autoCreateOption, this.token, DataStoreId);
            //store = new BitFrameworksWebApiHttp(this.server, this.autoCreateOption, this.token, DataStoreId);
        }

        public static string GetConnectionString(string Url,string Controller, string Token, string DataStoreId)
        {

            return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};{UrlPart}={Url};{ControllerPart}={Controller};{TokenPart}={Token};{DataStoreIdPart}={DataStoreId}";
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
        private string controller;
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