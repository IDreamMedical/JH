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
using UniGuy.Windows.View;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 车位租赁信息
    /// </summary>
    public partial class AddParkingRentalDialog : Window
    {
        #region Public Prop


        public NewOrEditRentalParkingViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public AddParkingRentalDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditRentalParkingViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion
    }
}
