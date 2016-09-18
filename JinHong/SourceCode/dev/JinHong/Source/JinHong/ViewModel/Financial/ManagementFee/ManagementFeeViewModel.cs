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
    /// 物业费明细表 包括已交和未交物业费明细
    /// </summary>
    public class ManagementFeeViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  查询到并显示的月租费信息;
        private DataTable propertyManagementFeeTbl;
        private PropertyManagementFeesInfo selectedPropertyManagementFeesInfo;

        private DataTable unPayPMFTbl;


        //  当前浏览或编辑的月租费信息;
        private EstateRentPriceInfo selectedEstateRentPriceInfo;

        private string whereName;
        private DateTime whereDate;


        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置查询到并显示的月租费信息
        /// </summary>
        public DataTable PropertyManagementFeeTbl
        {
            get { return propertyManagementFeeTbl; }
            set
            {
                if (propertyManagementFeeTbl != value)
                {
                    propertyManagementFeeTbl = value;
                    OnPropertyChanged("PropertyManagementFeeTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前浏览或编辑的月租费信息
        /// </summary>
        public EstateRentPriceInfo SelectedEstateRentPriceInfo
        {
            get { return selectedEstateRentPriceInfo; }
            set
            {
                if (selectedEstateRentPriceInfo != value)
                {
                    selectedEstateRentPriceInfo = value;
                    OnPropertyChanged("SelectedEstateRentPriceInfo");
                }
            }
        }

        public PropertyManagementFeesInfo SelectedPropertyManagementFeesInfo
        {
            get { return selectedPropertyManagementFeesInfo; }
            set
            {
                if (selectedPropertyManagementFeesInfo != value)
                {
                    selectedPropertyManagementFeesInfo = value;
                    OnPropertyChanged("SelectedPropertyManagementFeesInfo");
                }
            }
        }
        public DataTable UnPayPMFTbl
        {
            get { return unPayPMFTbl; }
            set
            {
                if (unPayPMFTbl != value)
                {
                    unPayPMFTbl = value;
                    OnPropertyChanged("UnPayPMFTbl");
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


        public DateTime WhereDate
        {
            get { return whereDate; }
            set
            {
                if (whereDate != value)
                {
                    whereDate = value;
                    OnPropertyChanged("WhereDate");
                }
            }
        }
        #endregion

        #region Constructors

        public ManagementFeeViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereDate = DateTime.Today;
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
                        // 查询并设置EstateRentPriceInfoTbl
                        string sql = @"select a.*,  e .name as BuildingId, d.name as RoomId,b.EffectiveDate,b.ExpirateDate  from  PropertyManagementFeesInfo a 
 INNER join  ContractInfo b on a.SocialUnitId=b.SocialUnitId
 INNER join  ContractDetail c  on c.ContractId=b.Id
 INNER join  RoomInfo  d on  c.RoomId=d.Id
 INNER join  BuildingInfo  e on  c.BuildingId=e.Id";
                        DataSet ds = GlobalVariables.Smc.Select(sql, null);
                        this.PropertyManagementFeeTbl = ds == null ? null : ds.Tables[0];
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
                    string sql = string.Format(@"select a.*,  e .name as BuildingId, d.name as RoomId,b.EffectiveDate,b.ExpirateDate  from  PropertyManagementFeesInfo a 
                                                 INNER join  ContractInfo b on a.SocialUnitId=b.SocialUnitId
                                                 INNER join  ContractDetail c  on c.ContractId=b.Id
                                                 INNER join  RoomInfo  d on  c.RoomId=d.Id
                                                 INNER join  BuildingInfo  e on  c.BuildingId=e.Id
                                                where  b.SocialUnitName Like '%{0}%'", queryStr);
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    this.PropertyManagementFeeTbl = ds == null ? null : ds.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        /// <summary>
        /// 获取为缴物业费的公司
        /// </summary>
        /// <returns></returns>
        public object GetUnPayPMF()
        {

            UnPayPMFTbl = null;
            try
            {
                string sql = string.Format(@"select  Id,SocialUnitId ,SocialUnitName, TimeFrom ,TimeTo ,Notes ,Amount from  PropertyManagementFeesInfo  where IsPay =0 and  strftime('%Y-%m-%d', TimeFrom,'localtime')<='{0}'", WhereDate.ToString("yyyy-MM-dd"));
                DataSet ds = GlobalVariables.Smc.Select(sql, null);
                UnPayPMFTbl = ds == null ? null : ds.Tables[0];
            }
            catch (Exception)
            {

                throw;
            }

            return UnPayPMFTbl;
        }

        #endregion
    }
}
