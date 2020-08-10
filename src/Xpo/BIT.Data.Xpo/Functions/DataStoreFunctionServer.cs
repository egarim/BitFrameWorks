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
            string id = Parameters.AdditionalValues["DataStoreId"].ToString();
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
                    dataResult.ResultValue =
                                 ObjectSerializationService
                                 .ToByteArray(
                                     commandChannel.Do(DoParams.Command,
                                     DoParams.Args)
                                     );
                }

            }

            return dataResult;
        }
    }
}
