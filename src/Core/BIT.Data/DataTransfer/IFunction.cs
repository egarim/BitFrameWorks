using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIT.Data.DataTransfer
{
    public interface IFunctionAsync
    {
        Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters);
      
    }
    public interface IFunction
    {
        IDataResult ExecuteFunction(IDataParameters Parameters);

    }
}
