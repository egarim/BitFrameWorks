using System;
using System.Collections.Generic;

namespace BIT.Data.Functions
{

    //HACK don't inherit from dictionary<T,T> because that will mess with the serialization of the other properties

    public interface IDataParameters
    {
        string MemberName { get; set; }
        byte[] ParametersValue { get; set; }

        DateTime DateUtc { get; set; }
        IDictionary<string, object> AdditionalValues { get; set; }
    }
}
