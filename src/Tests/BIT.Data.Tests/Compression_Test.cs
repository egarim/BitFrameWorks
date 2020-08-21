using BIT.Data.Services;
using NUnit.Framework;
using System;

namespace BIT.Data.Tests
{
    public class Compression_Test
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

            var DeflatData = DeflatSerialization.ToByteArray<string>(Text);
            var GzipData = GzipSerialization.ToByteArray<string>(Text);
            var UnCompressedData = UnCompressedSerialization.ToByteArray<string>(Text);
            var CompressedXml = compressXmlObjectSerializationService.ToByteArray(Text);



            Assert.NotNull(DeflatData);


        }

    }
}
