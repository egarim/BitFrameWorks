using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace BIT.Data.Xpo.Collections
{
    /// <summary>
    /// A descendant  of a  XPCollection<T> that implements INotifyCollectionChanged
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XpoObservableCollection<T> : XPCollection<T>, INotifyCollectionChanged
    {


        protected override void OnCollectionChanged(XPCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);


            NotifyCollectionChangedEventArgs ObservableArgs = null;
            switch (args.CollectionChangedType)
            {
                case XPCollectionChangedType.BeforeAdd:
                    return;
                case XPCollectionChangedType.AfterAdd:
                    ObservableArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, args.ChangedObject, args.NewIndex);
                    break;
                case XPCollectionChangedType.BeforeRemove:

                    return;
                case XPCollectionChangedType.AfterRemove:

                    ObservableArgs = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, args.ChangedObject);
                    break;
            }





            OnInternalCollectionChanged(this, ObservableArgs);
        }

        event NotifyCollectionChangedEventHandler InternalCollectionChanged;
        protected virtual void OnInternalCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            InternalCollectionChanged?.Invoke(sender, e);
        }


        object objectLock = new Object();

        public XpoObservableCollection(Session session, object theOwner, XPMemberInfo refProperty) : base(session, theOwner, refProperty)
        {
        }

        public XpoObservableCollection()
        {
        }

        public XpoObservableCollection(CriteriaOperator theCriteria, params SortProperty[] sortProperties) : base(theCriteria, sortProperties)
        {
        }

        public XpoObservableCollection(Session session) : base(session)
        {
        }

        public XpoObservableCollection(Session session, CriteriaOperator theCriteria, params SortProperty[] sortProperties) : base(session, theCriteria, sortProperties)
        {
        }

        public XpoObservableCollection(Session session, bool loadingEnabled) : base(session, loadingEnabled)
        {
        }

        public XpoObservableCollection(Session session, IEnumerable originalCollection, CriteriaOperator copyFilter, bool caseSensitive) : base(session, originalCollection, copyFilter, caseSensitive)
        {
        }

        public XpoObservableCollection(Session session, IEnumerable originalCollection, CriteriaOperator copyFilter) : base(session, originalCollection, copyFilter)
        {
        }

        public XpoObservableCollection(Session session, IEnumerable originalCollection) : base(session, originalCollection)
        {
        }

        public XpoObservableCollection(Session session, XPBaseCollection originalCollection, CriteriaOperator copyFilter, bool caseSensitive) : base(session, originalCollection, copyFilter, caseSensitive)
        {
        }

        public XpoObservableCollection(Session session, XPBaseCollection originalCollection, CriteriaOperator copyFilter) : base(session, originalCollection, copyFilter)
        {
        }

        public XpoObservableCollection(XPBaseCollection originalCollection, CriteriaOperator filter) : base(originalCollection, filter)
        {
        }

        public XpoObservableCollection(XPBaseCollection originalCollection, CriteriaOperator filter, bool caseSensitive) : base(originalCollection, filter, caseSensitive)
        {
        }

        public XpoObservableCollection(XPBaseCollection originalCollection) : base(originalCollection)
        {
        }

        public XpoObservableCollection(Session session, XPBaseCollection originalCollection) : base(session, originalCollection)
        {
        }

        public XpoObservableCollection(PersistentCriteriaEvaluationBehavior criteriaEvaluationBehavior, Session session, CriteriaOperator condition, bool selectDeleted) : base(criteriaEvaluationBehavior, session, condition, selectDeleted)
        {
        }

        public XpoObservableCollection(PersistentCriteriaEvaluationBehavior criteriaEvaluationBehavior, Session session, CriteriaOperator condition) : base(criteriaEvaluationBehavior, session, condition)
        {
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                lock (objectLock)
                {
                    InternalCollectionChanged += value;
                }
            }

            remove
            {
                lock (objectLock)
                {
                    InternalCollectionChanged -= value;
                }
            }
        }
    }
}