using System;

namespace BIT.Data.Services
{
    public interface IObjectSerializationService
    {
        T GetObjectsFromByteArray<T>(byte[] bytes);
        byte[] ToByteArray<T>(T Data);
    }
}
