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
using System.Diagnostics;
using System.Threading.Tasks;
using JinHong.BaseUserControl;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for InLease.xaml
    /// </summary>
    public partial class InLease : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(InRentaledVM), typeof(InLease), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private string _moduleName = "租赁户入住表";

        #endregion

        #region Properties

        public InRentaledVM ViewModel
        {
            get { return (InRentaledVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public InLease()
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
            InLease @this = (InLease)d;
            InRentaledVM viewModel = args.NewValue as InRentaledVM;
            if (viewModel != null)
                @this.Query();
        }

        #endregion


        #region Command handlers

        //  View
        private void View_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedLeasingInfo != null;
        }

        private void View_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            throw new NotImplementedException();
        }

        //  Edit
        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedLeasingInfo != null;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            throw new NotImplementedException();
        }

        //  入租
        private void Register_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = true;
        }

        private void Register_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            AddCheckInDialog dialog = new AddCheckInDialog
            {
                Owner = Window.GetWindow(this),
                LeasingInfo = new WpfCheckIn(Guid.NewGuid().ToString()) { LeaseBeginDate = DateTime.Today },
                AvailableSocialUnits = ViewModel.AvailableSocialUnitTbl

            };
            if (ViewModel.AvailableSocialUnitTbl == null || ViewModel.AvailableSocialUnitTbl.Rows.Count <= 0)
            {
                MessageBox.Show("您没有需要做入租登记的客户！如需新增，请按合同管理-》缴纳押金-》入租流程操作！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
                return;
            }
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                dialog.LeasingInfo.Add();
                Query();    //  刷新
            }
        }

        //  退租
        private void Deregister_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedLeasingInfo != null && !ViewModel.SelectedLeasingInfo.LeaseEndDate.HasValue;
        }

        private void Deregister_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO
            //  获得对应的租赁信息
            // LeasingInfo leasingInfo = GlobalVariables.Smc.Load<LeasingInfo>(ViewModel.SelectedLeasingInfo.Id);
            //Debug.Assert(leasingInfo != null);
            LeaseCancelDialog dialog = new LeaseCancelDialog
            {

                Owner = Window.GetWindow(this)
            };
            var ds = GlobalVariables.Smc.Select(String.Format(@"select a.Id, a.SocialUnitId,d.Name as SocialUnitName ,a.IsExpirate as IsExpired , a.Notes,
                                                              a.UnLeaseName,a.WYEmpName,a.ZSEmpName,
                                                              b.EffectiveDate ,b.ExpirateDate, b.ContractNo,
                                                              c.BuildingId,c.RoomId,f.Name as RoomName,e.Name as BuildingName,  c.Area,c.MonthPropManageFee,c.MonthRentalFee,
                                                              (select  MAX (TimeTo)from RentalFeesInfo  where SocialUnitId='{0}') as RentalFeeEndDate,
                                                             (select  MAX (TimeTo)from PropertyManagementFeesInfo where SocialUnitId='{0}' ) as PropertyManagementFeesEndDate
                                                             from LeasingInfo   a 
                                                              INNER JOIN ContractInfo b on a.SocialUnitId=b.SocialUnitId
                                                              INNER JOIN ContractDetail c  on c.ContractId=b.Id
                                                              INNER  JOIN SocialUnitInfo  d on d.Id=a.SocialUnitId
                                                              INNER  JOIN BuildingInfo  e on e.Id=a.BuildingId
                                                              INNER  JOIN RoomInfo  f on f.Id=a.RoomId
                                                                 where a.Id='{0}';
                                                            select MAX(a.TimeTo) as ParkingFeesInfoEndDate, c.Price as ParkingFee from   ParkingFeesInfo a
                                                            INNER join  ParkingLotRentalInfo  b  on a.ParkingLotRentalInfoId=b.Id
                                                            INNER join  ParkingLotInfo  c on c.Id=b.ParkingLotId
                                                            where b.SocialUnitId='{0}';"
                                                             , ViewModel.SelectedLeasingInfo.Id));
            if (null == ds || ds.Tables.Count <= 0)
            {
                MessageBox.Show("构造数据出错！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return;
            }

            if (ds.Tables[0].Rows.Count<=0)
            {
                MessageBox.Show("没有数据！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                return;
            }
            dialog.CurrentUnLeaseDetail = ds.Tables[0].Rows[0].BuildEntity<UnLeaseDetail>();
            dialog.CurrentUnLeaseVM = new OutRentaledVM()
            {

                SocalUnitName = ds.Tables[0].Rows[0]["SocialUnitName"] + "",
                ContractStartDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["EffectiveDate"]),
                ContractEndDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirateDate"]),
                RentalFee = Convert.ToDouble(ds.Tables[0].Rows[0]["MonthRentalFee"]),
                Area = Convert.ToInt32(ds.Tables[0].Rows[0]["Area"]),
                PropertyManagementFee = Convert.ToDouble(ds.Tables[0].Rows[0]["MonthPropManageFee"]),
                Buliding = ds.Tables[0].Rows[0]["BuildingName"] + "" + ds.Tables[0].Rows[0]["RoomName"] + ""
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Query();
            }
        }

        #endregion

        #region Event handlers

        private void listViewLeasingInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedLeasingInfo = row.BuildEntity<WpfCheckIn>();
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        private void LeasingInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  TODO...根据权限View
            //LeasingStatusViewDialog dialog = new LeasingStatusViewDialog { Owner = Window.GetWindow(this) };//  TODO, 设置多个依赖属性, SocialUnit, LeasingInfo, ContractActivity集合等
            //dialog.SocialUnitInfo = GlobalVariables.Smc.Load<SocialUnitInfo>(ViewModel.SelectedLeasingStatusInfo.SocialUnitId);
            //dialog.ShowDialog();
        }

        #endregion

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.LeasingInfoTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.LeasingInfoTbl, _moduleName);

        }

        //  TODO

        #endregion
    }
}
