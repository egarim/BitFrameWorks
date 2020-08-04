using BIT.Data.DataTransfer;
using BIT.Data.Services;
using System;
using System.Web.Http;

namespace BIT.Xpo.Providers.WebApi.AspNet
{
    public class XpoWebApiController : ApiController
    {
        //DataParameters
        // POST api/values
        public void Post([FromBody] DataParameters value)
        {
        }
        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiController(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;


            IFunction function = new BIT.Xpo.Functions.DataStoreFunctionServer(new XpoDataStoreResolver(""), new SimpleObjectSerializationService());

        }
    }
}
