using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using UniGuy.Core;
using UniGuy.Core.Data;
using System.Xml;
using System.Xml.Serialization;

namespace UniGuy.Entity
{
    [Serializable]
    public abstract class EntityBase :  INotifyPropertyChanged
    {
        // Events
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        // Methods
        protected EntityBase()
        {
        }

        /// <summary>
        /// 继承类加特性[OnDeserialized]
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnDeserialized(StreamingContext context)
        {
            //  None now.
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
