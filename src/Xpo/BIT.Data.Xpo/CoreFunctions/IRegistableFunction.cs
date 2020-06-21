using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Xpo.CoreFunctions
{
    public interface IRegistableFunction
    {
        bool Register(Session session);
        bool Exist(Session session);
    }
}
