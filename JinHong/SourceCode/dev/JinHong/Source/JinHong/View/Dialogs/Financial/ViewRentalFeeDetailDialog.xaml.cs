using System;
using System.Windows;
using System.Diagnostics;
using System.Data;
using JinHong.Model;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    public partial class ViewRentalFeeDetailDialog : Window, INotifyPropertyChanged
    {

        #region Field

        private string _info;


        #endregion
        #region Dependency properties

        public static readonly DependencyProperty RentalFeeDetailTblProperty
            = DependencyProperty.Register("RentalFeeDetailTbl", typeof(DataTable), typeof(ViewRentalFeeDetailDialog));
        public static readonly DependencyProperty SocialUnitNameProperty
           = DependencyProperty.Register("SocialUnitName", typeof(String), typeof(ViewRentalFeeDetailDialog));


        #endregion

        #region Properties

        public DataTable RentalFeeDetailTbl
        {
            get { return (DataTable)GetValue(RentalFeeDetailTblProperty); }
            set { SetValue(RentalFeeDetailTblProperty, value); }


        }

        public string SocialUnitName
        {
            get { return (string)GetValue(SocialUnitNameProperty); }
            set { SetValue(SocialUnitNameProperty, value); }


        }
        public string Info
        {
            get { return _info; }
            set
            {
                if (_info != value)
                {
                    _info = value;
                    OnPropertyChanged("Info");
                }
            }
        }


        #endregion

        #region Constructors

        public ViewRentalFeeDetailDialog()
        {
            InitializeComponent();

        }



        #endregion

        #region Methods


        public void SetInfo()
        {
            if (RentalFeeDetailTbl != null && RentalFeeDetailTbl.Rows.Count > 0)
            {
                this.Info = string.Format("总计：{0}条记录", RentalFeeDetailTbl.Rows.Count);
            }
            else
            {
                this.Info = string.Format("没有记录！");
            }

        }

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
