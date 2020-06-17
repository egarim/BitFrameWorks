using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Xpo;
using BIT.Data.Xpo.Functions;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.Network.HttpApiServer
{
    public static class WebApiHttpDataTransferExtensions
    {

        public static IServiceCollection AddXpoWebApiHttpDataTransfer(this IServiceCollection serviceCollection)
        {

            return serviceCollection.AddXpoWebApiHttpDataTransfer("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationService());
        }

        public static IServiceCollection AddXpoWebApiHttpDataTransfer(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApiHttpDataTransfer(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApiHttpDataTransfer(this IServiceCollection serviceCollection, IConfigResolver<IDataStore> dataStoreResolver, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationService)
        {

      
       
           
            IFunction function = new DataStoreFunctionServer(dataStoreResolver, simpleObjectSerializationService);
            serviceCollection.AddSingleton<IConfigResolver<IDataStore>>(dataStoreResolver);
            serviceCollection.AddSingleton<IStringSerializationService>(stringSerializationHelper);
            serviceCollection.AddSingleton<IObjectSerializationService>(simpleObjectSerializationService);
            serviceCollection.AddSingleton<IFunction>(function);

            return serviceCollection;
        }
    }
}
