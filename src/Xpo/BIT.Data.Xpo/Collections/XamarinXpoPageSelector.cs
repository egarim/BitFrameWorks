using BIT.Data.Collections;
using DevExpress.Xpo;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BIT.Data.Xpo.Collections
{
    public class XamarinXpoPageSelector<T> : INotifyPropertyChanged
    {
        public readonly XPPageSelector Selector;
        ObservableRangeCollection<T> _ObservableData = new ObservableRangeCollection<T>();
        public XpoObservablePageSelectorBehavior _Behavior;
        public XamarinXpoPageSelector(XPCollection<T> collection, int PageSize, XpoObservablePageSelectorBehavior Behavior)
        {
            this.Selector = new XPPageSelector(collection);
            this.Selector.PageSize = PageSize;
            _Behavior = Behavior;
            CurrentPage = 0;

        }
        int _currentPage = -1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                if (_currentPage == value)
                    return;

                if (-1 == value)
                    return;

                _currentPage = value;
                this.Selector.CurrentPage = _currentPage;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
                ProcessData();
            }
        }
      

        public ObservableRangeCollection<T> ObservableData { get => _ObservableData; set => _ObservableData = value; }

        public XpoObservablePageSelectorBehavior Behavior
        {
            get
            {
                return _Behavior;
            }
            set
            {
                _Behavior = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ProcessData()
        {


            IEnumerable<T> InvokeData = ((IListSource)this.Selector).GetList().Cast<T>();
            if (this.Behavior == XpoObservablePageSelectorBehavior.AppendPage)
                _ObservableData.AddRange(InvokeData);
            else
            {
                _ObservableData.Clear();
                _ObservableData.AddRange(InvokeData);
            }
        }

    }
}