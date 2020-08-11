using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace XpoWebApiAspNet.Controllers
{
    public class XpoWebApiController : XpoWebApiControllerBase
    {
        public XpoWebApiController(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }
        public override Task<IHttpActionResult> Post()
        {
            Task<IHttpActionResult> task = base.Post();
            return task;
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
