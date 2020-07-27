using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.IO;
using XpoDemoOrm;
using DevExpress.Xpo;

namespace BIT.Xpo.Tests
{
    //TODO Joche Ojeda: check that the test name are consistent 
    public class XpoInitializer_Test
    {
        //private const string db1CnxConst = "XpoProvider=InitSchema;Data Source=db1.xml;Read Only=false";
        string GetConnectionString(string DatabaseName)
        {
            return $"XpoProvider=InMemoryDataStore;Data Source={DatabaseName}.xml;Read Only=false";
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void XpoInitializerUpdateSchema_ShouldPass()
        {


            XpoInitializer XpoInitializer = new XpoInitializer(this.GetConnectionString("UpdateSchema"), typeof(Customer), typeof(Invoice));
            XpoInitializer.InitSchema();
            DevExpress.Xpo.UnitOfWork UnitOfWork = XpoInitializer.CreateUnitOfWork();
            Customer Joche = new Customer(UnitOfWork);
            if (UnitOfWork.InTransaction)
                UnitOfWork.CommitChanges();
            Assert.IsNotNull(Joche);

        }
        [Test]
        public void XpoInitializerCreateThreadSafeDal_ShouldPass()
        {


            XpoInitializer XpoInitializer = new XpoInitializer(this.GetConnectionString("ThreadSafe"), DataLayerType.ThreadSafe, typeof(Customer), typeof(Invoice));

            XpoInitializer.InitSchema();
            DevExpress.Xpo.UnitOfWork UnitOfWork = XpoInitializer.CreateUnitOfWork();


            Assert.IsTrue(UnitOfWork.DataLayer.GetType() == typeof(ThreadSafeDataLayer));

        }
        [Test]
        public void XpoInitializerCreateSimpleDal_ShouldPass()
        {


            XpoInitializer XpoInitializer = new XpoInitializer(this.GetConnectionString("SimpleDal"), DataLayerType.Simple, typeof(Customer), typeof(Invoice));

            XpoInitializer.InitSchema();
            DevExpress.Xpo.UnitOfWork UnitOfWork = XpoInitializer.CreateUnitOfWork();


            Assert.IsTrue(UnitOfWork.DataLayer.GetType() == typeof(SimpleDataLayer));

        }
        //TODO test the secondary overloads of the XpoInitializer constructor


    }
}