using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Xpo;
using BIT.Data.Xpo.Functions;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.WebApi.Server
{
    public static class WebApiServerExtensions
    {

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection)
        {

            return serviceCollection.AddXpoWebApi("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationService());
        }

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApi(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, IConfigResolver<IDataStore> dataStoreResolver, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {

            //serviceCollection.AddSingleton<IConfigResolver<IDataStore>>(dataStoreResolver);
            //serviceCollection.AddSingleton<IStringSerializationService>(stringSerializationHelper);
            //serviceCollection.AddSingleton<IObjectSerializationService>(simpleObjectSerializationHelper);
            //IConfigResolver<IDataStore> DataStoreResolver = new XpoDataStoreResolver("appsettings.json");
            //IStringSerializationService stringSerializationHelper = new StringSerializationHelper();
            //IObjectSerializationService objectSerializationHelper = new SimpleObjectSerializationService();
            IFunction function = new DataStoreFunctionServer(dataStoreResolver, simpleObjectSerializationHelper);
            serviceCollection.AddSingleton<IConfigResolver<IDataStore>>(dataStoreResolver);
            serviceCollection.AddSingleton<IStringSerializationService>(stringSerializationHelper);
            serviceCollection.AddSingleton<IObjectSerializationService>(simpleObjectSerializationHelper);
            serviceCollection.AddSingleton<IFunction>(function);

            return serviceCollection;
        }
    }
}
