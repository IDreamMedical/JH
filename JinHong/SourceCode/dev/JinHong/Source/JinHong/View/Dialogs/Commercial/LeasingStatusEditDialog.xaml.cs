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
    public partial class LeasingStatusEditDialog : Window, INotifyPropertyChanged
    {

        #region Fields

        private UnLeaseDetail currentUnLeaseDetail;


        //  TODO

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty LeasingInfoProperty = DependencyProperty.Register(
            "LeasingInfo", typeof(WpfCheckIn), typeof(LeasingStatusEditDialog));
        public static readonly DependencyProperty SocialUnitInfoProperty = DependencyProperty.Register(
            "SocialUnitInfo", typeof(SocialUnitInfo), typeof(LeasingStatusEditDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty RentalDepositFeesInfoTblProperty = DependencyProperty.Register(
            "RentalDepositFeesInfoTbl", typeof(DataTable), typeof(LeasingStatusEditDialog));
        //  界面直接绑定其中的索引为0-9的
        public static readonly DependencyProperty PropertyManagementFeesInfoTblProperty = DependencyProperty.Register(
            "PropertyManagementFeesInfoTbl", typeof(DataTable), typeof(LeasingStatusEditDialog));

        public static readonly DependencyProperty FeesInfoTblProperty = DependencyProperty.Register(
           "FeesInfoTbl", typeof(DataTable), typeof(LeasingStatusEditDialog));

        public static readonly DependencyProperty FeesInfoTbl1Property = DependencyProperty.Register(
           "FeesInfoTbl1", typeof(DataTable), typeof(LeasingStatusEditDialog));

        //  TODO

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

        public DataTable FeesInfoTbl
        {
            get { return (DataTable)GetValue(FeesInfoTblProperty); }
            set { SetValue(FeesInfoTblProperty, value); }
        }

        public DataTable FeesInfoTbl1
        {
            get { return (DataTable)GetValue(FeesInfoTbl1Property); }
            set { SetValue(FeesInfoTbl1Property, value); }
        }



        public UnLeaseDetail CurrentUnLeaseDetail
        {
            get { return currentUnLeaseDetail; }
            set
            {
                if (currentUnLeaseDetail != value)
                {
                    currentUnLeaseDetail = value;
                    OnPropertyChanged("CurrentUnLeaseDetail");
                }
            }
        }

        #endregion

        #region Constructors

        public LeasingStatusEditDialog()
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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SocialUnitInfo.Id))
            {
                SocialUnitInfo.Id = Guid.NewGuid().ToString();
            }
            GlobalVariables.Smc.Update<SocialUnitInfo>(SocialUnitInfo);
            DialogResult = true;
        }

        #endregion

        private void Add_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Del_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
           // e.CanExecute = CurrentUnLeaseDetail != null;

        }

        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Del_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CurrentUnLeaseDetail = this.dataGridDailyIncomeInfoTbl.SelectedCells[0].Item as UnLeaseDetail;
            if (MessageBox.Show("确定删除？ ", "系统提示", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                DataRowView row = dataGridDailyIncomeInfoTbl.SelectedValue as DataRowView;
                if (CurrentUnLeaseDetail != null)
                {
                    GlobalVariables.Smc.Delete<UnLeaseDetail>(CurrentUnLeaseDetail.Id);
                }
                if (row != null)
                {
                    this.FeesInfoTbl1.Rows.Remove(row.Row);
                    dataGridDailyIncomeInfoTbl.Items.Refresh();
                }
            }

        }

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

        private void dataGridDailyIncomeInfoTbl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentUnLeaseDetail = this.dataGridDailyIncomeInfoTbl.SelectedCells[0].Item as UnLeaseDetail;
        }

    }
}
