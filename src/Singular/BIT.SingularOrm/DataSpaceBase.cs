using System;
using System.Collections;
using System.Collections.Generic;

namespace BIT.SingularOrm
{
    public class DataSpaceBase : IDataSpace
    {
        //public DataSpaceBase(IBrevitasAppConfiguration AppConfiguration)
        //{
        //    DataSpacesObjects = new List<object>();
        //    this.AppConfiguration = AppConfiguration;
        //}

        //public IBrevitasAppConfiguration AppConfiguration { get; protected set; }

        public IList<object> DataSpacesObjects { get; protected set; }

        public virtual T CreateInstance<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public virtual T FindObject<T>(Func<T, bool> Criteria) where T : class
        {
            throw new NotImplementedException();
        }

        public virtual void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public virtual object CreateInstance(string ObjectType)
        {
            //Use the following line to locate the type
            //var Type = AppConfiguration.GetOrmType(ObjectType);
            throw new NotImplementedException();
        }

        public virtual object CreateInstance(Type ObjectType)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<object> CreateList(Func<dynamic, bool> Criteria, Type ObjectType)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> CreateListPage<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber)
            where T : class
        {
            throw new NotImplementedException();
        }

        public virtual int GetObjectCount<T>(Func<T, bool> Criteria) where T : class
        {
            throw new NotImplementedException();
        }

        public virtual T GetObjectWithKey<T>(object key) where T : class
        {
            throw new NotImplementedException();
        }

        public virtual object GetObjectWithKey(object key, Type ObjectType)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable CreateListPage(Type ObjectType, int ItemsPerPage, int PageNumber)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> CreateList<T>(Func<T, bool> Criteria) where T : class
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteObject(object ObjectToDelete)
        {
            throw new NotImplementedException();
        }

        //public virtual List<T> CreateList<T>(Func<T, bool> Criteria) where T : class
        //{
        //    throw new NotImplementedException();
        //}
    }
}