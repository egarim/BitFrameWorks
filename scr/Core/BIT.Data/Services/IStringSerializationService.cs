using System;

namespace BIT.Data.Services
{
    public interface IStringSerializationService
    {
        T DeserializeObjectFromString<T>(string Object);
       
        string SerializeObjectToString<T>(T toSerialize);
    }
}
