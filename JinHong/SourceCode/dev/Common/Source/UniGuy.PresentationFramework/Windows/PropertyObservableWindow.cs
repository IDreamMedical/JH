using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace UniGuy.Windows
{
    /// <summary>
    /// 一个实现INotifyPropertyChanged的Window
    /// ----
    /// 在M-V-VM Pattern中, 每个View会包含自己的ViewModel; 比如一个MainWindow上放了一个MainMenu, MainToolBar, MainStatusBar, 其对应的ViewModel分别为MainWindowViewModel, MainMenuViewModel等;
    /// 当然MainMenuViewModel等可以简单通过MainWindowViewModel封装;
    /// 为了避免歧义等, 这些ViewModel一般不会直接赋给View的DataContext, 而是需要在View上新增一个ViewModel属性来引用其实例;
    /// 有时候我们不想用额外的DependencyProperty来定义这个ViewModel属性,嫌繁琐, 则可以修改View所继承的父类为此有PropertyObservable_前缀的类型;
    /// 然后可直接用普通CLR属性来定义ViewModel属性.
    /// 这里只包含几个常用的派生父类. 其它类型必须每次自己仿照此方法新增这段代码, 比如上面说的MainMenu:Menu等类型
    /// </summary>
    public class PropertyObservableWindow: Window, INotifyPropertyChanged
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

        #region Constructors
        public PropertyObservableWindow()
            : base()
        {
        }
        #endregion
    }

    public class PropertyObservableControl : Control, INotifyPropertyChanged
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

        #region Constructors
        public PropertyObservableControl()
            : base()
        {
        }
        #endregion
    }

    public class PropertyObservableUserControl : UserControl, INotifyPropertyChanged
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

        #region Constructors
        public PropertyObservableUserControl()
            : base()
        {
        }
        #endregion
    }
}
