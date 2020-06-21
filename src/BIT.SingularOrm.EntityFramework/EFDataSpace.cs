using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BIT.SingularOrm;
using BIT.Data.Extensions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Brevitas.AppFramework.DataAccess.EF
{
    public class EFDataSpace : DataSpaceBase
    {
        private IDbContextTransaction transaction;

        //public EFDataSpace(EFDataSpaceProvider DataSpaceProvider, IBrevitasAppConfiguration AppConfiguration) : base(
        //    AppConfiguration)
        //{
        //    this.DataSpaceProvider = DataSpaceProvider;
        //    transaction = DataSpaceProvider.Context.Database.BeginTransaction();
        //}

        private EFDataSpaceProvider DataSpaceProvider { get; }

        public override void CommitChanges()
        {
            try
            {
                //https://exceptionnotfound.net/entity-change-tracking-using-dbcontext-in-entity-framework-6/

                DataSpaceProvider.Context.SaveChanges();
                transaction.Commit();
                transaction = DataSpaceProvider.Context.Database.BeginTransaction();
            }
            catch (Exception)
            {
                // TODO: Handle failure
            }
        }

        public override T CreateInstance<T>()
        {
            var Instance = Activator.CreateInstance<T>();
            DataSpaceProvider.Context.Set<T>().Add(Instance);
            DataSpacesObjects.Add(Instance);
            return Instance;
        }

        public override object CreateInstance(string ObjectType)
        {
            return base.CreateInstance(ObjectType);
        }

        public override object CreateInstance(Type ObjectType)
        {
            var Instance = Activator.CreateInstance(ObjectType);
            DataSpacesObjects.Add(Instance);
            return Instance;
        }

        public override IEnumerable<T> CreateListPage<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber)
        {
            //TODO in entity framework the records don't return in the same order they were inserted, needs to implement sort function
            //http://www.codewrecks.com/blog/index.php/2009/03/21/entity-framework-dynamic-sorting-and-pagination/
            var Skip = ItemsPerPage * PageNumber;
            var dbSet = DataSpaceProvider.Context.Set<T>();

            //var Result = dbSet.FromSql("select VALUE Customers from Customers where Customers.ContactName like '%Customer%' order by Customers.Name skip @s limit @l", Skip, ItemsPerPage);


            var test = dbSet.Where(Criteria).OrderBy("Id DES");

            if (Criteria == null)
                return dbSet.Skip(Skip).Take(ItemsPerPage); //.OrderBy("Name");
            return dbSet.Where(Criteria).Skip(Skip).Take(ItemsPerPage);
        }

        public override IEnumerable CreateListPage(Type ObjectType, int ItemsPerPage, int PageNumber)
        {
            return base.CreateListPage(ObjectType, ItemsPerPage, PageNumber);
        }

        public override int GetObjectCount<T>(Func<T, bool> Criteria)
        {
            if (Criteria != null)
                return DataSpaceProvider.Context.Set<T>().Where(Criteria).Count();
            return DataSpaceProvider.Context.Set<T>().Count();
        }

        public override object GetObjectWithKey(object key, Type ObjectType)
        {
            return base.GetObjectWithKey(key, ObjectType);
        }

        public override T FindObject<T>(Func<T, bool> Criteria)
        {
            return DataSpaceProvider.Context.Set<T>().Where(Criteria).FirstOrDefault();
        }

        public override IEnumerable<T> CreateList<T>(Func<T, bool> Criteria)
        {
            if (Criteria == null)
                return DataSpaceProvider.Context.Set<T>();
            return DataSpaceProvider.Context.Set<T>().Where(Criteria);
        }

        public override IEnumerable<object> CreateList(Func<dynamic, bool> Criteria, Type ObjectType)
        {
            return base.CreateList(Criteria, ObjectType);
        }
    }
}