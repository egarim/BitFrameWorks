//using Brevitas.AppFramework.DataAccess.Xpo;
//using XpoOrm = Brevitas.AppFramework.Xpo.DemoORM;

//namespace Brevitas.AppFramework.Tests
//{
//    public static class Helpers
//    {
//        private const string ConnectionString = "XpoProvider=SQLite;Data Source={0}";

//        public static string FormatConnectionString(string DatabaseName)
//        {
//            return string.Format(ConnectionString, DatabaseName);
//        }
//    }

//    [TestClass]
//    public class XpoDataSpaceProviderTests

//    {
//        //private const string ConnectionString = "XpoProvider=InMemoryDataStore;Data Source=DataSet.xml;Read Only=false";
//        //XpoProvider=SQLite;Data Source=filename

//        private const string DatabaseFileName = "database.sqlite";
//        private const string InitTypesDatabase = "InitTypesDatabase.sqlite";
//        private const string InitAssembliesDatabase = "InitAssembliesDatabase.sqlite";

//        private const string CreateDataSpaceTypesDatabase = "CreateDataSpaceTypesDatabase.sqlite";
//        private const string CreateDataSpaceAssembliesDatabase = "CreateDataSpaceAssembliesDatabase.sqlite";
//        private const string CommitToDatabaseConstructorAssemblies = "CommitToDatabaseConstructorAssemblies.sqlite";

//        private void CreateSampleData(IDataSpace DataSpace)
//        {
//            for (int i = 0; i < 100; i++)
//            {
//                var Instance = DataSpace.CreateInstance<Customer>();
//                Instance.Code = $"{i}";
//                Instance.Name = $"Customer {i}";
//            }
//        }

//        [TestMethod()]
//        public void CreateInstance_ConstructorWithAssemblies_CommitToDatabase()
//        {
//            File.Delete(CommitToDatabaseConstructorAssemblies);
//            var Assembly = typeof(XpoOrm.Customer).Assembly;

//            var Provider = new XpoDataSpaceProvider(null,
//                Helpers.FormatConnectionString(CommitToDatabaseConstructorAssemblies), Assembly);

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();

//            IDataSpace dataSpace = Provider.CreateDataSpace();
//            CreateSampleData(dataSpace);
//            dataSpace.CommitChanges();
//        }

//        [TestMethod()]
//        public void Constructor_WithAssemblies_GetTheInstanceOfTheClass()
//        {
//            File.Delete(DatabaseFileName);
//            var Assembly = typeof(XpoOrm.Customer).Assembly;

//            var Provider = new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(DatabaseFileName), Assembly);
//            Assert.IsNotNull(Provider);
//            ;
//        }

//        [TestMethod()]
//        public void Constructor_WithTypes_GetTheInstanceOfTheClass()
//        {
//            File.Delete(DatabaseFileName);
//            var Provider = new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(DatabaseFileName),
//                typeof(XpoOrm.Customer));
//            Assert.IsNotNull(Provider);
//        }

//        [TestMethod()]
//        public void InitWithTypes_UpdateSchema_DataLayerNotNull()
//        {
//            File.Delete(InitTypesDatabase);
//            var Provider = new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(InitTypesDatabase),
//                typeof(XpoOrm.Customer));

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();
//            Assert.IsNotNull(Provider.DataLayer);
//        }

//        [TestMethod()]
//        public void InitWithAssemblies_UpdateSchema_DataLayerNotNull()
//        {
//            File.Delete(InitAssembliesDatabase);
//            var Assembly = typeof(XpoOrm.Customer).Assembly;

//            var Provider =
//                new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(InitAssembliesDatabase), Assembly);

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();
//            Assert.IsNotNull(Provider.DataLayer);
//        }

//        [TestMethod()]
//        public void CreateDataSpaceWithAssemblies_UpdateSchema_DataLayerNotNull()
//        {
//            File.Delete(CreateDataSpaceAssembliesDatabase);
//            var Assembly = typeof(XpoOrm.Customer).Assembly;

//            var Provider = new XpoDataSpaceProvider(null,
//                Helpers.FormatConnectionString(CreateDataSpaceAssembliesDatabase), Assembly);

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();

//            Assert.IsNotNull(Provider.CreateDataSpace());
//        }

//        [TestMethod()]
//        public void CreateDataSpaceWithTypes_UpdateSchema_DataLayerNotNull()
//        {
//            File.Delete(CreateDataSpaceTypesDatabase);
//            var Provider = new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(CreateDataSpaceTypesDatabase),
//                typeof(XpoOrm.Customer));

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();
//            Assert.IsNotNull(Provider.CreateDataSpace());
//        }
//    }
//}