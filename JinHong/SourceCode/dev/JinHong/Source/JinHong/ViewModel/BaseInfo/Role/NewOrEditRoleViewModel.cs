using JinHong.ServiceContract;
using JinHong.Services;
using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Commands;
using UniGuy.Corek;
using UniGuy.Entity;

namespace JinHong.ViewModel
{
    public class NewOrEditRoleViewModel : NewOrEditViewModelBase
    {

        #region private Field

        private Role _role;

        private static readonly Lazy<IRoleService> lazy = new Lazy<IRoleService>(() => new RoleService());

        #endregion

        #region Public Prop


        public Role Role
        {
            get { return _role; }
            set
            {

                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged("Role");
                }
            }
        }

        public static IRoleService Service { get { return lazy.Value; } }

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
            if (IsExist())
            {
                MessageBox.Show("该角色已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateRole(Role);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateRole(Role);

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
                    return Service.HasRole(string.Empty, Role.Name);
                case OperateModeEnum.Edit:
                    return Service.HasRole(Role.Id, Role.Name);
                default:
                    return false;
            }

        }

        #endregion


    }
}
