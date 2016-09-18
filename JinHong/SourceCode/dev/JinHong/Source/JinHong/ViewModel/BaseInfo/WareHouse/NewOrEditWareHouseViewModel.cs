using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Model;
using JinHong.Services;
using UniGuy.Corek;
using UniGuy.Commands;
using Microsoft.Windows.Controls;
using JinHong.ServiceContract;

namespace JinHong.ViewModel
{
    public class NewOrEditWareHouseViewModel : NewOrEditViewModelBase
    {

        #region private Field
        private WareHouseInfo _wareHouse;


        private static readonly Lazy<WareHouseService> lazy = new Lazy<WareHouseService>(() => new WareHouseService());

        #endregion

        #region Public Prop
        public static IWareHouseService Service { get { return lazy.Value; } }
        public WareHouseInfo WareHouse
        {
            get { return _wareHouse; }
            set
            {
                if (_wareHouse != value)
                {
                    _wareHouse = value;
                    OnPropertyChanged("WareHouse");
                }
            }
        }

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditWareHouse);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditWareHouse()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该仓库已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateWareHouse(WareHouse);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateWareHouse(WareHouse);

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
                    return Service.HasWareHouse(string.Empty, WareHouse.Name);
                case OperateModeEnum.Edit:
                    return Service.HasWareHouse(WareHouse.Id, WareHouse.Name);
                default:
                    return false;
            }

        }


        #endregion

    }
}
