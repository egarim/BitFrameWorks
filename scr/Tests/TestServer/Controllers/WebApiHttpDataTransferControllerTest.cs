using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Xpo.Providers.WebApi.Server;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebApiHttpDataTransferControllerTest : WebApiHttpDataTransferController
    {
        public WebApiHttpDataTransferControllerTest(IConfigResolver<IDataStore> DataStoreResolver) : base(DataStoreResolver)
        {

        }
        public override Task<DataResult> Post()
        {
            return base.Post();
        }
    }
}
