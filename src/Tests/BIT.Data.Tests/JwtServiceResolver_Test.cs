using BIT.Data.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace BIT.Data.Tests
{
    public class JwtServiceResolver_Test
    {
        AppSettingsDictionaryResolverBase _configurationResolverBase;
        [SetUp]
        public void Setup()
        {
            _configurationResolverBase = new AppSettingsDictionaryResolverBase("JwtSettings.json", "Tokens");
        }


        [Test]
        public void GetById_DictionaryNotNull()
        {

            JwtServiceResolver jwtServiceResolver = new JwtServiceResolver(_configurationResolverBase);
            var Jwt1 = jwtServiceResolver.GetById("db1");
            Assert.NotNull(Jwt1);




        }
        [Test]
        public void Get2InstanceById_InstancesShouldBeDifferent()
        {

            JwtServiceResolver jwtServiceResolver = new JwtServiceResolver(_configurationResolverBase);
            var Jwt1 = jwtServiceResolver.GetById("db1");
            var Jwt2 = jwtServiceResolver.GetById("db2");
            Assert.AreNotEqual(Jwt1, Jwt2);
        }
        [Test]
        public void GetById_ShouldFail()
        {
            JwtServiceResolver jwtServiceResolver = new JwtServiceResolver(_configurationResolverBase);

            Assert.Throws<ArgumentException>(() =>
                    jwtServiceResolver.GetById("db99")
            );


        }
        

    }
}
