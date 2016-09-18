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
    /// 月度水电费信息
    /// </summary>
    public class MonthlyWaterAndElectricityFeesInfo : EntityBase, IIdObject
    {
        #region Fields

        //  Id, Guid自动生成
        private string id;

        //  租赁信息Id
        private string leasingInfoId;
        //  年月部分有效
        private DateTime date;

        private string roomId;



        /// <summary>
        /// 公司或个人Id
        /// </summary>
        private string socialUnitId;

        //  公司或个人名称(实际应该为Id)
        private string socialUnitName;
        //  金额
        private double waterAmount;
        private double electricityAmount;

        //  票别(发票类别)
        private string receiptCategory;

        //private double amount;
        //  备注
        private string notes;

        private int isPay;

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

        /// <summary>
        /// 获得或者设置租赁信息Id
        /// </summary>
        [Column]
        public string LeasingInfoId
        {
            get { return leasingInfoId; }
            set
            {
                if (leasingInfoId != value)
                {
                    leasingInfoId = value;
                    OnPropertyChanged("LeasingInfoId");
                }
            }
        }

        [Column]
        public string RoomId
        {
            get { return roomId; }
            set
            {
                if (leasingInfoId != value)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置年份月份
        /// </summary>
        [Column]
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        /// <summary>
        /// 获得或者设置公司或个人名称
        /// </summary>
        [Column]
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
        /// 获得或者设置金额
        /// </summary>
        [Column]
        public double WaterAmount
        {
            get { return waterAmount; }
            set
            {
                if (this.waterAmount != value)
                {
                    this.waterAmount = value;
                    OnPropertyChanged("WaterAmount");
                    OnPropertyChanged("Amount");
                }
            }
        }
        [Column]
        public double ElectricityAmount
        {
            get { return electricityAmount; }
            set
            {
                if (this.electricityAmount != value)
                {
                    this.electricityAmount = value;
                    OnPropertyChanged("ElectricityAmount");
                    OnPropertyChanged("Amount");
                }
            }
        }
        [Column(ColumnIoType = ColumnIoType.Write)]
        public double Amount
        {
            get { return WaterAmount + ElectricityAmount; }
        }

        /// <summary>
        /// 获得或者设置票别
        /// </summary>
        [Column]
        public string ReceiptCategory
        {
            get { return receiptCategory; }
            set
            {
                if (this.receiptCategory != value)
                {
                    this.receiptCategory = value;
                    OnPropertyChanged("ReceiptCategory");
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
                if (this.notes != value)
                {
                    this.notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        /// <summary>
        /// 获得或者设置电话
        /// </summary>
        [Column]
        public int IsPay
        {
            get { return isPay; }
            set
            {
                if (isPay != value)
                {
                    isPay = value;
                    OnPropertyChanged("IsPay");
                }
            }
        }



        [Column]
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

        ///// <summary>
        ///// 获得或者设置金额
        ///// </summary>
        //[Column]
        //public double Amount
        //{
        //    get { return amount; }
        //    set
        //    {
        //        if (amount != value)
        //        {
        //            amount = value;
        //            OnPropertyChanged("Amount");
        //        }
        //    }
        //}

        #endregion

        #region Constructors

        public MonthlyWaterAndElectricityFeesInfo()
        {
        }

        public MonthlyWaterAndElectricityFeesInfo(string id)
        {
            this.Id = id;
        }


        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO... Id, etc
    }
}
