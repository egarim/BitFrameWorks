using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TestServer.NetFramework.Controllers
{
    public class XpoWebApiControllerImp : XpoWebApiController
    {
        public XpoWebApiControllerImp(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        {

        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
        //DataParameters
        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
