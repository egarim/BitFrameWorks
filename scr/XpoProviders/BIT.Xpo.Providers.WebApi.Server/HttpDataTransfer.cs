using BIT.AspNetCore.Controllers;
using BIT.Data.Services;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.Providers.WebApi.Server
{
    class WebApiHttpDataTransferController: HttpDataTransferController
    {
        protected const string DataStoreIdHeader = "DataStoreId";
        private IConfigResolver<IDataStore> resolver;
        public IConfigResolver<IDataStore> Resolver { get => resolver; protected set => resolver = value; }

        public WebApiHttpDataTransferController(IConfigResolver<IDataStore> DataStoreResolver)
        {
            Resolver = DataStoreResolver;
         
        }
    }
}
