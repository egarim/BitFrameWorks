using BIT.Data.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace BIT.AspNetCore.Extensions
{
    public static class JwtHttpRequestExtensions
    {

        public static JwtPayload GetJwtPayload(this IJwtService IJwtService, HttpContext Context)
        {

            var Token = Context.Request.Headers["Authorization"];
            return IJwtService.TokenToJwtPayload(Token);
        }
    }
}