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
    /// 新增楼宇
    /// </summary>
    public partial class AddBuildingInfoDialog : Window
    {
        #region public Prop

        public NewOrEditBuildingViewModel ViewModel { get; private set; }

        #endregion

        public AddBuildingInfoDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditBuildingViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }

    }
}
