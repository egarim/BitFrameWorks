

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIT.Data.Services
{
    public class DictionaryResolver : ConfigurationResolverBase<IDictionary<string, string>>, IResolver<IDictionary<string, string>>

    {

        Func<IConfiguration, string, IDictionary<string, string>> instaceBuilder;
        string DictionaryName = string.Empty;

        public DictionaryResolver(string configuratioName, Func<IConfiguration, string, IDictionary<string, string>> instaceBuilder,string DictionaryName) : base(configuratioName, instaceBuilder)
        {
            this.instaceBuilder = instaceBuilder;
         
        }



        public DictionaryResolver(string configuratioName, string DictionaryName) : base(configuratioName, null)
        {
            this.DictionaryName = DictionaryName;
            this.InstanceBuilder = new Func<IConfiguration, string, IDictionary<string, string>>(CreateNewInstance);
        }

        private IDictionary<string, string> CreateNewInstance(IConfiguration configuration, string arg2)
        {
            Dictionary<string, string> instance = new Dictionary<string, string>();
            var EnumerableData = configuration.GetSection(DictionaryName).AsEnumerable();
            var AutoCreateOptionsFromConfig = configuration.GetSection(DictionaryName).AsEnumerable().ToList();
            return instance;
        }

    }
}
