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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UniGuy.Printing;
using JinHong.Model;
using System.Data;

namespace JinHong.View.Dialogs
{
    public partial class WareHouseLeasingStatusDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty LeasingInfoProperty = DependencyProperty.Register(
            "LeasingInfo", typeof(WareHouseLeasingInfo), typeof(WareHouseLeasingStatusDialog));
        public static readonly DependencyProperty SocialUnitInfoProperty = DependencyProperty.Register(
            "SocialUnitInfo", typeof(SocialUnitInfo), typeof(WareHouseLeasingStatusDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty RentalDepositFeesInfoTblProperty = DependencyProperty.Register(
            "RentalDepositFeesInfoTbl", typeof(DataTable), typeof(WareHouseLeasingStatusDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty PropertyManagementFeesInfoTblProperty = DependencyProperty.Register(
            "PropertyManagementFeesInfoTbl", typeof(DataTable), typeof(WareHouseLeasingStatusDialog));

        //  TODO

        #endregion

        #region Properties

        public WareHouseLeasingInfo LeasingInfo
        {
            get { return (WareHouseLeasingInfo)GetValue(LeasingInfoProperty); }
            set { SetValue(LeasingInfoProperty, value); }
        }

        public SocialUnitInfo SocialUnitInfo
        {
            get { return (SocialUnitInfo)GetValue(SocialUnitInfoProperty); }
            set { SetValue(SocialUnitInfoProperty, value); }
        }

        public DataTable RentalDepositFeesInfoTbl
        {
            get { return (DataTable)GetValue(RentalDepositFeesInfoTblProperty); }
            set { SetValue(RentalDepositFeesInfoTblProperty, value); }
        }

        public DataTable PropertyManagementFeesInfoTbl
        {
            get { return (DataTable)GetValue(PropertyManagementFeesInfoTblProperty); }
            set { SetValue(PropertyManagementFeesInfoTblProperty, value); }
        }

        #endregion

        #region Constructors

        public WareHouseLeasingStatusDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            //  TODO...Edit
            //  throw new NotImplementedException();
            PrintHelper.PrintPreview(this, dpMain);
        }

        #endregion

        #endregion
    }
}
