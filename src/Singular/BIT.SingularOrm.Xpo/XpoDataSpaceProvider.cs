using System;
using System.Linq;
using System.Reflection;
using BIT.Data.Services;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;
using DevExpress.Xpo.Metadata;

namespace BIT.SingularOrm.Xpo
{
    public class XpoDataSpaceProvider : DataSpaceProvider
    {
        private const string Message = "Please provide an array of persistent types or an array of assemblies";

        public XpoDataSpaceProvider( string ConnectionString,
            params Assembly[] Assemblies) 
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString));

            this.ConnectionString = ConnectionString;
            this.Assemblies = Assemblies;
            PersistentTypes = null;
        }

        public XpoDataSpaceProvider(string ConnectionString,
            params Type[] PersistentTypes) 
        {
            if (string.IsNullOrEmpty(ConnectionString) || string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString));

            this.ConnectionString = ConnectionString;
            this.PersistentTypes = PersistentTypes;
            Assemblies = null;
        }

        public XPDictionary Dictionary { get; protected set; }
        public Type[] PersistentTypes { get; protected set; }
        public Assembly[] Assemblies { get; protected set; }
        public string ConnectionString { get; protected set; }
        public IDataLayer DataLayer { get; protected set; }

        public static void KeepReference()
        {
        }

        public override void UpdateOrCreateSchema(SchemaStatus SchemaStatus)
        {
            if (XpoDefault.DataLayer == null)
                using (var updateDataLayer =
                    new SimpleDataLayer(Dictionary, CreateUpdatingDataStore(true)))

                {
                    //HACK you can use any of the following versions to pass persistent types or assemblies
                    //updateDataLayer.UpdateSchema(false, dictionary.CollectClassInfos(GetPersistentTypes()));
                    if (Assemblies != null)
                    {
                        updateDataLayer.UpdateSchema(false, Dictionary.CollectClassInfos(Assemblies));

                        new UnitOfWork(updateDataLayer).CreateObjectTypeRecords();
                        return;
                    }

                    if (PersistentTypes != null)
                    {
                        updateDataLayer.UpdateSchema(false, Dictionary.CollectClassInfos(PersistentTypes));

                        new UnitOfWork(updateDataLayer).CreateObjectTypeRecords();
                        return;
                    }

                    throw new Exception(Message);
                }
        }

        public override void Init()
        {
            XpoDefault.DataLayer = null;
            XpoDefault.Session = null;
            var Args = new SchemaMismatchEventArgs(this, SchemaStatus.None);
            PrepareDictionary();

            //Check if the schema requires update https://www.devexpress.com/Support/Center/Question/Details/Q245466/determining-if-schema-needs-to-be-updated
            if (XpoDefault.DataLayer == null)
            {
                try
                {
                    using (var CheckSchemaDataLayer = new SimpleDataLayer(Dictionary, CreateSchemaCheckingDataStore()))
                    {
                        //HACK new way to compare schema https://www.devexpress.com/Support/Center/Question/Details/T562023/xpo-how-to-detect-of-chnage-structure-of-database-by-new-change-in-db-model
                        var types = Dictionary.Classes.Cast<XPClassInfo>().Where(ci => ci.IsPersistent).ToArray();
                        var Result = CheckSchemaDataLayer.UpdateSchema(true, types);
                        if (Result == UpdateSchemaResult.FirstTableNotExists) OnSchemaMismatch(this, Args);
                        //HACK the old version of checking the schema https://www.devexpress.com/Support/Center/Question/Details/T189303/xpo-how-to-check-if-db-schema-needs-to-be-changed-without-changing-it
                        //using (var session = new Session(CheckSchemaDataLayer))
                        //{

                        //    session.UpdateSchema();
                        //    //session.CreateObjectTypeRecords();
                        //}
                    }

                    Args.Handled = true;
                }
                catch (SchemaCorrectionNeededException)
                {
                    OnSchemaMismatch(this, Args);
                }

                //catch (DevExpress.Xpo.DB.Exceptions.SchemaCorrectionNeededException)
                //{
                //    OnSchemaMismatch(this, Args);
                //}
                if (Args.Handled)
                {
                    workingStore =
                        XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.SchemaAlreadyExists);
                    DataLayer = new ThreadSafeDataLayer(Dictionary, workingStore);
                }
            }
        }

        private void PrepareDictionary()
        {
            Dictionary = new ReflectionDictionary();
            if (Assemblies != null)
            {
                Dictionary.GetDataStoreSchema(Assemblies);
                return;
            }

            if (PersistentTypes != null)
            {
                Dictionary.GetDataStoreSchema(PersistentTypes.Where(pt =>
                    pt.IsAbstract == false ||
                    !MetadataService.DoesTypeImplementsAttribute(pt, typeof(NonPersistentAttribute))).ToArray());
                return;
            }

            throw new Exception(Message);
        }

        public override IDataSpace CreateDataSpace()
        {
            //TODO fix constructor
            throw new NotImplementedException();
            //return new XpoDataSpace(this, AppConfiguration);
        }

        public UnitOfWork CreateUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        #region DataStoreCreation

        private IDataStore workingStore;

        protected virtual IDataStore CreateWorkingDataStore()
        {
            if (workingStore == null)
                workingStore = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.SchemaAlreadyExists);
            return workingStore;
        }

        protected virtual IDataStore CreateUpdatingDataStore(bool allowUpdateSchema)
        {
            var createOption = allowUpdateSchema
                ? AutoCreateOption.DatabaseAndSchema
                : AutoCreateOption.SchemaAlreadyExists;
            var dataStore = XpoDefault.GetConnectionProvider(ConnectionString, createOption);

            return dataStore;
        }

        public virtual IDataStore CreateSchemaCheckingDataStore()
        {
            var dataStore = XpoDefault.GetConnectionProvider(ConnectionString, AutoCreateOption.None);
            return dataStore;
        }

        #endregion
    }
}