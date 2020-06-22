using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.IO;
using XpoDemoOrm;

namespace BIT.Xpo.Tests
{
    //TODO Joche Ojeda: check that the test name are consistent 
    public class XpoInitializer_Test
    {
        private const string db1CnxConst = "XpoProvider=InitSchema;Data Source=db1.xml;Read Only=false";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void XpoInitializerUpdateSchema_ShouldPass()
        {


            XpoInitializer XpoInitializer = new XpoInitializer(db1CnxConst, typeof(Customer), typeof(Invoice));
            XpoInitializer.InitSchema();
            DevExpress.Xpo.UnitOfWork UnitOfWork = XpoInitializer.CreateUnitOfWork();
            Customer Joche = new Customer(UnitOfWork);
            if (UnitOfWork.InTransaction)
                UnitOfWork.CommitChanges();
            Assert.IsNotNull(Joche);

        }
 
    }
}