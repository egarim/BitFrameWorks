using System;
using System.Collections;
using System.Collections.Generic;

namespace BIT.SingularOrm
{
    public interface IDataSpace
    {
        IList<object> DataSpacesObjects { get; }

        T CreateInstance<T>() where T : class;

        T GetObjectWithKey<T>(object key) where T : class;

        object GetObjectWithKey(object key, Type ObjectType);

        object CreateInstance(string ObjectType);

        object CreateInstance(Type ObjectType);

        void DeleteObject(object ObjectToDelete);

        IEnumerable<object> CreateList(Func<dynamic, bool> Criteria, Type ObjectType);

        IEnumerable<T> CreateList<T>(Func<T, bool> Criteria) where T : class;

        IEnumerable CreateListPage(Type ObjectType, int ItemsPerPage, int PageNumber);

        IEnumerable<T> CreateListPage<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber) where T : class;

        int GetObjectCount<T>(Func<T, bool> Criteria) where T : class;

        T FindObject<T>(Func<T, bool> Criteria) where T : class;

        void CommitChanges();
    }
}