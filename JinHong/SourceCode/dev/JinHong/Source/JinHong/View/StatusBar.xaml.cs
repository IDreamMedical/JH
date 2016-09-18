using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Diagnostics;
using JinHong.ViewModel;

namespace JinHong.View
{
    /// <summary>
    /// StatusBar.xaml 的交互逻辑
    /// </summary>
    public partial class StatusBar : System.Windows.Controls.Primitives.StatusBar
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(StatusBarViewModel), typeof(StatusBar), new PropertyMetadata(null, OnViewModelPropertyChanged));

        #endregion

        #region Fields

        #endregion

        #region Properties

        public StatusBarViewModel ViewModel
        {
            get { return (StatusBarViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion

        #region Constructors

        public StatusBar()
        {
            InitializeComponent();
        }

        public StatusBar(StatusBarViewModel viewModel)
            : this()
        {
            this.ViewModel = viewModel;
        }

        #endregion

        #region Methods

        #region Callbacks

        static void OnViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            StatusBar @this = (StatusBar)d;
            //  TODO
        }

        #endregion

        #endregion
    }
}
