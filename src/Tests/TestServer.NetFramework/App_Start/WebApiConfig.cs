using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo;
using BIT.Xpo.Functions;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TestServer.NetFramework.App_Start;
using Unity;

namespace TestServer.NetFramework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();



            // IFunction function = new DataStoreFunctionServer(dataStoreResolver, simpleObjectSerializationService);
            //container.RegisterInstance<IConfigResolver<IDataStore>>(new XpoDataStoreResolver());
            container.RegisterInstance<IFunction>(new DataStoreFunctionServer(new XpoDataStoreResolver(), new SimpleObjectSerializationService()));
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
