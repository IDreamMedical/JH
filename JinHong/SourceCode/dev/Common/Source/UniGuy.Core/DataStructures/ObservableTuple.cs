using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace UniGuy.Core.DataStructures
{
    [Serializable]
    public class ObservableTuple<T1, T2> : IEquatable<ObservableTuple<T1, T2>>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Fields
        protected T1 _item1;
        protected T2 _item2;

        // Methods
        public ObservableTuple()
        {
        }

        public ObservableTuple(T1 item1, T2 item2)
        {
            this._item1 = item1;
            this._item2 = item2;
        }

        public bool Equals(ObservableTuple<T1, T2> tuple)
        {
            return (!object.ReferenceEquals(tuple, null) && Equals(_item1, tuple._item1) && Equals(_item2, tuple._item2));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ObservableTuple<T1, T2>);
        }

        public static bool Equals(ObservableTuple<T1, T2> x, ObservableTuple<T1, T2> y)
        {
            if (object.ReferenceEquals(x, null))
            {
                return object.ReferenceEquals(y, null);
            }
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            int temp = -1;
            if (!object.Equals(this._item1,default(T1)))
                temp ^= this._item1.GetHashCode();
            if (!object.Equals(this._item2,default(T2)))
                temp ^= this._item2.GetHashCode();
            return temp;
        }

        public static bool operator ==(ObservableTuple<T1, T2> x, ObservableTuple<T1, T2> y)
        {
            return ObservableTuple<T1, T2>.Equals(x, y);
        }

        public static bool operator !=(ObservableTuple<T1, T2> x, ObservableTuple<T1, T2> y)
        {
            return !ObservableTuple<T1, T2>.Equals(x, y);
        }

        public override string ToString()
        {
            return string.Format("({0})", 
                string.Join(", ", Item1, Item2));
        }

        // Properties
        public virtual T1 Item1
        {
            [DebuggerStepThrough]
            get
            {
                return this._item1;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item1,value))
                {
                    this._item1 = value;
                    OnPropertyChanged("Item1");
                }
            }
        }

        public virtual T2 Item2
        {
            [DebuggerStepThrough]
            get
            {
                return this._item2;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item2, value))
                {
                    this._item2 = value;
                    OnPropertyChanged("Item2");
                }
            }
        }
    }

    [Serializable]
    public class ObservableTuple<T1, T2, T3> : IEquatable<ObservableTuple<T1, T2, T3>>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Fields
        protected T1 _item1;
        protected T2 _item2;
        protected T3 _item3;

        // Methods
        public ObservableTuple()
        {
        }

        public ObservableTuple(T1 item1, T2 item2, T3 item3)
        {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
        }

        public bool Equals(ObservableTuple<T1, T2, T3> tuple)
        {
            return (!object.ReferenceEquals(tuple, null) && Equals(_item1, tuple._item1) && Equals(_item2, tuple._item2)
                 && Equals(_item3,tuple._item3));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ObservableTuple<T1, T2, T3>);
        }

        public static bool Equals(ObservableTuple<T1, T2, T3> x, ObservableTuple<T1, T2, T3> y)
        {
            if (object.ReferenceEquals(x, null))
            {
                return object.ReferenceEquals(y, null);
            }
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            int temp = -1;
            if (!object.Equals(this._item1, default(T1)))
                temp ^= this._item1.GetHashCode();
            if (!object.Equals(this._item2, default(T2)))
                temp ^= this._item2.GetHashCode();
            if (!object.Equals(this._item3, default(T3)))
                temp ^= this._item3.GetHashCode();
            return temp;
        }

        public static bool operator ==(ObservableTuple<T1, T2, T3> x, ObservableTuple<T1, T2, T3> y)
        {
            return ObservableTuple<T1, T2, T3>.Equals(x, y);
        }

        public static bool operator !=(ObservableTuple<T1, T2, T3> x, ObservableTuple<T1, T2, T3> y)
        {
            return !ObservableTuple<T1, T2, T3>.Equals(x, y);
        }

        public override string ToString()
        {
            return string.Format("({0})",
                string.Join(", ", Item1, Item2, Item3));
        }

        // Properties
        public virtual T1 Item1
        {
            [DebuggerStepThrough]
            get
            {
                return this._item1;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item1,value))
                {
                    this._item1 = value;
                    OnPropertyChanged("Item1");
                }
            }
        }

        public virtual T2 Item2
        {
            [DebuggerStepThrough]
            get
            {
                return this._item2;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item2, value))
                {
                    this._item2 = value;
                    OnPropertyChanged("Item2");
                }
            }
        }

        public virtual T3 Item3
        {
            [DebuggerStepThrough]
            get
            {
                return this._item3;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item3, value))
                {
                    this._item3 = value;
                    OnPropertyChanged("Item3");
                }
            }
        }
    }

    [Serializable]
    public class ObservableTuple<T1, T2, T3, T4> : IEquatable<ObservableTuple<T1, T2, T3, T4>>, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        // Fields
        protected T1 _item1;
        protected T2 _item2;
        protected T3 _item3;
        protected T4 _item4;

        // Methods
        public ObservableTuple()
        {
        }

        public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this._item1 = item1;
            this._item2 = item2;
            this._item3 = item3;
            this._item4 = item4;
        }

        public bool Equals(ObservableTuple<T1, T2, T3, T4> tuple)
        {
            return (!object.ReferenceEquals(tuple, null) && Equals(_item1, tuple._item1) && Equals(_item2,tuple._item2)
                 && Equals(_item3, tuple._item3) && Equals(_item4, tuple._item4));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ObservableTuple<T1, T2, T3, T4>);
        }

        public static bool Equals(ObservableTuple<T1, T2, T3, T4> x, ObservableTuple<T1, T2, T3, T4> y)
        {
            if (object.ReferenceEquals(x, null))
            {
                return object.ReferenceEquals(y, null);
            }
            return x.Equals(y);
        }

        public override int GetHashCode()
        {
            int temp = -1;
            if (!object.Equals(this._item1, default(T1)))
                temp ^= this._item1.GetHashCode();
            if (!object.Equals(this._item2, default(T2)))
                temp ^= this._item2.GetHashCode();
            if (!object.Equals(this._item3, default(T3)))
                temp ^= this._item3.GetHashCode();
            if (!object.Equals(this._item4, default(T4)))
                temp ^= this._item4.GetHashCode();
            return temp;
        }

        public static bool operator ==(ObservableTuple<T1, T2, T3, T4> x, ObservableTuple<T1, T2, T3, T4> y)
        {
            return ObservableTuple<T1, T2, T3, T4>.Equals(x, y);
        }

        public static bool operator !=(ObservableTuple<T1, T2, T3, T4> x, ObservableTuple<T1, T2, T3, T4> y)
        {
            return !ObservableTuple<T1, T2, T3, T4>.Equals(x, y);
        }

        public override string ToString()
        {
            return string.Format("({0})",
                string.Join(", ", Item1, Item2, Item3, Item4));
        }

        // Properties
        public virtual T1 Item1
        {
            [DebuggerStepThrough]
            get
            {
                return this._item1;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item1,value))
                {
                    this._item1 = value;
                    OnPropertyChanged("Item1");
                }
            }
        }

        public virtual T2 Item2
        {
            [DebuggerStepThrough]
            get
            {
                return this._item2;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item2, value))
                {
                    this._item2 = value;
                    OnPropertyChanged("Item2");
                }
            }
        }

        public virtual T3 Item3
        {
            [DebuggerStepThrough]
            get
            {
                return this._item3;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item3,value))
                {
                    this._item3 = value;
                    OnPropertyChanged("Item3");
                }
            }
        }

        public virtual T4 Item4
        {
            [DebuggerStepThrough]
            get
            {
                return this._item4;
            }
            [DebuggerStepThrough]
            set
            {
                if (!Equals(_item3,value))
                {
                    this._item4 = value;
                    OnPropertyChanged("Item4");
                }
            }
        }
    }

    /// <summary>
    /// Mutable observable的Tuple的工厂工具
    /// </summary>
    public static class ObservableTuple
    {
        public static ObservableTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
        {
            return new ObservableTuple<T1, T2>(item1, item2);
        }

        public static ObservableTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
        {
            return new ObservableTuple<T1, T2, T3>(item1, item2, item3);
        }

        public static ObservableTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            return new ObservableTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

    }
}
