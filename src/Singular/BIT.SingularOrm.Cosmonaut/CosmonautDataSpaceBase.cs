using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Cosmonaut;
using Cosmonaut.Extensions;

namespace BIT.SingularOrm.Cosmonaut
{
    public class CosmonautDataSpaceBase : DataSpaceBase
    {
        private readonly Dictionary<Type, object> stores;

        //public CosmonautDataSpaceBase(IBrevitasAppConfiguration appConfiguration, Dictionary<Type, object> stores) :
        //    base(appConfiguration)
        //{
        //    this.stores = stores;
        //}

        private ICosmosStore<T> CastStore<T>() where T : class
        {
            var TheStore = stores[typeof(T)];
            var store = stores[typeof(T)] as ICosmosStore<T>;
            return store;
        }

        private IEnumerable<T> CreateListPageInternal<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber)
            where T : class
        {
            List<T> Data = null;
            Task.Run(async () =>
            {
                //Data.Where()

                var TheStore = CastStore<T>();
                Data = await TheStore.Query().WithPagination(PageNumber, ItemsPerPage).ToPagedListAsync();
            }).Wait();
            return Data;
        }

        public override void CommitChanges()
        {
            throw new NotImplementedException();
        }

        public override T CreateInstance<T>()
        {
            var Object = Activator.CreateInstance<T>();
            DataSpacesObjects.Add(Object);
            return Object;
        }

        public override object CreateInstance(Type ObjectType)
        {
            var Object = Activator.CreateInstance(ObjectType);
            DataSpacesObjects.Add(Object);
            return Object;
        }

        public override object CreateInstance(string ObjectType)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> CreateList<T>(Func<T, bool> Criteria)
        {
            return CreateListInternal(Criteria);
        }

        public IEnumerable<T> CreateListInternal<T>(Func<T, bool> Criteria) where T : class
        {
            List<T> Data = null;
            Task.Run(async () =>
            {
                //Data.Where()

                var TheStore = CastStore<T>();
                Data = await TheStore.Query().ToListAsync();
                //MethodInfo Where = typeof(System.Linq.Enumerable).GetTypeInfo().GetDeclaredMethods("Where").First();
                //MethodInfo QueryMethod = TheStore.GetType().GetTypeInfo().GetDeclaredMethods("Query").ToArray().First();
                //var Query = (dynamic)QueryMethod.Invoke(TheStore, new object[1] { null });
                //var ResultExtension = typeof(CosmosResultExtensions);
                //var ToListAsync = ResultExtension.GetMethods().Where(m => m.Name == "ToListAsync").FirstOrDefault();
                //IQueryable<T> queryable = (IQueryable<T>)Query;
                //var FromD = queryable.Where(Criteria);
                //var count = FromD.Count();
                //var ToListAsyncMethod = ToListAsync.MakeGenericMethod(typeof(T));
                //var users = await cosmoStore.Query().Where(x => x.HairColor == HairColor.Black).ToListAsync(cancellationToken);

                //Data = await ToListAsyncMethod.Invoke(Query, new object[2] { Query, null }) as List<T>;
            }).Wait();
            return Data;
        }

        public override IEnumerable<object> CreateList(Func<dynamic, bool> Criteria, Type ObjectType)
        {
            var CreateListInternal = GetType().GetTypeInfo().GetDeclaredMethods("CreateListInternal").First();
            var CreateListGeneric = CreateListInternal.MakeGenericMethod(ObjectType);
            return CreateListGeneric.Invoke(this, new object[1] {null}) as List<object>;
        }

        public override IEnumerable CreateListPage(Type ObjectType, int ItemsPerPage, int PageNumber)
        {
            var CreateListInternalMethod = GetType().GetMethod(nameof(CreateListPageInternal),
                BindingFlags.NonPublic | BindingFlags.Instance);
            var generic = CreateListInternalMethod.MakeGenericMethod(ObjectType);
            return generic.Invoke(this, new object[3] {null, ItemsPerPage, PageNumber}) as IEnumerable<object>;
        }

        public override IEnumerable<T> CreateListPage<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber)
        {
            return CreateListPageInternal(Criteria, ItemsPerPage, PageNumber);
        }

        public override T FindObject<T>(Func<T, bool> Criteria)
        {
            var TheStore = CastStore<T>();
            return TheStore.Query().First(Criteria);
        }

        public override int GetObjectCount<T>(Func<T, bool> Criteria)
        {
            var TheStore = CastStore<T>();
            return TheStore.Query().Where(Criteria).Count();
        }

        public override T GetObjectWithKey<T>(object key)
        {
            throw new NotImplementedException();
        }

        public override object GetObjectWithKey(object key, Type ObjectType)
        {
            throw new NotImplementedException();
        }

        public override void DeleteObject(object ObjectToDelete)
        {
            base.DeleteObject(ObjectToDelete);
        }
    }
}