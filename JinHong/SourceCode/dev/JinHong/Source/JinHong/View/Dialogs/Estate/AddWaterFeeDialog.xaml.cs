using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.ComponentModel;
using System.Data;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增探头
    /// </summary>
    public partial class AddWaterFeeDialog : ParentDialagWindow, INotifyPropertyChanged
    {
        #region Fields
        private MonthlyWaterAndElectricityFeesInfo _currentWaterFeeInfo;
        private object _availableSocialUnits;
        private object _availableRoom;


        #endregion


        public MonthlyWaterAndElectricityFeesInfo CurrentWaterFeeInfo
        {
            get { return _currentWaterFeeInfo; }
            set
            {
                if (_currentWaterFeeInfo != value)
                {
                    _currentWaterFeeInfo = value;
                    OnPropertyChanged("CurrentWaterFeeInfo");
                }
            }
        }

        public object AvailableSocialUnits
        {
            get { return _availableSocialUnits; }
            set
            {
                if (_availableSocialUnits != value)
                {
                    _availableSocialUnits = value;
                    OnPropertyChanged("AvailableSocialUnits");
                }
            }
        }


        public object AvailableRoom
        {
            get { return _availableRoom; }
            set
            {
                if (_availableRoom != value)
                {
                    _availableRoom = value;
                    OnPropertyChanged("AvailableRoom");
                }
            }
        }





        /// <summary>
        /// 新增探头,对数据库已有探头名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddWaterFeeDialog()
        {
            InitializeComponent();
            CurrentWaterFeeInfo = new MonthlyWaterAndElectricityFeesInfo() { Id = Guid.NewGuid().ToString() };
        }
        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentWaterFeeInfo.SocialUnitId))
            {
                MessageBox.Show("单位名称不能为空!", "新增水电费", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CurrentWaterFeeInfo.Date = DateTime.Now;
            GlobalVariables.Smc.Insert<MonthlyWaterAndElectricityFeesInfo>(CurrentWaterFeeInfo);
            DialogResult = true;
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                {

                    string sql = string.Format(@"select  c.id, c.Name  from   ContractDetail  a 
                                                    INNER join  ContractInfo  b on a.ContractId=b.Id
                                                    INNER join  RoomInfo c  on c.Id=a.RoomId
                                                    where  b.SocialUnitId='{0}';", CurrentWaterFeeInfo.SocialUnitId);
                    var ds = GlobalVariables.Smc.Select(sql);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        AvailableRoom = ds.Tables[0];
                    }
                }
            }

        }

    }
}
