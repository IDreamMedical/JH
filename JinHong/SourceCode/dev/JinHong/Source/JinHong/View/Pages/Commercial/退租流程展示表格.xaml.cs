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
using JinHong.BaseUserControl;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 退租流程展示表格.xaml
    /// </summary>
    public partial class 退租流程展示表格 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(ReverseRentalVM), typeof(退租流程展示表格), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields
        private string _moduleName = "退租流程展示表";


        #endregion

        #region Properties

        public ReverseRentalVM ViewModel
        {
            get { return (ReverseRentalVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 退租流程展示表格()
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
            退租流程展示表格 @this = (退租流程展示表格)d;
            ReverseRentalVM viewModel = args.NewValue as ReverseRentalVM;
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
            //LeasingInfo leasingInfo = GlobalVariables.Smc.Load<LeasingInfo>(ViewModel.SelectedLeasingStatusInfo.LeasingInfoId);
            //Debug.Assert(leasingInfo != null);
            //LeaseCancelDialog dialog = new LeaseCancelDialog { Owner = Window.GetWindow(this), LeasingInfo = leasingInfo };
            //if (dialog.ShowDialog().GetValueOrDefault())
            //{
            //    //dialog.LeasingInfo.Cancel();
            //    Query();    //  刷新
            //}
        }

        #endregion

        #region Event handlers

        private void listViewLeasingInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = e.AddedItems[0] as DataRow;
                if (row != null)
                    ViewModel.SelectedLeasingInfo = row.BuildEntity<CheckIn>();
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

      

        #endregion
    }
}
