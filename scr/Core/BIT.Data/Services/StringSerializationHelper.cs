using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BIT.Data.Services
{
    public class StringSerializationHelper : IStringSerializationHelper
    {
        public virtual T DeserializeObjectFromString<T>(string Object)
        {
            var Type = typeof(T);
            using (TextReader reader = new StringReader(Object))
            {
                XmlSerializer serializer = new XmlSerializer(Type);
                var Statement = (T)Convert.ChangeType(serializer.Deserialize(reader), Type);
                return Statement;
            }
        }
        public virtual string SerializeObjectToString<T>(T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
      
    }
}
