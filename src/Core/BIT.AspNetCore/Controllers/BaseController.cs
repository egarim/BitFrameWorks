using BIT.Data.Services;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BIT.AspNetCore.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        private const string TokenHeader = "Token";
        IJwtService jwtService { get; set; }
        public BaseController(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }
        protected JwtPayload GetPayload()
        {
            var Token = this.HttpContext.Request.Headers[TokenHeader];

            return this.jwtService.TokenToJwtPayload(Token);
        }
        protected string GetHeader(string HeaderName)
        {
            return this.HttpContext.Request.Headers[HeaderName];
        }
        protected IConfigurationBuilder GetConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
        }
    }
}