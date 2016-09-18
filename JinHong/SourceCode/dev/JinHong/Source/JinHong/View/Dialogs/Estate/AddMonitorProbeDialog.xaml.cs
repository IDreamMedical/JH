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
    /// 新增探头
    /// </summary>
    public partial class AddMonitorProbeDialog : Window
    {

        #region Public  ViewModel
        public NewOrEditProbeViewModel ViewModel { get; set; }
        #endregion

        /// <summary>
        /// 新增探头,对数据库已有探头名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddMonitorProbeDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditProbeViewModel();
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }
    }
}
