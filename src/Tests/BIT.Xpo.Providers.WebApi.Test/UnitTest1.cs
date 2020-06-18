using BIT.Data.Xpo;
using BIT.Xpo.Providers.WebApi.Client;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using NUnit.Framework;
using Test.Shared;
using XpoDemoOrm;

namespace BIT.Xpo.Providers.WebApi.Test
{
    public class Tests: BaseServerTest
    {
       
        public override void Setup()
        {
            base.Setup();
        }

        [Test]
        public void Test1()
        {
            XPOWebApi.Register();

            var Cnx = XPOWebApi.GetConnectionString("", "", "001","");

            XpoInitializer xpoInitializer = new XpoInitializer(Cnx, typeof(Customer), typeof(Invoice));
            xpoInitializer.InitXpo(XpoDefault.GetConnectionProvider(Cnx, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema));


            var UoW = xpoInitializer.CreateUnitOfWork();
            Customer customer = new Customer(UoW);
            customer.Name = "Jose Manuel Ojeda Melgar";
            if (UoW.InTransaction)
                UoW.CommitChanges();


            //var UoW2 = xpoInitializer.CreateUnitOfWork();
            var CustomerFromDataStore = UoW.FindObject<Customer>(new BinaryOperator(nameof(customer.Code), customer.Code));
            Assert.Pass();
        }
    }
}