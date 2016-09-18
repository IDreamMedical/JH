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
using System.Windows.Shapes;
using System.Data;

using JinHong.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// LeaseAddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class WareHouseLeaseAddDialog : Window, INotifyPropertyChanged
    {


        #region Dependency properties

        public static readonly DependencyProperty AvailableSocialUnitsProperty
= DependencyProperty.Register("AvailableSocialUnits", typeof(object), typeof(WareHouseLeaseAddDialog));

        public static readonly DependencyProperty AvailableWareHouseIdsProperty = DependencyProperty.Register(
            "AvailableWareHouseIds", typeof(object), typeof(WareHouseLeaseAddDialog));



        #endregion

        #region Fields

        private WareHouseLeasingInfo currentWareHouseLeasing;
        private ContractInfo currentContractInfo;

        private ObservableCollection<WareHouseLeasingInfo> wareHouseLeasingInfos;



        #endregion

        #region Properties

        public object AvailableWareHouseIds
        {
            get { return (object)GetValue(AvailableWareHouseIdsProperty); }
            set { SetValue(AvailableWareHouseIdsProperty, value); }
        }

        public ContractInfo CurrentContractInfo
        {
            get { return currentContractInfo; }
            set
            {
                if (currentContractInfo != value)
                {
                    currentContractInfo = value;
                    OnPropertyChanged("CurrentContractInfo");
                }
            }
        }

        public WareHouseLeasingInfo CurrentWareHouseLeasing
        {
            get { return currentWareHouseLeasing; }
            set
            {
                if (currentWareHouseLeasing != value)
                {
                    currentWareHouseLeasing = value;
                    OnPropertyChanged("CurrentWareHouseLeasing");
                }
            }
        }


        public ObservableCollection<WareHouseLeasingInfo> WareHouseLeasingInfos
        {
            get { return wareHouseLeasingInfos; }
            set
            {
                if (wareHouseLeasingInfos != value)
                {
                    wareHouseLeasingInfos = value;
                    OnPropertyChanged("WareHouseLeasingInfos");
                }
            }
        }

        public object AvailableSocialUnits
        {
            get { return (object)GetValue(AvailableSocialUnitsProperty); }
            set { SetValue(AvailableSocialUnitsProperty, value); }
        }

        #endregion

        #region Constructors

        public WareHouseLeaseAddDialog()
        {
            InitializeComponent();
            CurrentWareHouseLeasing = new WareHouseLeasingInfo();
        }

        #endregion

        #region Methods

        #region Event handlers



        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void comboBoxWareHouseId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                CurrentWareHouseLeasing.Area = 0;
            }
        }

        #endregion


        #region Command handlers

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

            e.CanExecute = CurrentContractInfo != null && !string.IsNullOrEmpty(CurrentContractInfo.SocialUnitId);
        }


        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentWareHouseLeasing.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(CurrentWareHouseLeasing.WareHouseId))
            {
                MessageBox.Show("仓库号不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CurrentWareHouseLeasing.MonthPropManageFee < 0)
            {
                MessageBox.Show("月物业费不能小于零!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (WareHouseLeasingInfos == null)
            {
                WareHouseLeasingInfos = new ObservableCollection<WareHouseLeasingInfo>();
            }

            if (WareHouseLeasingInfos.Where(w => w.WareHouseId == CurrentWareHouseLeasing.WareHouseId).FirstOrDefault() == null)
            {
                WareHouseLeasingInfo temp = Clone(CurrentWareHouseLeasing);

                if (temp != null)
                {
                    WareHouseLeasingInfos.Add(temp);
                }
            }
            else
            {
                MessageBox.Show(string.Format("'{0}'已存在请勿重复新增！", CurrentWareHouseLeasing.WareHouseName), "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        private void Del_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Del_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void AddFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void AddFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void DelFile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void DelFile_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentContractInfo.SocialUnitId))
            {
                MessageBox.Show("名称不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (wareHouseLeasingInfos == null)
            {
                MessageBox.Show("租赁明细不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            CurrentContractInfo.LeaseType = 1;
            GlobalVariables.Smc.Insert<ContractInfo>(CurrentContractInfo);
            foreach (var item in WareHouseLeasingInfos)
            {
                item.ContractId = CurrentContractInfo.Id;
                GlobalVariables.Smc.Insert<WareHouseLeasingInfo>(item);
            }
            DialogResult = true;

        }

        private void dtpEffectiveDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void dtpExpirateDate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void txtRentalFees_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGridContractDetail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { }
        #endregion

        #region Common Methods

        private WareHouseLeasingInfo Clone(WareHouseLeasingInfo sourceItem)
        {
            WareHouseLeasingInfo temp = null;
            if (sourceItem != null)
            {
                temp = new WareHouseLeasingInfo();
                temp.Id = sourceItem.Id;
                temp.Area = sourceItem.Area;
                temp.ContractId = sourceItem.ContractId;
                temp.DayPropManageFee = sourceItem.DayPropManageFee;
                temp.DayRentalFee = sourceItem.DayRentalFee;
                temp.LeaseBeginDate = sourceItem.LeaseBeginDate;
                temp.LeaseEndDate = sourceItem.LeaseEndDate;
                temp.MonthPropManageFee = sourceItem.MonthPropManageFee;
                temp.MonthRentalFee = sourceItem.MonthRentalFee;
                temp.Notes = sourceItem.Notes;
                temp.SocialUnitId = sourceItem.SocialUnitId;
                temp.SocialUnitName = sourceItem.SocialUnitName;
                temp.WareHouseName = sourceItem.WareHouseName;
                temp.WareHouseId = sourceItem.WareHouseId;
            }
            return temp;

        }
        #endregion

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {

                handler(this, new PropertyChangedEventArgs(propertyName));
            }

        }



    }
}
