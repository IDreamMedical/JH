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
using System.Diagnostics;
using System.IO;
using JinHong.ViewModel;
using JinHong.Model;
using Microsoft.Win32;
using JinHong.View.Dialogs;
using System.IO.Compression;
using JinHong.Helper;
using pt = System.IO.Path;
using JinHong.Extensions;
using System.Data;
using System.Windows.Threading;
using UniGuy.Core.Extensions;



namespace JinHong.View
{
    /// <summary>
    /// 预租赁列表
    /// </summary>
    public partial class PreContractList : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(UnContractListVM), typeof(PreContractList), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields
        private string _moduleName = "UnContractList";

        #endregion

        #region Properties

        public UnContractListVM ViewModel
        {
            get { return (UnContractListVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public PreContractList()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            PreContractList @this = (PreContractList)d;
            UnContractListVM viewModel = args.NewValue as UnContractListVM;
            if (viewModel != null)
            {
                @this.Query();
                //viewModel.ContractViewRequired += (o, a) => @this.ViewContract(viewModel.ViewedContractFile);
                //if (!string.IsNullOrEmpty(viewModel.ViewedContractFile))
                //    viewModel.ViewContractFile(viewModel.ViewedContractFile);
            }
        }

        #endregion

        #region Command handlers
        private void Remove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel != null && ViewModel.SelectedContractInfo != null;
        }

        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (ViewModel.SelectedContractInfo.PreDate.CompareTo(DateTime.Today) >= 0)
            {
                MessageBox.Show("已交押金，不能删除！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
                return;
            }

            if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                SocialUnitInfo sui = new SocialUnitInfo()
                {
                    Id = ViewModel.SelectedContractInfo.SocialUnitId,
                    Status = "1"
                };
                GlobalVariables.Smc.Update<SocialUnitInfo>(sui);
                var row = listViewContractTbl.SelectedValue as DataRowView;

                if (row != null)
                {
                    ViewModel.UnContractListTbl.Rows.Remove(row.Row);
                    listViewContractTbl.Items.Refresh();
                }
                Query();
            }


        }


        #endregion

        #region Event handlers


        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
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

        private void listViewContractTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                ViewModel.SelectedContractInfo = ViewModel.ToModel(row);
                ViewModel.ContractNO = new GenerateCodeHelper().CreateReportCode();
            }

        }

        private void ContractTbl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //  根据权限, 浏览汇总的详细信息
            //  查看汇总详情
            if (ViewModel.SelectedContractInfo != null)
            {
                ViewContractDetailDialog dialog = new ViewContractDetailDialog
                {
                    Owner = Window.GetWindow(this),
                    ContractDetailTbl = ViewModel.GetContractDetailTbl(),
                    SocialUnitName = ViewModel.SelectedContractInfo.SocialUnitName

                };
                dialog.ShowDialog();
            }
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.UnContractListTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.UnContractListTbl, _moduleName);

        }


        #endregion

        #endregion

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

        private void AddRoomContract_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel != null && ViewModel.SelectedContractInfo != null;

        }

        private void AddRoomContract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //NewOrEditContract acid = new NewOrEditContract()
            //{
            //    Owner = Window.GetWindow(this),
            //    CurrentContractInfo = ViewModel.SelectedContractInfo,
            //};
            //acid.CurrentContractInfo.ContractNo = ViewModel.ContractNO;
            //if (acid.AvailableRooms.Rows.Count <= 0)
            //{
            //    MessageBox.Show(Window.GetWindow(this), "房间已租完！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            //    return;

            //}
            //if (acid.ShowDialog().GetValueOrDefault())
            //{
            //    ViewModel.Save(acid.CurrentContractInfo, acid.ResultContractDetails, acid.txtFileName.Text.Trim());
            //    Query();

            //}

        }

        private void AddWareHouseContract_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ViewModel != null && ViewModel.SelectedContractInfo != null;
        }

        private void AddWareHouseContract_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WareHouseLeaseAddDialog dialog = new WareHouseLeaseAddDialog
            {
                Owner = Window.GetWindow(this),
                CurrentContractInfo = ViewModel.SelectedContractInfo
            };
            dialog.CurrentContractInfo.ContractNo = ViewModel.ContractNO;
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                Query();
            }

        }




    }
}
