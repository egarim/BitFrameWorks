using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpApiServerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiHttpDataTransferImpController : WebApiHttpDataTransferController
    {
        public WebApiHttpDataTransferImpController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {
        }
        public override Task<string> Get()
        { 
            return base.Get();
        }
    }
}
