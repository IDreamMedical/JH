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
    public partial class WpfDayIODetail : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(IODetailOfDayViewModel), typeof(WpfDayIODetail), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public IODetailOfDayViewModel ViewModel
        {
            get { return (IODetailOfDayViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfDayIODetail()
        {
            InitializeComponent();
            if (true)
            {

            }
            else
            {
            }
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
            WpfDayIODetail @this = (WpfDayIODetail)d;
            IODetailOfDayViewModel viewModel = args.NewValue as IODetailOfDayViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers
        /*
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
        */

        #endregion

        #region Event handlers

        /*
        private void listViewDailyIncomeInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedDailyIncomeInfo = row.BuildEntity<XXX>();
            }
        }*/

        private void dataGridDailyIncomeInfoTbl_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridDailyIncomeInfoTbl.CurrentItem is DataRow)
                ViewModel.SelectedIncomeInfoRow = (DataRow)dataGridDailyIncomeInfoTbl.CurrentItem;
            else if (dataGridDailyIncomeInfoTbl.CurrentItem is DataRowView)
                ViewModel.SelectedIncomeInfoRow = ((DataRowView)dataGridDailyIncomeInfoTbl.CurrentItem).Row;
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        //  TODO 界面仿抄电表数设计, 双击数据转到对应费用收支登记界面的对话框修改, 然后把值
        private void dataGridDailyIncomeInfoTbl_PreparingCellForEdit(object sender, DataGridPreparingCellForEditEventArgs e)
        {
            //   TODO...根据e中的行和列确定费用具体信息, 弹对话框修改
            string columnName = e.Column.Header as string;
            if (string.IsNullOrEmpty(columnName))
                return;

            switch (columnName)
            {
                //  TODO, 改列名
                case "RentalFees":
                    {
                        string rentalFeesInfoId = ViewModel.SelectedIncomeInfoRow["RentalFeesInfoId"].ToStringEx();
                        double? amount = ViewModel.SelectedIncomeInfoRow["Amount"] is DBNull ? null : (double?)ViewModel.SelectedIncomeInfoRow["Amount"].ConvertTo(typeof(double));
                        string notes = ViewModel.SelectedIncomeInfoRow["Notes"].ToStringEx();
                        EditRentalFeesDialog dialog = new EditRentalFeesDialog { Owner = Window.GetWindow(this), Amount = amount, Notes = notes };
                        dialog.ShowDialog();
                        e.EditingEventArgs.Handled = true;
                    }
                    break;
                case "DepositFees":
                    {
                        string depositFeesInfoId = ViewModel.SelectedIncomeInfoRow["DepositFeesInfoId"].ToStringEx();
                        double? amount = ViewModel.SelectedIncomeInfoRow["Amount"] is DBNull ? null : (double?)ViewModel.SelectedIncomeInfoRow["Amount"].ConvertTo(typeof(double));
                        string notes = ViewModel.SelectedIncomeInfoRow["Notes"].ToStringEx();
                        EditDepositFeesDialog dialog = new EditDepositFeesDialog { Owner = Window.GetWindow(this), Amount = amount, Notes = notes };
                        dialog.ShowDialog();
                        e.EditingEventArgs.Handled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private string _moduleName = "收入日报表";
        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.DailyIncomeInfoTbl, _moduleName);
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.DailyIncomeInfoTbl, _moduleName);

        }

        #endregion


        private void EditIncome_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EditIncome_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void AddIncome_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void AddIncome_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddFeesDialog addFeeDialog = new AddFeesDialog()
            {
                Owner = Window.GetWindow(this),
                AvailableSocialUnits = GlobalVariables.Smc.Select(string.Format("SELECT * FROM SocialUnitInfo  a  where  a.Id  not IN( select  SocialUnitId from FeeRecord where SUBSTR(PayDate,1,10) ='{0}') and Status='0'", DateTime.Today.ToString("yyyy-MM-dd"))).Tables[0],
                FeeRecords = new FeeRecord() { Id = Guid.NewGuid().ToString() },
                DepositFees = new DepositFeeInfo() { Id = Guid.NewGuid().ToString() },
                PropertyManagementFees = new PropertyManagementFeesInfo() { Id = Guid.NewGuid().ToString() },
                RentalFees = new RentalFeesInfo() { Id = Guid.NewGuid().ToString() },
                ParkinglFees = new ParkingFeesInfo() { Id = Guid.NewGuid().ToString() }
            };
            if (addFeeDialog.ShowDialog().GetValueOrDefault())
            {
            }
        }

        private void AddDepositFee_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void AddDepositFee_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddDepositFeesDialog adFDialog = new AddDepositFeesDialog()
            {

                Owner = Window.GetWindow(this),
                //CurrentDepositFee = new DepositFeeInfo() { Id = Guid.NewGuid().ToString() },
                //AvailableSocialUnits = GlobalVariables.Smc.Select("select *from SocialUnitInfo b  where b .Id not in ( SELECT   IFNULL(SocialUnitId ,'')from  DepositFeesInfo   )  and  Status=0;").Tables[0],
            };
            if (adFDialog.ShowDialog().GetValueOrDefault())
            {

            }


        }

        private void AddRentalFee_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = true;
        }

        private void AddRentalFee_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddRentalFeesDialog adFDialog = new AddRentalFeesDialog()
            {

                Owner = Window.GetWindow(this),
                RentalFees = new RentalFeesInfo() { Id = Guid.NewGuid().ToString() },
                AvailableSocialUnits = GlobalVariables.Smc.Select("select *from SocialUnitInfo b  where b .Id not in ( SELECT   IFNULL(SocialUnitId ,'')from  RentalFeesInfo   )  and  Status=0;").Tables[0],
            };
            if (adFDialog.ShowDialog().GetValueOrDefault())
            {

            }

        }

        private void AddParkingFee_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void AddParkingFee_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void AddPropertyManagementFee_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void AddPropertyManagementFee_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }


        //  TODO

        #endregion
    }
}
