using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using UniGuy.Commands;
using UniGuy.Corek;
using JinHong.Extensions;

namespace JinHong.ViewModel
{
    public class NewOrEditCheckInViewModel : NewOrEditViewModelBase
    {

        #region Fields

        private CheckIn _checkIn;

        private ObservableCollection<SocialUnitInfo> _socialUnits;
        private ObservableCollection<BuildingInfo> _buildings;


        private ObservableCollection<RoomInfo> _rooms;


        private static readonly Lazy<ICheckInService> lazy = new Lazy<ICheckInService>(() => new CheckInService());

        #endregion

        #region Properties

        public static ICheckInService Service { get { return lazy.Value; } }

        public CheckIn CheckIn
        {
            get { return _checkIn; }
            set
            {

                if (_checkIn != value)
                {
                    _checkIn = value;
                    OnPropertyChanged("CheckIn");
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

        #endregion

        #region Methods



        public void Initialize()
        {
            this.BtnOKCommand = new DelegateCommand(CreateOrEditCheckIn);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);

        }

        #region Private Method

        private void CreateOrEditCheckIn()
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
                    result = Service.CreateCheckIn(CheckIn);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateCheckIn(CheckIn);

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
                    return Service.HasCheckIn(string.Empty, CheckIn.Id);
                case OperateModeEnum.Edit:
                    return Service.HasCheckIn(CheckIn.Id, CheckIn.RoomId);
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

        #endregion

    }
}
