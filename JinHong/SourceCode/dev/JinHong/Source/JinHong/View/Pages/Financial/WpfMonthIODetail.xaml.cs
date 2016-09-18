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
    /// Interaction logic for 月度收支明细表.xaml
    /// </summary>
    public partial class WpfMonthIODetail : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IODetailOfMonthViewModel), typeof(WpfMonthIODetail), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public IODetailOfMonthViewModel ViewModel
        {
            get { return (IODetailOfMonthViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfMonthIODetail()
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
            WpfMonthIODetail @this = (WpfMonthIODetail)d;
            IODetailOfMonthViewModel viewModel = args.NewValue as IODetailOfMonthViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Event handlers

        private void listViewIncomeAndExpenditureGatherTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;

                //  TODO
            }*/
        }

        private void buttonGather_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }


        private string _moduleName = "月度收支明细表";
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
            double powerValue = 0;
            if (double.TryParse(((TextBox)e.EditingElement).Text, out powerValue))
            {

                MonthFeeDetail temp = new MonthFeeDetail()
                {
                    Id = Guid.NewGuid().ToString(),
                    ItemId = drv["Id"] + "",
                    Amount = powerValue,
                    TypeId = 0,
                    Month = DateTime.Today.Month,
                    RcdDate = DateTime.Now
                };


                ViewModel.SaveFeeValue(temp);
            }
            //}
        }

        //  TODO

        #endregion
    }
}
