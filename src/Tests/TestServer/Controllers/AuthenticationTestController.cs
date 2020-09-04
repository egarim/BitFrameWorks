using BIT.AspNetCore;
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

        AuthenticationTestController()
        {

        }
        [HttpGet]
        public async Task<string> Get()
        {
            return "It's working";//Task.FromResult("It's working");
        }
    }
}
