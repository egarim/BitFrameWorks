using NUnit.Framework;

namespace BIT.SingularOrm.Tests
{
    public interface IDataSpaceTestScenarios
    {
        void CreateInstance_CreateCustomerWithGenericType_InstanceNotNull();

        void CreateInstance_CreateCustomerWithTypeParameter_InstanceNotNull();

        void CreateInstanceOfNonOrmObject_PassObjectAsArgument_ThrowsExceptionCannotResolveClassInfoException();

        void CreateList_CreateListWithGenericTypeAndNotNullCriteria_ListIsCreated();

        void CreateList_CreateListWithGenericTypeAndNullCriteria_ReturnAListWithValues();

        void DataSpacesObjects_DataSpaceCreateListPageGenericWithCriteria_ListOfObjectsIsCreated();

        void DataSpacesObjects_DataSpaceCreateListPageGenericWithNullCriteria_ListOfObjectsIsCreated();

        void DataSpacesObjects_DataSpaceCreateListPageWithNegativePageValue_ArgumentExceptionIsThrown();

        void DataSpacesObjects_DataSpaceCreateListPageWithNullTypeParameter_ArgumentNullExceptionIsThrown();

        void DataSpacesObjects_DataSpaceCreateListPageWithPageNumberBiggerThanPageCount_ValueReturnedIsNull();

        void DataSpacesObjects_DataSpaceCreateListPageWithTypeParameter_ListOfObjectsIsCreated();

        void DataSpacesObjects_DataSpaceCreateListPageWithZeroItemsPerPage_ArgumentExceptionIsThrown();

        void DataSpacesObjects_DataSpaceCreateListWithTypeParameter_ListOfObjectsIsCreated();

        void DataSpacesObjects_DataSpaceObjectsContainsInstance_ContainsInstanceTrue();

        void DataSpacesObjects_FindObjectWithGenericParameter_ObjectIsFound();

        void DataSpacesObjects_GetObjectCountWithGenericType_ListOfObjectsIsCreated();

        void DataSpacesObjects_GetObjectWithKeyWithTypeParameter_ListIsNotNull();
    }
}