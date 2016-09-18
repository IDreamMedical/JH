using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;
using UniGuy.Core.Extensions;
using System.Collections.ObjectModel;
using UniGuy.Commands;
using JinHong.ServiceContract;
using JinHong.Services;
using JinHong.Helper;

namespace JinHong.ViewModel
{

    /// <summary>
    /// 停车费明细 包括如下：
    /// a.
    ///     1、一段时间内已收停车费
    ///     2、一段时间内未收停车费
    /// b. 一段时间某个人的停车费明细；
    /// c  一段时间内一家单位的停车费明细；
    /// </summary>
    public class ParkingFeeDetailViewModel : BaseViewModel
    {
        #region Fields

        private static readonly Lazy<IParkingLotRentalService> lazy = new Lazy<IParkingLotRentalService>(() => new ParkingLotRentalService());

        private static readonly Lazy<ISocialUnitService> socialLazy = new Lazy<ISocialUnitService>(() => new SocialUnitService());

        /// <summary>
        /// 所属公司
        /// </summary>
        private ObservableCollection<SocialUnitInfo> _socialUnits;

        /// <summary>
        /// 车主
        /// </summary>
        private string _carOwnerName;

        /// <summary>
        /// 费用状态
        /// </summary>
        private string _parkingFeeStatus;

        /// <summary>
        /// 费用状态
        /// </summary>
        private DateTime _startDate;

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime _endDate;

        /// <summary>
        /// 选中的费用明细
        /// </summary>
        private DataRow _selectedParkingFeeDetailRow;

        #endregion

        #region Properties

        public static IParkingLotRentalService Service { get { return lazy.Value; } }

        public static ISocialUnitService SocialService { get { return socialLazy.Value; } }

        public ObservableCollection<SocialUnitInfo> SocialUnits
        {
            get { return _socialUnits; }
            set
            {
                _socialUnits = value;
                if (_socialUnits != value)
                {
                    _socialUnits = value;
                    OnPropertyChanged("SocialUnits");

                }
            }
        }

        public string CarOwnerName
        {
            get { return _carOwnerName; }
            set
            {
                _carOwnerName = value;
                if (_carOwnerName != value)
                {
                    _carOwnerName = value;
                    OnPropertyChanged("CarOwnerName");

                }
            }
        }

        public string ParkingFeeStatus
        {
            get { return _parkingFeeStatus; }
            set
            {
                if (_parkingFeeStatus != value)
                {
                    _parkingFeeStatus = value;
                    OnPropertyChanged("ParkingFeeStatus");

                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {

                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged("StartDate");

                }
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged("EndDate");

                }
            }
        }
        #endregion

        #region Constructors

        public ParkingFeeDetailViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion



        #region Methods


        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            StartDate = DateTime.Now.AddDays(-7);
            EndDate = DateTime.Now;
            InitializeSocialUnits();
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
        }

        /// <summary>
        /// 初始化社会单元
        /// </summary>
        private void InitializeSocialUnits()
        {
            SocialUnits = SocialUnitsHelper.LoadSocialUnits();
        }

        /// <summary>
        /// 查询刷新界面
        /// </summary>
        private void OnRefreshCommand()
        {

        }


        #endregion
    }
}
