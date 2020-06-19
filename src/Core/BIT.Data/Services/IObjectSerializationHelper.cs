using System;

namespace BIT.Data.Services
{
    //TODO require that T have a default parameterless contructor
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
    public interface IObjectSerializationService
    {
        T GetObjectsFromByteArray<T>(byte[] bytes);// where T : new();
        byte[] ToByteArray<T>(T Data);// where T : new();
    }
}
