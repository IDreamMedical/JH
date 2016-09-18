using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    public class AddContractDetailVM : NewOrEditViewModelBase
    {
        #region private var
        private ContractInfo _contractInfo;

        private ContractDetail _contractDetail;
        private ObservableCollection<BuildingInfo> _buildings;
        private ObservableCollection<RoomInfo> _rooms;
        private static readonly Lazy<IContractDetailService> lazy = new Lazy<IContractDetailService>(() => new ContractDetailService());

        #endregion

        #region public  prop

        public ContractInfo ContractInfo
        {
            get { return _contractInfo; }
            set
            {
                if (_contractInfo != value)
                {
                    _contractInfo = value;
                    OnPropertyChanged("ContractInfo");
                }
            }
        }


        public ContractDetail ContractDetail
        {
            get { return _contractDetail; }
            set
            {
                if (_contractDetail != value)
                {
                    _contractDetail = value;
                    OnPropertyChanged("ContractDetail");
                }
            }
        }

        public ObservableCollection<RoomInfo> Rooms
        {
            get { return _rooms; }
            set
            {
                if (_rooms != value)
                {
                    _rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }

        public ObservableCollection<BuildingInfo> Buildings
        {
            get { return _buildings; }
            set
            {

                if (_buildings != value)
                {
                    _buildings = value;
                    OnPropertyChanged("Buildings");
                }
            }
        }

        public static IContractDetailService Service { get { return lazy.Value; } }

        /// <summary>
        /// 刷新主窗体
        /// </summary>
        public Action<ContractDetail> RefreshParentForm { get; set; }

        #endregion


        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            Buildings = BuildingsHelper.InitializeBuildings();
            this.BtnOKCommand = new DelegateCommand(CreateContractDetail);
            this.BtnContinueAddCommand = new DelegateCommand(CreateContractDetail);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
        }

        public void BuildingChanged(object sender, SelectionChangedEventArgs e)
        {
            global::System.Windows.MessageBox.Show("Test");


        }

        public void RoomChanged(object sender, SelectionChangedEventArgs e)
        {
            global::System.Windows.MessageBox.Show("Test");


        }
        #endregion

        #region Private Method

        private void CreateContractDetail()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该客户已存在！", "系统提示");
                return;
            }
            result = Service.CreateContractDetail(ContractDetail);
            if (result)
            {
                MessageBox.Show("保存成功！", "系统提示");
                if (null != base.RefreshParentForm)
                {
                    RefreshParentForm.Invoke(ContractDetail);
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
            return Service.HasContractDetail(ContractDetail.ContractId, ContractDetail.BuildingId, ContractDetail.RoomId);
        }


        #endregion



    }
}
