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

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// TODO...暂时只是入职登记, 不包括职工信息修改功能
    /// </summary>
    public partial class AddRentalFeesDialog : Window, INotifyPropertyChanged
    {



        #region Dependency properties
        //        public static readonly DependencyProperty AvailableSocialUnitsProperty
        //= DependencyProperty.Register("AvailableSocialUnits", typeof(object), typeof(LeaseAddDialog));

        //        public static readonly DependencyProperty DepositFeesProperty = DependencyProperty.Register(
        //          "DepositFees", typeof(IncomeAndExpenditure), typeof(AddRentalFeesDialog));
        //        public static readonly DependencyProperty PropertyManagementFeesProperty = DependencyProperty.Register(
        //          "PropertyManagementFees", typeof(IncomeAndExpenditure), typeof(AddRentalFeesDialog));
        //        public static readonly DependencyProperty RentalFeesProperty = DependencyProperty.Register(
        //          "RentalFees", typeof(IncomeAndExpenditure), typeof(AddRentalFeesDialog));

        #endregion

        #region Fields

        private RentalFeesInfo rentalFees;

        private object availableSocialUnits;





        //  TODO

        #endregion


        #region Properties
        public RentalFeesInfo RentalFees
        {
            get { return rentalFees; }
            set
            {
                if (rentalFees != value)
                {
                    rentalFees = value;
                    OnPropertyChanged("RentalFees");
                }
            }
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





        #endregion

        #region Constructors

        public AddRentalFeesDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #region Common Method
        /// <summary>
        /// 生成下一个季度的租金费用信息
        /// </summary>
        /// <param name="prePMFInfo"></param>
        private void GenNextPMFInfo(RentalFeesInfo preRFInfo)
        {
            RentalFeesInfo rfi = new RentalFeesInfo()
            {
                Id = Guid.NewGuid().ToString(),
                TimeFrom = preRFInfo.TimeTo.AddDays(1),
                TimeTo = preRFInfo.TimeTo.AddMonths(3).AddDays(-1),
                SocialUnitId = preRFInfo.SocialUnitId,
                SocialUnitName = preRFInfo.SocialUnitName,
                IsPay = 0,
                Amount = preRFInfo.Amount,
                Date = DateTime.Now,

            };
            GlobalVariables.Smc.Insert<RentalFeesInfo>(rfi);


        }


        #endregion


        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (RentalFees == null)
            {
                MessageBox.Show("没有数据，请知晓！", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (RentalFees.Amount <= 0)
            {
                MessageBox.Show("租金不能小于等于0!请知晓", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {

                GlobalVariables.Smc.Update<RentalFeesInfo>(RentalFees);
                GenNextPMFInfo(RentalFees);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                DataRow row = null;
                if (e.AddedItems[0] is DataRow)
                    row = (DataRow)e.AddedItems[0];
                else if (e.AddedItems[0] is DataRowView)
                    row = ((DataRowView)e.AddedItems[0]).Row;
                if (row != null)
                {
                    RentalFees = row.BuildEntity<RentalFeesInfo>();
                    RentalFees.IsPay = 1;
                    RentalFees.Date = DateTime.Now;
                }
            }



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
