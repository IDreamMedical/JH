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

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 仓库租赁情况.xaml
    /// </summary>
    public partial class RentalWareHouse : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(RentalWareHouseVM), typeof(RentalWareHouse), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private string _moduleName = "仓库租赁情况";


        #endregion

        #region Properties

        public RentalWareHouseVM ViewModel
        {
            get { return (RentalWareHouseVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public RentalWareHouse()
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

        private void Query(string queryStr)
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(queryStr, () => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        #endregion

        #region Overrides

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //  TODO
        }

        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            RentalWareHouse @this = (RentalWareHouse)d;
            RentalWareHouseVM viewModel = args.NewValue as RentalWareHouseVM;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        private void View_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedWareHouseLeasingInfo != null;
        }

        private void View_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            WareHouseLeasingStatusDialog dialog = new WareHouseLeasingStatusDialog { Owner = Window.GetWindow(this) };
            dialog.SocialUnitInfo = GlobalVariables.Smc.Load<SocialUnitInfo>(ViewModel.SelectedWareHouseLeasingInfo.SocialUnitId);
            dialog.ShowDialog();
        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = true;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Edit
            throw new NotImplementedException();
        }

        //  TODO 入租
        private void Register_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = true;
        }

        private void Register_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WareHouseLeaseAddDialog dialog = new WareHouseLeaseAddDialog
            {
                Owner = Window.GetWindow(this),
                AvailableSocialUnits = ViewModel.AvailableSocialUnitTbl,
                AvailableWareHouseIds = ViewModel.AvailableWareHouseTbl,
                CurrentContractInfo = new ContractInfo() { Id = Guid.NewGuid().ToString() },
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Query();
            }
        }

        //  TODO 退租
        private void Deregister_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedWareHouseLeasingInfo != null;
        }

        private void Deregister_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  获得对应的租赁信息
            //WareHouseLeasingInfo leasingInfo = 
           // Debug.Assert(leasingInfo != null);
            WareHouseLeaseCancelDialog dialog = new WareHouseLeaseCancelDialog
            {
                Owner = Window.GetWindow(this),
                //LeasingInfo = leasingInfo
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Query();    //  刷新
            }
        }

        #endregion

        #region Event handlers

        private void listViewWareHouseLeasingStatusInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedWareHouseLeasingInfo = row.BuildEntity<WareHouseLeasingInfo>();
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null && !string.IsNullOrEmpty(ViewModel.WhereName))
            {
                Query(ViewModel.WhereName);
            }
            else
            {
                Query();
            }
        }

        private void WareHouseLeasingStatusInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  TODO...根据权限View or Edit
            if (ViewModel.SelectedWareHouseLeasingInfo == null)
                return;
            WareHouseLeasingStatusDialog dialog = new WareHouseLeasingStatusDialog { Owner = Window.GetWindow(this) };//  TODO, 设置多个依赖属性, SocialUnit, LeasingInfo, ContractActivity集合等
            dialog.SocialUnitInfo = GlobalVariables.Smc.Load<SocialUnitInfo>(ViewModel.SelectedWareHouseLeasingInfo.SocialUnitId);
            dialog.ShowDialog();
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.WareHouseLeasingInfoTbl, _moduleName);
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.WareHouseLeasingInfoTbl, _moduleName);
        }

        //  TODO

        #endregion

        //  TODO

        #endregion
    }
}
