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
using JinHong.View.Dialogs;
using UniGuy.Report;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using UniGuy.Core.Extensions;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 水电费总厂开票一览表.xaml
    /// </summary>
    public partial class WpfPowerFeeInvoice : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(PowerFeeResultViewModel), typeof(WpfPowerFeeInvoice), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public PowerFeeResultViewModel ViewModel
        {
            get { return (PowerFeeResultViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfPowerFeeInvoice()
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
            WpfPowerFeeInvoice @this = (WpfPowerFeeInvoice)d;
            PowerFeeResultViewModel viewModel = args.NewValue as PowerFeeResultViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        //  TODO

        #endregion

        #region Event handlers

        private void listViewWaterAndElectricityFeesInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                {
                    string monthlyWaterAndElectricityFeesInfoId = row["MonthlyWaterAndElectricityFeesInfoId"].ToStringEx();
                    ViewModel.SelectedWaterAndElectricityFeesInfo = monthlyWaterAndElectricityFeesInfoId == null ? null : GlobalVariables.Smc.Load<MonthlyWaterAndElectricityFeesInfo>(monthlyWaterAndElectricityFeesInfoId);
                }
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }


        private string _moduleName = "水电费总厂开票一览";
        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.WaterAndElectricityFeesInfoTbl, _moduleName);
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.WaterAndElectricityFeesInfoTbl, _moduleName);

        }

        //  TODO

        #endregion

        //  TODO

        #endregion
    }
}
