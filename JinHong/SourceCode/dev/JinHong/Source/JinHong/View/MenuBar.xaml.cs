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
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(MenuItemViewModel), typeof(MenuBar));

        #endregion

        #region Properties

        public MenuItemViewModel ViewModel
        {
            get { return (MenuItemViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        #endregion

        #region Constructors

        public MenuBar()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                MenuItemViewModel menuItemViewModel = fe.DataContext as MenuItemViewModel;
                if (menuItemViewModel != null)
                    ViewModel.Root.ParentVM.VisitPage(menuItemViewModel);
            }
        }

        #endregion

        #endregion
    }
}
