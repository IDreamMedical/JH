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
using System.Windows.Threading;
using UniGuy.Core.Data;
using JinHong.Model;
using System.Data;
using JinHong.Extensions;
using JinHong.View.Dialogs;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 权限.xaml
    /// </summary>
    public partial class Privilege : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(任职全厂职工名单VM), typeof(任职全厂职工名单), new PropertyMetadata(ViewModelPropertyChanged));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public 任职全厂职工名单VM ViewModel
        {
            get { return (任职全厂职工名单VM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public Privilege()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region General

        public void Query()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            ViewModel.Query(() => Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            任职全厂职工名单 @this = (任职全厂职工名单)d;
            任职全厂职工名单VM viewModel = args.NewValue as 任职全厂职工名单VM;
            if (viewModel != null)
                @this.Query();
        }

        #endregion

        #region Command handlers

        //  入职登记
        private void Enter_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null;
        }

        private void Enter_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Enter
            EmployeeEnterDialog dialog = new EmployeeEnterDialog { Owner = Window.GetWindow(this) };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                GlobalVariables.Smc.Save2<JHEmployeeInfo>(dialog.EmployeeInfo);
                Query();
            }
        }

        private void Leave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedEmployeeInfo!=null;
        }

        private void Leave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Leave
            JHEmployeeInfo selectedEmployeeInfo = ViewModel.SelectedEmployeeInfo;
            EmployeeLeaveDialog dialog = new EmployeeLeaveDialog { Owner = Window.GetWindow(this), EmployeeName = selectedEmployeeInfo.Name };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                selectedEmployeeInfo.LeaveDate = dialog.LeaveDate;
                selectedEmployeeInfo.LeaveReasonCode = dialog.LeaveReasonCode;
                GlobalVariables.Smc.Save2<JHEmployeeInfo>(selectedEmployeeInfo);
                Query();
            }
        }

        #endregion

        #region Event handlers

        private void listViewOnDutyEmployeeInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                    ViewModel.SelectedEmployeeInfo = row.BuildEntity<JHEmployeeInfo>();
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        //  TODO

        #endregion

        #endregion
    }
}
