using BIT.Data.Helpers;
using BIT.Data.Transfer;
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

namespace BIT.AspNetCore.Tests
{
    public class RestClientNet_Tests
    {
        Client client;
        Dictionary<string, string> headers = new Dictionary<string, string>();
        public const string Token = "abcde";
        public const string Id = "001";
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
            headers.Add(nameof(Token), Token);
            headers.Add(nameof(Id), Id);
            client = new Client(new NewtonsoftSerializationAdapter(), httpClientFactory: _testServerHttpClientFactory);

            //TODO check why the serialization does not work when the class DataResult inherits from dictionary

            SimpleObjectSerializationHelper simpleObjectSerializationHelper = new SimpleObjectSerializationHelper();
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, "http://localhost/HttpDataTransferTest",headers);
            IDataResult Result = await restFunctionClient.ExecuteFunction(new DataParameters() { MemberName = "NoErrors" });
            var ResultValue= simpleObjectSerializationHelper.GetObjectsFromByteArray<string>(Convert.FromBase64String(Result.ResultValue2));
            Assert.AreEqual("Hello Data Transfer", ResultValue);



        }
    }
}