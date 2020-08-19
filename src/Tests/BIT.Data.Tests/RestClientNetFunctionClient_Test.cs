
using BIT.Data.Functions;
using BIT.Data.Functions.RestClientNet;
using BIT.Data.Services;
using NUnit.Framework;
using RestClient.Net;
using System.Collections.Generic;
using System.Net.Http;
using Test.Shared;

namespace BIT.AspNetCore.Tests
{
    public class RestClientNetFunctionClient_Test : XpoWebApiBaseServerTest
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

           


            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());
        



            //TODO check why the serialization does not work when the class DataResult inherits from dictionary

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            ApiFunction restFunctionClient = new ApiFunction(client, "http://localhost/HttpDataTransferTest", headers);
            IDataResult Result =  restFunctionClient.ExecuteFunction(new DataParameters() { MemberName = "NoErrors" });
            var ResultValue = simpleObjectSerializationHelper.GetObjectsFromByteArray<string>(Result.ResultValue);
            Assert.AreEqual("Hello Data Transfer", ResultValue);



        }

       
    }
}