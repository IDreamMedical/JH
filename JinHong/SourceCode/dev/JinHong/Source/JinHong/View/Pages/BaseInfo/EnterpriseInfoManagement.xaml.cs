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

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 楼宇信息.xaml
    /// </summary>
    public partial class EnterpriseInfoManagement : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(BuildingInfoVM), typeof(EnterpriseInfoManagement), new PropertyMetadata(ViewModelPropertyChanged));
        public static readonly DependencyProperty CurrenBuildingInfoProperty
    = DependencyProperty.Register("CurrenBuildingInfo", typeof(BuildingInfo), typeof(AddBuildingInfoDialog));

        public static readonly DependencyProperty BuildingInfosProperty
            = DependencyProperty.Register("BuildingInfos", typeof(ObservableCollection<BuildingInfo>), typeof(AddBuildingInfoDialog));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public BuildingInfoVM ViewModel
        {
            get { return (BuildingInfoVM)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }
        #region Properties

        public BuildingInfo CurrenBuildingInfo
        {
            get { return (BuildingInfo)GetValue(CurrenBuildingInfoProperty); }
            set { SetValue(CurrenBuildingInfoProperty, value); }
        }

        public ObservableCollection<BuildingInfo> BuildingInfos
        {
            get { return (ObservableCollection<BuildingInfo>)GetValue(BuildingInfosProperty); }
            set { SetValue(BuildingInfosProperty, value); }
        }
        #endregion


        //  TODO

        #endregion

        #region Constructors

        public EnterpriseInfoManagement()
        {
            InitializeComponent();
            var dt = GlobalVariables.Smc.Select("SELECT * FROM BuildingInfo", null);

            if (null != dt)
            {
                BuildingInfos = DataRowToObjt(dt.Tables[0]);
            }
            else
            {
                BuildingInfos = new ObservableCollection<BuildingInfo>();
            }
        }

        #endregion

        #region Methods

        #region General

        public void Query()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
        }

        #endregion

        #region Callbacks

        private static void ViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            EnterpriseInfoManagement @this = (EnterpriseInfoManagement)d;
            BuildingInfoVM viewModel = args.NewValue as BuildingInfoVM;
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
           
        }

        private void Leave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //  TODO 需要对应权限
            e.CanExecute = ViewModel != null && ViewModel.SelectedBuilding!=null;
        }

        private void Leave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //  TODO...Leave
            //BuildingInfo selectedEmployeeInfo = ViewModel.SelectedBuildingInfo;
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
                    ViewModel.SelectedBuilding = row.BuildEntity<BuildingInfo>();
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Query();
        }

        private string _moduleName = "企业管理";


        //  TODO

        #endregion

        public ObservableCollection<BuildingInfo> DataRowToObjt(DataTable dt)
        {
            ObservableCollection<BuildingInfo> observableList = new ObservableCollection<BuildingInfo>();
            foreach (DataRow item in dt.Rows)
            {
                observableList.Add(new BuildingInfo()
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
