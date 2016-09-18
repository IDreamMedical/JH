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
using System.Collections.ObjectModel;
using System.Data;
using JinHong.Extensions;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 添加停车费
    /// </summary>
    public partial class AddOrEditParkingFeeDialog : Window
    {
        #region Prop
        public NewOrEditParkingFeeViewModel ViewModel { get; private set; }
        #endregion

        #region Constructors

        public AddOrEditParkingFeeDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditParkingFeeViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion


        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //ParkingFeesInfoTbl = GlobalVariables.Smc.Select(string.Format("select  Id, ParkingLotId,Amount,'' as TimeFrom ,'' as TimeTo,LicensePlateNo,   ParkingType, Notes,''as TotalMoney from ParkingLotRentalInfo where SocialUnitId='{0}'", CurrentSocialUnitInfo.Id)).Tables[0];

        }



    }
}
