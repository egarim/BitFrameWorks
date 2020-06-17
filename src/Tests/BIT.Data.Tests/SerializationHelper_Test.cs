
using BIT.Data.Services;
using BIT.Data.Tests.Infrastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BIT.Data.Tests
{
    public class SerializationHelper_Test
    {

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
           .UseUrls("http://127.0.0.1:6001")
             .UseStartup<Startup>();

        //TODO add more test cases with complex objects

        StringSerializationHelper stringSerializationHelper = new StringSerializationHelper();
        [SetUp]
        public void Setup()
        {
         
        }

        [Test]
        public void ServerTest()
        {
            Task.Run(() =>
            {

                string[] args = null;
                CreateWebHostBuilder(args).Build().Run();
            });


            Task.Run(async () =>
            {

                //string[] args = null;
                //CreateWebHostBuilder(args).Build().Run();

                try
                {
                    HttpClient client = new HttpClient();
                    var test = await client.GetAsync("http://127.0.0.1:6001/api/Test");
                    Debug.WriteLine(test);
                    Assert.IsNotNull(test);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    throw ex;
                }
             
            });


          
        }


        [Test]
        public void SerializeDatetime_ShouldPass()
        {
          
            var StringDate=    stringSerializationHelper.SerializeObjectToString<DateTime>(new DateTime(2020,1,1));
            Assert.AreEqual(File.ReadAllText("StringDate.xml"), StringDate);
        }
        [Test]
        public void DerializeDatetime_ShouldPass()
        {

            var DateFromString = stringSerializationHelper.DeserializeObjectFromString<DateTime>(File.ReadAllText("StringDate.xml"));
            Assert.AreEqual(new DateTime(2020, 1, 1), DateFromString);
        }
    }
}
