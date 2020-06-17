using System;
using System.Collections.Generic;

namespace BIT.Data.DataTransfer
{

    //HACK don't inherit from dictionary<T,T> because that will mess with the serialization of the other properties

    public interface IDataResult
    {
        byte[] ResultValue { get; set; }
        DateTime DateUtc { get; set; }
        IDictionary<string,string> Errors { get; set; }
        IDictionary<string, object> AdditionalValues { get; set; }
        string MemberName { get; set; }
    }
}
