using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIT.Data.Transfer
{
    public interface IFunctionClient
    {
        Task<IDataResult> ExecuteFunction(IDataParameters Parameters);
      
    }
}
