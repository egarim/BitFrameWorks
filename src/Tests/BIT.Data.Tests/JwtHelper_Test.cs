
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
        AppSettingsDictionaryResolverBase _configurationResolverBase;
        [SetUp]
        public void Setup()
        {
            _configurationResolverBase = new AppSettingsDictionaryResolverBase("JwtSettings.json", "Tokens");
        }

        [Test]
        public void GenerateValidToken_ShouldPass()
        {
            var Dictionary=  _configurationResolverBase.GetById("db1");

            JwtService service = new JwtService(Dictionary["Key"], Dictionary["ValidIssuer"]);
          
            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss, Dictionary["ValidIssuer"] },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);
            Debug.WriteLine(string.Format("{0}:{1}", "Token", StringToken));


            Assert.True(service.VerifyToken(StringToken));

        }
        [Test]
        public void CompareTokens_ShouldPass()
        {

            var Dictionary = _configurationResolverBase.GetById("db1");

            JwtService service = new JwtService(Dictionary["Key"], Dictionary["ValidIssuer"]);

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss, Dictionary["ValidIssuer"] },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);
            Debug.WriteLine(string.Format("{0}:{1}", "Token", StringToken));


            var PayloadFromValidation = service.TokenToJwtPayload(StringToken);

            Assert.AreEqual(InitialPayload.SerializeToJson(), PayloadFromValidation.SerializeToJson());

        }
        [Test]
        public void GenerateTokenWithInvalidKey_ShouldFail()
        {


            var Dictionary = _configurationResolverBase.GetById("db1");

            JwtService service = new JwtService("InvalidKey", Dictionary["ValidIssuer"]);

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss, Dictionary["ValidIssuer"] },
            };

       

            Assert.Throws<ArgumentException>(() =>

            service.JwtPayloadToToken(InitialPayload)

            );



        }
    }
}