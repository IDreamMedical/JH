using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 新增或编辑Building
    /// </summary>
    public class NewOrEditBuildingViewModel : NewOrEditViewModelBase
    {

        #region private Field

        private BuildingInfo _building;


        private static readonly Lazy<IBuildingService> lazy = new Lazy<IBuildingService>(() => new BuildingService());

        #endregion

        #region Public Prop
        public BuildingInfo Building
        {
            get { return _building; }
            set
            {
                if (_building != value)
                {
                    _building = value;
                    OnPropertyChanged("Building");
                }
            }
        }
        public static IBuildingService Service { get { return lazy.Value; } }

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
                    result = Service.CreateBuilding(Building);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateBuilding(Building);

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
                    return Service.HasBuilding(string.Empty, Building.Name);
                case OperateModeEnum.Edit:
                    return Service.HasBuilding(Building.Id, Building.Name);
                default:
                    return false;
            }

        }


        #endregion

    }
}
