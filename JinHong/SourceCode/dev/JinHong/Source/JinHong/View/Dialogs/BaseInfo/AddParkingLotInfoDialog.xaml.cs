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
    /// 新增车位
    /// </summary>
    public partial class AddParkingLotInfoDialog : Window
    {
        #region public Prop

        public NewOrEditParkingViewModel ViewModel { get; private set; }

        #endregion


        #region Constructors

        public AddParkingLotInfoDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditParkingViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

        #endregion
    }
}
