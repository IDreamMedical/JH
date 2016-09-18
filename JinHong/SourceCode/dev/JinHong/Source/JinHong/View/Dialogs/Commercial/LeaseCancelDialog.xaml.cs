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
using JinHong.Model;
using System.ComponentModel;
using System.Data;
using JinHong.Extensions;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// LeaseCancelDialog.xaml 的交互逻辑
    /// </summary>
    public partial class LeaseCancelDialog : Window, INotifyPropertyChanged
    {
        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
    "ViewModel", typeof(OutRentaledViewModel), typeof(LeaseCancelDialog));
        #endregion

        #region Fields
        private DataTable unLeasingTbl;
        private DataTable unLeasinglParkIngTbl;

        private OutRentaledViewModel currentUnLeaseVM;
        private UnLeaseDetail currentUnLeaseDetail;
        private ContractInfo currentContractInfo;

        #endregion

        #region Properties

        public OutRentaledViewModel CurrentUnLeaseVM
        {
            get { return currentUnLeaseVM; }
            set
            {
                if (currentUnLeaseVM != value)
                {
                    currentUnLeaseVM = value;
                    OnPropertyChanged("CurrentUnLeaseVM");

                }
            }
        }

        public UnLeaseDetail CurrentUnLeaseDetail
        {
            get { return currentUnLeaseDetail; }
            set
            {
                if (currentUnLeaseDetail != value)
                {
                    currentUnLeaseDetail = value;
                    OnPropertyChanged("CurrentUnLeaseDetail");

                }
            }
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

        public DataTable UnLeasingTbl
        {
            get { return unLeasingTbl; }
            set
            {
                if (unLeasingTbl != value)
                {

                    unLeasingTbl = value;
                    OnPropertyChanged("UnLeasingTbl");

                }
            }
        }

        public DataTable UnLeasinglParkIngTbl
        {
            get { return unLeasinglParkIngTbl; }
            set
            {
                if (unLeasinglParkIngTbl != value)
                {

                    unLeasinglParkIngTbl = value;
                    OnPropertyChanged("UnLeasinglParkIngTbl");

                }
            }
        }

        #endregion

        #region Constructors

        public LeaseCancelDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (null == CurrentUnLeaseDetail || null == CurrentUnLeaseVM)
            {
                MessageBox.Show(" 构造对象出错了");
                return;
            }

            if (string.IsNullOrEmpty(CurrentUnLeaseDetail.WYSureName))
            {

                MessageBox.Show(" 物业经办人是必填项");
                return;
            }
            UnLeaseVMToUnLeaseDetail(CurrentUnLeaseDetail, CurrentUnLeaseVM);
            GlobalVariables.Smc.Insert<UnLeaseDetail>(CurrentUnLeaseDetail);
            GlobalVariables.Smc.NonQuery(string.Format("UPDATE  LeasingInfo  set status=1  where  id='{0}'", CurrentUnLeaseDetail.Id));
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #region Callbacks

        #endregion

        #region Common Method


        /// <summary>
        /// 物业情况的转换
        /// </summary>
        /// <param name="targetItem"></param>
        /// <param name="sourceItem"></param>
        private void UnLeaseVMToUnLeaseDetail(UnLeaseDetail targetItem, OutRentaledViewModel sourceItem)
        {
            targetItem.PowerStr = string.Format("{0},{1},{2}", sourceItem.PowerStatus, sourceItem.PowerSureName, sourceItem.PowerNotes);
            targetItem.WaterStr = string.Format("{0},{1},{2}", sourceItem.WaterStatus, sourceItem.WaterSureName, sourceItem.WaterNotes);
            targetItem.RoomStr = string.Format("{0},{1},{2}", sourceItem.RoomStatus, sourceItem.RoomSureName, sourceItem.RoomNotes);
            targetItem.KeyStr = string.Format("{0},{1},{2}", sourceItem.KeyStatus, sourceItem.KeySureName, sourceItem.KeyNotes);
            targetItem.AirStr = string.Format("{0},{1},{2}", sourceItem.AirStatus, sourceItem.AirSureName, sourceItem.AirNotes);
            targetItem.AirCtrlStr = string.Format("{0},{1},{2}", sourceItem.AirCtrlStatus, sourceItem.AirCtrlSureName, sourceItem.AirCtrlNotes);
            targetItem.ParkingCardStr = string.Format("{0},{1},{2}", sourceItem.ParkingStatus, sourceItem.ParkingSureName, sourceItem.ParkingNotes);
            targetItem.TelStr = string.Format("{0},{1},{2}", sourceItem.TelStatus, sourceItem.TelSureName, sourceItem.TelNotes);
            targetItem.InvoiceStr = string.Format("{0},{1},{2}", sourceItem.InvoiceStatus, sourceItem.InvoiceSureName, sourceItem.InvoiceNotes);
            targetItem.UnLeaseDate = targetItem.WYDate = DateTime.Now;

        }

        #endregion


        #endregion


        #region Interface Mothds
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
