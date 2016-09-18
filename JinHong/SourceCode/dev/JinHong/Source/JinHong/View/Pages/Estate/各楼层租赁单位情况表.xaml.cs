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

namespace JinHong.View
{
    /// <summary>
    /// Interaction logic for 各楼层租赁单位情况表.xaml
    /// </summary>
    public partial class 各楼层租赁单位情况表 : UserControl
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(RentanlDetailViewModel), typeof(各楼层租赁单位情况表));

        #endregion

        #region Fields

        private string _moduleName = "各楼层租赁单位情况表";

        #endregion

        #region Properties

        public RentanlDetailViewModel ViewModel
        {
            get { return (RentanlDetailViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        //  TODO

        #endregion

        #region Constructors

        public 各楼层租赁单位情况表()
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


        #region Event handlers

        private void listViewLeasingStatusInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = e.AddedItems[0] as DataRow;
                if (row != null)
                    ViewModel.SelectedLeasingStatusInfo = row.BuildEntity<LeasingStatusInfo>();
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ViewModel.WhereBuildingId))
            {
                Query(ViewModel.WhereBuildingId);
            }
            else
            {
                Query();
            }

        }

        private void cmbBuilding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel == null) return;
            if (e.AddedItems.Count > 0)
            {
                DataRow dr = e.AddedItems[0] as DataRow;
                ViewModel.WhereBuildingId = dr["Id"] + "";
            }
            if (!string.IsNullOrEmpty(ViewModel.WhereBuildingId))
            {
                Query(ViewModel.WhereBuildingId);
            }
        }

        private void buttonExportToPdf_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToPdf(ViewModel.LeasingStatusInfoTbl, _moduleName);

        }

        private void buttonExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            GlobalVariables.ExportHelper.ExportToExcel(ViewModel.LeasingStatusInfoTbl, _moduleName);

        }


        #endregion


        #endregion
    }
}
