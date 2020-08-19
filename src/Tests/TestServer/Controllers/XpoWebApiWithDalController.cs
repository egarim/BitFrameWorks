using BIT.Data.Services;
using BIT.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevExpress.Xpo;
using XpoDemoOrm;

namespace TestServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class XpoWebApiWithDalController : ControllerBase
    {
        IResolver<IXpoInitializer> _XpoInitializerResolver;
        public XpoWebApiWithDalController(IResolver<IXpoInitializer> XpoInitializerResolver)
        {
            _XpoInitializerResolver = XpoInitializerResolver;
        }
        [HttpGet]
        public int Get()
        {

            string DataStoreId = HttpContext.Request.Query["DataStoreId"].ToString();
            var XpoInitializer =  _XpoInitializerResolver.GetById(DataStoreId);


            return XpoInitializer.CreateUnitOfWork().Query<Customer>().Count();

            //return -1;
        }
    }
}
