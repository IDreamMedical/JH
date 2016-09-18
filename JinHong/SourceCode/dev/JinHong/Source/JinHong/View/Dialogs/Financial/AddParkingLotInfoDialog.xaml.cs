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
    public partial class AddParkingLotInfoDialog : Window, INotifyPropertyChanged
    {
        #region Fields


        private ParkingLotInfo currentParkingLotInfo;



        //  TODO

        #endregion


        #region Dependency properties

        public static readonly DependencyProperty EmployeeInfoProperty = DependencyProperty.Register(
            "EmployeeInfo", typeof(ParkingLotInfo), typeof(AddParkingLotInfoDialog));

        #endregion

        #region Properties
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

        public AddParkingLotInfoDialog()
        {
            InitializeComponent();
            CurrentParkingLotInfo = new ParkingLotInfo();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentParkingLotInfo.Id))
            {
                MessageBox.Show("车位号不能为K空!请知晓", "车位信息", MessageBoxButton.OK, MessageBoxImage.Warning);
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

    }
}
