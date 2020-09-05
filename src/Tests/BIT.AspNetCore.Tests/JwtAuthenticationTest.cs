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
    public class JwtAuthenticationTest : JwtAuthorizationTestBase
    {

        const string ValidKey = "9hxFcrQah802faqJj53VveXz27Aex03vA4DboR8xP1unQMNDi0iv92i0yBlQG6QnWGY0Y2o9qJpDpY6r6HNkjFgJGGnP5X1M8Lu846R6c40fbg7iiIf4h1GDQmCGM8Qa";
        const string InvalidKey = "1hxFcrQah802faqJj53VveXz27Aex03vA4DboR8xP1unQMNDi0iv92i0yBlQG6QnWGY0Y2o9qJpDpY6r6HNkjFgJGGnP5X1M8Lu846R6c40fbg7iiIf4h1GDQmCGM8Qa";
        private const string UriString = "http://localhost:8080/AuthenticationTest/";

        [Test]
        public async Task SendValidToken()
        {



            JwtService service = new JwtService(ValidKey
              , "Xari");

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "db1");
            Headers.Add("Authorization", StringToken);
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri(UriString);
            var result = await client.GetAsync<string>(resource, Headers);
            Assert.AreEqual("It's working", result.Body);
        }
        [Test]
        public void SendInvalidToken()
        {



            JwtService service = new JwtService(InvalidKey
              , "Xari");

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "db1");
            Headers.Add("Authorization", StringToken);
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri(UriString);


            string result = string.Empty;

            Assert.Throws<HttpStatusException>(
             () =>
             {
                 try
                 {
                     result = client.GetAsync<string>(resource, Headers).Result;
                 }
                 catch (Exception ex)
                 {

                     throw ex.InnerException;
                 }

             });
        }
        [Test]
        public void MissingAuthId()
        {
            JwtService service = new JwtService(InvalidKey
              , "Xari");

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid ", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "");
            Headers.Add("Authorization", StringToken);
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri("http://localhost:8080/AuthenticationTest");

            string result = string.Empty;

             Assert.Throws<HttpStatusException>(
             () =>
             {
                 try
                 {
                     result = client.GetAsync<string>(resource, Headers).Result;
                 }
                 catch (Exception ex)
                 {

                     throw ex.InnerException;
                 }

             });
        }

        [Test]
        public void MissingToken()
        {
            JwtService service = new JwtService(InvalidKey
              , "Xari");

            JwtPayload InitialPayload;
            InitialPayload = new JwtPayload {
                { "UserOid", "001" },
                { JwtRegisteredClaimNames.Iat, service.DateToNumber(DateTime.Now).ToString() },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "db1");
            Headers.Add("Authorization", "");
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri(UriString);

            string result = string.Empty;

            Assert.Throws<SendException<object>>(
            () =>
            {
                try
                {
                    result = client.GetAsync<string>(resource, Headers).Result;
                }
                catch (Exception ex)
                {
                    var type = ex.InnerException.GetType();
                    throw ex.InnerException;
                }

            });
        }
        [Test]
        public async Task ValidateValuesFromAToken()
        {



            JwtService service = new JwtService(ValidKey
              , "Xari");

            JwtPayload InitialPayload;
            string Date = service.DateToNumber(DateTime.Now).ToString();
            InitialPayload = new JwtPayload {
                { "UserOid", "001" },
                { JwtRegisteredClaimNames.Iat, Date },
                  { JwtRegisteredClaimNames.Iss,"Xari" },
            };

            var StringToken = service.JwtPayloadToToken(InitialPayload);

            var Headers = new RequestHeadersCollection();
            Headers.Add("AuthId", "db1");
            Headers.Add("Authorization", StringToken);
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            Uri resource = new Uri($"{UriString}GetTokenValues");
            var result = await client.GetAsync<bool>(resource, Headers);
            Assert.AreEqual(true, result.Body);
        }
    }
}