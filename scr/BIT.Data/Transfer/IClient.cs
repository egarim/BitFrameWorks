using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Transfer
{
    public interface IParams
    {
        
    }
    public interface IResult
    {
        
    }
    interface IClient
    {
        IResult ExecuteFunction(IParams Parameters);
        void ExecuteMethod(IParams Parameters);
    }
}
