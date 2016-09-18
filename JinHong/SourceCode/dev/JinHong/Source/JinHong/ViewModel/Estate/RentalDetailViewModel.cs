using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;

namespace JinHong.ViewModel
{
    public class RentanlDetailViewModel : BaseViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  租赁情况表
        private DataTable leasingStatusInfoTbl;
        //  当前选中租赁信息
        private LeasingStatusInfo selectedLeasingStatusInfo;
        //  要查询的座号, null表示所有
        private string whereBuildingId;
        //  所有可用座号
        private object availableBuildingIds;

        private SocialUnitInfo currentSocialUnitInfo;


        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置租赁情况表
        /// </summary>
        public DataTable LeasingStatusInfoTbl
        {
            get { return leasingStatusInfoTbl; }
            set
            {
                if (leasingStatusInfoTbl != value)
                {
                    leasingStatusInfoTbl = value;
                    OnPropertyChanged("LeasingStatusInfoTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前选中租赁信息
        /// </summary>
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

        /// <summary>
        /// 获得或者设置要查询的座号, null表示所有
        /// </summary>
        public string WhereBuildingId
        {
            get { return whereBuildingId; }
            set
            {
                if (whereBuildingId != value)
                {
                    whereBuildingId = value;
                    OnPropertyChanged("WhereBuildingId");
                }
            }
        }
        public SocialUnitInfo CurrentSocialUnitInfo
        {
            get { return currentSocialUnitInfo; }
            set
            {
                if (currentSocialUnitInfo != value)
                {
                    currentSocialUnitInfo = value;
                    OnPropertyChanged("CurrentSocialUnitInfo");
                }
            }
        }




        /// <summary>
        /// 获得或者设置所有可用座号
        /// </summary>
        public object AvailableBuildingIds
        {
            get { return availableBuildingIds; }
            set
            {
                if (availableBuildingIds != value)
                {
                    availableBuildingIds = value;
                    OnPropertyChanged("AvailableBuildingIds");
                }
            }
        }

        //  TODO

        #endregion

        #region Constructors

        public RentanlDetailViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            QueryAvailableBuildingIds();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 查询可用单位名称并设置到AvailableSocialUnitNames, 第一项加上null
        /// </summary>
        private void QueryAvailableBuildingIds()
        {
            DataSet ds = GlobalVariables.Smc.Select("select *from  BuildingInfo");
            if (ds != null)
            {

                AvailableBuildingIds = ds.Tables[0] == null ? null : ds.Tables[0];
                //string[] buildingIds = new string[ds.Tables[0].Rows.Count];
                //for (int i = 0; i < buildingIds.Length; i++)
                //    buildingIds[i] = ds.Tables[0].Rows[i][0] as string;
                //AvailableBuildingIds = buildingIds;
            }
        }

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {

                        // 查询并设置LeasingStatusInfoTbl
                        DataSet ds = GlobalVariables.Smc.Select(string.Format(@"select a.RoomId,a.BuildingId,b.SocialUnitName,c.TelNo from  ContractDetail a
inner join  ContractInfo b on a.ContractId=b.Id
inner join  SocialUnitInfo c on c.Id=b.SocialUnitId
where  SUBSTR(b.ExpirateDate,1,7)>='{0}'and c.Status='0'
group  by BuildingId,b.SocialUnitName,RoomId, TelNo", DateTime.Now.ToString("yyyy-MM")), null);
                        LeasingStatusInfoTbl = ds == null ? null : ds.Tables[0];


                    });
                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        public void Query(string queryStr, Action actCompleted)
        {
            //if (null == LeasingStatusInfoTbl) return;
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    Task.Factory.StartNew(() =>
                    {
                        DataSet ds = GlobalVariables.Smc.Select(string.Format(@"select a.RoomId,a.BuildingId,b.SocialUnitName,c.TelNo from  ContractDetail a
inner join  ContractInfo b on a.ContractId=b.Id
inner join  SocialUnitInfo c on c.Id=b.SocialUnitId
where  SUBSTR(b.ExpirateDate,1,7)>='{0}'and c.Status='0' and BuildingId='{1}'
group  by BuildingId,b.SocialUnitName,RoomId, TelNo", DateTime.Now.ToString("yyyy-MM"), queryStr), null);
                        LeasingStatusInfoTbl = ds == null ? null : ds.Tables[0];
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
