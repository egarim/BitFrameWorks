using BIT.Data.Services;
using NUnit.Framework;
using System;

namespace BIT.Data.Tests
{
    public class BinaryObjectSerializationService_Test
    {
        private const string Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed quis luctus massa. Maecenas eleifend sollicitudin dui, sit amet consequat velit vestibulum eu. Aliquam porta diam non sem dictum, sed pretium ex vehicula. Praesent massa diam, posuere id sodales at, lobortis a purus. Sed nec nunc mi. Quisque hendrerit felis sit amet ex maximus scelerisque. Curabitur libero est, varius eu leo pharetra, ullamcorper tempus sapien. Pellentesque pretium porta sem non tempor.";

        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void SerializeAString_TestShouldPass()
        {

            BinaryObjectSerializationService DeflatSerialization = new BinaryObjectSerializationService(StreamType.Deflated);

            BinaryObjectSerializationService GzipSerialization = new BinaryObjectSerializationService(StreamType.GZip);


            BinaryObjectSerializationService UnCompressedSerialization = new BinaryObjectSerializationService(StreamType.UnCompressed);

            CompressXmlObjectSerializationService compressXmlObjectSerializationService = new CompressXmlObjectSerializationService();

            var DeflatData = DeflatSerialization.ToByteArray<string>(BinaryObjectSerializationService_Test.Text);
            var GzipData = GzipSerialization.ToByteArray<string>(BinaryObjectSerializationService_Test.Text);
            var UnCompressedData = UnCompressedSerialization.ToByteArray<string>(BinaryObjectSerializationService_Test.Text);
            var CompressedXml = compressXmlObjectSerializationService.ToByteArray(BinaryObjectSerializationService_Test.Text);



            Assert.NotNull(DeflatData);

           
        }
        [Test]
        public void SerializeAndDeserialize_TestShouldPass()
        {

            BinaryObjectSerializationService DeflatSerialization = new BinaryObjectSerializationService(StreamType.Deflated);



            var Data = DeflatSerialization.ToByteArray<string>(BinaryObjectSerializationService_Test.Text);
            var StringFromData = DeflatSerialization.GetObjectsFromByteArray<string>(Data);

            Assert.AreEqual(BinaryObjectSerializationService_Test.Text,StringFromData);

          
        }
    }
}
