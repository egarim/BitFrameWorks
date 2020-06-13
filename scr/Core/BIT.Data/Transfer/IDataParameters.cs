using System;

namespace BIT.Data.Transfer
{
    public interface IDataParameters
    {
        string MemberName { get; set; }
        byte[] ParametersValue { get; set; }
    }
}
