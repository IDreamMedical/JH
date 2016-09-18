using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JinHong.Model;
using JinHong.ServiceContract;
using JinHong.Services;
using UniGuy.Commands;
using UniGuy.Corek;
using System.Windows;
using System.Collections.ObjectModel;
using System.Data;
using JinHong.Extensions;


namespace JinHong.ViewModel
{
    public class NewOrEditBroadBandFeeViewModel : NewOrEditViewModelBase
    {
        #region Private var
        private BroadBandFee _broadBandFee;

        private ObservableCollection<RoomInfo> _rooms;
        private ObservableCollection<SocialUnitInfo> _socialUnits;




        private static readonly Lazy<IBroadBandService> lazy = new Lazy<IBroadBandService>(() => new BroadBandService());

        #endregion


        #region public var

        public static IBroadBandService Service { get { return lazy.Value; } }

        public BroadBandFee BroadBandFee
        {
            get { return _broadBandFee; }
            set
            {
                if (_broadBandFee != value)
                {
                    _broadBandFee = value;
                    OnPropertyChanged("BroadBandFee");
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

        #endregion

        #region Public  Method


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditBroadBandFee);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);
            InitializeSocialUnits();
        }
        #endregion

        #region Private Method

        private void CreateOrEditBroadBandFee()
        {
            var result = false;
            if (IsExist())
            {
                MessageBox.Show("该月该公司费用已存在！", "系统提示");
                return;
            }
            switch (base.OperateMode)
            {
                case OperateModeEnum.New:
                    result = Service.CreateBroadBandFee(BroadBandFee);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateBroadBandFee(BroadBandFee);

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
                    return Service.HasBroadBandFee(string.Empty, BroadBandFee.Id);
                case OperateModeEnum.Edit:
                    return Service.HasBroadBandFee(BroadBandFee.Id, BroadBandFee.LeasingInfoId);
                default:
                    return false;
            }

        }


        /// <summary>
        /// 初始化租赁区域
        /// </summary>
        /// <param name="socialUnitId"></param>
        private void InitializeSocialUnits()
        {


            SocialUnits = new ObservableCollection<SocialUnitInfo>();
            var dt = new SocialUnitService().GetAllSocialUnits();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    SocialUnits.Add(item.BuildEntity<SocialUnitInfo>());
                }
            }
        }
        #endregion



    }
}
