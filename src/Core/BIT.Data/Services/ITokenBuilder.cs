using BIT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Services
{
    public interface ITokenBuilder
    {
        string BuildToken(ILoginParameters loginParameters,params string[] AdditionalValues);
    }
  
}
