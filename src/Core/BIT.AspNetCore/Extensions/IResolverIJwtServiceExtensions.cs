using BIT.Data.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace BIT.AspNetCore.Extensions
{
    public static class IResolverIJwtServiceExtensions
    {

        public static JwtPayload GetJwtPayload(this IResolver<IJwtService> Resolver, HttpContext Context)
        {
            var AuthId = Context.Request.Headers["AuthId"];
            var Token = Context.Request.Headers["Authorization"];
            return Resolver.GetById(AuthId).TokenToJwtPayload(Token);
        }
    }
}