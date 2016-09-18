using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using UniGuy.Core;

namespace UniGuy.Core.Model
{
    /// <summary>
    /// Model 基类 The most simple one.
    /// </summary>
    public abstract class ModelBase:IIdObject, INamedObject, INotifyPropertyChanged
    {
        #region Interface implementations
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                this.OnPropertyChanged(property);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        private void VerifyPropertyName(string propertyName)
        {
            if (this.GetType().GetProperty(propertyName) == null)
            {
                throw new Exception(string.Format("The property name:{0} does not exist!!!!", propertyName));
            }
        }
        #endregion  //  Interface implementations

        #region Fields
        private string id;
        private string name;
        private string description;
        #endregion  //  Fields

        #region Properties
        public string Id
        {
            get { return id; }
            protected set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public virtual string Name
        {
            get { return name; }
            internal protected set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public virtual string Description
        {
            get { return description; }
            internal protected set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        #endregion

        #region Constructors

        public ModelBase()
        {
            InitializeModel();
        }

        #endregion  //  Constructors

        #region Methods

        protected virtual void InitializeModel()
        {
        }

        #endregion //   Methods
    }
}
