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
        //  年份
        private int year;
        //  月份
        private int month;
        //  公司或个人名称(实际应该为Id)
        private string socialUnitName;
        //  金额
        private double amount;

        //  票别(发票类别)
        private string receiptCategory;

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
        /// 获得或者设置年份
        /// </summary>
        [Column]
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        /// <summary>
        /// 获得或者设置月份
        /// </summary>
        [Column]
        public int Month
        {
            get { return month; }
            set
            {
                if (month != value)
                {
                    month = value;
                    OnPropertyChanged("Month");
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
        public double Amount
        {
            get { return amount; }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    OnPropertyChanged("Amount");
                }
            }
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
