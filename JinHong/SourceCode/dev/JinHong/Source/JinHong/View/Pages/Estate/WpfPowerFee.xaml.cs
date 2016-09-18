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
    /// Interaction logic for 水电费收费清单.xaml
    /// </summary>
    public partial class WpfPowerFee : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(PowerFeeDetailViewModel), typeof(WpfPowerFee), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields
        private string _moduleName = "水电费收费清单";

        //  TODO

        #endregion

        #region Properties

        public PowerFeeDetailViewModel ViewModel
        {
            get { return (PowerFeeDetailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfPowerFee()
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
            WpfPowerFee @this = (WpfPowerFee)d;
            PowerFeeDetailViewModel viewModel = args.NewValue as PowerFeeDetailViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        private void ViewFees_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedWaterAndElectricityFeesInfo != null;
        }

        private void ViewFees_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...查看交费详情
            if (ViewModel.SelectedWaterAndElectricityFeesInfo != null)
            {
                EditWaterAndElectricityFeesDialog dialog = new EditWaterAndElectricityFeesDialog
                {
                    Owner = Window.GetWindow(this),
                    Mode = EditWaterAndElectricityFeesDialog.EditingMode.View,
                    WaterAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.WaterAmount,
                    ElectricityAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.ElectricityAmount,
                    Notes = ViewModel.SelectedWaterAndElectricityFeesInfo.Notes
                };
                dialog.ShowDialog();
            }
        }

        private void EditFees_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedWaterAndElectricityFeesInfo != null;
        }

        private void EditFees_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Edit, 登记交费信息
            if (ViewModel.SelectedWaterAndElectricityFeesInfo == null)
            {
                ViewModel.SelectedWaterAndElectricityFeesInfo = new MonthlyWaterAndElectricityFeesInfo(Guid.NewGuid().ToString())
                {
                    Date = ViewModel.WhereDate,
                    LeasingInfoId = ViewModel.SelectedRow["LeasingInfoId"].ToStringEx(),
                    SocialUnitName = ViewModel.SelectedRow["SocialUnitName"].ToStringEx()
                };
            }
            EditWaterAndElectricityFeesDialog dialog = new EditWaterAndElectricityFeesDialog
            {
                //  TODO...还要设置IsDepositNeeded, 根据是否是第一个月
                Owner = Window.GetWindow(this),
                WaterAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.WaterAmount,
                ElectricityAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.ElectricityAmount,
                Notes = ViewModel.SelectedWaterAndElectricityFeesInfo.Notes
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                if ((dialog.WaterAmount.HasValue && dialog.WaterAmount.Value != 0) || (dialog.ElectricityAmount.HasValue && dialog.ElectricityAmount.Value != 0))
                {
                    ViewModel.SelectedWaterAndElectricityFeesInfo.WaterAmount = dialog.WaterAmount.Value;
                    ViewModel.SelectedWaterAndElectricityFeesInfo.ElectricityAmount = dialog.ElectricityAmount.Value;
                    ViewModel.SelectedWaterAndElectricityFeesInfo.Notes = dialog.Notes;
                    GlobalVariables.Smc.Save2<MonthlyWaterAndElectricityFeesInfo>(ViewModel.SelectedWaterAndElectricityFeesInfo);
                }
                Query();
            }
        }

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
                ViewModel.SelectedRow = row;
                if (row != null)
                {
                    ViewModel.SelectedWaterAndElectricityFeesInfo = row.BuildEntity<MonthlyWaterAndElectricityFeesInfo>();
                }
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private void WaterAndElectricityFeesInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  TODO...根据权限, 浏览或者登记 交费信息
            //  TODO...显示对话框来做这个事情
            if (ViewModel.SelectedWaterAndElectricityFeesInfo != null)
            {
                EditWaterAndElectricityFeesDialog dialog = new EditWaterAndElectricityFeesDialog
                {
                    Owner = Window.GetWindow(this),
                    Mode = EditWaterAndElectricityFeesDialog.EditingMode.View,
                    WaterAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.WaterAmount,
                    ElectricityAmount = ViewModel.SelectedWaterAndElectricityFeesInfo.ElectricityAmount,
                    Notes = ViewModel.SelectedWaterAndElectricityFeesInfo.Notes
                };
                dialog.ShowDialog();
            }
        }



        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.WaterAndElectricityFeesInfoTbl, _moduleName);

        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.WaterAndElectricityFeesInfoTbl, _moduleName);

        }

        #endregion

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddWaterFeeDialog dialog = new AddWaterFeeDialog
            {
                Owner = Window.GetWindow(this),
                AvailableSocialUnits = ViewModel.AvailableSocialUnitTbl

            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Query();    //  刷新
            }

        }



        #endregion
    }
}
