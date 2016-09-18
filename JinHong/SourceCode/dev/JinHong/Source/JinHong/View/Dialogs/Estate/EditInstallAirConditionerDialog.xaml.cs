using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增ContractInfo
    /// </summary>
    public partial class EditInstallAirConditionerDialog : ParentDialagWindow, INotifyPropertyChanged
    {

        #region Dependency properties

        public static readonly DependencyProperty CurrentContractInfoProperty = DependencyProperty.Register(
            "CurrentContractInfo", typeof(AirConditioner), typeof(EditInstallAirConditionerDialog));
        #endregion
        #region Fields
        private IList<ContractInfo> _contractInfos;


        private InstallAirRecord currentInstallAirRecord;


        private DataTable socialUnitTbl;

        private DataTable airConditionerTbl;

        private ObservableCollection<InstallAirRecord> installAirRecords;

        private object availableRooms;




        #endregion

        #region Properties


        public DataTable SocialUnitTbl
        {
            get { return socialUnitTbl; }
            set
            {
                if (socialUnitTbl != value)
                {
                    socialUnitTbl = value;
                    OnPropertyChanged("SocialUnitTbl");
                }
            }
        }

        public DataTable AirConditionerTbl
        {
            get { return airConditionerTbl; }
            set
            {
                if (airConditionerTbl != value)
                {
                    airConditionerTbl = value;
                    OnPropertyChanged("AirConditionerTbl");
                }
            }
        }
        public InstallAirRecord CurrentInstallAirRecord
        {
            get { return currentInstallAirRecord; }
            set
            {
                if (currentInstallAirRecord != value)
                {
                    currentInstallAirRecord = value;
                    OnPropertyChanged("CurrentInstallAirRecord");
                }
            }
        }


        public object AvailableRooms
        {
            get { return availableRooms; }
            set
            {
                if (availableRooms != value)
                {
                    availableRooms = value;
                    OnPropertyChanged("AvailableRooms");
                }
            }
        }
        public ObservableCollection<InstallAirRecord> InstallAirRecords
        {
            get { return installAirRecords; }
            set
            {
                if (installAirRecords != value)
                {
                    installAirRecords = value;
                    OnPropertyChanged("InstallAirRecords");
                }
            }
        }





        /// <summary>
        /// ContractInfo名
        /// </summary>
        public string ContractInfoName { get; set; }

        #endregion

        #region Constructors


        public EditInstallAirConditionerDialog()
        {
            InitializeComponent();

        }
        /// <summary>
        /// 新增ContractInfo,对数据库已有ContractInfo名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public EditInstallAirConditionerDialog(IList<ContractInfo> ContractInfos)
        {
            InitializeComponent();
            this._contractInfos = ContractInfos;
            CurrentInstallAirRecord = new InstallAirRecord(Guid.NewGuid().ToString());

        }

        #endregion

        /// <summary>
        /// 确认新增
        /// </summary>
        /// <param employeeName="sender"></param>
        /// <param employeeName="e"></param>
        /// 
        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            base.Mode = DialogViewMode.Add;

            //if (string.IsNullOrEmpty(CurrentContractInfo.Name))
            //{
            //    MessageBox.Show("名称不能为空!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //if (null != _contractInfos)
            //{
            //    if (_contractInfos.FirstOrDefault(ac => ac.Name == ContractInfoName) != null)
            //    {
            //        MessageBox.Show("名称已经存在!", "新增合同", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }
            //}
            //GlobalVariables.Smc.Insert<ContractInfo>(new ContractInfo()
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = ContractInfoName,
            //    Description = this.txtDescription.Text.Trim()
            //});
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

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentInstallAirRecord.SocialUnitId))
            {
                DataView dv = SocialUnitTbl.DefaultView;
                dv.RowFilter = string.Format(" SocialUnitId='{0}'", CurrentInstallAirRecord.SocialUnitId);
                AvailableRooms = dv.ToTable();

            }
        }

    }

    //public class AirConditionerCategories
    //{
    //    public DataView GetCategories()
    //    {
    //        return GlobalVariables.Smc.Select("select *from AirConditioner").Tables[0].DefaultView;
    //    }


    //}
}
