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
    public class NewOrEditInstallAirViewModel : NewOrEditViewModelBase
    {
        #region Fields

        private InstallAirRecord _installAir;

        private ObservableCollection<SocialUnitInfo> _socialUnits;
        private ObservableCollection<RoomInfo> _rooms;
        private ObservableCollection<AirConditioner> _airConditioners;


        private static readonly Lazy<IInstallAirService> lazy = new Lazy<IInstallAirService>(() => new InstallAirService());

        #endregion

        #region Properties

        public static IInstallAirService Service { get { return lazy.Value; } }

        public InstallAirRecord InstallAir
        {
            get { return _installAir; }
            set
            {

                if (_installAir != value)
                {
                    _installAir = value;
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
        public ObservableCollection<AirConditioner> AirConditioners
        {
            get { return _airConditioners; }
            set
            {
                if (_airConditioners != value)
                {
                    _airConditioners = value;
                    OnPropertyChanged("AirConditioners");
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
            this.BtnOKCommand = new DelegateCommand(CreateOrEditInstallAir);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);

        }

        #region Private Method

        private void CreateOrEditInstallAir()
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
                    result = Service.CreateInstallAirRecord(InstallAir);
                    break;
                case OperateModeEnum.Edit:
                    result = Service.UpdateInstallAirRecord(InstallAir);

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
                    return Service.HasInstallAirRecord(string.Empty, InstallAir.Id);
                case OperateModeEnum.Edit:
                    return Service.HasInstallAirRecord(InstallAir.Id, InstallAir.RoomId);
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
