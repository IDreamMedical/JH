using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;

namespace JinHong.ViewModel
{
    public class SecurityAgreementViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  安全管理协议信息表
        private DataTable securityManagementContractInfoTbl;
        private DataTable availableSocialUnitsTbl;


        //  当前选中安全管理协议信息
        private SecurityManagementContractInfo selectedSecurityManagementContractInfo;
        //  查询开始时间
        private DateTime whereTimeFrom = DateTime.Now.Date.AddDays(-7);
        //  查询结束时间
        private DateTime whereTimeTo = DateTime.Now.Date;


        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置水电费信息表
        /// </summary>
        public DataTable SecurityManagementContractInfoTbl
        {
            get { return securityManagementContractInfoTbl; }
            set
            {
                if (securityManagementContractInfoTbl != value)
                {
                    securityManagementContractInfoTbl = value;
                    OnPropertyChanged("SecurityManagementContractInfoTbl");
                }
            }
        }

        public DataTable AvailableSocialUnitsTbl
        {
            get { return availableSocialUnitsTbl; }
            set
            {
                if (availableSocialUnitsTbl != value)
                {
                    availableSocialUnitsTbl = value;
                    OnPropertyChanged("AvailableSocialUnitsTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前选中安全管理协议信息
        /// </summary>
        public SecurityManagementContractInfo SelectedSecurityManagementContractInfo
        {
            get { return selectedSecurityManagementContractInfo; }
            set
            {
                if (selectedSecurityManagementContractInfo != value)
                {
                    selectedSecurityManagementContractInfo = value;
                    OnPropertyChanged("SelectedSecurityManagementContractInfo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置查询开始时间
        /// </summary>
        public DateTime WhereTimeFrom
        {
            get { return whereTimeFrom; }
            set
            {
                if (whereTimeFrom != value)
                {
                    whereTimeFrom = value;
                    OnPropertyChanged("WhereTimeFrom");
                }
            }
        }

        /// <summary>
        /// 获得或者设置查询结束时间
        /// </summary>
        public DateTime WhereTimeTo
        {
            get { return whereTimeTo; }
            set
            {
                if (whereTimeTo != value)
                {
                    whereTimeTo = value;
                    OnPropertyChanged("WhereTimeTo");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public SecurityAgreementViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereTimeFrom = DateTime.Today.AddDays(-30);
            WhereTimeTo = DateTime.Today.AddDays(30);
        }

        #endregion

        #region Methods

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        string sql = string.Format(@"SELECT  a .Id, a.SocialUnitName, b.Name as RoomName , 
					                                a.Area, a.EffectiveDate,a.ExpirateDate, a.LeasingType,a.LiablePerson ,
                                                    a.Notes, a.SocialUnitId
                                                    FROM SecurityManagementContractInfo  a 
                                                    INNER join   RoomInfo  b on  a .RoomId =b.Id
                                                    WHERE ('{0}' > EffectiveDate AND '{0}' < ExpirateDate) OR ('{1}' > EffectiveDate AND '{1}' < ExpirateDate);
                                                    SELECT *from  SocialUnitInfo where id  
                                                    not in  ( SELECT  IFNULL(SocialUnitId,'')
                                                    from  SecurityManagementContractInfo ); ", WhereTimeFrom, WhereTimeTo);
                            DataSet ds = GlobalVariables.Smc.Select(sql, null);
                        if (ds != null && ds.Tables.Count == 2)
                        {
                            SecurityManagementContractInfoTbl = ds.Tables[0];
                            AvailableSocialUnitsTbl = ds.Tables[1];
                        }
                        else
                        {
                            SecurityManagementContractInfoTbl = AvailableSocialUnitsTbl = null;

                        }

                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public void Query(string queryStr, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        string sql = string.Format(@"select *FROM ParkingLotRentalInfo 
                                                        plri where Status !=2  and  SocialUnitName like '%{0}%' and    plri.TimeTo>datetime('now');
                                                        SELECT * FROM ParkingLotInfo where Status!=1;
                                                        SELECT * FROM SocialUnitInfo where Status='0';", queryStr);
                        DataSet ds = GlobalVariables.Smc.Select(sql);
                        //if (ds != null && ds.Tables.Count == 3)
                        //{
                        //    ParkingLotRentalInfoTbl = ds.Tables[0];
                        //    AvailableParkingLotTbl = ds.Tables[1];
                        //    AvailableSocialUnitTbl = ds.Tables[2];
                        //}
                        //else
                        //{
                        //    ParkingLotRentalInfoTbl = AvailableParkingLotTbl = AvailableSocialUnitTbl = null;
                        //}
                        //SelectedParkingLotRentalInfo = null;
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        #endregion
    }
}
