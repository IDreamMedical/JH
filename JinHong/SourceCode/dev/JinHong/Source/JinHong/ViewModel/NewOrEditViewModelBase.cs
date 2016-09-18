using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UniGuy.Windows.ViewModels;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 弹出框ViewModel 基类
    /// </summary>
    public class NewOrEditViewModelBase : ViewModelBase
    {

        #region private Field

        private Window _viewDialog;

        private Action _refreshParentForm;


        #endregion

        #region Prop

        public Window ViewDialog
        {
            get { return _viewDialog; }
            set { _viewDialog = value; }
        }
        public Action RefreshParentForm
        {
            get { return _refreshParentForm; }
            set { _refreshParentForm = value; }
        }


        #region Command

        /// <summary>
        /// 确定按钮
        /// </summary>
        public ICommand BtnOKCommand { get; set; }
        /// <summary>
        /// 保存并继续按钮
        /// </summary>
        public ICommand BtnContinueAddCommand { get; set; }

        /// <summary>
        /// 取消按钮
        /// </summary>
        public ICommand BtnCancelCommand { get; set; }
        #endregion

        #endregion

        #region

        public virtual void Cancel()
        {
            if (null != ViewDialog)
            {
                this.ViewDialog.Close();
            }
        }




        #endregion
    }
}
