using System;

namespace BIT.Data.Helpers
{
    public interface IObjectSerializationHelper
    {
        T GetObjectsFromByteArray<T>(byte[] bytes);
        byte[] ToByteArray<T>(T Data);
    }
}
