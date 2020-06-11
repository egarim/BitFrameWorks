using BIT.Data.Helpers;
using BIT.Xpo.Providers.WebApi.Server;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XpoWebApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XpoWebApiController : XPOWebApiControllerBase
    {
        public XpoWebApiController(IResolver<IDataStore> DataStoreResolver, IObjectSerializationHelper objectSerializationHelper, IStringSerializationHelper stringSerializationHelper) : base(DataStoreResolver, objectSerializationHelper, stringSerializationHelper)
        {
        }

        public override Task<IActionResult> Do()
        {
            return base.Do();
        }

        public override byte[] GetAutoCreateOptions()
        {
            return base.GetAutoCreateOptions();
        }

        public override Task<IActionResult> ModifyData()
        {
            return base.ModifyData();
        }

        public override Task<IActionResult> SelectData()
        {
            return base.SelectData();
        }

        public override Task<IActionResult> UpdateSchema()
        {
            return base.UpdateSchema();
        }
    }
}
