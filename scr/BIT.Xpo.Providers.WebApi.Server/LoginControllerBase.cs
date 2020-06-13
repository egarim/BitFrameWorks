using BIT.AspNetCore.Extensions;
using BIT.Data.Helpers;
using BIT.Data.Models;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace BIT.Xpo.Providers.WebApi.Server
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginControllerBase: XPOWebApiControllerBase
    {
        public LoginControllerBase(IConfigResolver<IDataStore> DataStoreResolver, IObjectSerializationHelper objectSerializationHelper, IStringSerializationHelper stringSerializationHelper) : base(DataStoreResolver, objectSerializationHelper, stringSerializationHelper)
        {
        }

       

       
    }
}
