using BIT.AspNetCore;
using BIT.AspNetCore.Extensions;
using BIT.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthentication()]
    public class AuthenticationTestController : ControllerBase
    {
        IResolver<IJwtService> JwtResolver;
        public AuthenticationTestController(IResolver<IJwtService> JwtResolver)
        {
            this.JwtResolver = JwtResolver;
        }
        [HttpGet]
        public async Task<string> Get()
        {
            //JwtResolver.GetById()
            return "It's working";//Task.FromResult("It's working");
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<bool> GetTokenValues()
        {
            var JwtPayload= this.JwtResolver.GetJwtPayload(this.HttpContext);
           
         
            return JwtPayload.Iss == "Xari" && JwtPayload["UserOid"].ToString() == "001";
          
        }

      
    }
}
