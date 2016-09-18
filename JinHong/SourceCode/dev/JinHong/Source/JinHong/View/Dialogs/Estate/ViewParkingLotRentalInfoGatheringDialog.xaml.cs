using System;
using System.Windows;
using System.Diagnostics;
using System.Data;
using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class ViewParkingLotRentalInfoGatheringDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ParkingLotRentalInfoDetailTblProperty
            = DependencyProperty.Register("ParkingLotRentalInfoDetailTbl", typeof(DataTable), typeof(ViewParkingLotRentalInfoGatheringDialog));

        #endregion

        #region Properties

        public DataTable ParkingLotRentalInfoDetailTbl
        {
            get { return (DataTable)GetValue(ParkingLotRentalInfoDetailTblProperty); }
            set { SetValue(ParkingLotRentalInfoDetailTblProperty, value); }
        }

        #endregion

        #region Constructors

        public ViewParkingLotRentalInfoGatheringDialog()
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
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

        #endregion
    }
}
