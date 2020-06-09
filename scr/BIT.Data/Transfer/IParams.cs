using System;

namespace BIT.Data.Transfer
{
    public interface IParams
    {
        string MemberName { get; set; }
        byte[] ParametersValue { get; set; }
    }
}
