using BIT.SingularOrm.Xpo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XpoDemoOrm;

namespace BIT.SingularOrm.Tests.Xpo
{
    public class XpoDataSpaceProvider_Tests:IDataSpaceTestScenarios
    {
        public void CreateInstanceOfNonOrmObject_PassObjectAsArgument_ThrowsExceptionCannotResolveClassInfoException()
        {
            throw new NotImplementedException();
        }

        public void CreateInstance_CreateCustomerWithGenericType_InstanceNotNull()
        {
            throw new NotImplementedException();
        }

        public void CreateInstance_CreateCustomerWithTypeParameter_InstanceNotNull()
        {
            throw new NotImplementedException();
        }

        public void CreateList_CreateListWithGenericTypeAndNotNullCriteria_ListIsCreated()
        {
            throw new NotImplementedException();
        }

        public void CreateList_CreateListWithGenericTypeAndNullCriteria_ReturnAListWithValues()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageGenericWithCriteria_ListOfObjectsIsCreated()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageGenericWithNullCriteria_ListOfObjectsIsCreated()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageWithNegativePageValue_ArgumentExceptionIsThrown()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageWithNullTypeParameter_ArgumentNullExceptionIsThrown()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageWithPageNumberBiggerThanPageCount_ValueReturnedIsNull()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageWithTypeParameter_ListOfObjectsIsCreated()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListPageWithZeroItemsPerPage_ArgumentExceptionIsThrown()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceCreateListWithTypeParameter_ListOfObjectsIsCreated()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_DataSpaceObjectsContainsInstance_ContainsInstanceTrue()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_FindObjectWithGenericParameter_ObjectIsFound()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_GetObjectCountWithGenericType_ListOfObjectsIsCreated()
        {
            throw new NotImplementedException();
        }

        public void DataSpacesObjects_GetObjectWithKeyWithTypeParameter_ListIsNotNull()
        {
            throw new NotImplementedException();
        }

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void TestOne()
        {
            var DataSpace = Arrange(nameof(CreateInstance_CreateCustomerWithGenericType_InstanceNotNull));

            Assert.IsNotNull(DataSpace.CreateInstance<Customer>());
            Assert.AreEqual(1, 1);
        }
        private static IDataSpace Arrange(string databaseFileName)
        {
            File.Delete(databaseFileName);

            var Assembly = typeof(Customer).Assembly;

            var Provider = new XpoDataSpaceProvider(Helpers.FormatConnectionString(databaseFileName), Assembly);

            Provider.SchemaMismatch += (sender, e) =>
            {
                e.DataSpaceProvider.UpdateOrCreateSchema(e.SchemaStatus);
                e.Handled = true;
            };
            Provider.Init();
            return Provider.CreateDataSpace();
        }

    }
}
