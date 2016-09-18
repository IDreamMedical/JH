using System;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using JinHong.Model;
using JinHong.Extensions;

namespace JinHong.View.Dialogs
{
    public partial class EditParkingLotRentalInfoDialog : ParentDialagWindow, INotifyPropertyChanged
    {

        #region Fields

        private object availableSocialUnits;

        private object availableParkingLots;
        private ParkingLotRentalInfo currentParkingLotRentalInfo;
        private ParkingLotInfo currentParkingLotInfo;
        #endregion

        #region Dependency properties


        public static readonly DependencyProperty ParkingLotRentalInfoProperty
            = DependencyProperty.Register("ParkingLotRentalInfo", typeof(ParkingLotRentalInfo), typeof(EditParkingLotRentalInfoDialog));



        #endregion

        #region Properties
        public ParkingLotRentalInfo ParkingLotRentalInfo
        {
            get { return (ParkingLotRentalInfo)GetValue(ParkingLotRentalInfoProperty); }
            set { SetValue(ParkingLotRentalInfoProperty, value); }
        }



        public object AvailableSocialUnits
        {
            get { return availableSocialUnits; }
            set
            {
                if (availableSocialUnits != value)
                {
                    availableSocialUnits = value;
                    OnPropertyChanged("AvailableSocialUnits");
                }
            }
        }

        public object AvailableParkingLots
        {
            get { return availableParkingLots; }
            set
            {
                if (availableParkingLots != value)
                {
                    availableParkingLots = value;
                    OnPropertyChanged("AvailableParkingLots");
                }
            }
        }

        public ParkingLotRentalInfo CurrentParkingLotRentalInfo
        {
            get { return currentParkingLotRentalInfo; }
            set
            {
                if (currentParkingLotRentalInfo != value)
                {
                    currentParkingLotRentalInfo = value;
                    OnPropertyChanged("CurrentParkingLotRentalInfo");
                }
            }
        }

        public ParkingLotInfo CurrentParkingLotInfo
        {
            get { return currentParkingLotInfo; }
            set
            {
                if (currentParkingLotInfo != value)
                {
                    currentParkingLotInfo = value;
                    OnPropertyChanged("CurrentParkingLotInfo");
                }
            }
        }

        #endregion



        #region Constructors

        public EditParkingLotRentalInfoDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Callbacks

        #endregion

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentParkingLotRentalInfo == null || string.IsNullOrEmpty(CurrentParkingLotRentalInfo.ParkingLotId))
            {
                MessageBox.Show("车位号不能为空!请知晓", "车位租赁", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(CurrentParkingLotRentalInfo.SocialUnitId))
            {
                MessageBox.Show("租赁单位不能空!请知晓", "车位租赁", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(CurrentParkingLotRentalInfo.LicensePlateNo))
            {
                MessageBox.Show("车牌号不能空!请知晓", "车位租赁", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (CurrentParkingLotRentalInfo.TotalAmount <= 0)
            {
                MessageBox.Show("金额不能小于等于0!请知晓", "车位租赁", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

      
        private void comboBoxParkingLot_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DataRow dr = this.comboBoxParkingLot.SelectedItem as DataRow;
            if (dr != null)
            {
                CurrentParkingLotInfo = dr.BuildEntity<ParkingLotInfo>();
                CurrentParkingLotInfo.Status = 1;
            }
        }

    }
}
