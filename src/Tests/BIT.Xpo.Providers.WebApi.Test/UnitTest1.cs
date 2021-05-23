
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Shared;
using XpoDemoOrm;

namespace BIT.Xpo.Providers.WebApi.Test
{
    public class Tests: XpoWebApiBaseServerTest
    {
       
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void Test1()
        {
            //XPOWebApi.Register();
            //XPOWebApiHttp.Register();

            ////var Cnx = XPOWebApi.GetConnectionString(BaseServerTest.LocalBaseUriString, "/XpoWebApiTest", "", "002");
            //var Cnx = XPOWebApiHttp.GetConnectionString(BaseServerTest.LocalBaseUriString, "/XpoWebApiTest", "", "002");


            //const string RequestUri = BaseServerTest.LocalBaseUriString + "/XpoWebApiTest";
            //var resu=  await TEstClient.GetAsync(RequestUri);



            //var json = JsonConvert.SerializeObject("Hello");
            //var data = new StringContent(json, Encoding.UTF8, "application/json");

            ////var url = "https://httpbin.org/post";
            ////using var client = new HttpClient();

            //var response = await TEstClient.PostAsync(RequestUri+ "/SelectData", data);

            //HttpClient InternalClient = this._testServerHttpClientFactory.CreateClient("Test");
            //XpoInitializer xpoInitializer = new XpoInitializer(Cnx, typeof(Customer), typeof(Invoice));
            //xpoInitializer.InitXpo(new XPOWebApiHttp(BaseServerTest.LocalBaseUriString, "/XpoWebApiTest", DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema,"","002", InternalClient));


            //var UoW = xpoInitializer.CreateUnitOfWork();
            //Customer customer = new Customer(UoW);
            //customer.Name = "Jose Manuel Ojeda Melgar";
            //if (UoW.InTransaction)
            //    UoW.CommitChanges();


            ////var UoW2 = xpoInitializer.CreateUnitOfWork();
            //var CustomerFromDataStore = UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));
            //Assert.Pass();
        }
    }
}