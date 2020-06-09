using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Transfer
{
    public abstract class ClientBase : IClient
    {
        public abstract IResult ExecuteFunction(IParams Parameters);


        public abstract void ExecuteMethod(IParams Parameters);
      
    }
}
