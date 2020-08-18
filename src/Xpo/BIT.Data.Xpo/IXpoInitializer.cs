using DevExpress.Xpo;
using System;
using System.Linq;

namespace BIT.Xpo
{
    public interface IXpoInitializer
    {
        UnitOfWork CreateUnitOfWork();
        void InitSchema();

        DataLayerType DataLayerType { get; }
    }
}
