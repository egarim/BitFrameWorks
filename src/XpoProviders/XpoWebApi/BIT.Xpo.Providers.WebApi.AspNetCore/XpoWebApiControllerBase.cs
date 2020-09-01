using BIT.AspNetCore.Controllers;
using BIT.Data.Functions;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BIT.Xpo.Providers.WebApi.AspNetCore
{
    public abstract class XpoWebApiControllerBase : FunctionControllerBase
    {

        public IFunction DataStoreFunctionServer { get; set; }

        public XpoWebApiControllerBase(IFunction DataStoreFunctionServer)
        {

            this.DataStoreFunctionServer = DataStoreFunctionServer;


        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task<string> Get()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
