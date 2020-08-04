using BIT.Data.DataTransfer;
using BIT.Xpo.Providers.WebApi.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TestServer.NetFramework.Controllers
{
    public class XpoWebApiImpController :ApiController //XpoWebApiController
    {
        //public XpoWebApiControllerImp(IFunction DataStoreFunctionServer) : base(DataStoreFunctionServer)
        //{

        //}
        //public XpoWebApiControllerImp() : base(null)
        //{

        //}

        //public XpoWebApiImpController()
        //{
        //}
        IFunction test;
        public XpoWebApiImpController(IFunction DataStoreFunctionServer) 
        {
            test = DataStoreFunctionServer;
        }

        //// GET api/values
        public IEnumerable<string> Get()
        {


            if (this.test == null)
            {
                throw new Exception("Dependency not working");
            }

            //if(this.DataStoreFunctionServer==null)
            //{
            //    throw new Exception("Dependency not working");
            //}
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
