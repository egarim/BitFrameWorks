using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BIT.SingularOrm;

using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace BIT.SingularOrm.Xpo
{
    public class XpoDataSpace : DataSpaceBase
    {
        //public XpoDataSpace(XpoDataSpaceProvider Provider, IBrevitasAppConfiguration AppConfiguration) : base(
        //    AppConfiguration)
        //{
        //    this.Provider = Provider;
        //    UnitOfWork = this.Provider.CreateUnitOfWork();
        //}

        private XpoDataSpaceProvider Provider { get; }
        public UnitOfWork UnitOfWork { get; protected set; }

        public override int GetObjectCount<T>(Func<T, bool> Criteria)
        {
            return Criteria != null ? UnitOfWork.Query<T>().Where(Criteria).Count() : UnitOfWork.Query<T>().Count();
        }

        public override T CreateInstance<T>()
        {
            var Object = (T) UnitOfWork.GetClassInfo<T>().CreateNewObject(UnitOfWork);
            DataSpaceEntityHelper.Link(ref Object, this);
            DataSpacesObjects.Add(Object);
            return Object;
        }

        public override object CreateInstance(Type ObjectType)
        {
            var Object = UnitOfWork.GetClassInfo(ObjectType).CreateNewObject(UnitOfWork);
            DataSpacesObjects.Add(Object);
            return Object;
        }

        public override T FindObject<T>(Func<T, bool> Criteria)
        {
            return UnitOfWork.Query<T>().Where(Criteria).FirstOrDefault();
        }

        public override void CommitChanges()
        {
            if (UnitOfWork.InTransaction)
                UnitOfWork.CommitChanges();
        }

        public override IEnumerable<T> CreateList<T>(Func<T, bool> Criteria)
        {
            if (Criteria == null)
                return UnitOfWork.Query<T>();
            return UnitOfWork.Query<T>().Where(Criteria);
        }

        public override IEnumerable<T> CreateListPage<T>(Func<T, bool> Criteria, int ItemsPerPage, int PageNumber)
        {
            return CreateListPage(typeof(T), ItemsPerPage, PageNumber).Cast<T>();
        }

        protected virtual List<T> CreateListInternal<T>(Func<T, bool> Criteria)
        {
            var list = UnitOfWork.Query<T>().Where(Criteria).ToList();
            return list;
        }

        public override IEnumerable<object> CreateList(Func<dynamic, bool> Criteria, Type ObjectType)
        {
            var CreateListInternalMethod = GetType().GetMethod(nameof(CreateListInternal),
                BindingFlags.NonPublic | BindingFlags.Instance);
            var generic = CreateListInternalMethod.MakeGenericMethod(ObjectType);
            return generic.Invoke(this, new object[1] {Criteria}) as IEnumerable<object>;
        }

        public override IEnumerable CreateListPage(Type ObjectType, int ItemsPerPage, int PageNumber)
        {
            if (ObjectType == null)
                throw new ArgumentNullException("The parameter ObjectType cannot be null");
            if (PageNumber < 0)
                throw new ArgumentException($"The parameter PageNumber:{PageNumber} cannot be negative");
            if (ItemsPerPage < 1)
                throw new ArgumentException($"The parameter ItemsPerPage:{ItemsPerPage} cannot be less than 1");

            var Class = UnitOfWork.GetClassInfo(ObjectType);
            var Selector =
                new XPPageSelector(new XPCollection(UnitOfWork, ObjectType)) {PageSize = ItemsPerPage};
            if (Selector.PageCount <= PageNumber)
                return null;

            Selector.CurrentPage = PageNumber;

            var List = (IListSource) Selector;
            return List.GetList();
        }

        public override void DeleteObject(object ObjectToDelete)
        {
            UnitOfWork.Delete(ObjectToDelete);
        }

        //public override IEnumerable<T> CreateList<T>(Func<T, bool> Criteria)
        //{
        //    unitOfWork.Query<T>().Where(Criteria);

        //    return unitOfWork.Query<T>().Where(Criteria);
        //}
        public override object GetObjectWithKey(object key, Type ObjectType)
        {
            var ClassInfo = UnitOfWork.GetClassInfo(ObjectType);
            var Where = new BinaryOperator(ClassInfo.KeyProperty.Name, key);
            return UnitOfWork.FindObject(ClassInfo, Where);
        }
    }
}