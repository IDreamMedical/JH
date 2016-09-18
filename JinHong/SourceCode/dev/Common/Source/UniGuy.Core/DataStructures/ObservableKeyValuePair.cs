using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UniGuy.Core.DataStructures
{
    /// <summary>
    /// 一个值可供重新设置(Mutable[这是class，和KeyValuePair的struct不同])和观察的KeyValuePair
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableKeyValuePair<TKey, TValue>:INotifyPropertyChanged where TValue:IEquatable<TValue>
    {
        private readonly TKey key;
        private TValue value;
        public ObservableKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public TKey Key
        {
            get
            {
                return this.key;
            }
        }
        public TValue Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (this.Key != null)
            {
                builder.Append(this.Key.ToString());
            }
            builder.Append(", ");
            if (this.Value != null)
            {
                builder.Append(this.Value.ToString());
            }
            builder.Append(']');
            return builder.ToString();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(propertyName));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
