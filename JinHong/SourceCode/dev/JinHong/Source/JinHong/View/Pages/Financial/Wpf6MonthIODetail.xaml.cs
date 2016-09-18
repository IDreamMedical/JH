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
    /// Interaction logic for 半年度收支明细表.xaml
    /// </summary>
    public partial class Wpf6MonthIODetail : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IODetailOf6MonthViewModel), typeof(Wpf6MonthIODetail), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private const string _moduleName = "半年度收支明细表";


        #endregion

        #region Properties

        public IODetailOf6MonthViewModel ViewModel
        {
            get { return (IODetailOf6MonthViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public Wpf6MonthIODetail()
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
            Wpf6MonthIODetail @this = (Wpf6MonthIODetail)d;
            IODetailOf6MonthViewModel viewModel = args.NewValue as IODetailOf6MonthViewModel;
            if (viewModel != null)
            {
                @this.Query();

                if (viewModel.WhereIsFirstHalf)
                    @this.radioButtonIsFirstHalf.IsChecked = true;
                else
                    @this.radioButtonIsSecondHalf.IsChecked = true;
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

        private void radioButtonIsFirstHalf_Checked(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.WhereIsFirstHalf = true;
                Query();
            }
        }

        private void radioButtonIsFirstHalf_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.WhereIsFirstHalf = false;
                Query();
            }
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
                    TypeId = 1,
                    Month = 13,
                    RcdDate = DateTime.Now
                };
                if (!ViewModel.WhereIsFirstHalf)
                {
                    temp.Month = 14;

                }
                ViewModel.SaveFeeValue(temp);
            }
        }

        //  TODO

        #endregion
    }
}
