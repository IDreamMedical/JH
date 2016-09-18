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
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// TODO...暂时只是入职登记, 不包括职工信息修改功能
    /// </summary>
    public partial class AddDepositFeesDialog : Window
    {
        public NewOrEditDepositFeeViewModel ViewModel { get; private set; }


        #region Constructors

        public AddDepositFeesDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditDepositFeeViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion
        private void comboBoxSocialUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

    



}
}
