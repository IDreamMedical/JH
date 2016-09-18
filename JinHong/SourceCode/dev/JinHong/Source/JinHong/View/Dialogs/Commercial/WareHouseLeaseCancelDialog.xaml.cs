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
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    public partial class WareHouseLeaseCancelDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty LeasingInfoProperty = DependencyProperty.Register(
            "LeasingInfo", typeof(WareHouseRentalViewModel), typeof(WareHouseLeaseCancelDialog));

        #endregion

        #region Fields

        //  TODO

        #endregion

        #region Properties

        public WareHouseRentalViewModel WareHouseLeasingVM
        {
            get { return (WareHouseRentalViewModel)GetValue(LeasingInfoProperty); }
            set { SetValue(LeasingInfoProperty, value); }
        }

        #endregion

        #region Constructors

        public WareHouseLeaseCancelDialog()
        {
            InitializeComponent();
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
    }
}
