using BIT.Data.Services;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace JwtTokenGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("please enter the name of the token issuer");
            string Issuer = Console.ReadLine();
            JwtService service = new JwtService();

            Console.WriteLine("Your key:" + System.Environment.NewLine);
            //in production you should not generate a random key but use a fixed key
            var Key = service.GenerateKey(128);
            Console.WriteLine(Key + System.Environment.NewLine);

            //List of standard Payload claims https://en.wikipedia.org/wiki/JSON_Web_Token#Standard_fields

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                { JwtRegisteredClaimNames.Iss, Issuer },
            };

            var StringToken = service.JwtPayloadToToken(Key, InitialPayload);
            Console.WriteLine("Your token:"+System.Environment.NewLine);
            Console.WriteLine(StringToken + System.Environment.NewLine);
            Console.WriteLine("Press any key to finish the program");
            Console.ReadKey();

        }
    }
}
