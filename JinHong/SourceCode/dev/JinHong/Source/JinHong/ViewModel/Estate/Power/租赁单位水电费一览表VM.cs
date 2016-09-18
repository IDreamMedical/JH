using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JinHong.Model;

namespace JinHong.ViewModel
{
    public class 租赁单位水电费一览表VM : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        //  可用单位信息表
        private DataTable availableSocialUnitTbl;
        //  单位Id, null表示所有
        private string whereSocialUnitId;
        //  日期筛选条件(只用年月)
        private DateTime whereDate;
        //  水电费信息表
        private DataTable waterAndElectricityFeesInfoTbl;

        //  当前选中水电费信息
        private MonthlyWaterAndElectricityFeesInfo selectedWaterAndElectricityFeesInfo;

        //  TODO

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置水电费信息表
        /// </summary>
        public DataTable WaterAndElectricityFeesInfoTbl
        {
            get { return waterAndElectricityFeesInfoTbl; }
            set
            {
                if (waterAndElectricityFeesInfoTbl != value)
                {
                    waterAndElectricityFeesInfoTbl = value;
                    OnPropertyChanged("WaterAndElectricityFeesInfoTbl");
                }
            }
        }

        /// <summary>
        /// 获得或者设置当前选中水电费信息
        /// </summary>
        public MonthlyWaterAndElectricityFeesInfo SelectedWaterAndElectricityFeesInfo
        {
            get { return selectedWaterAndElectricityFeesInfo; }
            set
            {
                if (selectedWaterAndElectricityFeesInfo != value)
                {
                    selectedWaterAndElectricityFeesInfo = value;
                    OnPropertyChanged("SelectedWaterAndElectricityFeesInfo");
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

        /// <summary>
        /// 获得或者设置单位Id, null表示所有
        /// </summary>
        public string WhereSocialUnitId
        {
            get { return whereSocialUnitId; }
            set
            {
                if (whereSocialUnitId != value)
                {
                    whereSocialUnitId = value;
                    OnPropertyChanged("WhereSocialUnitId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置日期筛选条件(只用年月)
        /// </summary>
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

        //  TODO

        #endregion

        #region Constructors

        public 租赁单位水电费一览表VM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            WhereDate = DateTime.Today;
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
                    // 查询并设置WaterAndElectricityFeesInfoTbl
                    string sql = string.Format(@"SELECT id, SocialUnitName, SocialUnitId ,Amount , strftime('%m', Date)  As Date
                                                    from  MonthlyWaterAndElectricityFeesInfo
                                                    where strftime('%Y-%m', Date) = '{0}';
                                                    select *from SocialUnitInfo  where  Status=0", WhereDate.ToString("yyyy-MM"));
                    DataSet dsTemp = GlobalVariables.Smc.Select(sql);
                    if (dsTemp != null && dsTemp.Tables.Count == 2)
                    {
                        WaterAndElectricityFeesInfoTbl = dsTemp.Tables[0];
                        AvailableSocialUnitTbl = dsTemp.Tables[1];
                    }
                    else
                    {
                        WaterAndElectricityFeesInfoTbl = AvailableSocialUnitTbl = null;
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
