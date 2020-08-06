using BIT.Data.DataTransfer;
using BIT.Data.Services;
using System;
using System.Web.Http;
using BIT.Xpo.Functions;
namespace BIT.Xpo.Providers.WebApi.AspNet
{

    public abstract class XpoWebApiControllerBase : ApiController
    {
        //DataParameters
        // POST api/values
        public IHttpActionResult Post([FromBody] DataParameters value)
        {

            //TODO get headers 
            //IDataParameters parameters = await DeserializeFromStream(Request.Body);
            //parameters.AdditionalValues.Add("DataStoreId", Header);
            return Ok(this.DataStoreFunctionServer.ExecuteFunction(value));
            //return Ok(new { Name="Jose",LastName="Ojeda" });
        }
        public IHttpActionResult Get()
        {
            return Ok($"This is working )) {this.GetType().FullName}");
        }


        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiControllerBase(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;
        }
    }
}
