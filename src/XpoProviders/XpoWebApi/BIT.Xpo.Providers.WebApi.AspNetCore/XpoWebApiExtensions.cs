using BIT.Data.Functions;
using BIT.Data.Services;
using BIT.Xpo.Functions;
using DevExpress.Data.Helpers;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.WebApi.AspNetCore
{
    public static class XpoWebApiExtensions
    {
        #region AddXpoWebApiWithDal

        public static IServiceCollection AddXpoWebApiWithDal(this IServiceCollection serviceCollection, params Type[] entityTypes)
        {

            return serviceCollection.AddXpoWebApiWithDal("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationService(), entityTypes);
        }
        public static IServiceCollection AddXpoWebApiWithDal(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper, params Type[] entityTypes)
        {
            XpoDataStoreResolver dataStoreResolver = new XpoDataStoreResolver(appsettingsjson);
            return AddXpoWebApiWithDal(serviceCollection, dataStoreResolver, stringSerializationHelper, simpleObjectSerializationHelper, new XpoInitializerResolver(dataStoreResolver, entityTypes));
            //return serviceCollection.AddXpoWebApi(dataStoreResolver, stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApiWithDal(this IServiceCollection serviceCollection, IResolver<IDataStore> dataStoreResolver, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationService, IResolver<IXpoInitializer> XpoInitializerResolver)
        {
            serviceCollection.AddSingleton<IResolver<IXpoInitializer>>(XpoInitializerResolver);
            return serviceCollection.AddXpoWebApi(dataStoreResolver, stringSerializationHelper, simpleObjectSerializationService);
        }
        #endregion

        #region XpoWebApi

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection)
        {

            return serviceCollection.AddXpoWebApi("appsettings.json", new StringSerializationHelper(), new SimpleObjectSerializationService());
        }

        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, string appsettingsjson, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationHelper)
        {
            return serviceCollection.AddXpoWebApi(new XpoDataStoreResolver(appsettingsjson), stringSerializationHelper, simpleObjectSerializationHelper);
        }
        public static IServiceCollection AddXpoWebApi(this IServiceCollection serviceCollection, IResolver<IDataStore> dataStoreResolver, IStringSerializationService stringSerializationHelper, IObjectSerializationService simpleObjectSerializationService)
        {




            IFunction function = new DataStoreFunctionServer(dataStoreResolver, simpleObjectSerializationService);
            serviceCollection.AddSingleton<IResolver<IDataStore>>(dataStoreResolver);
            serviceCollection.AddSingleton<IStringSerializationService>(stringSerializationHelper);
            serviceCollection.AddSingleton<IObjectSerializationService>(simpleObjectSerializationService);
            serviceCollection.AddSingleton<IFunction>(function);

            return serviceCollection;
        }
        #endregion
    }
}
