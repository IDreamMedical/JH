using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Core.Model;
using UniGuy.Entity;
using System.Linq;
using JinHong.ViewModel;
using System.ComponentModel;

namespace JinHong.View.Dialogs
{
    /// <summary>
    /// 新增角色
    /// </summary>
    public partial class AddRoleDialog : Window
    {

        #region  public prop
        public NewOrEditRoleViewModel ViewModel { get; private set; }

        #endregion

        public AddRoleDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditRoleViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;
        }
    }
}
