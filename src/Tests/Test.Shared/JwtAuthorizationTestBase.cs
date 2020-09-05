using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using RestClient.Net;
using System;
using System.Net.Http;
using TestServer;

namespace Test.Shared
{
    public class JwtAuthorizationTestBase
    {
        protected Client client;



        [SetUp]
        public virtual void Setup()
        {
            _testServerHttpClientFactory = GetTestClientFactory(typeof(StartupJwt));



        }
        private static Microsoft.AspNetCore.TestHost.TestServer _testServer;
        public static TestClientFactory GetTestClientFactory(Type startupType)
        {
            if (_testServer == null)
            {
                var hostBuilder = new WebHostBuilder();
                hostBuilder.UseStartup(startupType);
                _testServer = new Microsoft.AspNetCore.TestHost.TestServer(hostBuilder);
            }

            var testClient = MintClient();
            var testServerHttpClientFactory = new TestClientFactory(testClient);
            return testServerHttpClientFactory;
        }
        protected TestClientFactory _testServerHttpClientFactory;
        private static HttpClient MintClient()
        {
            return _testServer.CreateClient();
        }
        public const string LocalBaseUriString = "http://localhost:8080";

    }
}
