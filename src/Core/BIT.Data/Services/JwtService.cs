using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BIT.Data.Services
{
    public class JwtService : IJwtService
    {
        public virtual string GenerateKey(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        protected string Key;
        protected string Issuer;
        public JwtService(string Key,string Issuer)
        {
            this.Key = Key;
            this.Issuer = Issuer;
        }
        public virtual bool VerifyToken(string token)
        {
            var validationParameters = new TokenValidationParameters()
;
            validationParameters.ValidIssuer = this.Issuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
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

        public virtual JwtPayload TokenToJwtPayload(string StringToken)
        {
            var Handler = new JwtSecurityTokenHandler();

            var Token = Handler.ReadJwtToken(StringToken);

            JwtPayload Payload = Token.Payload;
            return Payload;
        }

    

        public virtual string JwtPayloadToToken(JwtPayload Payload)
        {
            
            var ByteLenght = System.Text.ASCIIEncoding.Unicode.GetByteCount(this.Key);
            if (ByteLenght < 256)
            {
                throw new ArgumentException($"the byte count of the key should be greater than 256 bytes,the current length is:{ByteLenght}");
            }

            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));

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

        public long DateToNumber(DateTime dateTime)
        {
             DateTimeOffset dto = new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, TimeSpan.Zero);
            return dto.ToUnixTimeSeconds();
        }

        public DateTime NumberToDate(long date)
        {

            return DateTimeOffset.FromUnixTimeSeconds(date).DateTime;
        }
    }
}