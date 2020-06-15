using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using BIT.Data.Xpo;
using BIT.Xpo.Providers.Network.Client.RestClientNet;
using NUnit.Framework;
using RestClient.Net;
using System;
using System.Threading.Tasks;
using XpoDemoOrm;

namespace BIT.AspNetCore.Tests
{
    public class RestClientNetProvider_Test : BaseTest
    {

        public RestClientNetProvider_Test()
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


            RestClientNetProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), httpClientFactory: _testServerHttpClientFactory);


           
            //TODO check why the serialization does not work when the class DataResult inherits from dictionary

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, "http://localhost/WebApiHttpDataTransferControllerTest", null);


            var Cnx = RestClientNetProvider.GetConnectionString("http://localhost", "/WebApiHttpDataTransferController", "", "001");
            RestClientNetProvider restClientNetProvider = new RestClientNetProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

        

            XpoInitializer xpoInitializer = new XpoInitializer(Cnx, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitXpo(restClientNetProvider);

        }
    }
}