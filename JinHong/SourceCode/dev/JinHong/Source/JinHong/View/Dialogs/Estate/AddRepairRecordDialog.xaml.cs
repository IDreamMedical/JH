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
    /// 系统提示
    /// </summary>
    public partial class AddRepairRecordDialog : ParentDialagWindow, INotifyPropertyChanged
    {
        #region Fields
        private RepairRecord _currentRepairRecord;

        private DataTable _socialUnitTbl;

        private object _availableRooms;


        #endregion



        #region Properties

        public RepairRecord CurrentRepairRecord
        {
            get { return _currentRepairRecord; }
            set
            {
                if (_currentRepairRecord != value)
                {
                    _currentRepairRecord = value;
                    OnPropertyChanged("CurrentRepairRecord");
                }
            }
        }

        public DataTable SocialUnitTbl
        {
            get { return _socialUnitTbl; }
            set
            {
                if (_socialUnitTbl != value)
                {
                    _socialUnitTbl = value;
                    OnPropertyChanged("SocialUnitTbl");
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


        #endregion


        #region Constructors
        public AddRepairRecordDialog()
        {
            InitializeComponent();
            CurrentRepairRecord = new RepairRecord() { Id = Guid.NewGuid().ToString() };
        }
        #endregion

        #region Methods


        #region Event handlers
        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentRepairRecord.SocialUnitId))
            {
                MessageBox.Show("单位名不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
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
        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentRepairRecord.SocialUnitId))
            {
                DataView dv = SocialUnitTbl.DefaultView;
                dv.RowFilter = string.Format(" SocialUnitId='{0}'", CurrentRepairRecord.SocialUnitId);
                AvailableRooms = dv.ToTable();

            }

        }


        #endregion
        #endregion


        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        #endregion

    }
}
