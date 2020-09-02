using BIT.Data.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BIT.Data.Tests
{
    public class DictionaryResolverBase_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        IDictionary<string, string> NewInstanceFunction(IConfiguration arg1, string arg2)
        {
            throw new NotImplementedException();
        }
        [Test]
        public void ConfigurationResolverBase_GetById_ShouldPass()
        {

            DictionaryResolver configurationResolverBase = new DictionaryResolver("DictionaryConfiguration.json", "Dictionary1");
            var Dictionary1 = configurationResolverBase.GetById("db1");
            Assert.NotNull(Dictionary1);


            //DictionaryResolver configurationResolverBase = new DictionaryResolver("", new Func<IConfiguration, string, System.Collections.Generic.IDictionary<string, string>>(NewInstanceFunction), "Dictionary1");

            //    //new ConfigurationResolverBase<string>("appsettings.json", (IConfiguration arg1, string arg2) =>
            //    //{

            //    //    System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>> enumerable = arg1.GetSection("ConnectionStrings").AsEnumerable();
            //    //    var ConnectionString = enumerable.Where(c => c.Key == $"ConnectionStrings:{arg2}").FirstOrDefault();
            //    //    return ConnectionString.Value;
            //    //});

            //var db1 = configurationResolverBase.GetById("db1");
            //Assert.AreEqual("XpoProvider=MSSqlServer;Data Source=Computer;User ID=sa;Password=ChangeMe123;Initial Catalog=UnitTest;Persist Security Info=true", db1);

        }
        [Test]
        public void ConfigurationResolverBase_GetById_ShouldFail()
        {
            ConfigurationResolverBase<string> configurationResolverBase =
            new ConfigurationResolverBase<string>("appsettings.json", (IConfiguration arg1, string arg2) =>
            {

                System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>> enumerable = arg1.GetSection("ConnectionStrings").AsEnumerable();
                var ConnectionString = enumerable.Where(c => c.Key == $"ConnectionStrings:{arg2}").FirstOrDefault();
                return ConnectionString.Value;
            });

            Assert.Throws<ArgumentException>(() =>
                    configurationResolverBase.GetById("db99")
            );


        }
        [Test]
        public void ConfigurationResolverBase_ResolveRuntime_ShouldPass()
        {
            ConfigurationResolverBase<string> configurationResolverBase =
            new ConfigurationResolverBase<string>("appsettings.json", (IConfiguration arg1, string arg2) =>
            {

                System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>> enumerable = arg1.GetSection("ConnectionStrings").AsEnumerable();
                var ConnectionString = enumerable.Where(c => c.Key == $"ConnectionStrings:{arg2}").FirstOrDefault();
                return ConnectionString.Value;
            });


            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json");
            builder.Build();


            var NewConfig = File.ReadAllText("NewConnectionString.json");
            File.WriteAllText("appsettings.json", NewConfig);

            var db99 = configurationResolverBase.GetById("db99");
            Assert.AreEqual("XpoProvider=MSSqlServer;Data Source=Computer;User ID=sa;Password=ChangeMe123;Initial Catalog=UnitTest;Persist Security Info=true", db99);

        }

    }
}
