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
    /// 职工登记
    /// </summary>
    public partial class WpfEmployeeDialog : Window
    {
        #region Public  ViewModel
        public NewOrEditEmployeeViewModel ViewModel { get; set; }
        #endregion

        #region Constructors

        public WpfEmployeeDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditEmployeeViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }

        #endregion

    }
}
