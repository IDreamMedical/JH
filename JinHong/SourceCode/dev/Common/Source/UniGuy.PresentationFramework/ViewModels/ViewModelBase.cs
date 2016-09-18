using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using UniGuy.Windows.View;
using System.Threading;
using System.Windows.Threading;
using System.Linq.Expressions;
using UniGuy.Corek;


//  wj
//  20110503
namespace UniGuy.Windows.ViewModels
{
    /// <summary>
    /// View Model 基类 The most simple one.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly IView view;


        #region Prop

        public OperateModeEnum OperateMode { get; set; }
        public virtual string ViewModelTitleName { get; set; }
        #endregion

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


        protected bool SetField<T>(ref T field, T value, Expression<Func<T>> selectorExpression)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(selectorExpression);
            return true;
        }

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> selectorExpression)
        {
            if (selectorExpression == null)
                throw new ArgumentNullException("selectorExpression");
            MemberExpression body = selectorExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("The body must be a member expression");
            OnPropertyChanged(body.Member.Name);
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
        #endregion  //  Fields

        #region Constructors

        public ViewModelBase()
        {
            InitializeModel();
            OperateMode = OperateModeEnum.Default;
        }

        #endregion  //  Constructors

        #region Methods

        /// <summary>
        /// 初始化ViewModel
        /// </summary>
        protected virtual void InitializeModel()
        {
        }




        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModel"/> class and attaches itself as <c>DataContext</c> to the view.
        /// </summary>
        /// <param name="view">The view.</param>
        protected ViewModelBase(IView view)
        {
            if (view == null) { throw new ArgumentNullException("view"); }
            this.view = view;

            // Check if the code is running within the WPF application model
            if (SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                // Set DataContext of the view has to be delayed so that the ViewModel can initialize the internal data (e.g. Commands)
                // before the view starts with DataBinding.
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate ()
                {
                    this.view.DataContext = this;
                });
            }
            else
            {
                // When the code runs outside of the WPF application model then we set the DataContext immediately.
                view.DataContext = this;
            }
        }


        /// <summary>
        /// Gets the associated view.
        /// </summary>
        public IView View { get { return view; } }


        #endregion //   Methods
    }
}
