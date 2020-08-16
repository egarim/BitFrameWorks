using BIT.AspNetCore.Controllers;
using BIT.Data.DataTransfer;
using BIT.Data.Services;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BIT.Xpo.Providers.WebApi.AspNetCore
{
    public abstract class XpoWebApiControllerBase : HttpDataTransferController
    {

        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiControllerBase(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;


        }
        public virtual async Task<string> Get()
        {
            return $"This is working )) {this.GetType().FullName}";
        }

        public override async Task<IDataResult> Post()
        {


        

            //TODO Jm should the datastore id be part of the heders or the parameters?
            var DataStoreId=  this.GetHeader("DataStoreId");
            IDataParameters parameters = await DeserializeFromStream(Request.Body);
            parameters.AdditionalValues.Add("DataStoreId", DataStoreId);
            return DataStoreFunctionServer.ExecuteFunction(parameters);

        }
   
       
    }
}
