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
    /// 收入支出项
    /// </summary>
    public class IncomeAndExpenditure : EntityBase, IIdObject
    {
        #region Fields

        //  Id, 一般自动生成
        private string id;
        //  相关获得Id, 比如入租时租金相关的入租登记活动的Id
        private string relatedActivityId;
        private DateTime date;
        //  收入支出项类别
        private string category;
        // 收入支出数量(元), 支出为-
        private double amount;

        //  费用所涉单位Id
        private string socialUnitId;
        //  费用所涉单位名称(冗余)
        private string socialUnitName;

        private int isPay;

        private string notes;
        private DateTime? startTime;

        private DateTime? endTime;







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
        public string RelatedActivityId
        {
            get { return relatedActivityId; }
            set
            {
                if (relatedActivityId != value)
                {
                    relatedActivityId = value;
                    OnPropertyChanged("RelatedActivityId");
                }
            }
        }

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

        [Column]
        public string Category
        {
            get { return category; }
            set
            {
                if (category != value)
                {
                    category = value;
                    OnPropertyChanged("Category");
                }
            }
        }

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

        [Column]
        public string SocialUnitId
        {
            get { return socialUnitId; }
            set
            {
                if (this.socialUnitId != value)
                {
                    this.socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }

        [Column]
        public string SocialUnitName
        {
            get { return socialUnitName; }
            set
            {
                if (this.socialUnitName != value)
                {
                    this.socialUnitName = value;
                    OnPropertyChanged("SocialUnitName");
                }
            }
        }


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
        public DateTime? StartTime
        {
            get { return startTime; }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    OnPropertyChanged("StartTime");
                }
            }
        }
        [Column]
        public DateTime? EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                    OnPropertyChanged("EndTime");
                }
            }
        }



        #endregion

        #region Constructors

        public IncomeAndExpenditure()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public IncomeAndExpenditure(string category, double amount, DateTime date)
        {
            this.Category = category;
            this.Amount = amount;
            this.Date = date;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
