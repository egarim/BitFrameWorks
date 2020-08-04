using BIT.Data.DataTransfer;
using BIT.Data.Services;
using BIT.Data.Transfer.RestClientNet;
using BIT.Xpo;
using BIT.Xpo.Providers.WebApi.Client;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NUnit.Framework;
using RestClient.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using XpoDemoOrm;

namespace BIT.AspNetCore.Tests
{
    public class XpoWebApiProvider_Test : BaseTest
    {
        private const string Controller = "/WebApiHttpDataTransferController";
        private const string Url = "http://localhost/WebApiHttpDataTransferControllerTest";

        public XpoWebApiProvider_Test()
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

            //HACK ToImplmement 001
            XpoWebApiProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

          

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(XpoWebApiProvider.TokenPart, "");
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "001");
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, Url, Headers);


            var Cnx = XpoWebApiProvider.GetConnectionString("http://localhost", Controller, "", "001");
            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);



            XpoInitializer xpoInitializer = new XpoInitializer(restClientNetProvider, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer customer = new Customer(UoW);
            customer.Name = "Jose Manuel Ojeda Melgar";
            if (UoW.InTransaction)
                UoW.CommitChanges();


            //var UoW2 = xpoInitializer.CreateUnitOfWork();
            var CustomerFromDataStore= UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));

            Assert.AreEqual(customer.Name, CustomerFromDataStore.Name);

        }
        [Test]
        public void CreateCollectionTest()
        {

            //HACK ToImplmement 001
            XpoWebApiProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());



            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(XpoWebApiProvider.TokenPart, "");
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "001");
            RestClientNetFunctionClient restFunctionClient = new RestClientNetFunctionClient(client, Url, Headers);


            var Cnx = XpoWebApiProvider.GetConnectionString("http://localhost", Controller, "", "001");
            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);



            XpoInitializer xpoInitializer = new XpoInitializer(Cnx, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer JoseManuel = new Customer(UoW);
            JoseManuel.Name = "Jose Manuel Ojeda Melgar";

            Customer OscarOjeda = new Customer(UoW);
            OscarOjeda.Name = "Oscar Ojeda Melgar";

            if (UoW.InTransaction)
                UoW.CommitChanges();


            //var UoW2 = xpoInitializer.CreateUnitOfWork();
            //var CustomerFromDataStore = UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));

            var people = new XPCollection<Customer>(UoW).ToList();

            Assert.IsNotNull(people);

        }
    }
}