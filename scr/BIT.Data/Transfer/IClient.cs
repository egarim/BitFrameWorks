using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BIT.Data.Transfer
{
    interface IClient
    {
        IResult ExecuteFunction(IParams Parameters);
        void ExecuteMethod(IParams Parameters);
    }
}
