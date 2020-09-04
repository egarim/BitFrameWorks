using BIT.Data.Functions.RestClientNet;
using BIT.Data.Services;
using NUnit.Framework;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Shared;

namespace BIT.AspNetCore.Tests
{
    public class JwtAuthenticationTest: JwtAuthorizationTestBase
    {
     

        [Test]
        public async Task Test1()
        {


            JwtService service = new JwtService("9hxFcrQah802faqJj53VveXz27Aex03vA4DboR8xP1unQMNDi0iv92i0yBlQG6QnWGY0Y2o9qJpDpY6r6HNkjFgJGGnP5X1M8Lu846R6c40fbg7iiIf4h1GDQmCGM8Qa"
              , "Xari");

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "db1");
            Headers.Add("Authorization", StringToken);
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri("http://localhost:8080/AuthenticationTest");
            var result = await client.GetAsync<string>(resource, Headers);
            Assert.AreEqual("It's working", result.Body);
        }
    }
}