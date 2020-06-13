using BIT.Data.Helpers;
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

            return serviceCollection.AddXpoWebApi("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationHelper());
        }

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationHelper stringSerializationHelper, IObjectSerializationHelper simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApi(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, IConfigResolver<IDataStore> dataStoreResolver, IStringSerializationHelper stringSerializationHelper, IObjectSerializationHelper simpleObjectSerializationHelper)
        {
           
            serviceCollection.AddSingleton<IConfigResolver<IDataStore>>(dataStoreResolver);
            serviceCollection.AddSingleton<IStringSerializationHelper>(stringSerializationHelper);
            serviceCollection.AddSingleton<IObjectSerializationHelper>(simpleObjectSerializationHelper);

            return serviceCollection;
        }
    }
}
