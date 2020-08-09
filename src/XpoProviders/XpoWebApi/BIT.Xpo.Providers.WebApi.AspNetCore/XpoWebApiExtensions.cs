using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo.Functions;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.WebApi.AspNetCore
{
    public static class XpoWebApiExtensions
    {

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection)
        {

            return serviceCollection.AddXpoWebApi("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationService());
        }

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApi(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }   
        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, IConfigResolver<IDataStore> dataStoreResolver, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationService)
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
