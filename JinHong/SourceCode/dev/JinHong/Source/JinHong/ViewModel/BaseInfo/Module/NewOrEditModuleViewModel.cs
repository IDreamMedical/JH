using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Core;
using JinHong.ServiceContract;
using JinHong.Services;
using Microsoft.Windows.Controls;
using UniGuy.Corek;
using UniGuy.Commands;
using System.Collections.ObjectModel;
using System.Data;
using JinHong.Extensions;

namespace JinHong.ViewModel
{
    public class NewOrEditModuleViewModel : NewOrEditViewModelBase
    {
        #region private Field
        private Module _module;
        private static readonly Lazy<IModuleService> lazy = new Lazy<IModuleService>(() => new ModuleService());


        #endregion
        #region Public Prop
        public Module Module
        {
            get { return _module; }
            set
            {
                if (_module != value)
                {
                    _module = value;
                    OnPropertyChanged("Module");
                }
            }
        }

        public static IModuleService Service { get { return lazy.Value; } }


        /// <summary>
        /// 父节点集合
        /// </summary>
        public ObservableCollection<Module> ParentModules { get; set; }

        #endregion


        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.ViewModelTitleName = "新建菜单";
            InitSysParentModules();
            this.BtnOKCommand = new DelegateCommand(CreateOrEditModule);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }
        #endregion

        #region Private Method

        private void CreateOrEditModule()
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
                    result = Service.CreateModule(Module);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateModule(Module);

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
                    return Service.HasModule(string.Empty, Module.Name);
                case OperateModeEnum.Edit:
                    return Service.HasModule(Module.Id, Module.Name);
                default:
                    return false;
            }

        }

        private void InitSysParentModules()
        {
            ParentModules = new ObservableCollection<Module>();
            var mDataTbl = Service.GetAllModules();
            var mDataView = mDataTbl.DefaultView;
            mDataView.RowFilter = string.Format("PId='0'");
            foreach (DataRow item in mDataView.ToTable().Rows)
            {
                ParentModules.Add(item.BuildEntity<Module>());
            };

        }


        #endregion



    }
}
