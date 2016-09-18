using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using System.ComponentModel;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增仓库
    /// </summary>
    public partial class AddOrEditWareHouseInfoDialog : Window
    {

        public NewOrEditWareHouseViewModel ViewModel { get; private set; }

        public AddOrEditWareHouseInfoDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditWareHouseViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

    }
}
