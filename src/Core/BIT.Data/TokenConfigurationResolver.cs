using BIT.Data.Models;
using BIT.Data.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIT.Data
{
    public class TokenConfigurationResolver : ConfigurationResolverBase<JwtAuthInfo>, IResolver<JwtAuthInfo>
    {

        Dictionary<string, JwtAuthInfo> TokenInfo;

        public TokenConfigurationResolver(string configuratioName, Func<IConfiguration, string, JwtAuthInfo> instaceBuilder) : base(configuratioName, instaceBuilder)
        {

        }
        public TokenConfigurationResolver(string configuratioName) : base(configuratioName, null)
        {
            this.InstaceBuilder = new Func<IConfiguration, string, JwtAuthInfo>(GetToken);
        }




        private JwtAuthInfo GetToken(IConfiguration configuration, string Id)
        {


            //https://andrewlock.net/how-to-use-the-ioptions-pattern-for-configuration-in-asp-net-core-rc2/

            var configurationSection = configuration.GetSection("Tokens"); ;

         





            List<KeyValuePair<string, string>> TokenInfoFromConfig = configurationSection.AsEnumerable().ToList();

            TokenInfo = new Dictionary<string, JwtAuthInfo>();
            foreach (KeyValuePair<string, string> Value in TokenInfoFromConfig)
            {


                if (Value.Value == null)
                    continue;

                string[] Split = Value.Key?.Split(':');
                //if (Split.Length > 1)
                //    TokenInfo.Add(Split[1], Value.Value);
            }

            if (!TokenInfo.ContainsKey(Id))
                throw new ArgumentException($"Missin AutoCreateOptions:{Id}");



            return new JwtAuthInfo();




        
        }


    }
}
