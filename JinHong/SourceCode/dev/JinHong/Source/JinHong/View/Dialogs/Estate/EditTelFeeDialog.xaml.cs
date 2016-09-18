using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EditTelFeeDialog : ParentDialagWindow, INotifyPropertyChanged
    {
        #region Fields
        private BroadBandFee _currentTelecomFeesInfo;
        private object _availableRooms;

        #endregion

        public BroadBandFee CurrentTelecomFeesInfo
        {
            get { return _currentTelecomFeesInfo; }
            set
            {
                if (_currentTelecomFeesInfo != value)
                {
                    _currentTelecomFeesInfo = value;
                    OnPropertyChanged("CurrentTelecomFeesInfo");
                }
            }
        }

        public object AvailableRooms
        {
            get { return _availableRooms; }
            set
            {
                if (_availableRooms != value)
                {
                    _availableRooms = value;
                    OnPropertyChanged("AvailableRooms");
                }
            }
        }





        /// <summary>
        ///
        /// </summary>
        /// <param employeeName="userList"></param>
        public EditTelFeeDialog()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentTelecomFeesInfo.RoomId))
            {
                MessageBox.Show("租赁区域不能为空!", "新增电信费", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GlobalVariables.Smc.Insert<BroadBandFee>(CurrentTelecomFeesInfo);
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
            if (!string.IsNullOrEmpty(CurrentTelecomFeesInfo.SocialUnitId))
            {
                try
                {
                    var ds = GlobalVariables.Smc.Select(string.Format(@"select a.*from  RoomInfo a 
                                                                        INNER join ContractDetail b  on a.Id=b.RoomId
                                                                        INNER join  ContractInfo  c  on  b.ContractId=c.Id
                                                                        where    c.SocialUnitId='{0}' and  a.Id not in 
                                                                        (SELECT  RoomId from  TelecomFeesInfo where SocialUnitId='{0}' )",
                                                                         CurrentTelecomFeesInfo.SocialUnitId));
                    AvailableRooms = ds == null ? null : ds.Tables[0];
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
    }
}
