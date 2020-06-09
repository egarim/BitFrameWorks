using System;
using System.Collections.Generic;

namespace BIT.Data.Transfer
{
    public interface IResult
    {
        byte[] ResultValue { get; set; }
        IList<string> Errors { get; set; }

    }
}
