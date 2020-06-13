using System;

namespace BIT.Data.Helpers
{
    public interface IStringSerializationHelper
    {
        T DeserializeObjectFromString<T>(string Object);
       
        string SerializeObjectToString<T>(T toSerialize);
    }
}
