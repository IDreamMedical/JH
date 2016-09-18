using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using JinHong.Model;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 退租流程表
    /// </summary>
    public class ReverseRentalVM:AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  查询到并显示的租赁信息表格;
        private DataTable leasingInfoTbl;
        //  当前浏览或编辑的租赁信息
        private CheckIn selectedLeasingInfo;
        //  当前浏览或编辑完整信息的租赁户企业基本信息
        private LeasingStatusInfo selectedLeasingStatusInfo;

        //  关联的租赁户信息
        private SocialUnitInfo relatedSocialUnitInfo;
        //  关联的座室信息
        private RoomInfo relatedRoomInfo;

        //  查询的租期开始时间区间上限
        private DateTime whereTimeFrom;
        //  查询的租期开始时间区间下限
        private DateTime  whereTimeTo;

        //  可用单位信息表
        private DataTable availableSocialUnitTbl;

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置查询到并显示的租赁信息表格
        /// </summary>
        public DataTable LeasingInfoTbl
        {
            get { return leasingInfoTbl; }
            set
            {
                if (leasingInfoTbl != value)
                {
                    leasingInfoTbl = value;
                    OnPropertyChanged("LeasingInfoTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前浏览或编辑的租赁信息
        /// </summary>
        public CheckIn SelectedLeasingInfo
        {
            get { return selectedLeasingInfo; }
            set
            {
                if (selectedLeasingInfo != value)
                {
                    selectedLeasingInfo = value;
                    OnPropertyChanged("SelectedLeasingInfo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置关联的租赁户信息
        /// </summary>
        public SocialUnitInfo RelatedSocialUnitInfo
        {
            get { return relatedSocialUnitInfo ?? (relatedSocialUnitInfo=(SelectedLeasingInfo == null ? null : GlobalVariables.Smc.Load<SocialUnitInfo>(SelectedLeasingInfo.SocialUnitId))); }
            set
            {
                if (relatedSocialUnitInfo != value)
                {
                    relatedSocialUnitInfo = value;
                    OnPropertyChanged("RelatedSocialUnitInfo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置关联的座室信息
        /// </summary>
        public RoomInfo RelatedRoomInfo
        {
            get { return relatedRoomInfo ?? (relatedRoomInfo = (SelectedLeasingInfo == null ? null : GlobalVariables.Smc.Load<RoomInfo>(SelectedLeasingInfo.RoomId))); }
            set
            {
                if (relatedRoomInfo != value)
                {
                    relatedRoomInfo = value;
                    OnPropertyChanged("RelatedRoomInfo");
                }
            }
        }


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


        /// <summary>
        /// 获得或者设置查询的租期开始时间区间上限
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
        /// 获得或者设置查询的租期开始时间区间下限
        /// </summary>
        public DateTime  WhereTimeTo
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


        public LeasingStatusInfo SelectedLeasingStatusInfo
        {
            get { return selectedLeasingStatusInfo; }
            set
            {
                if (selectedLeasingStatusInfo != value)
                {
                    selectedLeasingStatusInfo = value;
                    OnPropertyChanged("SelectedLeasingStatusInfo");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public ReverseRentalVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereTimeTo = DateTime.Now.Date;
            WhereTimeFrom = DateTime.Now.Date.AddDays(-30);
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
                        // 查询并设置LeasingInfoTbl
                        string sql = string.Format(@"select a.id	, b.Name as SocialUnitName , f.EffectiveDate, f.ExpirateDate, f.ContractNo ,c.TerminateDate ,e.Area from   LeasingInfo a 
INNER JOIN  SocialUnitInfo b on a.SocialUnitId=b.Id
INNER join  UnLeaseDetail  c on a.SocialUnitId= c.SocialUnitId
INNER JOIN RoomInfo e   on  a.RoomId =e.Id 
INNER join  ContractInfo  f on a.SocialUnitId= f.SocialUnitId
where    a.Status=1 and c.UnLeaseDate<=f.ExpirateDate  and c.UnLeaseDate>='{0}'  and c.UnLeaseDate<='{1}'", WhereTimeFrom.ToString("yyyy-MM-dd"), WhereTimeTo.ToString("yyyy-MM-dd"));
                        DataSet ds = GlobalVariables.Smc.Select(sql, null);
                        this.LeasingInfoTbl = ds == null ? null : ds.Tables[0];
                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion
    }
}
