using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace BIT.Data.Services
{
    public class SimpleObjectSerializationService : IObjectSerializationService
    {
        private byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory,
                    CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }

                return memory.ToArray();
            }
        }

        public T GetObjectsFromByteArray<T>(byte[] bytes)
        {
            var Type = typeof(T);
            using (MemoryStream fs = new MemoryStream(bytes))
            using (var gZipStream = new GZipStream(fs, CompressionMode.Decompress))
            {
                using (XmlDictionaryReader reader =
                    XmlDictionaryReader.CreateTextReader(gZipStream, XmlDictionaryReaderQuotas.Max))
                {
                    XmlSerializer serializer = new XmlSerializer(Type);
                    var Statement = (T)Convert.ChangeType(serializer.Deserialize(reader), Type);
                    return Statement;
                }
            }
        }
        public byte[] ToByteArray<T>(T Data)
        {
            try
            {
                var StatementType = typeof(T);

                var fs = new MemoryStream();
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs))
                {
                    XmlSerializer serializer = new XmlSerializer(StatementType);
                    serializer.Serialize(writer, Data);

                }
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    //HACK only for debug how much data are we sending
                    var array = fs.ToArray();
                    Debug.WriteLine(string.Format("{0}:{1} kb", "Length before compression", Convert.ToDecimal(array.Length) / Convert.ToDecimal(1000)));
                    array = Compress(array);
                    Debug.WriteLine(string.Format("{0}:{1} kb", "Length after compression", Convert.ToDecimal(array.Length) / Convert.ToDecimal(1000)));
                    return array;
                }
                return Compress(fs.ToArray());
            }
            catch (Exception exception)
            {
                Debug.WriteLine(string.Format("{0}:{1}", "exception.Message", exception.Message));
                if (exception.InnerException != null)
                {
                    Debug.WriteLine(string.Format("{0}:{1}", "exception.InnerException.Message", exception.InnerException.Message));
                }
                Debug.WriteLine(string.Format("{0}:{1}", " exception.StackTrace", exception.StackTrace));
            }
            return null;
        }
        public SimpleObjectSerializationService()
        {

        }
    }
}
