using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIT.Data.Functions
{
    public interface IFunction
    {
        IDataResult ExecuteFunction(IDataParameters Parameters);

    }
}
