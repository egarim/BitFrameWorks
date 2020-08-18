using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Linq;
using System.IO;
using XpoDemoOrm;
using DevExpress.Data.Filtering;

namespace BIT.Xpo.Tests
{
    //TODO Joche Ojeda: check that the test name are consistent 
    public class XpoInitializerResolver_Test 
    {
        private const string ConfiguratioName = "XpoInitializerResolver_appsettings.json";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetById_db1NotNull_ShouldPass()
        {


            XpoDataStoreResolver DataStoreResolver =
                new XpoDataStoreResolver(ConfiguratioName);


            XpoInitializerResolver xpoInitializerResolver = new XpoInitializerResolver(DataStoreResolver,typeof(Invoice),typeof(Customer));

            IXpoInitializer xpoinitializer = xpoInitializerResolver.GetById("db1");


            Assert.IsNotNull(xpoinitializer);

        }
        [Test]
        public void GetById_db1NotNull_ShouldPassA()
        {


            XpoDataStoreResolver DataStoreResolver =
                new XpoDataStoreResolver(ConfiguratioName);


            XpoInitializerResolver xpoInitializerResolver = new XpoInitializerResolver(DataStoreResolver, typeof(Invoice), typeof(Customer));

            IXpoInitializer FirstInitializer = xpoInitializerResolver.GetById("db1");

            var FirstUoW = FirstInitializer.CreateUnitOfWork();



            Customer FirstPerson = new Customer(FirstUoW);
            FirstPerson.Name = "Jose Manuel Ojeda Melgar";
            if(FirstUoW.InTransaction)
                FirstUoW.CommitChanges();


            IXpoInitializer SecondInitializer = xpoInitializerResolver.GetById("db1");
            var SecondUoW = SecondInitializer.CreateUnitOfWork();

            var SecondPerson = SecondUoW.FindObject<Customer>(new BinaryOperator(nameof(Customer.Name), FirstPerson.Name));






            Assert.AreEqual(FirstPerson.Name,SecondPerson.Name);

        }
        [Test]
        public void GetById_ArgumentException_ShouldFail()
        {
            XpoDataStoreResolver DataStoreResolver =
            new XpoDataStoreResolver(ConfiguratioName);


            XpoInitializerResolver xpoInitializerResolver = new XpoInitializerResolver(DataStoreResolver, typeof(Invoice), typeof(Customer));

            IXpoInitializer xpoinitializer = xpoInitializerResolver.GetById("db1");


            Assert.Throws<ArgumentException>(() =>
                    xpoInitializerResolver.GetById("db99")
            );


        }
        [Test]
        public void ResolveRuntime_ShouldPass()
        {
            XpoDataStoreResolver DataStoreResolver =
            new XpoDataStoreResolver(ConfiguratioName);


            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile(ConfiguratioName);
            builder.Build();


            var NewConfig = File.ReadAllText("NewConnectionString.json");
            File.WriteAllText(ConfiguratioName, NewConfig);



            XpoInitializerResolver xpoInitializerResolver = new XpoInitializerResolver(DataStoreResolver, typeof(Invoice), typeof(Customer));

            var db99  = xpoInitializerResolver.GetById("db99");

            
            Assert.IsNotNull(db99);


        }
    }
}