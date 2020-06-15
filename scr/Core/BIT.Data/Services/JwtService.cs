using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace BIT.Data.Services
{
    public static class JwtService
    {
        public static string GenerateKey(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool VerifyToken(string token, string key, string Issuer)
        {
            var validationParameters = new TokenValidationParameters()
;
            validationParameters.ValidIssuer = Issuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            validationParameters.ValidateLifetime = false;
            validationParameters.ValidateAudience = false;
            validationParameters.ValidateIssuer = true;
            validationParameters.ValidateIssuerSigningKey = true;

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch (SecurityTokenException Se)
            {
                Console.WriteLine(Se.ToString()); //something else happened
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); //something else happened
                //throw;
                return false;
            }
            //... manual validations return false if anything untoward is discovered
            return validatedToken != null;
        }

        public static JwtPayload ReadToken(string StringToken)
        {
            var Handler = new JwtSecurityTokenHandler();

            var Token = Handler.ReadJwtToken(StringToken);

            JwtPayload Payload = Token.Payload;
            return Payload;
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        public static string GenerateToken(string key, JwtPayload Payload)
        {
            var StringLenght = key.Length;
            var ByteLenght = System.Text.ASCIIEncoding.Unicode.GetByteCount(key);
            if (ByteLenght < 256)
            {
                throw new ArgumentException($"the byte count of the key should be greater than 256 bytes,the current length is:{ByteLenght}");
            }

            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // securityKey length MUST be >256 based on the length of the key
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            // Lets create the token
            var header = new JwtHeader(credentials);

            // Lets add the header and payload
            JwtSecurityToken SecurityToken = new JwtSecurityToken(header, Payload);

            var Handler = new JwtSecurityTokenHandler();

            // Convert the token to string and send it to your client
            string TokenString = Handler.WriteToken(SecurityToken);
            return TokenString;
        }
    }
}