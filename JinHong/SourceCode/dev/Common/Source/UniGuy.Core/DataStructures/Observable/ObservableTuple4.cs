using System;
using System.Collections.Generic;
#if DOT_NET_35
using System.Linq;
#endif
using System.Text;
using System.ComponentModel;
using UniGuy.Core.Extensions;

#if DOT_NET_35
namespace UniGuy.Core.DataStructures.Observable
{
    /// <summary>
    /// 一个Observable的Tuple4
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    public class ObservableTuple<T1, T2, T3, T4> : Tektosyne.MutableTuple<T1, T2, T3, T4>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        public override T1 Item1
        {
            get { return _item1; }
            set
            {
                if (!_item1.EqualsEx(value))
                {
                    _item1 = value;
                    OnPropertyChanged("Item1");
                }
            }
        }

        public override T2 Item2
        {
            get { return _item2; }
            set
            {
                if (!_item2.EqualsEx(value))
                {
                    _item2 = value;
                    OnPropertyChanged("Item2");
                }
            }
        }

        public override T3 Item3
        {
            get { return _item3; }
            set
            {
                if (!_item3.EqualsEx(value))
                {
                    _item3 = value;
                    OnPropertyChanged("Item3");
                }
            }
        }

        public override T4 Item4
        {
            get { return _item4; }
            set
            {
                if (!_item4.EqualsEx(value))
                {
                    _item4 = value;
                    OnPropertyChanged("Item4");
                }
            }
        }

        #endregion //   Properties

        #region Constructor

        public ObservableTuple() : base() { }

        public ObservableTuple(T1 t1, T2 t2, T3 t3, T4 t4) : base(t1, t2, t3, t4) { }

        #endregion //   Constructors
    }
}
#endif