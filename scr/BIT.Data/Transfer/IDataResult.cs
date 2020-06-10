using System;
using System.Collections.Generic;

namespace BIT.Data.Transfer
{
    public interface IDataResult
    {
        byte[] ResultValue { get; set; }
        IList<string> Errors { get; set; }

    }
}
