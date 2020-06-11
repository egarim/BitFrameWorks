using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using BIT.Xpo;
using BIT.Data.Helpers;
using System.Linq;
using System.IO;

namespace BIT.Xpo.Tests
{
    //TODO Joche Ojeda: check that the test name are consistent 
    public class XpoDataStoreResolver_Test
    {
        private const string db1CnxConst = "XpoProvider=InMemoryDataStore;Data Source=db1.xml;Read Only=false";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void XpoDataStoreResolver_GetById_db1NotNull_ShouldPass()
        {


            XpoDataStoreResolver configurationResolverBase =
                new XpoDataStoreResolver("appsettings.json");

            var db1 = configurationResolverBase.GetById("db1") as InMemoryDataStore;
            Assert.IsNotNull(db1CnxConst);

        }
        [Test]
        public void XpoDataStoreResolver_GetById_ArgumentException_ShouldFail()
        {
            XpoDataStoreResolver configurationResolverBase =
            new XpoDataStoreResolver("appsettings.json");

            Assert.Throws<ArgumentException>(() =>
                    configurationResolverBase.GetById("db99")
            );


        }
        [Test]
        public void XpoDataStoreResolver_ResolveRuntime_ShouldPass()
        {
            XpoDataStoreResolver configurationResolverBase =
            new XpoDataStoreResolver("appsettings.json");


            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json");
            builder.Build();


            var NewConfig = File.ReadAllText("NewConnectionString.json");
            File.WriteAllText("appsettings.json", NewConfig);

          
            var db99 = configurationResolverBase.GetById("db99");
            Assert.IsNotNull(db99);


        }
    }
}