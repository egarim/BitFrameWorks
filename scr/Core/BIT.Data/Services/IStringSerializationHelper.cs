using System;

namespace BIT.Data.Services
{
    public interface IStringSerializationHelper
    {
        T DeserializeObjectFromString<T>(string Object);
       
        string SerializeObjectToString<T>(T toSerialize);
    }
}
