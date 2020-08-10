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

            
        }
        public IHttpActionResult Get()
        {
            return Ok($"This is working )) {this.GetType().FullName}");
        }

        protected async virtual Task<IDataParameters> DeserializeFromStream(Stream stream)
        {
            var sr = new StreamReader(stream);
            var json = await sr.ReadToEndAsync();
            return JsonConvert.DeserializeObject<DataParameters>(json);

        }
        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiControllerBase(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;
        }
    }
}
