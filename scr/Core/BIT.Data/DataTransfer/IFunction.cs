using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIT.Data.DataTransfer
{
    public interface IFunction
    {
        Task<IDataResult> ExecuteFunction(IDataParameters Parameters);
      
    }
}
