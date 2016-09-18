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
    /// Interaction logic for 应收停车费汇总表.xaml
    /// </summary>
    public partial class 应收停车费汇总表 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ParkingFeeDetailViewModel), typeof(应收停车费汇总表), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public ParkingFeeDetailViewModel ViewModel
        {
            get { return (ParkingFeeDetailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 应收停车费汇总表()
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
            应收停车费汇总表 @this = (应收停车费汇总表)d;
            ParkingFeeDetailViewModel viewModel = args.NewValue as ParkingFeeDetailViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        private void View_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedRow != null;
        }

        private void View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  查看汇总详情
            if (ViewModel.SelectedRow != null)
            {
                ViewParkingLotRentalInfoGatheringDialog dialog = new ViewParkingLotRentalInfoGatheringDialog
                {
                    Owner = Window.GetWindow(this),
                    ParkingLotRentalInfoDetailTbl = ViewModel.GetParkingLotRentalInfoDetailTbl()
                };
                dialog.ShowDialog();
            }
        }

        #endregion

        #region Event handlers

        private void listViewParkingLotRentalInfoGatheringTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                ViewModel.SelectedRow = row;
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void ParkingLotRentalInfoGathering_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  根据权限, 浏览汇总的详细信息
            //  查看汇总详情
            if (ViewModel.SelectedRow != null)
            {
                ViewParkingLotRentalInfoGatheringDialog dialog = new ViewParkingLotRentalInfoGatheringDialog
                {
                    Owner = Window.GetWindow(this),
                    ParkingLotRentalInfoDetailTbl = ViewModel.GetParkingLotRentalInfoDetailTbl()
                };
                dialog.ShowDialog();
            }
        }

        private string _moduleName = "应收停车费汇总表";
        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.ParkingLotRentalInfoGatheringTbl, _moduleName);

        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.ParkingLotRentalInfoGatheringTbl, _moduleName);
        }
        #endregion

        //  TODO

        #endregion
    }
}
