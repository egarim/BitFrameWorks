using BIT.Data.Services;
using BIT.Xpo.Providers.WebApi.Server;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XpoWebApiTestController: XPOWebApiControllerBase
    {
        public XpoWebApiTestController(IConfigResolver<IDataStore> DataStoreResolver, IObjectSerializationService objectSerializationHelper, IStringSerializationService stringSerializationHelper) : base(DataStoreResolver, objectSerializationHelper, stringSerializationHelper)
        {

        }
    }
}
