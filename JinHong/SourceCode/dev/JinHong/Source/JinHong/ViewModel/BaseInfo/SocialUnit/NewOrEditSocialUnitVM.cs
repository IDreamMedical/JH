using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using Microsoft.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 添加社会单位VM
    /// </summary>
    public class NewOrEditSocialUnitVM : NewOrEditViewModelBase
    {

        #region private var
        private SocialUnitInfo _socialUnit;
        private static readonly Lazy<ISocialUnitService> lazy = new Lazy<ISocialUnitService>(() => new SocialUnitService());

        #endregion

        #region public  prop

        public SocialUnitInfo SocialUnit
        {
            get { return _socialUnit; }
            set
            {
                if (_socialUnit != value)
                {
                    _socialUnit = value;
                    OnPropertyChanged("SocialUnit");
                }
            }
        }

        public static ISocialUnitService Service { get { return lazy.Value; } }

        #endregion


        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditSocialUnit);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditSocialUnit()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该客户已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateSocialUnit(SocialUnit);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateSocialUnit(SocialUnit);

                    break;
                default:
                    break;
            }
            if (result)
            {
                MessageBox.Show("保存成功！", "系统提示");
                if (null != base.RefreshParentForm)
                {
                    RefreshParentForm.Invoke();
                    ViewDialog.Close();
                }
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
                    return Service.HasSocialUnit(string.Empty, SocialUnit.Name);
                case OperateModeEnum.Edit:
                    return Service.HasSocialUnit(SocialUnit.Id, SocialUnit.Name);
                default:
                    return false;
            }

        }


        #endregion






    }
}
