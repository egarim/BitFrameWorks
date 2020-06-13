using BIT.Data.Helpers;
using BIT.Data.Transfer;
using BIT.Data.Xpo.DataStores;
using BIT.Data.Xpo.Models;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Helpers;
using System;
using System.Drawing.Text;
using System.Threading.Tasks;

namespace BIT.Xpo.Providers.Network
{
    public class NetworkClientProviderBase : IDataStore, ICommandChannel
    {
        public static IDataStore CreateProviderFromString(string connectionString, AutoCreateOption autoCreateOption, out IDisposable[] objectsToDisposeOnDisconnect)
        {
            objectsToDisposeOnDisconnect = null;
            ConnectionStringParser Parser = new ConnectionStringParser(connectionString);
            var EndPoint = Parser.GetPartByName("EndPoint");
            var Token = Parser.GetPartByName("Token");
            var DataStoreId = Parser.GetPartByName("DataStoreId");

            return new AsyncDataStoreWrapper(new NetworkClientProviderBase(null,new SimpleObjectSerializationHelper(), autoCreateOption));
        }
        static NetworkClientProviderBase()
        {
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }
        IFunctionClient functionClient;
        IObjectSerializationHelper objectSerializationHelper;
        public NetworkClientProviderBase(IFunctionClient functionClient, IObjectSerializationHelper objectSerializationHelper, AutoCreateOption autoCreateOption)
        {
            this.objectSerializationHelper = objectSerializationHelper;
        }

        public static string GetConnectionString(string EndPoint, string param1, string Param2)
        {

            return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};EndPoint={EndPoint};Token={param1};DataStoreId={param1}";
        }
        public static void Register()
        {
            DataStoreBase.RegisterDataStoreProvider(XpoProviderTypeString, CreateProviderFromString);

        }


        public const string XpoProviderTypeString = nameof(NetworkClientProviderBase);
        private string server;
        private AutoCreateOption autoCreateOption;
        private string token;
        private string DataStoreId;
        public AutoCreateOption AutoCreateOption => autoCreateOption;

        public async Task<ModificationResult> ModifyData(params ModificationStatement[] dmlStatements)
        {

            IDataParameters Parameters = new DataParameters();
            Parameters.MemberName = nameof(ModifyData);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<ModificationStatement[]>(dmlStatements);
            var DataResult= await functionClient.ExecuteFunction(Parameters);
            var ModificationResults =  this.objectSerializationHelper.GetObjectsFromByteArray<ModificationResult>(DataResult.ResultValue);
            return ModificationResults ;
        }

        public async Task<SelectedData> SelectData(params SelectStatement[] selects)
        {
            IDataParameters Parameters = new DataParameters();
            Parameters.MemberName = nameof(SelectData);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<SelectStatement[]>(selects);
            var DataResult = await functionClient.ExecuteFunction(Parameters);
            var SelectedData = this.objectSerializationHelper.GetObjectsFromByteArray<SelectedData>(DataResult.ResultValue);
            return SelectedData;
        }

        public async Task<UpdateSchemaResult> UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            IDataParameters Parameters = new DataParameters();
            UpdateSchemaParameters updateSchemaParameters = new UpdateSchemaParameters(dontCreateIfFirstTableNotExist, tables);
            Parameters.MemberName = nameof(UpdateSchema);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<UpdateSchemaParameters>(updateSchemaParameters);
            IDataResult DataResult = await functionClient.ExecuteFunction(Parameters);
            var UpdateSchemaResult = this.objectSerializationHelper.GetObjectsFromByteArray<UpdateSchemaResult>(DataResult.ResultValue);
            return UpdateSchemaResult;
        }

        async Task<object> Do(string command,object args)
        {
            IDataParameters Parameters = new DataParameters();
            CommandChannelDoParams commandChannelDoParams = new CommandChannelDoParams(command, args);
            Parameters.MemberName = nameof(UpdateSchema);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<CommandChannelDoParams>(commandChannelDoParams);
            IDataResult DataResult = await functionClient.ExecuteFunction(Parameters);
            var UpdateSchemaResult = this.objectSerializationHelper.GetObjectsFromByteArray<object>(DataResult.ResultValue);
            return UpdateSchemaResult;
        }
        object ICommandChannel.Do(string command, object args)
        {

            return this.Do(command,args).GetAwaiter().GetResult();

        }

        UpdateSchemaResult IDataStore.UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
        {
           return this.UpdateSchema(doNotCreateIfFirstTableNotExist,tables).GetAwaiter().GetResult();
        }

        SelectedData IDataStore.SelectData(params SelectStatement[] selects)
        {
            return this.SelectData(selects).GetAwaiter().GetResult();
        }

        ModificationResult IDataStore.ModifyData(params ModificationStatement[] dmlStatements)
        {
            return this.ModifyData(dmlStatements).GetAwaiter().GetResult();
        }
    }
}
