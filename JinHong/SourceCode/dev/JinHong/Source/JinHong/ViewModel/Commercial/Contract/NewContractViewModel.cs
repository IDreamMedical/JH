using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs.Commercial;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 合同主表
    /// </summary>
    public class NewContractViewModel : NewOrEditViewModelBase
    {

        #region private var

        private ContractInfo _contract;

        private ObservableCollection<SocialUnitInfo> _socialUnits;

        private ObservableCollection<ContractDetail> _contractDetails;

        private static readonly Lazy<IContractService> lazy = new Lazy<IContractService>(() => new ContractService());




        #endregion


        #region public  Prop
        /// <summary>
        /// 合同列表
        /// </summary>
        public ContractInfo Contract
        {
            get { return _contract; }
            set
            {
                if (_contract != value)
                {
                    _contract = value;
                    OnPropertyChanged("Contract");
                }
            }
        }

        public ObservableCollection<ContractDetail> ContractDetails
        {
            get { return _contractDetails; }
            set
            {
                if (_contractDetails != value)
                {
                    _contractDetails = value;
                    OnPropertyChanged("ContractDetails");
                }
            }
        }
        /// <summary>
        /// 客户列表
        /// </summary>
        public ObservableCollection<SocialUnitInfo> SocialUnits
        {
            get { return _socialUnits; }
            set
            {

                if (_socialUnits != value)
                {
                    _socialUnits = value;
                    OnPropertyChanged("SocialUnits");
                }
            }
        }



        public static IContractService Service { get { return lazy.Value; } }


        #region Commond

        /// <summary>
        /// 添加明细
        /// </summary>
        public ICommand BtnAddContractDetail { get; set; }

        /// <summary>
        /// 添加合同
        /// </summary>
        public ICommand BtnAddContractFile { get; set; }
        #endregion
        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditContract);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
            this.BtnAddContractDetail = new DelegateCommand(AddContractDetail);
            this.BtnAddContractFile = new DelegateCommand(AddContractFile);

        }
        #endregion

        #region Private Method

        private void CreateOrEditContract()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该合同已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateContract(Contract);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateContract(Contract);

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
        /// 添加明细
        /// </summary>
        private void AddContractDetail()
        {
            var dlg = new NewContractDetail();
            dlg.ViewModel.ContractDetail = new ContractDetail
            {
                Id = Guid.NewGuid().ToString(),
                ContractId = this.Contract.Id
            };
            dlg.ViewModel.RefreshParentForm = RefreshDetailListView;
            dlg.ShowDialog();
        }

        /// <summary>
        /// 添加合同
        /// </summary>
        private void AddContractFile()
        {

        }

        /// <summary>
        /// 刷新ListView
        /// </summary>
        /// <param name="contractDetail"></param>
        private void RefreshDetailListView(ContractDetail contractDetail)
        {
            if (null == this.ContractDetails)
            {
                ContractDetails = new ObservableCollection<ContractDetail>();
            }
            ContractDetails.Add(contractDetail);
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
                    return Service.HasContract(this.Contract.SocialUnitId);
                default:
                    return false;
            }

        }


        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {

            return true;
        }

        #endregion

    }
}
