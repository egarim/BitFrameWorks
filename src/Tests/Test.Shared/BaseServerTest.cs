using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestServer;

namespace Test.Shared
{
    public class BaseServerTest
    {
        protected Client client;
        
      
        
        [SetUp]
        public virtual void Setup()
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
        protected TestClientFactory _testServerHttpClientFactory;
        private static HttpClient MintClient()
        {
            return _testServer.CreateClient();
        }
        public const string LocalBaseUriString = "http://localhost:8080";
        
    }
}