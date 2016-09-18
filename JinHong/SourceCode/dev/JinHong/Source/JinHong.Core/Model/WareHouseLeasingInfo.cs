using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Entity;
using UniGuy.Core;
using UniGuy.Core.Data;

namespace JinHong.Model
{
    /// <summary>
    /// 仓库租赁信息
    /// </summary>
    public class WareHouseLeasingInfo : EntityBase, IIdObject
    {
        #region Fields

        //  Id, 一般由Guid生成
        private string id;

        //  仓库编号
        private string wareHouseId;
        //  仓库名称
        private string wareHouseName;


        //  单位编号
        private string socialUnitId;
        //  单位名称
        private string socialUnitName;

        //  入租时间
        private DateTime? leaseBeginDate;
        //  退租时间
        private DateTime? leaseEndDate;

        //  合同编号(租赁生效日期和到期时期在合同信息里面)
        private string contractId;

        private int area;


        //  备注
        private string notes;

        /// <summary>
        /// 每日租金
        /// </summary>
        private double dayRentalFee;
        /// <summary>
        /// 每月租金
        /// </summary>
        private double monthRentalFee;

        /// <summary>
        /// 每日物业费
        /// </summary>
        private double dayPropManageFee;


        /// <summary>
        /// 每月物业费
        /// </summary>
        private double monthPropManageFee;

        //  TODO...

        #endregion

        #region Properties

        [PrimaryKey]
        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [Column]
        public string WareHouseId
        {
            get { return wareHouseId; }
            set
            {
                if (wareHouseId != value)
                {
                    wareHouseId = value;
                    OnPropertyChanged("WareHouseId");
                }
            }
        }

        //[Column]
        public string SocialUnitId
        {
            get { return socialUnitId; }
            set
            {
                if (socialUnitId != value)
                {
                    socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }

       // [Column]
        public string SocialUnitName
        {
            get { return socialUnitName; }
            set
            {
                if (socialUnitName != value)
                {
                    socialUnitName = value;
                    OnPropertyChanged("SocialUnitName");
                }
            }
        }

        /// <summary>
        /// 获得或者设置入租时间
        /// </summary>
       // [Column]
        public DateTime? LeaseBeginDate
        {
            get { return leaseBeginDate; }
            set
            {
                if (leaseBeginDate != value)
                {
                    leaseBeginDate = value;
                    OnPropertyChanged("LeaseBeginDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置退租时间
        /// </summary>
       // [Column]
        public DateTime? LeaseEndDate
        {
            get { return leaseEndDate; }
            set
            {
                if (leaseEndDate != value)
                {
                    leaseEndDate = value;
                    OnPropertyChanged("LeaseEndDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置合同编号
        /// </summary>
        [Column]
        public string ContractId
        {
            get { return contractId; }
            set
            {
                if (contractId != value)
                {
                    contractId = value;
                    OnPropertyChanged("ContractId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置备注
        /// </summary>
        [Column]
        public string Notes
        {
            get { return notes; }
            set
            {
                if (notes != value)
                {
                    notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        [Column]
        public double DayPropManageFee
        {
            get { return dayPropManageFee; }
            set
            {

                if (dayPropManageFee != value)
                {
                    dayPropManageFee = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("DayPropManageFee");
                }

            }
        }
        [Column]
        public double MonthPropManageFee
        {
            get { return monthPropManageFee; }
            set
            {
                if (monthPropManageFee != value)
                {
                    monthPropManageFee = value;
                    OnPropertyChanged("MonthPropManageFee");
                }
            }
        }
        [Column]
        public double MonthRentalFee
        {
            get { return monthRentalFee; }
            set
            {
                if (monthRentalFee != value)
                {
                    monthRentalFee = value;
                    OnPropertyChanged("MonthRentalFee");
                }
            }
        }
        [Column]
        public double DayRentalFee
        {
            get { return dayRentalFee; }
            set
            {
                if (dayRentalFee != value)
                {
                    dayRentalFee = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("DayRentalFee");
                }
            }
        }


        public int Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("Area");

                }
            }
        }
        public string WareHouseName
        {
            get { return wareHouseName; }
            set
            {
                if (wareHouseName != value)
                {
                    wareHouseName = value;
                    OnPropertyChanged("WareHouseName");
                }
            }
        }




        #endregion

        #region Constructors

        public WareHouseLeasingInfo()
        {
        }

        public WareHouseLeasingInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods


        private void SetMonthPropManageFee()
        {
            MonthPropManageFee = DayPropManageFee * Area;
        }

        private void SetMonthRentalFee()
        {
            MonthRentalFee = DayRentalFee * Area;
        }
        //  TODO

        #endregion
        //  TODO... Id, etc
    }
}
