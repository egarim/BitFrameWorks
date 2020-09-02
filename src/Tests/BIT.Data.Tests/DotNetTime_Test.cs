using BIT.Data.Services;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BIT.Data.Tests
{
    //TODO rename test
    class DotNetTime_Test
    {
        DateTime InitialDate;
        DateTime InitialDatePlusOneMilisecond;
        DateTime InitialDatePlusOne;
        [SetUp]
        public void Setup()
        {
            InitialDate = new DateTime(2020, 01, 01, 1, 1, 1);
            InitialDatePlusOneMilisecond = new DateTime(2020, 01, 01, 1, 1, 1,1);
            InitialDatePlusOne = new DateTime(2020, 01, 01, 1, 1, 2);
        }

        [Test]
        public void GetDotNetTime_TestShouldPass()
        {
            var InitialResult=  DotNetTime.ToDotNetTime(InitialDate);
            var InitialPlusOneResult = DotNetTime.ToDotNetTime(InitialDatePlusOne);
            Assert.AreNotEqual(InitialResult, InitialPlusOneResult);
        }
        [Test]
        public void GetDotNetTimeMilisecond_TestShouldPass()
        {
            var InitialResult = DotNetTime.ToDotNetTime(InitialDate);
            var InitialPlusOneResult = DotNetTime.ToDotNetTime(InitialDatePlusOneMilisecond);
            Assert.AreNotEqual(InitialResult, InitialPlusOneResult);
        }
        [Test]
        public void GetDotNetTimeDotNetNowSmall_TestShouldPass()
        {
            var InitialResult = DotNetTime.DotNetNowSmall(InitialDate);
            var InitialPlusOneResult = DotNetTime.DotNetNowSmall(InitialDatePlusOne);
            Assert.AreNotEqual(InitialResult, InitialPlusOneResult);
        }
        [Test]
        public void GetDotNetTimeDotNetNowSmallPlus1Milisecond_TestShouldPass()
        {
            var InitialResult = DotNetTime.DotNetNowSmall(InitialDate);
            var InitialPlusOneResult = DotNetTime.DotNetNowSmall(InitialDatePlusOneMilisecond);
            Assert.AreEqual(InitialResult, InitialPlusOneResult);
        }
    }
}
