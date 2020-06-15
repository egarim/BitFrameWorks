using BIT.AspNetCore.Controllers;
using BIT.Data.DataTransfer;
using BIT.Data.Services;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BIT.Xpo.Providers.WebApi.Server
{
    public class WebApiHttpDataTransferController : HttpDataTransferController
    {

        public IFunction DataStoreFunctionServer { get; set; }

        public WebApiHttpDataTransferController(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;


        }
        public virtual async Task<string> Get()
        {
            return this.GetType().FullName;
        }

        public override async Task<IDataResult> Post()
        {
            //TODO Jm should the datastore id be part of the heders or the parameters?
            var Header=  this.GetHeader("DataStoreId");
            IDataParameters parameters = await DeserializeFromStream(Request.Body);
            parameters.AdditionalValues.Add("DataStoreId", Header);
            return DataStoreFunctionServer.ExecuteFunction(parameters);

        }
        //public virtual IDataResult Post()
        //{


        //    IDataParameters parameters = DeserializeFromStream(Request.Body);
        //    return DataStoreFunctionServer.ExecuteFunction(parameters);

        //}
    }
}
