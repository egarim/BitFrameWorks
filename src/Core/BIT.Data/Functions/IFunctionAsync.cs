using System;
using System.Threading;
using System.Threading.Tasks;

namespace BIT.Data.Functions
{
    public interface IFunctionAsync
    {
        Task<IDataResult> ExecuteFunctionAsync(IDataParameters Parameters, CancellationToken cancellationToken = default);

    }
}
