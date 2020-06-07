using System;
using System.Collections.Generic;
using System.Text;

namespace BIT.Data.Transfer
{
    interface IClient
    {
        void SendData<T>(T Data);
    }
}
