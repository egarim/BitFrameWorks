//using Brevitas.AppFramework.DataAccess.Xpo;
//using TestSharedScenarios;
//using XpoOrm = Brevitas.AppFramework.Xpo.DemoORM;

//namespace Brevitas.AppFramework.Tests
//{
//    [TestClass()]
//    public class XpoDataSpaceTests : IDataSpaceTestScenarios
//    {
//        [TestMethod()]
//        public void CreateInstance_CreateCustomerWithGenericType_InstanceNotNull()
//        {
//            var DataSpace = Arrange(nameof(CreateInstance_CreateCustomerWithGenericType_InstanceNotNull));

//            Assert.IsNotNull(DataSpace.CreateInstance<XpoOrm.Customer>());
//        }

//        [TestMethod()]
//        public void CreateInstance_CreateCustomerWithTypeParameter_InstanceNotNull()
//        {
//            var DataSpace = Arrange(nameof(CreateInstance_CreateCustomerWithTypeParameter_InstanceNotNull));

//            Assert.IsNotNull(DataSpace.CreateInstance(typeof(XpoOrm.Customer)));
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_GetObjectCountWithGenericType_ListOfObjectsIsCreated()
//        {
//            var DataSpace = Arrange(nameof(DataSpacesObjects_GetObjectCountWithGenericType_ListOfObjectsIsCreated));

//            for (int i = 1; i <= 5; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            var Count = DataSpace.GetObjectCount<Customer>(null);
//            Assert.IsTrue(Count == 5);
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_FindObjectWithGenericParameter_ObjectIsFound()
//        {
//            var DataSpace = Arrange(nameof(DataSpacesObjects_FindObjectWithGenericParameter_ObjectIsFound));
//            List<Customer> Customers = new List<Customer>();
//            for (int i = 1; i <= 5; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                Customers.Add(customer);
//                customer.Name = $"Customer {i}";
//            }

//            var FirstCustomer = Customers.First();
//            DataSpace.CommitChanges();
//            var ObjectFromKey =
//                DataSpace.FindObject<Customer>(new Func<Customer, bool>(o => o.Oid == FirstCustomer.Oid));
//            Assert.IsTrue(ObjectFromKey == FirstCustomer);
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_DataSpaceObjectsContainsInstance_ContainsInstanceTrue()
//        {
//            var DataSpace = Arrange(nameof(DataSpacesObjects_DataSpaceObjectsContainsInstance_ContainsInstanceTrue));
//            var Instance = DataSpace.CreateInstance<XpoOrm.Customer>();
//            Assert.IsTrue(DataSpace.DataSpacesObjects.Contains(Instance));
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_DataSpaceCreateListPageGenericWithCriteria_ListOfObjectsIsCreated()
//        {
//            var DataSpace =
//                Arrange(nameof(DataSpacesObjects_DataSpaceCreateListPageGenericWithCriteria_ListOfObjectsIsCreated));

//            for (int i = 1; i <= 20; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            for (int i = 1; i <= 20; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Client {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            IEnumerable<Customer> Instance = DataSpace.CreateListPage<Customer>(null, 5, 1);
//            var Count = Instance.Where(c =>
//                c.Name == "Customer 5" || c.Name == "Customer 6" || c.Name == "Customer 7" || c.Name == "Customer 8" ||
//                c.Name == "Customer 9" || c.Name == "Customer 10").Count();
//            Assert.IsTrue(Count == 5);
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_DataSpaceCreateListWithTypeParameter_ListOfObjectsIsCreated()
//        {
//            var DataSpace =
//                Arrange(nameof(DataSpacesObjects_DataSpaceCreateListWithTypeParameter_ListOfObjectsIsCreated));

//            var Oscar = DataSpace.CreateInstance<XpoOrm.Customer>();
//            Oscar.Name = nameof(Oscar);
//            var Joche = DataSpace.CreateInstance<XpoOrm.Customer>();
//            Joche.Name = nameof(Joche);
//            DataSpace.CommitChanges();

//            Func<dynamic, bool> Criteria =
//                new Func<dynamic, bool>(o => o.Name == nameof(Oscar) || o.Name == nameof(Joche));
//            var Instance = DataSpace.CreateList(Criteria, typeof(XpoOrm.Customer));
//            Assert.IsTrue(Instance.Count() == 2);
//        }

//        [TestMethod()]
//        public void CreateInstanceOfNonOrmObject_PassObjectAsArgument_ThrowsExceptionCannotResolveClassInfoException()
//        {
//            //TODO the exception thrown is XPO it should be a generic exception in for all the dataspaces
//            var DataSpace =
//                Arrange(
//                    nameof(
//                        CreateInstanceOfNonOrmObject_PassObjectAsArgument_ThrowsExceptionCannotResolveClassInfoException
//                    ));

//            Assert.ThrowsException<CannotResolveClassInfoException>(() => DataSpace.CreateInstance<Object>());
//        }

//        public void DataSpacesObjects_GetObjectWithKeyWithTypeParameter_ListIsNotNull()
//        {
//            throw new NotImplementedException();
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_GetObjectWithKeyWithTypeParameter_ListOfObjectsIsCreated()
//        {
//            var DataSpace = Arrange(nameof(DataSpacesObjects_GetObjectWithKeyWithTypeParameter_ListOfObjectsIsCreated));
//            List<Customer> Customers = new List<Customer>();
//            for (int i = 1; i <= 5; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                Customers.Add(customer);
//                customer.Name = $"Customer {i}";
//            }

//            var FirstCustomer = Customers.First();
//            DataSpace.CommitChanges();
//            var ObjectFromKey = DataSpace.GetObjectWithKey(FirstCustomer.Oid, typeof(Customer));
//            Assert.IsTrue(ObjectFromKey == FirstCustomer);
//        }

//        private static IDataSpace Arrange(string databaseFileName)
//        {
//            File.Delete(databaseFileName);

//            var Assembly = typeof(XpoOrm.Customer).Assembly;

//            var Provider = new XpoDataSpaceProvider(null, Helpers.FormatConnectionString(databaseFileName), Assembly);

//            Provider.SchemaMismatch += (sender, e) =>
//            {
//                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
//                e.Handled = true;
//            };
//            Provider.Init();
//            return Provider.CreateDataSpace();
//        }

//        #region CreateList<T>(Func<T,bool>)

//        [TestMethod()]
//        public void CreateList_CreateListWithGenericTypeAndNullCriteria_ReturnAListWithValues()
//        {
//            var DataSpace = Arrange(nameof(CreateList_CreateListWithGenericTypeAndNullCriteria_ReturnAListWithValues));

//            for (int i = 1; i <= 5; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            var List = DataSpace.CreateList<Customer>(null);
//            Assert.IsTrue(List.Count() == 5);
//        }

//        [TestMethod()]
//        public void CreateList_CreateListWithGenericTypeAndNotNullCriteria_ListIsCreated()
//        {
//            var DataSpace = Arrange(nameof(CreateList_CreateListWithGenericTypeAndNotNullCriteria_ListIsCreated));

//            for (int i = 1; i <= 5; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            var List = DataSpace.CreateList<Customer>(new Func<Customer, bool>(o => o.Name.StartsWith("Customer")));
//            Assert.IsTrue(List.Count() == 5);
//        }

//        #endregion CreateList<T>(Func<T,bool>)

//        #region CreateListPage(Type,int,int)

//        [TestMethod()]
//        public void DataSpacesObjects_DataSpaceCreateListPageGenericWithNullCriteria_ListOfObjectsIsCreated()
//        {
//            var DataSpace =
//                Arrange(
//                    nameof(DataSpacesObjects_DataSpaceCreateListPageGenericWithNullCriteria_ListOfObjectsIsCreated));

//            for (int i = 1; i <= 100; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            var Instance = DataSpace.CreateListPage<Customer>(null, 5, 5);
//            Assert.IsTrue(Instance.Count() == 5);
//        }

//        [TestMethod()]
//        public void DataSpacesObjects_DataSpaceCreateListPageWithTypeParameter_ListOfObjectsIsCreated()
//        {
//            var DataSpace =
//                Arrange(nameof(DataSpacesObjects_DataSpaceCreateListPageWithTypeParameter_ListOfObjectsIsCreated));

//            for (int i = 1; i <= 100; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            var Instance = DataSpace.CreateListPage(typeof(Customer), 5, 1).Cast<Customer>();
//            var Count = Instance.Where(c =>
//                c.Name == "Customer 5" || c.Name == "Customer 6" || c.Name == "Customer 7" || c.Name == "Customer 8" ||
//                c.Name == "Customer 9" || c.Name == "Customer 10").Count();
//            Assert.IsTrue(Count == 5);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentNullException), "The parameter ObjectType cannot be null")]
//        public void DataSpacesObjects_DataSpaceCreateListPageWithNullTypeParameter_ArgumentNullExceptionIsThrown()
//        {
//            var DataSpace =
//                Arrange(
//                    nameof(DataSpacesObjects_DataSpaceCreateListPageWithNullTypeParameter_ArgumentNullExceptionIsThrown
//                    ));

//            for (int i = 1; i <= 10; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            var Instance = DataSpace.CreateListPage(null, 5, 1).Cast<Customer>();
//            var Count = Instance.Where(c =>
//                c.Name == "Customer 5" || c.Name == "Customer 6" || c.Name == "Customer 7" || c.Name == "Customer 8" ||
//                c.Name == "Customer 9" || c.Name == "Customer 10").Count();
//            Assert.IsTrue(Count == 5);
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException), "The parameter PageNumber:-1 cannot be negative")]
//        public void DataSpacesObjects_DataSpaceCreateListPageWithNegativePageValue_ArgumentExceptionIsThrown()
//        {
//            var DataSpace =
//                Arrange(
//                    nameof(DataSpacesObjects_DataSpaceCreateListPageWithNegativePageValue_ArgumentExceptionIsThrown));

//            for (int i = 1; i <= 10; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            var Instance = DataSpace.CreateListPage(typeof(Customer), 5, -1).Cast<Customer>();
//            var Count = Instance.Where(c =>
//                c.Name == "Customer 5" || c.Name == "Customer 6" || c.Name == "Customer 7" || c.Name == "Customer 8" ||
//                c.Name == "Customer 9" || c.Name == "Customer 10").Count();
//        }

//        [TestMethod]
//        [ExpectedException(typeof(ArgumentException), "The parameter ItemsPerPage:0 cannot be less than 1")]
//        public void DataSpacesObjects_DataSpaceCreateListPageWithZeroItemsPerPage_ArgumentExceptionIsThrown()
//        {
//            var DataSpace =
//                Arrange(
//                    nameof(DataSpacesObjects_DataSpaceCreateListPageWithZeroItemsPerPage_ArgumentExceptionIsThrown));

//            for (int i = 1; i <= 10; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            var Instance = DataSpace.CreateListPage(typeof(Customer), 0, 1).Cast<Customer>();
//            var Count = Instance.Where(c =>
//                c.Name == "Customer 5" || c.Name == "Customer 6" || c.Name == "Customer 7" || c.Name == "Customer 8" ||
//                c.Name == "Customer 9" || c.Name == "Customer 10").Count();
//        }

//        [TestMethod]
//        public void DataSpacesObjects_DataSpaceCreateListPageWithPageNumberBiggerThanPageCount_ValueReturnedIsNull()
//        {
//            var DataSpace =
//                Arrange(
//                    nameof(
//                        DataSpacesObjects_DataSpaceCreateListPageWithPageNumberBiggerThanPageCount_ValueReturnedIsNull
//                    ));

//            for (int i = 1; i <= 10; i++)
//            {
//                var customer = DataSpace.CreateInstance<XpoOrm.Customer>();
//                customer.Name = $"Customer {i}";
//            }

//            DataSpace.CommitChanges();
//            Func<Customer, bool> Criteria = new Func<Customer, bool>(o => o.Name.Contains("Customer"));
//            var Instance = DataSpace.CreateListPage(typeof(Customer), 5, 3);
//            Assert.IsTrue(Instance == null);
//        }

//        #endregion CreateListPage(Type,int,int)
//    }
//}