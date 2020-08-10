using BIT.Data.DataTransfer;
using BIT.Data.Services;
using System;
using System.Web.Http;
using BIT.Xpo.Functions;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BIT.Xpo.Providers.WebApi.AspNet
{

    public abstract class XpoWebApiControllerBase : ApiController
    {
       
      
        public async virtual Task<IHttpActionResult> Post()
        {
            var body=this.Request.Content;



            IDataParameters parameters = await DeserializeFromStream(await body.ReadAsStreamAsync());

            parameters.AdditionalValues.Add("DataStoreId", "001");
            IDataResult content = this.DataStoreFunctionServer.ExecuteFunction(parameters);
            return base.Ok(content);

            //return Ok("this is ok");
            //TODO get headers 
            //IDataParameters parameters = await DeserializeFromStream(Request.Body);
            //parameters.AdditionalValues.Add("DataStoreId", Header);
            //return Ok(this.DataStoreFunctionServer.ExecuteFunction(value));
            //return Ok(new { Name="Jose",LastName="Ojeda" });
        }
        public IHttpActionResult Get()
        {
            return Ok($"This is working )) {this.GetType().FullName}");
        }

        protected async virtual Task<IDataParameters> DeserializeFromStream(Stream stream)
        {
            try
            {

                var sr = new StreamReader(stream);
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<DataParameters>(json);

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                throw;
            }

        }
        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiControllerBase(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;
        }
    }
}
