using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TestServer.NetFramework.Controllers
{
    public class XpoWebApiController :XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }

      

       
    }
}
