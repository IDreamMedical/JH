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
    /// Interaction logic for 月度收支汇总表.xaml
    /// </summary>
    public partial class WpfMonthIOSummary : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IOSummaryOfMonthViewModel), typeof(WpfMonthIOSummary));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public IOSummaryOfMonthViewModel ViewModel
        {
            get { return (IOSummaryOfMonthViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfMonthIOSummary()
        {
            InitializeComponent();
        }

        #endregion

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {

        }

        #region Methods

        //  TODO

        #endregion
    }
}
