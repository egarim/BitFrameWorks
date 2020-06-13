

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIT.Data.Helpers
{
    public class ConfigurationResolverBase<T>: IConfigResolver<T> where T : class
    {
        protected Dictionary<string, T> Instances = new Dictionary<string, T>();
       
        private string ConfigurationName { get;set; }
        protected Func<IConfiguration,string, T> InstaceBuilder;
        public T GetById(string Id)
        {
            if (!Instances.ContainsKey(Id))
            {

                T Instance = InstaceBuilder.Invoke(LoadConfiguration(), Id);


                if(Instance==null)
                    throw new ArgumentException($"there is no configuration for the ID:{Id}",nameof(Id));

                this.Instances.Add(Id, Instance);
                return Instances[Id];
            }
            else
                return Instances[Id];

        }
        public ConfigurationResolverBase(string configuratioName,Func<IConfiguration,string,T> instaceBuilder)
        {
            ConfigurationName = configuratioName;
            InstaceBuilder = instaceBuilder;
        }
        private IConfigurationRoot LoadConfiguration()
        {
            
           var  builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile(ConfigurationName);
           return builder.Build();
        }
    }
}
