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

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// TODO...暂时只是入职登记, 不包括职工信息修改功能
    /// </summary>
    public partial class AddFeesDialog : Window, INotifyPropertyChanged
    {



        #region Dependency properties
        //        public static readonly DependencyProperty AvailableSocialUnitsProperty
        //= DependencyProperty.Register("AvailableSocialUnits", typeof(object), typeof(LeaseAddDialog));

        //        public static readonly DependencyProperty DepositFeesProperty = DependencyProperty.Register(
        //          "DepositFees", typeof(IncomeAndExpenditure), typeof(AddFeesDialog));
        //        public static readonly DependencyProperty PropertyManagementFeesProperty = DependencyProperty.Register(
        //          "PropertyManagementFees", typeof(IncomeAndExpenditure), typeof(AddFeesDialog));
        //        public static readonly DependencyProperty RentalFeesProperty = DependencyProperty.Register(
        //          "RentalFees", typeof(IncomeAndExpenditure), typeof(AddFeesDialog));

        #endregion

        #region Fields

        private string socialUnitName;

        private string socialUnitId;

        private DateTime? startTime;
        private DateTime? endTime;
        private FeeRecord feeRecords;


        private DepositFeeInfo depositFees;
        private PropertyManagementFeesInfo propertyManagementFees;

        private RentalFeesInfo rentalFees;
        private ParkingFeesInfo parkinglFees;

        private object availableSocialUnits;

        private DataRow payDepositFees;





        //  TODO

        #endregion


        #region Properties


        ///// <summary>
        ///// 押金
        ///// </summary>
        //public IncomeAndExpenditure DepositFees
        //{
        //    get { return (IncomeAndExpenditure)GetValue(DepositFeesProperty); }
        //    set { SetValue(DepositFeesProperty, value); }
        //}
        ///// <summary>
        ///// 物业费
        ///// </summary>
        //public IncomeAndExpenditure PropertyManagementFees
        //{
        //    get { return (IncomeAndExpenditure)GetValue(PropertyManagementFeesProperty); }
        //    set { SetValue(PropertyManagementFeesProperty, value); }
        //}
        ///// <summary>
        ///// 租金
        ///// </summary>
        //public IncomeAndExpenditure RentalFees
        //{
        //    get { return (IncomeAndExpenditure)GetValue(RentalFeesProperty); }
        //    set { SetValue(RentalFeesProperty, value); }
        //}

        //public object AvailableSocialUnits
        //{
        //    get { return GetValue(AvailableSocialUnitsProperty); }
        //    set { SetValue(AvailableSocialUnitsProperty, value); }
        //}

        public FeeRecord FeeRecords
        {
            get { return feeRecords; }
            set
            {
                if (feeRecords != value)
                {
                    feeRecords = value;
                    OnPropertyChanged("FeeRecords");
                }
            }
        }
        public string SocialUnitName
        {
            get { return socialUnitName; }
            set
            {
                if (socialUnitName != value)
                {
                    socialUnitName = value;
                    OnPropertyChanged("SocialUnitName");
                }
            }
        }
        public string SocialUnitId
        {
            get { return socialUnitId; }
            set
            {
                if (socialUnitId != value)
                {
                    socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }

        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    OnPropertyChanged("EndTime");
                }
            }
        }

        public PropertyManagementFeesInfo PropertyManagementFees
        {
            get { return propertyManagementFees; }
            set
            {
                if (propertyManagementFees != value)
                {
                    propertyManagementFees = value;
                    OnPropertyChanged("PropertyManagementFees");
                }
            }
        }
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

        public DepositFeeInfo DepositFees
        {
            get { return depositFees; }
            set
            {
                if (depositFees != value)
                {
                    depositFees = value;
                    OnPropertyChanged("DepositFees");
                }
            }
        }

        public ParkingFeesInfo ParkinglFees
        {
            get { return parkinglFees; }
            set
            {
                if (parkinglFees != value)
                {
                    parkinglFees = value;
                    OnPropertyChanged("ParkinglFees");
                }
            }
        }



        #endregion

        #region Constructors

        public AddFeesDialog()
        {
            InitializeComponent();
            FeeRecords = new FeeRecord();
        }

        #endregion

        #region Methods

        #region Event handlers

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {

            if (ckbRentalFees.IsChecked.Value && this.RentalFees.Amount <= 0)
            {
                MessageBox.Show("租金不能0!请知晓", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                RentalFees.FeeRecordId = FeeRecords.Id;

            }
            if (ckbPropertyManagementFees.IsChecked.Value && this.PropertyManagementFees.Amount <= 0)
            {
                MessageBox.Show("物管费不能0!请知晓", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                PropertyManagementFees.FeeRecordId = FeeRecords.Id;
            }
            if (ckbParkFees.IsChecked.Value && this.ParkinglFees.Amount <= 0)
            {
                MessageBox.Show("停车费不能0!请知晓", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                ParkinglFees.FeeRecordId = FeeRecords.Id;
            }
            try
            {
                FeeRecords.PayDate = ParkinglFees.PayDate = RentalFees.Date = PropertyManagementFees.Date = DateTime.Now;
                GlobalVariables.Smc.Insert<FeeRecord>(FeeRecords);
                GlobalVariables.Smc.Insert<RentalFeesInfo>(RentalFees);
                GlobalVariables.Smc.Insert<PropertyManagementFeesInfo>(PropertyManagementFees);
                GlobalVariables.Smc.Insert<ParkingFeesInfo>(ParkinglFees);
                if (this.gpDeposite.Visibility == Visibility.Visible)
                {
                    if (ckbDepositFees.IsChecked.Value && this.DepositFees.Amount <= 0)
                    {
                        MessageBox.Show("押金不能0!请知晓", "费用录入", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    else
                    {
                       // DepositFees.FeeRecordId = FeeRecords.Id;
                    }
                   // DepositFees.Date = DateTime.Now;
                    GlobalVariables.Smc.Insert<DepositFeeInfo>(DepositFees);
                }

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

        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FeeRecords.Id))
            {
                ///ToDo  加载 合同明细里的物管费信息
                ///ToDo  加载 停车租赁信息的停车费信息
                ///ToDo  加载 押金的物管费信息
                ///ToDo  加载 停车租赁信息的停车费信息
                ///

                StringBuilder querySql = new StringBuilder();
                // 费用主记录FeeRecords
                querySql.Append(@"select EffectiveDate as StartDate,DepositFee as Amount  from   ContractInfo where SocialUnitId='{0}';");
                // 押金
                querySql.Append(@"select a.*  from DepositFeesInfo   a  INNER join  FeeRecord  b  on a.FeeRecordId=b.Id  where b.SocialUnitId='{0}';");
                // 物业费，租金费
                querySql.Append(@"select   (a.Area*a.DayRentalFee) as MonthRentalFee,(a.Area*a.DayPropManageFee) as MonthPropManageFee,SUM(a.Area) as TotalArea from  ContractDetail   a 
INNER  join ContractInfo   b on b.Id=a.ContractId
where b. SocialUnitId='{0}';");

                // 停车费
                querySql.Append(@"select   (a.Area*a.DayRentalFee) as MonthRentalFee,(a.Area*a.DayPropManageFee) as MonthPropManageFee,SUM(a.Area) as TotalArea from  ContractDetail   a 
INNER  join ContractInfo   b on b.Id=a.ContractId
where b. SocialUnitId='{0}';");

                var ds = GlobalVariables.Smc.Select(string.Format(querySql.ToString(), FeeRecords.SocialUnitId));
                FeeRecords.StartDate = Convert.ToDateTime(ds.Tables[0].Rows[0][0]);
                FeeRecords.EndDate = FeeRecords.StartDate.Value.AddMonths(3).AddDays(-1);
                if (payDepositFees != null && Convert.ToInt32(payDepositFees["IsPay"]) == 1)
                {
                    this.gpDeposite.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.gpDeposite.Visibility = Visibility.Visible;
                    //押金
                    DepositFees.Amount = Convert.ToDouble(ds.Tables[1].Rows[0][1]);
                    DepositFees.IsPay = 1;
                }
                // 租金
                RentalFees.Amount = Convert.ToDouble(ds.Tables[0].Rows[2][0]);
                RentalFees.IsPay = 1;
                PropertyManagementFees.Amount = Convert.ToDouble(ds.Tables[2].Rows[0][1]);
                PropertyManagementFees.IsPay = 1;
                FeeRecords.TotalArea = Convert.ToDouble(ds.Tables[2].Rows[0]["TotalArea"]);
                FeeRecords.TotalAmount = RentalFees.Amount + PropertyManagementFees.Amount + DepositFees.Amount;
            }

        }



    }
}
