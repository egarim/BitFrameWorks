using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo;
using BIT.Xpo.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using XpoWebApiAspNet.App_Start;
namespace XpoWebApiAspNet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            //TODO update this line to reflect the location of the config file in your local setup

            container.RegisterInstance<IFunction>(new DataStoreFunctionServer(new XpoDataStoreResolver(@"C:\Users\Joche\source\repos\BitFrameWorks\src\Demos\XpoWebApi\XpoWebApiAspNet\appsettings.json"), new SimpleObjectSerializationService()));
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
