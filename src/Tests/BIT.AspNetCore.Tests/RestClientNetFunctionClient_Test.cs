using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using NUnit.Framework;
using RestClient.Net;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BIT.AspNetCore.Tests
{
    public class RestClientNetFunctionClient_Test : BaseTest
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        public const string Token = "abcde";
        public const string Id = "001";
        public RestClientNetFunctionClient_Test()
        {

        }
        [SetUp]
        public override void Setup()
        {
            base.Setup();

        }
        [Test]
        public void Test1()
        {
            headers.Add(nameof(Token), Token);
            headers.Add(nameof(Id), Id);


            //https://christianfindlay.com/2020/05/15/c-delegates-with-ioc-containers-and-dependency-injection/

            var test = _testServerHttpClientFactory;

            //CreateHttpClient createHttpClient =new CreateHttpClient(CreateClientMethod);
            client = new Client(new NewtonsoftSerializationAdapter() , createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            //client = new Client(new NewtonsoftSerializationAdapter(), httpClientFactory: _testServerHttpClientFactory);
            //client = new Client(new NewtonsoftSerializationAdapter());


            //TODO check why the serialization does not work when the class DataResult inherits from dictionary

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, "http://localhost/HttpDataTransferTest", headers);
            IDataResult Result =  restFunctionClient.ExecuteFunction(new DataParameters() { MemberName = "NoErrors" });
            var ResultValue = simpleObjectSerializationHelper.GetObjectsFromByteArray<string>(Result.ResultValue);
            Assert.AreEqual("Hello Data Transfer", ResultValue);



        }

       
    }
}