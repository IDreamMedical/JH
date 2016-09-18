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
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    public partial class LeasingStatusViewDialog : Window, INotifyPropertyChanged
    {


        #region Filed
        private DataTable viewFeesInfoTbl;

        private DataTable viewFeesInfoTbl1;


        #endregion
        #region Dependency properties

        public static readonly DependencyProperty LeasingInfoProperty = DependencyProperty.Register(
            "LeasingInfo", typeof(WpfCheckIn), typeof(LeasingStatusViewDialog));
        public static readonly DependencyProperty SocialUnitInfoProperty = DependencyProperty.Register(
            "SocialUnitInfo", typeof(SocialUnitInfo), typeof(LeasingStatusViewDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty RentalDepositFeesInfoTblProperty = DependencyProperty.Register(
            "RentalDepositFeesInfoTbl", typeof(DataTable), typeof(LeasingStatusViewDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty PropertyManagementFeesInfoTblProperty = DependencyProperty.Register(
            "PropertyManagementFeesInfoTbl", typeof(DataTable), typeof(LeasingStatusViewDialog));

        public static readonly DependencyProperty ViewFeesInfoTblProperty = DependencyProperty.Register(
            "ViewFeesInfoTbl", typeof(DataTable), typeof(LeasingStatusEditDialog));

        public static readonly DependencyProperty ViewFeesInfoTbl1Property = DependencyProperty.Register(
           "ViewFeesInfoTbl1", typeof(DataTable), typeof(LeasingStatusEditDialog));

        #endregion

        #region Properties

        public WpfCheckIn LeasingInfo
        {
            get { return (WpfCheckIn)GetValue(LeasingInfoProperty); }
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


        public DataTable ViewFeesInfoTbl
        {
            get { return viewFeesInfoTbl; }
            set
            {
                if (viewFeesInfoTbl != value)
                {
                    viewFeesInfoTbl = value;
                    OnPropertyChanged("ViewFeesInfoTbl");
                }
            }
        }

        public DataTable ViewFeesInfoTbl1
        {
            get { return viewFeesInfoTbl1; }
            set
            {
                if (viewFeesInfoTbl1 != value)
                {
                    viewFeesInfoTbl1 = value;
                    OnPropertyChanged("ViewFeesInfoTbl1");
                }
            }
        }

        #endregion

        #region Constructors

        public LeasingStatusViewDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {

            PrintHelper.PrintPreview(this, dpMain);
        }

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

        #endregion
    }
}
