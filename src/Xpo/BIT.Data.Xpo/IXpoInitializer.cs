using DevExpress.Xpo;
using DevExpress.Xpo.DB;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BIT.Xpo
{
    public interface IXpoInitializer
    {
        UnitOfWork CreateUnitOfWork();
        UpdateSchemaResult? InitSchema();

        DataLayerType DataLayerType { get; }
    }

    public interface IAsyncXpoInitializer
    {
        Task<UpdateSchemaResult?> InitSchemaAsync(CancellationToken cancellationToken = default);
    }
}
