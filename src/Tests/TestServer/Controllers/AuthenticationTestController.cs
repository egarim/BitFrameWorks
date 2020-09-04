using BIT.AspNetCore;
using BIT.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthentication()]
    public class AuthenticationTestController
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
    }
}
