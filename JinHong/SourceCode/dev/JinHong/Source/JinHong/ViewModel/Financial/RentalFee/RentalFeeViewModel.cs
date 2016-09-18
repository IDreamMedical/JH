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
    /// 租金明细表 包括已交和未交房租明细
    /// </summary>
    public class RentalFeeViewModel : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  查询到并显示的月租费信息;
        private DataTable rentalFeTbl;
        private RentalFeesInfo selectedRentalFeesInfo;

        private DateTime whereDate;





        //  当前浏览或编辑的月租费信息;
        private EstateRentPriceInfo selectedEstateRentPriceInfo;

        private string whereName;


        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置查询到并显示的月租费信息
        /// </summary>
        public DataTable RentalFeTbl
        {
            get { return rentalFeTbl; }
            set
            {
                if (rentalFeTbl != value)
                {
                    rentalFeTbl = value;
                    OnPropertyChanged("RentalFeTbl");
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

        public RentalFeesInfo SelectedRentalFeesInfo
        {
            get { return selectedRentalFeesInfo; }
            set
            {
                if (selectedRentalFeesInfo != value)
                {
                    selectedRentalFeesInfo = value;
                    OnPropertyChanged("SelectedRentalFeesInfo");
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

        public RentalFeeViewModel(MainWindowViewModel parentVM)
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
                        string sql = @"select a.*,  e .name as BuildingId, d.name as RoomId,b.EffectiveDate,b.ExpirateDate  from  RentalFeesInfo a 
 INNER join  ContractInfo b on a.SocialUnitId=b.SocialUnitId
 INNER join  ContractDetail c  on c.ContractId=b.Id
 INNER join  RoomInfo  d on  c.RoomId=d.Id
 INNER join  BuildingInfo  e on  c.BuildingId=e.Id";
                        DataSet ds = GlobalVariables.Smc.Select(sql, null);
                        this.RentalFeTbl = ds == null ? null : ds.Tables[0];
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
                    string sql = string.Format(@"select a.*,  e .name as BuildingId, d.name as RoomId,b.EffectiveDate,b.ExpirateDate  from  RentalFeesInfo a 
                                                 INNER join  ContractInfo b on a.SocialUnitId=b.SocialUnitId
                                                 INNER join  ContractDetail c  on c.ContractId=b.Id
                                                 INNER join  RoomInfo  d on  c.RoomId=d.Id
                                                 INNER join  BuildingInfo  e on  c.BuildingId=e.Id
                                                where  b.SocialUnitName Like '%{0}%'", queryStr);
                    DataSet ds = GlobalVariables.Smc.Select(sql, null);
                    this.RentalFeTbl = ds == null ? null : ds.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }
        public object GetUnPayRentalFee()
        {

            DataTable dt = null;
            try
            {
                string sql = string.Format(@"select  Id,SocialUnitId ,SocialUnitName, TimeFrom ,TimeTo ,Notes ,Amount from  RentalFeesInfo  where IsPay =0 and  strftime('%Y-%m-%d', TimeFrom,'localtime')<='{0}'", WhereDate.ToString("yyyy-MM-dd"));
                DataSet ds = GlobalVariables.Smc.Select(sql, null);
                dt = ds == null ? null : ds.Tables[0];
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }

        #endregion
    }
}
