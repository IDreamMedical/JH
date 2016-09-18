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
    public class NewOrEditAirConditionerViewModel : NewOrEditViewModelBase
    {
        #region private Field

        private AirConditioner _airConditioner;


        private static readonly Lazy<IAirConditioneService> lazy = new Lazy<IAirConditioneService>(() => new AirConditionerService());

        #endregion

        #region Public Prop
        public AirConditioner AirConditioner
        {
            get { return _airConditioner; }
            set
            {
                if (_airConditioner != value)
                {
                    _airConditioner = value;
                    OnPropertyChanged("AirConditioner");
                }
            }
        }
        public static IAirConditioneService Service { get { return lazy.Value; } }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditBuilding);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditBuilding()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该楼宇已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateAirConditioner(AirConditioner);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateAirConditioner(AirConditioner);

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
                    return Service.HasAirConditioner(string.Empty, AirConditioner.Name);
                case OperateModeEnum.Edit:
                    return Service.HasAirConditioner(AirConditioner.Id, AirConditioner.Name);
                default:
                    return false;
            }

        }


        #endregion

    }
}
