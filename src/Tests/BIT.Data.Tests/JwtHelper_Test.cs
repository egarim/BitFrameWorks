
using BIT.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BIT.Data.Tests
{


    public class JwtHelper_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GenerateValidToken_ShouldPass()
        {
            //in production you should not generate a random key but use a fixed key
            var Key = JwtService.GenerateKey(128);
            Debug.WriteLine(string.Format("{0}:{1}", "Key", Key));

            //List of standard Payload claims https://en.wikipedia.org/wiki/JSON_Web_Token#Standard_fields

            const string Issuer = "Jose Manuel Ojeda";

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, JwtService.ConvertToUnixTime(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss, Issuer },
            };

            var StringToken = JwtService.GenerateToken(Key, InitialPayload);
            Debug.WriteLine(string.Format("{0}:{1}", "Token", StringToken));


            Assert.True(JwtService.VerifyToken(StringToken, Key, Issuer));

        }
        [Test]
        public void CompareTokens_ShouldPass()
        {
            //in production you should not generate a random key but use a fixed key
            var Key = JwtService.GenerateKey(128);
            Debug.WriteLine(string.Format("{0}:{1}", "Key", Key));

            //List of standard Payload claims https://en.wikipedia.org/wiki/JSON_Web_Token#Standard_fields

            const string Issuer = "Jose Manuel Ojeda";

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                        { "UserOid ", "001" },
                        { JwtRegisteredClaimNames.Iat, JwtService.ConvertToUnixTime(DateTime.Now).ToString() },
                          { JwtRegisteredClaimNames.Iss, Issuer },
                    };

            var StringToken = JwtService.GenerateToken(Key, InitialPayload);
            Debug.WriteLine(string.Format("{0}:{1}", "Token", StringToken));


            var PayloadFromValidation = JwtService.ReadToken(StringToken);

            Assert.AreEqual(InitialPayload.SerializeToJson(), PayloadFromValidation.SerializeToJson());

        }
        [Test]
        public void GenerateTokenWithInvalidKey_ShouldFail()
        {
            //in production you should not generate a random key but use a fixed key
            var Key = "abcd";




            const string Issuer = "Jose Manuel Ojeda";

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload 
            {
                        { "UserOid ", "001" },
                        { JwtRegisteredClaimNames.Iat, JwtService.ConvertToUnixTime(DateTime.Now).ToString() },
                          { JwtRegisteredClaimNames.Iss, Issuer },
            };

            Assert.Throws<ArgumentException>(() =>
            JwtService.GenerateToken(Key, InitialPayload));



        }
    }
}