using BIT.Data.Transfer;
using BIT.Data.Transfer.RestClientNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TestServer;

namespace BIT.AspNetCore.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            _testServerHttpClientFactory = GetTestClientFactory();
        }
        private static Microsoft.AspNetCore.TestHost.TestServer _testServer;
        public static TestClientFactory GetTestClientFactory()
        {
            if (_testServer == null)
            {
                var hostBuilder = new WebHostBuilder();
                hostBuilder.UseStartup<Startup>();
                _testServer = new Microsoft.AspNetCore.TestHost.TestServer(hostBuilder);
            }

            var testClient = MintClient();
            var testServerHttpClientFactory = new TestClientFactory(testClient);
            return testServerHttpClientFactory;
        }
        private static TestClientFactory _testServerHttpClientFactory;
        private static HttpClient MintClient()
        {
            return _testServer.CreateClient();
        }
        public const string LocalBaseUriString = "http://localhost";
        [Test]
        public async Task Test1()
        {

            var client = new Client(new NewtonsoftSerializationAdapter(), httpClientFactory: _testServerHttpClientFactory);
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, "http://localhost/HttpDataTransferTest");
            var Result = await restFunctionClient.ExecuteFunction(new DataParameters() { MemberName = "Test" });


        }
    }
}