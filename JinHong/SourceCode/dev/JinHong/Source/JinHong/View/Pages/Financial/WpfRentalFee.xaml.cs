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
    /// Interaction logic for RentalFee.xaml
    /// </summary>
    public partial class WpfRentalFee : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(RentalFeeViewModel), typeof(WpfRentalFee), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        private const string _moduleName = "租赁费";


        #endregion

        #region Properties

        public RentalFeeViewModel ViewModel
        {
            get { return (RentalFeeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors


        public WpfRentalFee()
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

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            WpfRentalFee @this = (WpfRentalFee)d;
            RentalFeeViewModel viewModel = args.NewValue as RentalFeeViewModel;
            if (viewModel != null)
                @this.Query();
        }

        #endregion



        #region Command handlers

        //  Add
        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null;
        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            AddRentalFeesDialog adFDialog = new AddRentalFeesDialog()
            {
                Owner = Window.GetWindow(this),
                AvailableSocialUnits = ViewModel.GetUnPayRentalFee()
            };
            if (adFDialog.ShowDialog().GetValueOrDefault())
            {
                Query();
            }
        }

        //  Remove
        private void Remove_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO...根据权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedRentalFeesInfo != null;
        }

        private void Remove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataRowView row = listViewRentalFeesInfoTbl.SelectedValue as DataRowView;
            if (ViewModel.SelectedRentalFeesInfo != null)
            {
                if (ViewModel.SelectedRentalFeesInfo.IsPay == 2)
                {
                    MessageBox.Show("已经删除，请勿重复操作！ ", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.No);
                    return;
                }
                if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    //0,未缴费，1 缴费， 2 删除
                    ViewModel.SelectedRentalFeesInfo.IsPay = 2;
                    GlobalVariables.Smc.Update<RentalFeesInfo>(ViewModel.SelectedRentalFeesInfo);
                    if (row != null)
                    {
                        ViewModel.RentalFeTbl.Rows.Remove(row.Row);
                        listViewRentalFeesInfoTbl.Items.Refresh();
                    }
                }
               
            }
        }

        #endregion

        #region Event handlers

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

        private void listViewRentalFeesInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedRentalFeesInfo = row.BuildEntity<RentalFeesInfo>();
            }
        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {

            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.RentalFeTbl, _moduleName);

        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.RentalFeTbl, _moduleName);

        }
        #endregion

        //  TODO

        #endregion
    }
}
