using BIT.Data.Functions;
using BIT.Data.Services;
using BIT.Xpo.Providers.WebApi.AspNetCore;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiHttpDataTransferControllerTest : XpoWebApiControllerBase
    {
        public WebApiHttpDataTransferControllerTest(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }
        public override Task<IDataResult> Post()
        {
            return base.Post();
        }

    }
}
