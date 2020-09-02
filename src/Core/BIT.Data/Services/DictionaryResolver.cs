

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
        Dictionary<string, Dictionary<string,string>> Dictionaries;
        private IDictionary<string, string> CreateNewInstance(IConfiguration configuration, string Id)
        {
           
            var EnumerableData = configuration.GetSection(DictionaryName).AsEnumerable();

            List<KeyValuePair<string, string>> AutoCreateOptionsFromConfig = configuration.GetSection(DictionaryName).AsEnumerable().ToList();

            Dictionaries = new Dictionary<string, Dictionary<string, string>>();
            foreach (KeyValuePair<string, string> Item in AutoCreateOptionsFromConfig)
            {


                if (Item.Value == null)
                    continue;

                string[] Split = Item.Key?.Split(':');
                if (Split.Length > 1)
                {
                    ConfigurationStringParser ConfigurationStringParser = new ConfigurationStringParser(Item.Value);
                    Dictionary<string, string> Data = new Dictionary<string, string>();
                    var parts=ConfigurationStringParser.ExtractParts(Item.Value);
                 
                    if (parts!=null)
                    {
                        foreach (string part in parts)
                        {
                            var KeyValuePair = part.Split('=');
                            Data.Add(KeyValuePair[0], ConfigurationStringParser.GetPartByName(KeyValuePair[0]));
                        }
                    }
                    Dictionaries.Add(Split[1],Data);
                }
                    
            }

            if (!Dictionaries.ContainsKey(Id))
                throw new ArgumentException($"Missing AutoCreateOptions:{Id}");



            //var AutoCreateOptionsFromConfig = configuration.GetSection(DictionaryName).AsEnumerable().ToList();
            return Dictionaries[Id];
        }

    }
}
