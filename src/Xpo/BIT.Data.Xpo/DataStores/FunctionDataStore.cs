using BIT.Data.Functions;
using BIT.Data.Services;
using BIT.Xpo.Models;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using System;
using System.Threading.Tasks;


namespace BIT.Xpo.DataStores
{
    public abstract class FunctionDataStore : IDataStore, ICommandChannel
    {

        IFunction FunctionClient { get; set; }
        IObjectSerializationService objectSerializationHelper;
        public FunctionDataStore(IFunction functionClient, IObjectSerializationService objectSerializationHelper, AutoCreateOption autoCreateOption)
        {
            this.FunctionClient = functionClient;
            this.objectSerializationHelper = objectSerializationHelper;
        }

        //HACK you need to implement the GetConnectionString on the child classes because you might need different information for differnt functions clients
        //public static string GetConnectionString(string EndPoint, string param1, string Param2)
        //{

        //    return $"{DataStoreBase.XpoProviderTypeParameterName}={XpoProviderTypeString};EndPoint={EndPoint};Token={param1};DataStoreId={param1}";
        //}

        private AutoCreateOption autoCreateOption;
        public AutoCreateOption AutoCreateOption => autoCreateOption;

        public virtual ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {

            IDataParameters Parameters = new DataParameters();
            Parameters.MemberName = nameof(ModifyData);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<ModificationStatement[]>(dmlStatements);
            var DataResult = FunctionClient.ExecuteFunction(Parameters);
            var ModificationResults = this.objectSerializationHelper.GetObjectsFromByteArray<ModificationResult>(DataResult.ResultValue);
            return ModificationResults;
        }

        public virtual SelectedData SelectData(params SelectStatement[] selects)
        {
            IDataParameters Parameters = new DataParameters();
            Parameters.MemberName = nameof(SelectData);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<SelectStatement[]>(selects);
            var DataResult = FunctionClient.ExecuteFunction(Parameters);
            var SelectedData = this.objectSerializationHelper.GetObjectsFromByteArray<SelectedData>(DataResult.ResultValue);
            return SelectedData;
        }

        public virtual UpdateSchemaResult UpdateSchema(bool dontCreateIfFirstTableNotExist, params DBTable[] tables)
        {
            IDataParameters Parameters = new DataParameters();
            UpdateSchemaParameters updateSchemaParameters = new UpdateSchemaParameters(dontCreateIfFirstTableNotExist, tables);
            Parameters.MemberName = nameof(UpdateSchema);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<UpdateSchemaParameters>(updateSchemaParameters);
            IDataResult DataResult = FunctionClient.ExecuteFunction(Parameters);
            var UpdateSchemaResult = this.objectSerializationHelper.GetObjectsFromByteArray<UpdateSchemaResult>(DataResult.ResultValue);
            return UpdateSchemaResult;
        }

        protected virtual object Do(string command, object args)
        {
            IDataParameters Parameters = new DataParameters();
            CommandChannelDoParams commandChannelDoParams = new CommandChannelDoParams(command, args);
            Parameters.MemberName = nameof(Do);
            Parameters.ParametersValue = this.objectSerializationHelper.ToByteArray<CommandChannelDoParams>(commandChannelDoParams);
            IDataResult DataResult = FunctionClient.ExecuteFunction(Parameters);


            switch (commandChannelDoParams.Command)
            {

                case CommandChannelHelper.Command_ExecuteScalarSQL:
                case CommandChannelHelper.Command_ExecuteScalarSQLWithParams:
                    return this.objectSerializationHelper.GetObjectsFromByteArray<object>(DataResult.ResultValue);
                    

                case CommandChannelHelper.Command_ExecuteQuerySQL:
                case CommandChannelHelper.Command_ExecuteQuerySQLWithParams:
                case CommandChannelHelper.Command_ExecuteQuerySQLWithMetadata:
                case CommandChannelHelper.Command_ExecuteQuerySQLWithMetadataWithParams:
                case CommandChannelHelper.Command_ExecuteStoredProcedure:
                case CommandChannelHelper.Command_ExecuteStoredProcedureParametrized:
                    return this.objectSerializationHelper.GetObjectsFromByteArray <SelectedData>(DataResult.ResultValue);
                    

                case CommandChannelHelper.Command_ExecuteNonQuerySQL:
                case CommandChannelHelper.Command_ExecuteNonQuerySQLWithParams:
                    return this.objectSerializationHelper.GetObjectsFromByteArray<int>(DataResult.ResultValue);
                  

                default:
                    throw new Exception($"ICommandChannel Do method retuned an unknow data type while processing {commandChannelDoParams.Command}");
            }


        
        }
        object ICommandChannel.Do(string command, object args)
        {

            
            return this.Do(command, args);

        }
   
    //    UpdateSchemaResult IDataStore.UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables)
    //    {
    //        UpdateSchemaResult result=new UpdateSchemaResult();
    //        Task.Run(async () =>
    //       {
    //           UpdateSchemaResult result = await this.UpdateSchema(doNotCreateIfFirstTableNotExist, tables);
    //       });
    //        return result;
    //        //return this.UpdateSchema(doNotCreateIfFirstTableNotExist, tables).GetAwaiter().GetResult();
    //    }

    //    SelectedData IDataStore.SelectData(params SelectStatement[] selects)
    //    {
    //        SelectedData selectedData = null;
    //        Task.Run(async () =>
    //        {
               
    //            selectedData = await this.SelectData(selects);
             
    //        }).Wait();
    //        return selectedData;
    //    }

    //    ModificationResult IDataStore.ModifyData(params ModificationStatement[] dmlStatements)
    //    {
    //        ModificationResult modificationResult = null;
    //        Task.Run(async () =>
    //        {

    //            modificationResult = await this.ModifyData(dmlStatements);
    //            return modificationResult;
    //        });
    //        return modificationResult;
    //    }
    }
}
