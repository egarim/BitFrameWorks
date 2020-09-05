using BIT.Data.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BIT.Data.Tests
{
    public class AppSettingsDictionaryResolverBase_Test
    {
        [SetUp]
        public void Setup()
        {
        }

      
        [Test]
        public void GetById_DictionaryNotNull()
        {

            AppSettingsDictionaryResolverBase configurationResolverBase = new AppSettingsDictionaryResolverBase("DictionaryConfiguration.json", "Dictionary1");
            var Dictionary = configurationResolverBase.GetById("Config1");
            Assert.NotNull(Dictionary);


     

        }
        [Test]
        public void GetById_CountShouldBe3()
        {

            AppSettingsDictionaryResolverBase configurationResolverBase = new AppSettingsDictionaryResolverBase("DictionaryConfiguration.json", "Dictionary1");
            var Dictionary = configurationResolverBase.GetById("Config2");
            Assert.AreEqual(Dictionary.Count,3);

        }
        [Test]
        public void GetById_ShouldFail()
        {
            AppSettingsDictionaryResolverBase configurationResolverBase = new AppSettingsDictionaryResolverBase("DictionaryConfiguration.json", "Dictionary1");

            Assert.Throws<ArgumentException>(() =>
                    configurationResolverBase.GetById("db99")
            );


        }
        [Test]
        public void ConfigurationResolverBase_ResolveRuntime_ShouldPass()
        {
             AppSettingsDictionaryResolverBase configurationResolverBase = new AppSettingsDictionaryResolverBase("TestDictionaryConfiguration.json", "Dictionary1");


            //var builder = new ConfigurationBuilder()
            //               .SetBasePath(Directory.GetCurrentDirectory())
            //               .AddJsonFile("appsettings.json");
            //builder.Build();


            var NewConfig = File.ReadAllText("NewDictionaryConfiguration.json");
            File.WriteAllText("TestDictionaryConfiguration.json", NewConfig);

            var NewItem = configurationResolverBase.GetById("NewItem");
            Assert.NotNull(NewItem);

        }

    }
}
