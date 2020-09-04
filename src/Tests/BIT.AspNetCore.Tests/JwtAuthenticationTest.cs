using BIT.Data.Functions.RestClientNet;
using NUnit.Framework;
using RestClient.Net;
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
            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());
            var result = await client.GetAsync<string>("/AuthenticationTest");
            Assert.Pass();
        }
    }
}