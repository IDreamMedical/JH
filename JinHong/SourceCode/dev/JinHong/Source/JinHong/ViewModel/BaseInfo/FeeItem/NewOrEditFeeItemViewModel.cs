
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 费用项目Item
    /// </summary>
    public class NewOrEditFeeItemViewModel : NewOrEditViewModelBase
    {
        #region private Field

        private FeeItem _feeItem;
        private static readonly Lazy<IFeeItemService> lazy = new Lazy<IFeeItemService>(() => new FeeItemService());

        #endregion

        #region Public Prop


        public FeeItem FeeItem
        {
            get { return _feeItem; }
            set
            {
                if (_feeItem != value)
                {
                    _feeItem = value;
                    OnPropertyChanged("FeeItem");
                }
            }
        }

        public static IFeeItemService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditFeeItem);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditFeeItem()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该费用项目已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateFeeItem(FeeItem);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateFeeItem(FeeItem);

                    break;
                default:
                    break;
            }
            if (result)
            {
                MessageBox.Show("保存成功！", "系统提示");
                if (null != RefreshParentForm)
                {
                    RefreshParentForm.Invoke();
                }
                base.Cancel();
            }
            else
            {
                MessageBox.Show("保存失败！", "系统提示");
            }

        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        private bool IsExist()
        {
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    return Service.HasFeeItem(string.Empty, FeeItem.Id);
                case OperateModeEnum.Edit:
                    return Service.HasFeeItem(FeeItem.Id, FeeItem.Name);
                default:
                    return false;
            }

        }

        #endregion

    }
}
