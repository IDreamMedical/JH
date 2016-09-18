using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.Data;
using System.Collections.ObjectModel;
using JinHong.Extensions;
using JinHong.ViewModel;
using Microsoft.Win32;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增合同
    /// </summary>
    public partial class NewOrEditContract : Window
    {


        #region Public Prop


        public NewContractViewModel ViewModel { get; private set; }
        #endregion

        #region Constructors

        public NewOrEditContract()
        {
            InitializeComponent();
            ViewModel = new NewContractViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }
        #endregion

        //    #region Methods

        //    #region Command handlers

        //    private void Add_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //    {
        //        e.CanExecute = CurrentContractInfo != null && !string.IsNullOrEmpty(CurrentContractInfo.Id);
        //    }

        //    private void Add_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        //    {
        //        CurrentContractDetail.ContractId = CurrentContractInfo.Id;
        //        if (string.IsNullOrEmpty(CurrentContractDetail.BuildingId))
        //        {
        //            MessageBox.Show("座号不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        if (CurrentContractDetail.MonthPropManageFee <= 0)
        //        {
        //            MessageBox.Show("月物业费不能小于或等于零!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        if (ResultContractDetails == null)
        //        {
        //            ResultContractDetails = new ObservableCollection<ContractDetail>();
        //        }

        //        if (ResultContractDetails.Where(c => c.RoomId == CurrentContractDetail.RoomId).FirstOrDefault() == null)
        //        {
        //            ContractDetail temp = Clone(CurrentContractDetail);
        //            if (temp != null)
        //            {
        //                ResultContractDetails.Add(temp);

        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(string.Format("'{0}'已存在请勿重复新增！", CurrentContractDetail.RoomName), "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //    }

        //    private void Del_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        //    {
        //        //SelectedContractDetailVM = GetSelectedRow(this.dataGridContractDetail);
        //        //if (null != SelectedContractDetailVM)
        //        //{
        //        //    if (MessageBox.Show("确定删除租赁该单元？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
        //        //    {
        //        //        ViewModel.ContractDetails.Remove(SelectedContractDetailVM);
        //        //        SelectedContractDetailVM = null;
        //        //    }
        //        //}
        //    }

        //    private void Del_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //    {
        //        e.CanExecute = ViewModel != null && ViewModel.ContractDetails != null && ViewModel.ContractDetails.Count > 0;
        //    }

        //    #endregion

        //    #region Event handlers
        //    /// <summary>
        //    /// 确认新增
        //    /// </summary>
        //    /// <param employeeName="sender"></param>
        //    /// <param employeeName="e"></param>
        //    /// 
        //    private void buttonAccept_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (string.IsNullOrEmpty(CurrentContractInfo.SocialUnitName))
        //        {
        //            MessageBox.Show("名称不能为空!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        if (null != _contractInfos)
        //        {
        //            if (_contractInfos.FirstOrDefault(ac => ac.Id == CurrentContractInfo.Id) != null)
        //            {
        //                MessageBox.Show("名称已经存在!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                return;
        //            }
        //        }
        //        if (CurrentContractInfo.DepositFee <= 0)
        //        {
        //            MessageBox.Show("押金不能为零!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        if (ResultContractDetails == null || ResultContractDetails.Count <= 0)
        //        {
        //            MessageBox.Show(string.Format("至少需要一个合同明细！"), "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        foreach (var item in ResultContractDetails)
        //        {
        //            if (item.MonthPropManageFee <= 0)
        //            {
        //                MessageBox.Show(string.Format("请填写第{0}行的月物业费！", ResultContractDetails.IndexOf(item) + 1), "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //                return;
        //            }
        //        }
        //        if (string.IsNullOrEmpty(this.txtFileName.Text.Trim()) || this.txtFileName.Text.Trim() == "合同文件路径")
        //        {
        //            MessageBox.Show("请新增合同文件!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
        //            return;
        //        }
        //        DialogResult = true;
        //    }
        //    /// <summary>
        //    /// 取消
        //    /// </summary>
        //    /// <param employeeName="sender"></param>
        //    /// <param employeeName="e"></param>
        //    private void buttonCancel_Click(object sender, RoutedEventArgs e)
        //    {
        //        DialogResult = false;
        //    }
        //    #endregion
        //    #endregion

        //    /// <summary>
        //    /// 获取选择行
        //    /// </summary>
        //    /// <returns></returns>
        //    /// 参考
        //    //http://blog.csdn.net/baimingchang/article/details/7495103
        //    private AddContractDetailVM GetSelectedRow(DataGrid dtGrid)
        //    {



        //        /*优化 
        //         * 无论 DataGrid的SelectionUnit跟SelectionMode两个属性取任何值
        //         * 都存在选中的单元格
        //         * 可以根据选中的单元格做统一处理，获取选中的行
        //         *  GetSelectedRows()方法获取选中多行原理相同
        //        */

        //        if (dtGrid != null && dtGrid.SelectedCells.Count != 0)
        //        {
        //            //只选中一个单元格时：返回单元格所在行
        //            //选中多个时：返回第一个单元格所在行
        //            return dtGrid.SelectedCells[0].Item as AddContractDetailVM;
        //        }

        //        return null;
        //    }



        //    private void cmbWarrantPeriod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        CurrentContractInfo.TotalRentalFees = GetTotalRentalFees();
        //    }

        //    private void txtRentalFees_TextChanged(object sender, TextChangedEventArgs e)
        //    {
        //        CurrentContractInfo.TotalRentalFees = GetTotalRentalFees();
        //    }



        //    private ContractDetail Clone(ContractDetail sourceItem)
        //    {
        //        ContractDetail temp = null;
        //        if (sourceItem != null)
        //        {
        //            temp = new ContractDetail();
        //            temp.Id = sourceItem.Id;
        //            temp.Area = sourceItem.Area;
        //            temp.ContractId = sourceItem.ContractId;
        //            temp.DayPropManageFee = sourceItem.DayPropManageFee;
        //            temp.DayRentalFee = sourceItem.DayRentalFee;
        //            temp.MonthPropManageFee = sourceItem.MonthPropManageFee;
        //            temp.MonthRentalFee = sourceItem.MonthRentalFee;
        //            temp.Notes = sourceItem.Notes;
        //            temp.SocialUnitId = sourceItem.SocialUnitId;
        //            temp.SocialUnitName = sourceItem.SocialUnitName;
        //            temp.RoomId = sourceItem.RoomId;
        //            temp.BuildingId = sourceItem.BuildingId;
        //            temp.RoomName = sourceItem.RoomName;
        //            temp.BuildingName = sourceItem.BuildingName;
        //        }
        //        return temp;

        //    }

        //    /// <summary>
        //    /// 计算总金额
        //    /// </summary>
        //    /// <returns></returns>
        //    private double GetTotalRentalFees()
        //    {
        //        if (string.IsNullOrEmpty(this.txtWarrantPeriod.Text) || string.IsNullOrEmpty(this.txtRentalFees.Text)) return 0;

        //        return Convert.ToDouble(this.txtRentalFees.Text) * Convert.ToDouble(this.txtWarrantPeriod.Text);
        //    }

        //    private void dtpEffectiveDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //    {
        //        GetWarrantPeriod();
        //    }

        //    private void dtpExpirateDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //    {
        //        GetWarrantPeriod();
        //    }



        //    private void GetWarrantPeriod()
        //    {
        //        //if (null == this.dtpExpirateDate.Value || null == this.dtpEffectiveDate.Value) return;
        //        //if (this.dtpExpirateDate.Value.Value.CompareTo(this.dtpEffectiveDate.Value.Value) > 0)
        //        //{
        //        //    var y = this.dtpExpirateDate.Value.Value.Year - this.dtpEffectiveDate.Value.Value.Year;
        //        //    var m = this.dtpExpirateDate.Value.Value.Month - this.dtpEffectiveDate.Value.Value.Month;
        //        //    var d = this.dtpExpirateDate.Value.Value.Day - this.dtpEffectiveDate.Value.Value.Day;
        //        //    if (y > 0)
        //        //    {
        //        //        CurrentContractInfo.WarrantPeriod = d > 0 ? y * 12 + m + 1 : y * 12 + m;
        //        //    }
        //        //    else
        //        //    {
        //        //        CurrentContractInfo.WarrantPeriod = d > 0 ? m + 1 : m;

        //        //    }

        //        //}
        //        //else
        //        //{

        //        //    MessageBox.Show("起租时间不能大于止租时间!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);

        //        //}


        //    }

        //    private void dataGridContractDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {

        //    }

        //    private void AddFile_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //    {
        //        e.CanExecute = true;

        //    }

        //    private void AddFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        //    {
        //        OpenFileDialog ofd = new OpenFileDialog
        //        {
        //            Filter = "doc files (*.doc)|*.doc|docx files (*.docx)|*.docx|All files (*.*)|*.*", //过滤文件类型
        //            InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,  //设定初始目录
        //            ShowReadOnly = true
        //        };
        //        if (ofd.ShowDialog().GetValueOrDefault())
        //        {
        //            this.txtFileName.Text = ofd.FileName;
        //        }
        //    }

        //    private void DelFile_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        //    {
        //        e.CanExecute = true;
        //    }

        //    private void DelFile_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        //    {
        //        if (this.txtFileName.Text.Trim() != "合同文件路径")
        //        {
        //            this.txtFileName.Text = "";
        //        }
        //    }


        //    public event PropertyChangedEventHandler PropertyChanged;

        //    private void OnPropertyChanged(string propertyName)
        //    {

        //        PropertyChangedEventHandler handler = PropertyChanged;

        //        if (handler != null)
        //        {

        //            handler(this, new PropertyChangedEventArgs(propertyName));
        //        }

        //    }

        //    private void cmbBuildings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        if (e.AddedItems.Count > 0)
        //        {

        //            DataView dv = AvailableRooms.DefaultView;
        //            dv.RowFilter = string.Format("BuildingId='{0}'", CurrentContractDetail.BuildingId);
        //            ResultRooms = dv.ToTable();
        //            var temp = new ContractDetail()
        //            {
        //                Id = CurrentContractDetail.Id,
        //                BuildingId = CurrentContractDetail.BuildingId
        //            };
        //            CurrentContractDetail = temp;
        //        }
        //    }

        //    private void cmbRooms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {

        //        if (e.AddedItems.Count > 0)
        //        {
        //            DataRow dr = e.AddedItems[0] as DataRow;
        //            CurrentContractDetail.Area = Convert.ToDouble(dr["Area"]);
        //        }
        //    }

        //    private void cmbSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //    {
        //        if (e.AddedItems.Count > 0)
        //        {
        //            DataRow dr = e.AddedItems[0] as DataRow;
        //            CurrentContractInfo.LicenceDate = dr["LicenceDate"] is DBNull ? DateTime.Now : Convert.ToDateTime(dr["LicenceDate"]);
        //            CurrentContractInfo.CustomerName = dr["SocialUnitLeader"] + "";
        //            CurrentContractInfo.DepositFee = dr["Amount"] is DBNull ? 0 : Convert.ToDouble(dr["Amount"]);
        //        }

        //    }

        //}

        //public class Categories
        //{
        //    public DataView GetCategories()
        //    {
        //        return GlobalVariables.Smc.Select("select *from BuildingInfo").Tables[0].DefaultView;
        //    }
        //}
    }
}
