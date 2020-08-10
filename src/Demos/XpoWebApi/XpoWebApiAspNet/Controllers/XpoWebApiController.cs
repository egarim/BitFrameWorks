using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Linq;

namespace XpoWebApiAspNet.Controllers
{
    public class XpoWebApiController : XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }
    }
}
