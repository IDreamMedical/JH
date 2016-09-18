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
    /// 仓库租赁
    /// 
    /// </summary>
    public class RentalWareHouseVM : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  查询并显示的当前仓库的租赁状态表
        private DataTable wareHouseLeasingInfoTbl;
        private DataTable availableSocialUnitTbl;
        private DataTable availableWareHouseTbl;


        //  当前选中的仓库租赁状态
        private WareHouseLeasingInfo selectedWareHouseLeasingInfo;
        private string whereName;


        //  TODO

        #endregion

        #region Properties

        public DataTable WareHouseLeasingInfoTbl
        {
            get { return wareHouseLeasingInfoTbl; }
            set
            {
                if (wareHouseLeasingInfoTbl != value)
                {
                    wareHouseLeasingInfoTbl = value;
                    OnPropertyChanged("WareHouseLeasingInfoTbl");
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

        public DataTable AvailableWareHouseTbl
        {
            get { return availableWareHouseTbl; }
            set
            {
                if (availableSocialUnitTbl != value)
                {
                    availableWareHouseTbl = value;
                    OnPropertyChanged("AvailableWareHouseTbl");
                }
            }
        }

        public string WhereName
        {
            get { return whereName; }
            set
            {
                if (whereName != value)
                {
                    whereName = value;
                    OnPropertyChanged("WhereName");
                }
            }
        }


        public WareHouseLeasingInfo SelectedWareHouseLeasingInfo
        {
            get { return selectedWareHouseLeasingInfo; }
            set
            {
                if (selectedWareHouseLeasingInfo != value)
                {
                    selectedWareHouseLeasingInfo = value;
                    OnPropertyChanged("SelectedWareHouseLeasingInfo");
                }
            }
        }

        #endregion

        #region Constructors

        public RentalWareHouseVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    string sql = string.Format(@"select a.Id,b.SocialUnitName ,b.SocialUnitId,a.WareHouseId, 
                                                c.Name as WareHouseName ,b.CustomerTel 
                                                from  WareHouseLeasingInfo  a 
                                                inner join  ContractInfo b on a.ContractId=b.Id
                                                INNER join WareHouseInfo  c  on  a.WareHouseId= c.Id;
                                                SELECT *from  SocialUnitInfo where Status=0;
                                                SELECT *from  WareHouseInfo;");
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    if (ds != null && ds.Tables.Count == 3)
                    {
                        WareHouseLeasingInfoTbl = ds.Tables[0];
                        AvailableSocialUnitTbl = ds.Tables[1];
                        AvailableWareHouseTbl = ds.Tables[2];

                    }
                    else
                    {
                        WareHouseLeasingInfoTbl = AvailableSocialUnitTbl = AvailableWareHouseTbl = null;
                    }

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
                    string sql = string.Format(@"select a.Id,b.SocialUnitName ,b.SocialUnitId,a.WareHouseId, 
                                                c.Name as WareHouseName ,b.CustomerTel 
                                                from  WareHouseLeasingInfo  a 
                                                inner join  ContractInfo b on a.ContractId=b.Id
                                                INNER join WareHouseInfo  c  on  a.WareHouseId= c.Id where SocialUnitName like '%{0}%'; 
                                                SELECT *from  SocialUnitInfo where Status=0 ;
                                                SELECT *from  WareHouseInfo;", queryStr);
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    if (ds != null && ds.Tables.Count == 3)
                    {
                        WareHouseLeasingInfoTbl = ds.Tables[0];
                        AvailableSocialUnitTbl = ds.Tables[1];
                        AvailableWareHouseTbl = ds.Tables[2];

                    }
                    else
                    {
                        WareHouseLeasingInfoTbl = AvailableSocialUnitTbl = AvailableWareHouseTbl = null;
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion
    }
}
