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

namespace JinHong.View
{
    /// <summary>
    ///  租赁单位安全管理协议表.xaml
    /// </summary>
    public partial class WpfSecurityAgreement : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(SecurityAgreementViewModel), typeof(WpfSecurityAgreement), new PropertyMetadata(OnViewModelPropertyChanged));

        #endregion

        #region Fields

        private string _moduleName = "租赁单位安全管理协议表";


        #endregion

        #region Properties

        public SecurityAgreementViewModel ViewModel
        {
            get { return (SecurityAgreementViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfSecurityAgreement()
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

        #region Callbacks
        private static void OnViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            WpfSecurityAgreement @this = (WpfSecurityAgreement)d;
            SecurityAgreementViewModel viewModel = args.NewValue as SecurityAgreementViewModel;
            if (viewModel != null)
                @this.Query();
        }
        #endregion

        #region Event handlers

        private void listViewSecurityManagementContractInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = e.AddedItems[0] as DataRow;
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedSecurityManagementContractInfo = row.BuildEntity<SecurityManagementContractInfo>();
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        private void SecurityManagementContractInfo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  查看协议
            if (ViewModel.SelectedSecurityManagementContractInfo == null)
                return;
            // ViewModel.ParentVM.VisitPage("合同管理", ViewModel.SelectedSecurityManagementContractInfo.FilePath);
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.SecurityManagementContractInfoTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.SecurityManagementContractInfoTbl, _moduleName);

        }

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AdSMContractInfoDialog acid = new AdSMContractInfoDialog()
            {
                Owner = Window.GetWindow(this),
                AvailableSocialUnits = ViewModel.AvailableSocialUnitsTbl
            };
            if (acid.ShowDialog().GetValueOrDefault())
            {
                Query();
            }

        }

        private void Remove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel != null && ViewModel.SelectedSecurityManagementContractInfo != null;

        }

        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            DataRowView row = listViewSecurityManagementContractInfoTbl.SelectedValue as DataRowView;

            if (ViewModel.SelectedSecurityManagementContractInfo != null)
            {
                if (ViewModel.SelectedSecurityManagementContractInfo.EffectiveDate.Value.CompareTo(DateTime.Today) < 0)
                {
                    MessageBox.Show("已生效，不能删除！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
                    return;
                }

                if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    //0,启用,1。 删除
                    GlobalVariables.Smc.Delete<SecurityManagementContractInfo>(ViewModel.SelectedSecurityManagementContractInfo.Id);
                    if (row != null)
                    {
                        ViewModel.SecurityManagementContractInfoTbl.Rows.Remove(row.Row);
                        listViewSecurityManagementContractInfoTbl.Items.Refresh();
                    }
                    Query();
                }
            }
        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        //  TODO

        #endregion

        //  TODO

        #endregion
    }
}
