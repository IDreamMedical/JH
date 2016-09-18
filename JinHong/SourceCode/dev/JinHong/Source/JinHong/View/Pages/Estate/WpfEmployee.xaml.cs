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
using System.Data;
using JinHong.Extensions;
using System.Windows.Threading;
using UniGuy.Core.Data;
using JinHong.Model;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 退休职工名单.xaml
    /// </summary>
    public partial class WpfEmployee : UserControl
    {
        #region public Properties
        public EmployeeViewModel ViewModel { get; set; }
        #endregion
        #region Constructors

        public WpfEmployee()
        {
            InitializeComponent();
            ViewModel = new EmployeeViewModel(null);
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion

        private void listViewRetiredEmployeeInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
