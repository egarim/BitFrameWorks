using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using BIT.Data.Services;

namespace BIT.AspNetCore.Services
{
    public class JwtTokenService:ITokenService<JwtPayload>
    {
        string TokenHeader;
        IJwtService JwtService;
        public JwtTokenService(string TokenHeader,IJwtService JwtService)
        {
            this.TokenHeader = TokenHeader;
            this.JwtService = JwtService;
        }

        public JwtPayload GetToken(HttpRequest request)
        {
            var Token = request.Headers[TokenHeader];
            var StringToken = Token.ToString().Replace("Bearer ", string.Empty);
            return this.JwtService.TokenToJwtPayload(StringToken);
        }
    }

   
}