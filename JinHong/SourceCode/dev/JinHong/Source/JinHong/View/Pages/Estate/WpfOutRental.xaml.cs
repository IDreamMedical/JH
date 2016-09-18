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
using System.Windows.Threading;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 租赁户退租表.xaml
    /// </summary>
    public partial class WpfOutRental : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(OutRentaledViewModel), typeof(WpfOutRental), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private string _moduleName = "退租流程展示表";

        #endregion

        #region Properties

        public OutRentaledViewModel ViewModel
        {
            get { return (OutRentaledViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfOutRental()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region General

        private void Query()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            //ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        #endregion

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            WpfOutRental @this = (WpfOutRental)d;
            CheckInViewModel viewModel = args.NewValue as CheckInViewModel;
            if (viewModel != null)
                @this.Query();
        }
        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
           // GlobalVariables.ExportHelper.ExportToExcel(ViewModel.LeasingInfoTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            //GlobalVariables.ExportHelper.ExportToPdf(ViewModel.LeasingInfoTbl, _moduleName);
        }



        #endregion

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            //ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }
    }
}
