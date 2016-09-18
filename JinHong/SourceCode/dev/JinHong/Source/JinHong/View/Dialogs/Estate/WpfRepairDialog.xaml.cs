using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.ComponentModel;
using System.Data;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 维修记录添加明细
    /// </summary>
    public partial class WpfRepairDialog : Window
    {
        #region Public  ViewModel
        public NewOrEditRepairViewModel ViewModel { get; set; }
        #endregion


        #region Constructors
        public WpfRepairDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditRepairViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }
        #endregion


    }
}
