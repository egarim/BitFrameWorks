using BIT.Data.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BIT.Jwt
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //in production you should not generate a random key but use a fixed key
            var Key = JwtHelper.GenerateKey(128);
            Debug.WriteLine(string.Format("{0}:{1}", "Key", Key));

            //List of standard Payload claims https://en.wikipedia.org/wiki/JSON_Web_Token#Standard_fields

            const string Issuer = "Jose Manuel Ojeda";

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, JwtHelper.ConvertToUnixTime(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss, Issuer },
            };

            var StringToken = JwtHelper.GenerateToken(Key, InitialPayload);
            Debug.WriteLine(string.Format("{0}:{1}", "Token", StringToken));

            if (!JwtHelper.VerifyToken(StringToken, Key, Issuer))
            {
                Console.WriteLine("Token is invalid");
            }
            else
            {
                Console.WriteLine("Token is valid");
            }
            var PayloadFromValidation = JwtHelper.ReadToken(StringToken);

            if (PayloadFromValidation.SerializeToJson() == InitialPayload.SerializeToJson())
            {
                Console.WriteLine("Payloads are equal");
                Console.WriteLine(string.Format("{0}:{1}", "PayloadFromValidation", PayloadFromValidation.SerializeToJson()));
            }
            else
            {
                Console.WriteLine("Payloads are NOT equal");
            }
            Console.WriteLine("Press any key to finish");
            Console.ReadLine();
        }
    }
    
}