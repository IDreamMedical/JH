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

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// TODO...暂时只是入职登记, 不包括职工信息修改功能
    /// </summary>
    public partial class AddParkingLotRentalInfoDialog : Window, INotifyPropertyChanged
    {
        #region Fields

        private object availableSocialUnits;

        private object availableParkingLots;
        private ParkingLotRentalInfo currentParkingLotRentalInfo;





        //  TODO

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty EmployeeInfoProperty = DependencyProperty.Register(
            "EmployeeInfo", typeof(JHEmployeeInfo), typeof(EmployeeEnterDialog));

        #endregion

        #region Properties

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
        #endregion

        #region Constructors

        public AddParkingLotRentalInfoDialog()
        {
            InitializeComponent();
            CurrentParkingLotRentalInfo = new ParkingLotRentalInfo() { Id = Guid.NewGuid().ToString() };
            AvailableParkingLots = GlobalVariables.Smc.Select("SELECT * FROM ParkingLotInfo", null);
            AvailableSocialUnits = GlobalVariables.Smc.Select("SELECT * FROM SocialUnitInfo ", null);
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
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

    }
}
