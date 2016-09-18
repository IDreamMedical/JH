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
    ///  消防资料.xaml
    /// </summary>
    public partial class WpfFire : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(FireViewModel), typeof(WpfFire), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public FireViewModel ViewModel
        {
            get { return (FireViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public WpfFire()
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
            ViewModel.Query(queryStr,() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }


        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            WpfFire @this = (WpfFire)d;
            FireViewModel viewModel = args.NewValue as FireViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Add
            AddFireFightingEquipmentInfoDialog ffeied = new AddFireFightingEquipmentInfoDialog { Owner = Window.GetWindow(this), Mode = AddFireFightingEquipmentInfoDialog.EditingMode.Add };
            ffeied.FireFightingEquipmentInfo = new FireFightingEquipmentInfo(Guid.NewGuid().ToString());
            if (ffeied.ShowDialog().GetValueOrDefault())
            {
                GlobalVariables.Smc.Insert<FireFightingEquipmentInfo>(ffeied.FireFightingEquipmentInfo);
                Query();
            }
        }

        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedFireFightingEquipmentInfo != null;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Edit
            FireFightingEquipmentInfoEditDialog ffeied = new FireFightingEquipmentInfoEditDialog { Owner = Window.GetWindow(this), Mode = FireFightingEquipmentInfoEditDialog.EditingMode.Modify };
            ffeied.FireFightingEquipmentInfo = ViewModel.SelectedFireFightingEquipmentInfo;
            if (ffeied.ShowDialog().GetValueOrDefault())
            {
                GlobalVariables.Smc.Update<FireFightingEquipmentInfo>(ffeied.FireFightingEquipmentInfo);
                Query();
            }
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedFireFightingEquipmentInfo != null;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Delete
            if (MessageBox.Show(Window.GetWindow(this), "确定删除吗?", "删除", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.OK) == MessageBoxResult.OK)
            {
                GlobalVariables.Smc.Delete<FireFightingEquipmentInfo>(ViewModel.SelectedFireFightingEquipmentInfo.Id);
                Query();
            }
        }

        #endregion

        #region Event handlers

        private void listViewFireFightingEquipmentInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedFireFightingEquipmentInfo = row.BuildEntity<FireFightingEquipmentInfo>();
            }
        }


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

        private string _moduleName = "消防资料";

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.FireFightingEquipmentInfoTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.FireFightingEquipmentInfoTbl, _moduleName);

        }

        #endregion


        #endregion
    }
}
