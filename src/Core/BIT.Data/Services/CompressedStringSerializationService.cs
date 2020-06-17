using System;
using System.IO;
using System.Text;

namespace BIT.Data.Services
{   

    //TODO JM add compression to the strings 

    public class CompressedStringSerializationService : StringSerializationHelper
    {

        public CompressedStringSerializationService()
        {
           
        }

        public override T DeserializeObjectFromString<T>(string Object)
        {
         
            return base.DeserializeObjectFromString<T>(Object);
        }

        public override string SerializeObjectToString<T>(T toSerialize)
        {
            return base.SerializeObjectToString(toSerialize);
        }
    }
}
