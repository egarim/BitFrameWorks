using System;

namespace BIT.SingularOrm
{
    public interface IDataSpaceProvider
    {
        IDataSpace CreateDataSpace();

        void UpdateOrCreateSchema(SchemaStatus SchemaStatus);

        event EventHandler<SchemaMismatchEventArgs> SchemaMismatch;
    }
}