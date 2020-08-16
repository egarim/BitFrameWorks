using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo.Models;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using System;

namespace BIT.Xpo.Functions
{




    public class DataStoreFunctionServer : IFunction
    {
        private const string DataStoreId = "DataStoreId";

        public IConfigResolver<IDataStore> ConfigResolver { get; set; }
        public IObjectSerializationService ObjectSerializationService { get; set; }
        public DataStoreFunctionServer(IConfigResolver<IDataStore> configResolver, IObjectSerializationService objectSerializationService)
        {
            ConfigResolver = configResolver;
            ObjectSerializationService = objectSerializationService;
        }
        public DataStoreFunctionServer(string ConfigName)
        {
            this.ConfigResolver = new XpoDataStoreResolver(ConfigName);
        }
        public DataStoreFunctionServer()
        {
            this.ConfigResolver = new XpoDataStoreResolver();
        }
        public IDataResult ExecuteFunction(IDataParameters Parameters)
        {
            DataResult dataResult = new DataResult();
            string id = Parameters.AdditionalValues[DataStoreId].ToString();
            IDataStore DataStore = null;
            DataStore = this.ConfigResolver.GetById(id);


            if (Parameters.MemberName == nameof(IDataStore.SelectData))
            {

                dataResult.ResultValue =
                    ObjectSerializationService
                    .ToByteArray(
                        DataStore.SelectData(
                            ObjectSerializationService
                            .GetObjectsFromByteArray<SelectStatement[]>(Parameters.ParametersValue)
                            )
                        );
            }
            if (Parameters.MemberName == nameof(IDataStore.ModifyData))
            {
                dataResult.ResultValue =
                   ObjectSerializationService
                   .ToByteArray(
                       DataStore.ModifyData(
                           ObjectSerializationService
                           .GetObjectsFromByteArray<ModificationStatement[]>(Parameters.ParametersValue)
                           )
                       );
            }
            if (Parameters.MemberName == nameof(IDataStore.UpdateSchema))
            {
                UpdateSchemaParameters updateSchemaParameters = ObjectSerializationService
                          .GetObjectsFromByteArray<UpdateSchemaParameters>(Parameters.ParametersValue);
                dataResult.ResultValue =
                  ObjectSerializationService
                  .ToByteArray(
                      DataStore.UpdateSchema(updateSchemaParameters.dontCreateIfFirstTableNotExist,
                      updateSchemaParameters.tables)
                      );
            }
            if (Parameters.MemberName == nameof(ICommandChannel.Do))
            {
                CommandChannelDoParams DoParams = ObjectSerializationService
                       .GetObjectsFromByteArray<CommandChannelDoParams>(Parameters.ParametersValue);

                ICommandChannel commandChannel = DataStore as ICommandChannel;
                if (commandChannel != null)
                {
                    object data = commandChannel.Do(DoParams.Command,
                                     DoParams.Args);


                    switch (DoParams.Command)
                    {
                       
                        case CommandChannelHelper.Command_ExecuteScalarSQL:
                        case CommandChannelHelper.Command_ExecuteScalarSQLWithParams:
                            dataResult.ResultValue =
                                        ObjectSerializationService
                                        .ToByteArray<object>(
                                        data
                                        );
                            break;

                        case CommandChannelHelper.Command_ExecuteQuerySQL:
                        case CommandChannelHelper.Command_ExecuteQuerySQLWithParams:
                        case CommandChannelHelper.Command_ExecuteQuerySQLWithMetadata:
                        case CommandChannelHelper.Command_ExecuteQuerySQLWithMetadataWithParams:
                        case CommandChannelHelper.Command_ExecuteStoredProcedure:
                        case CommandChannelHelper.Command_ExecuteStoredProcedureParametrized:
                            dataResult.ResultValue =
                                        ObjectSerializationService
                                        .ToByteArray<SelectedData>(
                                        data as SelectedData
                                        );

                            break;

                        case CommandChannelHelper.Command_ExecuteNonQuerySQL:
                        case CommandChannelHelper.Command_ExecuteNonQuerySQLWithParams:
                            dataResult.ResultValue =
                                        ObjectSerializationService
                                        .ToByteArray<int>(
                                        (int)data
                                        );
                            break;

                        default:
                            throw new Exception($"ICommandChannel Do method retuned an unknow data type while processing {DoParams.Command}");
                    }
                }
            }
            return dataResult;
        }
    }
}
