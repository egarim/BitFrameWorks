using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Services
{
    
    public interface IConfigResolver<T>
    {
        
        T GetById(string Id);
       
    }
}
