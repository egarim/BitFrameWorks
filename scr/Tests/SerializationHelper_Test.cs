using BIT.Data.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests
{
    public class SerializationHelper_Test
    {

        //TODO add more test cases with complex objects

        IStringSerializationHelper stringSerializationHelper = new StringSerializationHelper();
        [SetUp]
        public void Setup()
        {
           
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
