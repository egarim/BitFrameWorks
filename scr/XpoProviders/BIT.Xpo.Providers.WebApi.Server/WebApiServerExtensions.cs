using BIT.Data.Services;
using BIT.Data.Xpo;
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

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationHelper stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApi(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, IConfigResolver<IDataStore> dataStoreResolver, IStringSerializationHelper stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
           
            serviceCollection.AddSingleton<IConfigResolver<IDataStore>>(dataStoreResolver);
            serviceCollection.AddSingleton<IStringSerializationHelper>(stringSerializationHelper);
            serviceCollection.AddSingleton<IObjectSerializationService>(simpleObjectSerializationHelper);

            return serviceCollection;
        }
    }
}
