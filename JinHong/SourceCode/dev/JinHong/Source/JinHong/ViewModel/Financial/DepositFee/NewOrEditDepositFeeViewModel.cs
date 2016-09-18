using JinHong.Model;
using JinHong.ServiceContract;
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
    public class NewOrEditDepositFeeViewModel : NewOrEditViewModelBase
    {

        #region private var
        private DepositFeeInfo _depositFee;
        private static readonly Lazy<IDepositFeeService> lazy = new Lazy<IDepositFeeService>(() => new DepositFeeService());
        #endregion


        #region Public Prop


        public DepositFeeInfo DepositFee
        {
            get { return _depositFee; }
            set
            {
                if (_depositFee != value)
                {
                    _depositFee = value;
                    OnPropertyChanged("DepositFee");
                }
            }
        }

        public static IDepositFeeService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditRole);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditRole()
        {
            var result = false;

            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateDepositFee(DepositFee);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateDepositFee(DepositFee);

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



        #endregion



    }
}
