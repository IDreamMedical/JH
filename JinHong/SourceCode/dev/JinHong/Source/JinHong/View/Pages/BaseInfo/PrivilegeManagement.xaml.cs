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
using System.Collections.ObjectModel;
using UniGuy.Entity;

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 权限管理.xaml
    /// </summary>
    public partial class PrivilegeManagement : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(PrivilegeVM), typeof(PrivilegeManagement), new PropertyMetadata(ViewModelPropertyChanged));
        public static readonly DependencyProperty CurrenPrivilegeProperty
    = DependencyProperty.Register("CurrenPrivilege", typeof(Privilege), typeof(AddPrivilegeDialog));

        public static readonly DependencyProperty PrivilegesProperty
            = DependencyProperty.Register("Privileges", typeof(ObservableCollection<Privilege>), typeof(AddPrivilegeDialog));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public PrivilegeVM ViewModel
        {
            get { return (PrivilegeVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        #region Properties

        public Privilege CurrenPrivilege
        {
            get { return (Privilege)GetValue(CurrenPrivilegeProperty); }
            set { SetValue(CurrenPrivilegeProperty, value); }
        }

        public ObservableCollection<Privilege> Privileges
        {
            get { return (ObservableCollection<Privilege>)GetValue(PrivilegesProperty); }
            set { SetValue(PrivilegesProperty, value); }
        }
        #endregion


        //  TODO

        #endregion

        #region Constructors

        public PrivilegeManagement()
        {
            InitializeComponent();
            var dt = GlobalVariables.Smc.Select("SELECT * FROM Privilege", null);

            if (null != dt)
            {
                Privileges = DataRowToObjt(dt.Tables[0]);
            }
            else
            {
                Privileges = new ObservableCollection<Privilege>();
            }
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
            PrivilegeManagement @this = (PrivilegeManagement)d;
            PrivilegeVM viewModel = args.NewValue as PrivilegeVM;
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
            AddPrivilegeDialog dialog = new AddPrivilegeDialog(Privileges) { Owner = Window.GetWindow(this) };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                //GlobalVariables.Smc.Save2<JHEmployeeInfo>(dialog.EmployeeInfo);
                Query();
            }
        }

        private void Leave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedPrivilege!=null;
        }

        private void Leave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Leave
            //Privilege selectedEmployeeInfo = ViewModel.SelectedPrivilege;
            //EmployeeLeaveDialog dialog = new EmployeeLeaveDialog { Owner = Window.GetWindow(this), EmployeeName = selectedEmployeeInfo.Name };
            //if (dialog.ShowDialog().GetValueOrDefault())
            //{
            //    //selectedEmployeeInfo.LeaveDate = dialog.LeaveDate;
            //    //selectedEmployeeInfo.LeaveReasonCode = dialog.LeaveReasonCode;
            //    //GlobalVariables.Smc.Save2<JHEmployeeInfo>(selectedEmployeeInfo);
            //    //Query();
            //}
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
                    ViewModel.SelectedPrivilege = row.BuildEntity<Privilege>();
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }


        private string _moduleName = "权限管理";

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.PrivilegeTbl, _moduleName);

        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.PrivilegeTbl, _moduleName);

        }

        #endregion

        public ObservableCollection<Privilege> DataRowToObjt(DataTable dt)
        {
            ObservableCollection<Privilege> observableList = new ObservableCollection<Privilege>();
            foreach (DataRow item in dt.Rows)
            {
                observableList.Add(new Privilege()
                {
                    Name = item["Name"] + "",
                    Description = item["Description"] + ""


                });
            }

            return observableList;

        }


        #endregion
    }
}
