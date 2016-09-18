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
using JinHong.ViewModel;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(StartPageViewModel), typeof(StartPage));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public StartPageViewModel ViewModel
        {
            get { return (StartPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public StartPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
