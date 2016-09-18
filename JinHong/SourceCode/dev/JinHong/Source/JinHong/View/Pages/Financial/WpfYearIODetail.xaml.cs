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
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using UniGuy.Core.Extensions;
using UniGuy.Report;
using System.Windows.Threading;
using JinHong.Model;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 年度收支明细表.xaml
    /// </summary>
    public partial class WpfYearIODetail : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IODetailOfYearViewModel), typeof(WpfYearIODetail), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private const string _moduleName = "年度收支明细表";


        #endregion

        #region Properties

        public IODetailOfYearViewModel ViewModel
        {
            get { return (IODetailOfYearViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfYearIODetail()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region General

        private void Query()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            WpfYearIODetail @this = (WpfYearIODetail)d;
            IODetailOfYearViewModel viewModel = args.NewValue as IODetailOfYearViewModel;
            if (viewModel != null)
            {
                @this.Query();
            }
        }

        #endregion

        #region Event handlers

        private void listViewIncomeAndExpenditureGatherTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  TODO
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.IncomeAndExpenditureGatherTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.IncomeAndExpenditureGatherTbl, _moduleName);

        }

        #endregion

        private void dataGridMonthFee_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataRowView drv = e.Row.Item as DataRowView;
            double feeValue = 0;
            if (double.TryParse(((TextBox)e.EditingElement).Text, out feeValue))
            {

                MonthFeeDetail temp = new MonthFeeDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    ItemId = drv["Id"] + "",
                    Amount = feeValue,
                    TypeId = 2,
                    Month =15,
                    RcdDate = DateTime.Now
                };


                ViewModel.SaveFeeValue(temp);
            }
        }

        //  TODO

        #endregion
    }
}
