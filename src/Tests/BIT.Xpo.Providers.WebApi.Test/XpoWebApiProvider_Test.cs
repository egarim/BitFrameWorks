using BIT.Data.Functions.RestClientNet;
using BIT.Data.Services;
using BIT.Xpo;
using BIT.Xpo.Providers.WebApi.Client;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NUnit.Framework;
using RestClient.Net;
using RestClient.Net.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Test.Shared;
using TestServer;
using XpoDemoOrm;

namespace BIT.AspNetCore.Tests
{
    public class XpoWebApiWithDal_Test: XpoWebApiBaseServerTest
    {
        private const string Url = "http://localhost/WebApiHttpDataTransferControllerTest";
        private const string UrlGet = "http://localhost/XpoWebApiWithDal";
        public XpoWebApiWithDal_Test()
        {
            
        }
        [SetUp]
        public override void Setup()
        {
            _testServerHttpClientFactory = GetXpoWebApiTestClientFactory(typeof(StartupXpoWebApiWithDal));

        }
        [Test]
        public async Task CreateAndReadRecordTest()
        {


            XpoWebApiProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());



            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(XpoWebApiProvider.TokenPart, "");
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "001");
            ApiFunction restFunctionClient = new ApiFunction(client, Url, Headers);


         
            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);



            XpoInitializer xpoInitializer = new XpoInitializer(restClientNetProvider, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer customer = new Customer(UoW);
            customer.Name = "Jose Manuel Ojeda Melgar";
            if (UoW.InTransaction)
                UoW.CommitChanges();



            var CustomerFromDataStore = UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));
            Response<int> count = await client.GetAsync<int>(new Uri(UrlGet + "/?DataStoreId=001", UriKind.Absolute));

            Assert.AreEqual(count.Body,1);
            Assert.AreEqual(customer.Name, CustomerFromDataStore.Name);


          
        }
    }
    public class XpoWebApiProvider_Test : XpoWebApiBaseServerTest
    {
        //for these test the provider should be created manually with the constructor that takes the IFucntion as first parameter otherwise is imposible to connect to the test controller
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
        public void CreateAndReadRecordTest()
        {

           
            XpoWebApiProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

          

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(XpoWebApiProvider.TokenPart, "");
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "001");
            ApiFunction restFunctionClient = new ApiFunction(client, Url, Headers);


            //var Cnx = XpoWebApiProvider.GetConnectionString("http://localhost", Controller, "", "001");
            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);



            XpoInitializer xpoInitializer = new XpoInitializer(restClientNetProvider, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer customer = new Customer(UoW);
            customer.Name = "Jose Manuel Ojeda Melgar";
            if (UoW.InTransaction)
                UoW.CommitChanges();


          
            var CustomerFromDataStore= UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));

            Assert.AreEqual(customer.Name, CustomerFromDataStore.Name);

        }
       
        [Test]
        public void ICommandChannelTest()
        {

       
            XpoWebApiProvider.Register();

            client = new Client(new NewtonsoftSerializationAdapter(), createHttpClient: (name) => _testServerHttpClientFactory.CreateClient());

            if (File.Exists("db011.db"))
                File.Delete("db011.db");

            SimpleObjectSerializationService simpleObjectSerializationHelper = new SimpleObjectSerializationService();
            Dictionary<string, string> Headers = new Dictionary<string, string>();
            Headers.Add(XpoWebApiProvider.TokenPart, "");
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "011");
            ApiFunction restFunctionClient = new ApiFunction(client, Url, Headers);

        
            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);



            XpoInitializer xpoInitializer = new XpoInitializer(restClientNetProvider, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer customer = new Customer(UoW);
            customer.Name = "Jose Manuel Ojeda Melgar";
            if (UoW.InTransaction)
                UoW.CommitChanges();


          
            var CustomerFromDataStore = UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));

            var ExecuteQueryData = UoW.ExecuteQuery("Select * from customer");

            var ExecuteScalarData = UoW.ExecuteScalar("Select * from customer");

            //
            var ExecuteNonQueryData = UoW.ExecuteNonQuery($"UPDATE customer SET Name = 'Joche Ojeda' WHERE Name = '{customer.Name}';");

            Assert.NotNull(CustomerFromDataStore);
            Assert.NotNull(ExecuteQueryData);
            Assert.NotNull(ExecuteScalarData);
            Assert.AreEqual(1,ExecuteNonQueryData);




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
            Headers.Add(XpoWebApiProvider.DataStoreIdPart, "002");
            ApiFunction restFunctionClient = new ApiFunction(client, Url, Headers);

            XpoWebApiProvider restClientNetProvider = new XpoWebApiProvider(restFunctionClient, new SimpleObjectSerializationService(), DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

            XpoInitializer xpoInitializer = new XpoInitializer(restClientNetProvider, typeof(Customer), typeof(Invoice));

            xpoInitializer.InitSchema();


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer JoseManuel = new Customer(UoW);
            JoseManuel.Name = "Jose Manuel Ojeda Melgar";

            Customer OscarOjeda = new Customer(UoW);
            OscarOjeda.Name = "Oscar Ojeda Melgar";

            if (UoW.InTransaction)
                UoW.CommitChanges();

            var people = new XPCollection<Customer>(UoW).ToList();

            Assert.IsNotNull(people);

        }
    }
}