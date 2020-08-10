using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Linq;
using System.Web.Http;

namespace XpoWebApiAspNet.Controllers
{
    public class XpoWebApiController : XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }
        //public IHttpActionResult Post()
        //{
        //    return base.Post(null);
        //}
        //public override IHttpActionResult Post([FromBody] DataParameters value)
        //{
        //    return Ok("Hi from the server");
        //    //return base.Post(value);
        //}

    }
}
