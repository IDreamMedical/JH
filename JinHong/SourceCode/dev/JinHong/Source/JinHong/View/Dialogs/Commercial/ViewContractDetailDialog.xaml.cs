using System;
using System.Windows;
using System.Diagnostics;
using System.Data;
using JinHong.Model;

namespace JinHong.View.Dialogs
{
    public partial class ViewContractDetailDialog : Window
    {
        #region Dependency properties

        public static readonly DependencyProperty ContractDetailTblProperty
            = DependencyProperty.Register("ContractDetailTbl", typeof(DataTable), typeof(ViewContractDetailDialog));
        public static readonly DependencyProperty SocialUnitNameProperty
           = DependencyProperty.Register("SocialUnitName", typeof(String), typeof(ViewContractDetailDialog));


        #endregion

        #region Properties

        public DataTable ContractDetailTbl
        {
            get { return (DataTable)GetValue(ContractDetailTblProperty); }
            set { SetValue(ContractDetailTblProperty, value); }


        }

        public string SocialUnitName
        {
            get { return (string)GetValue(SocialUnitNameProperty); }
            set { SetValue(SocialUnitNameProperty, value); }


        }


        #endregion

        #region Constructors

        public ViewContractDetailDialog()
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
