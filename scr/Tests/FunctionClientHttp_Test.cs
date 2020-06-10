using BIT.Data.Helpers;
using BIT.Data.Transfer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Tests
{
    public class FunctionClientHttp_Test
    {
        IFunctionClient client;
        [SetUp]
        public void Setup()
        {
            client = new FunctionClientHttp("", "", new StringSerializationHelper());
        }

        [Test]
        void MethodName()
        {
            
        }
    }
}
