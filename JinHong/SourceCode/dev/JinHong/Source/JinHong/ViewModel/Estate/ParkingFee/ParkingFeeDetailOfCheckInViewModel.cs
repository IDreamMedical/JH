using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;

namespace JinHong.ViewModel
{
     /// <summary>
    /// 收缴停车费明细
    /// </summary>
    public class ParkingFeeDetailOfCheckInViewModel:AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  停车位租赁信息表
        private DataTable parkingLotRentalInfoTbl;
        //  当前选中租赁信息
        private ParkingLotRentalInfo selectedParkingLotRentalInfo;
        //  租赁开始时间查询范围上限
        private DateTime whereDateFrom;
        //  租赁开始时间查询范围下限
        private DateTime whereDateTo;

        //  可用单位信息表
        private DataTable availableSocialUnitTbl;

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置停车位租赁信息表
        /// </summary>
        public DataTable ParkingLotRentalInfoTbl
        {
            get { return parkingLotRentalInfoTbl; }
            set
            {
                if (parkingLotRentalInfoTbl != value)
                {
                    parkingLotRentalInfoTbl = value;
                    OnPropertyChanged("ParkingLotRentalInfoTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前选中租赁信息
        /// </summary>
        public ParkingLotRentalInfo SelectedParkingLotRentalInfo
        {
            get { return selectedParkingLotRentalInfo; }
            set
            {
                if (selectedParkingLotRentalInfo != value)
                {
                    selectedParkingLotRentalInfo = value;
                    OnPropertyChanged("SelectedParkingLotRentalInfo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置租赁开始时间查询范围上限
        /// </summary>
        public DateTime WhereDateFrom
        {
            get { return whereDateFrom; }
            set
            {
                if (whereDateFrom != value)
                {
                    whereDateFrom = value;
                    OnPropertyChanged("WhereDateFrom");
                }
            }
        }

        /// <summary>
        /// 获得或者设置租赁开始时间查询范围下限
        /// </summary>
        public DateTime WhereDateTo
        {
            get { return whereDateTo; }
            set
            {
                if (whereDateTo != value)
                {
                    whereDateTo = value;
                    OnPropertyChanged("WhereDateTo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置可用单位
        /// </summary>
        public DataTable AvailableSocialUnitTbl
        {
            get { return availableSocialUnitTbl; }
            set
            {
                if (availableSocialUnitTbl != value)
                {
                    availableSocialUnitTbl = value;
                    OnPropertyChanged("AvailableSocialUnitTbl");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public ParkingFeeDetailOfCheckInViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereDateFrom = DateTime.Now.Date.AddDays(-7);
            WhereDateTo = DateTime.Now.Date;
            Task.Factory.StartNew(() => QueryAvailableSocialUnits());
        }

        #endregion

        #region Methods

        /// <summary>
        /// 查询可用单位名称并设置到AvailableSocialUnitTbl
        /// </summary>
        private void QueryAvailableSocialUnits()
        {
            string sql = "SELECT * FROM SocialUnitInfo";
            DataSet dsTemp = GlobalVariables.Smc.Select(sql);
            AvailableSocialUnitTbl = dsTemp != null && dsTemp.Tables.Count > 0 ? dsTemp.Tables[0] : null;
        }

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    // 查询并设置ParkingLotRentalInfoTbl
                    string sql = string.Format(
@"SELECT * FROM ParkingLotRentalInfo WHERE strftime('%Y-%m', TimeFrom) >= '{0}' AND strftime('%Y-%m', TimeFrom) <= '{1}'", WhereDateFrom.ToString("yyyy-MM"), WhereDateTo.ToString("yyyy-MM"));
                    DataSet dsTemp = GlobalVariables.Smc.Select(sql);
                    ParkingLotRentalInfoTbl = dsTemp == null ? null : dsTemp.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion
    }
}
