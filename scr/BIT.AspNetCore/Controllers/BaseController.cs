using BIT.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BIT.AspNetCore.Controllers
{
    
    public class BaseController : ControllerBase
    {
        protected JwtPayload GetPayload()
        {
            var Token = this.HttpContext.Request.Headers["Token"];

            return JwtHelper.ReadToken(Token);
        }
        protected string GetHeader(string HeaderName)
        {
          return  this.HttpContext.Request.Headers[HeaderName];
        }
        protected  IConfigurationBuilder GetConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
        }
    }
}