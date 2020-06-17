using BIT.AspNetCore.Extensions;
using BIT.Data.Models;
using BIT.Data.Services;
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
        public LoginControllerBase(IConfigResolver<IDataStore> DataStoreResolver, IObjectSerializationService objectSerializationHelper, IStringSerializationService stringSerializationHelper) : base(DataStoreResolver, objectSerializationHelper, stringSerializationHelper)
        {
        }

       

       
    }
}
