using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.Model;
using System;
using JinHong.ViewModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增SocialUnitInfo
    /// </summary>
    public partial class AddSocialUnitInfoDialog : Window
    {

        #region  public prop
        public NewOrEditSocialUnitVM ViewModel { get; private set; }

        #endregion

        public AddSocialUnitInfoDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditSocialUnitVM { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }
    }
}
