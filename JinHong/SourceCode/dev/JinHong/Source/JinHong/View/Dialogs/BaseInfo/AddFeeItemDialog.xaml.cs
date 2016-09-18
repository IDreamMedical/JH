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
    /// 新增空调
    /// </summary>
    public partial class AddFeeItemDialog : Window
    {

        #region  public prop
        public NewOrEditFeeItemViewModel ViewModel { get; private set; }

        #endregion


        /// <summary>
        /// 新增空调,对数据库已有空调名进行验证,如果存在,则提示.
        /// </summary>
        /// <param employeeName="userList"></param>
        public AddFeeItemDialog()
        {
            InitializeComponent();
            ViewModel = new NewOrEditFeeItemViewModel { ViewDialog = this };
            ViewModel.Initialize();
            this.DataContext = ViewModel;

        }
        

        
    }
}
