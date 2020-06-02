using BIT.Data.Helpers;
using BIT.Data.Models;
using BIT.Xpo.AspNetCore;
using DevExpress.Xpo;
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
      
     
        public LoginControllerBase(IDataStoreResolver DataStoreResolver) : base(DataStoreResolver)
        {
            
        }
        
        protected virtual ApiAuthenticationResult Authenticate(UnitOfWork UoW,LoginParameters LoginParameters)
        {

            throw new NotImplementedException();
        }
       
        protected virtual LoginResult BuildLoginResult(string Key,string Issuer, ApiAuthenticationResult AuthenticationResult)
        {
            LoginResult LoginResult = new LoginResult();
            LoginResult.Authenticated = AuthenticationResult.Authenticated;
            LoginResult.Username = AuthenticationResult.Username;
            LoginResult.UserId = AuthenticationResult.UserId;
            LoginResult.LastError = AuthenticationResult.LastError;
            //System.IdentityModel.Tokens.Jwt;
            List<Claim> Claims = new List<Claim>();
         
            if(AuthenticationResult != null)
            {
                foreach (KeyValuePair<string, object> Parameter in AuthenticationResult)
                {
                    Claims.Add(new Claim(Parameter.Key, Parameter.Value?.ToString()));
                }
            }
            if(AuthenticationResult.Authenticated)
            {
                Claims.Add(new Claim(JwtRegisteredClaimNames.Iat, JwtHelper.ConvertToUnixTime(DateTime.Now).ToString()));
                Claims.Add(new Claim(JwtRegisteredClaimNames.Iss, Issuer));
                JwtPayload InitialPayload = new JwtPayload(Claims);
                LoginResult.Token = JwtHelper.GenerateToken(Key, InitialPayload);
            }
            else
            {
                LoginResult.Token = string.Empty;
            }
            //JwtPayload InitialPayload=new JwtPayload(Claims);
            //LoginResult.Token= JwtHelper.GenerateToken(Key, InitialPayload);
            return LoginResult;

        }
        [HttpPost]
        [Route("[action]")]
        public virtual async Task<IActionResult> Login()
        {


            byte[] Bytes = null;
            try
            {
                Bytes = await Request.GetRawBodyBytesAsync();

                var LoginParametersJsonString = Utilities.GetObjectsFromByteArray<string>(Bytes);

                var LoginParameters = JsonConvert.DeserializeObject<LoginParameters>(LoginParametersJsonString);
                string dataStoreId = GetHeader(DataStoreIdHeader);
                var UoW = this._Resolver.GetUnitOfWork(dataStoreId);


               

                IConfigurationRoot Configuration =GetConfigurationBuilder().Build();
              
                var Key = Configuration["Token:Key"];
                var Issuer = Configuration["Token:Issuer"];

                var AuthenticationResult = Authenticate(UoW, LoginParameters);
                var data = BuildLoginResult(Key, Issuer, AuthenticationResult);

                //byte[] value = null;
                //try
                //{
                //    value = Utilities.ToByteArray(data);
                //}
                //catch (Exception ex)
                //{

                //    var message = ex.Message;
                //}

                //return base.Ok(value);

                return base.Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }


        }

       
    }
}
