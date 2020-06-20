using System.ComponentModel;

namespace BIT.SingularOrm
{
    public enum SchemaStatus
    {
        None,
        SchemaDoesNotExist,
        SchemaNeedsToBeUpdated
    }

    public class SchemaMismatchEventArgs : HandledEventArgs
    {
        public SchemaMismatchEventArgs(IDataSpaceProvider DataSpaceProvider, SchemaStatus SchemaStatus)
        {
            this.DataSpaceProvider = DataSpaceProvider;
            this.SchemaStatus = SchemaStatus;
        }

        public SchemaStatus SchemaStatus { get; }
        public IDataSpaceProvider DataSpaceProvider { get; }
    }
}