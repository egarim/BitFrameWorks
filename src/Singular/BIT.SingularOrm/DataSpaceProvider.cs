using System;

namespace BIT.SingularOrm
{
    public class DataSpaceProvider : IDataSpaceProvider
    {
        //public DataSpaceProvider(IBrevitasAppConfiguration appConfiguration)
        //{
        //    AppConfiguration = appConfiguration;
        //}

        //public IBrevitasAppConfiguration AppConfiguration { get; protected set; }

        public virtual IDataSpace CreateDataSpace()
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateOrCreateSchema(SchemaStatus SchemaStatus)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<SchemaMismatchEventArgs> SchemaMismatch;

        protected virtual void OnSchemaMismatch(IDataSpaceProvider source, SchemaMismatchEventArgs e)
        {
            SchemaMismatch?.Invoke(source, e);
        }

        public virtual void Init()
        {
        }
    }
}