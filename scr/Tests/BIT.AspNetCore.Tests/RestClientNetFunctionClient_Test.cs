using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using NUnit.Framework;
using RestClient.Net;
using System;
using System.Collections.Generic;
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
        public async Task Test1()
        {
            headers.Add(nameof(Token), Token);
            headers.Add(nameof(Id), Id);
            client = new Client(new NewtonsoftSerializationAdapter(), httpClientFactory: _testServerHttpClientFactory);

            //TODO check why the serialization does not work when the class DataResult inherits from dictionary

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, "http://localhost/HttpDataTransferTest", headers);
            IDataResult Result = await restFunctionClient.ExecuteFunction(new DataParameters() { MemberName = "NoErrors" });
            var ResultValue = simpleObjectSerializationHelper.GetObjectsFromByteArray<string>(Result.ResultValue);
            Assert.AreEqual("Hello Data Transfer", ResultValue);



        }
    }
}