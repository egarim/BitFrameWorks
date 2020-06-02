using BIT.Xpo.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BIT.Xpo.XPOWebApi.Server;

namespace BIT.Server.Controllers
{
    [Authenticate()]
    [Route("api/[controller]")]
    [ApiController]
    public class XPOWebApiController : XPOWebApiControllerBase
    {
        public XPOWebApiController(IDataStoreResolver DataStoreResolver) : base(DataStoreResolver)
        {

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
