using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace XpoWebApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XpoWebApiController : XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }
    }
}
