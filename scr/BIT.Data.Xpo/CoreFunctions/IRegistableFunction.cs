using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data
{
    public interface IRegistableFunction
    {
        bool Register(Session session);
        bool Exist(Session session);
    }
}
