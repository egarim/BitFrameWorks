using System;
using System.Collections.Generic;
using System.Linq;

namespace BIT.Data.Services
{
    public class JwtServiceResolver : IResolver<IJwtService>
    {
        protected Dictionary<string, IJwtService> IJwtServices = new Dictionary<string, IJwtService>();
        protected IResolver<IDictionary<string, string>> AppSettingsDictionary;
        public JwtServiceResolver(IResolver<IDictionary<string, string>> AppSettingsDictionary)
        {
            this.AppSettingsDictionary = AppSettingsDictionary;
        }

        public IJwtService GetById(string Id)
        {
            if(IJwtServices.ContainsKey(Id))
            {
                return IJwtServices[Id];
            }
            else
            {
                var JwtInfo = AppSettingsDictionary.GetById(Id);
                var JwtService = new JwtService(JwtInfo["Key"], JwtInfo["ValidIssuer"]);
                IJwtServices.Add(Id, JwtService);
                return JwtService;
            }
        }
    }
}
