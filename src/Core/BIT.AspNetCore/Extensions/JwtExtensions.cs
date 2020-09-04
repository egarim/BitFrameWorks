using BIT.Data.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIT.AspNetCore.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceCollection)
        {

            return AddJwtAuthorization(serviceCollection, "appsettings.json", "JwtInfo");
        }
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceCollection,string ConfigurationName,string SectionName)
        {
            AppSettingsDictionaryResolverBase appSettingsDictionary = new AppSettingsDictionaryResolverBase(ConfigurationName, SectionName);
            return AddJwtAuthorization(serviceCollection, appSettingsDictionary);
        }
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceCollection, ConfigurationResolverBase<IDictionary<string, string>> configurationResolver)
        {

            return AddJwtAuthorization(serviceCollection,new JwtServiceResolver(configurationResolver));
        }
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection serviceCollection, IResolver<IJwtService> JwtServiceResolver)
        {

            return serviceCollection.AddSingleton<IResolver<IJwtService>>(JwtServiceResolver);
        }
       
        
    }
}