using System;
using BIT.SingularOrm;

using Microsoft.EntityFrameworkCore;

namespace Brevitas.AppFramework.DataAccess.EF
{
    public class EFDataSpaceProvider : DataSpaceProvider
    {
        //public EFDataSpaceProvider(IBrevitasAppConfiguration appConfiguration, DbContext Context) : base(
        //    appConfiguration)
        //{
        //    if (Context == null) throw new ArgumentNullException($"the parameter {nameof(Context)} cannot be null");
        //    this.Context = Context;
        //}

        public DbContext Context { get; }
        //TODO learn about entity framework http://www.entityframeworktutorial.net/what-is-entityframework.aspx

        public static void KeepReference()
        {
        }

        public override IDataSpace CreateDataSpace()
        {
            //TODO fix constructor
            throw new NotImplementedException();
            //return new EFDataSpace(this, null);
        }

        public override void UpdateOrCreateSchema(SchemaStatus SchemaStatus)
        {
            switch (SchemaStatus)
            {
                case SchemaStatus.None:

                    break;

                case SchemaStatus.SchemaDoesNotExist:
                    Context.Database.EnsureCreated();
                    break;

                case SchemaStatus.SchemaNeedsToBeUpdated:
                    Context.Database.Migrate();
                    break;
            }
        }

        public override void Init()
        {
            try
            {
                if (Context.Database.CanConnect())
                    OnSchemaMismatch(this, new SchemaMismatchEventArgs(this, SchemaStatus.SchemaNeedsToBeUpdated));
                else
                    OnSchemaMismatch(this, new SchemaMismatchEventArgs(this, SchemaStatus.SchemaDoesNotExist));
            }
            catch (Exception)
            {
                OnSchemaMismatch(this, new SchemaMismatchEventArgs(this, SchemaStatus.SchemaDoesNotExist));
            }
        }
    }
}