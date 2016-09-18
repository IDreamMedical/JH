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
    /// 租金交费信息
    /// </summary>
    public class RentalFeesInfo : EntityBase, IIdObject
    {
        #region Fields

        //  交费活动Id
        private string id;

        //  关联的租赁信息Id
        private string feeRecordId;

        //  交费日期
        private DateTime date;

        //  实交金额
        private double amount;

        private int isPay;
        private DateTime timeFrom;
        private DateTime timeTo;

        private string socialUnitId;
        //  费用所涉单位名称(冗余)
        private string socialUnitName;




        //  备注
        private string notes;

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
        /// 获得或者设置关联的费用信息Id
        /// </summary>
        [Column]
        public string FeeRecordId
        {
            get { return feeRecordId; }
            set
            {
                if (feeRecordId != value)
                {
                    feeRecordId = value;
                    OnPropertyChanged("FeeRecordId");
                }
            }
        }


        /// <summary>
        /// 获得或者设置交费日期
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
        /// 获得或者设置实交金额
        /// </summary>
        [Column]
        public double Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
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
        public DateTime TimeFrom
        {
            get { return timeFrom; }
            set
            {
                if (timeFrom != value)
                {
                    timeFrom = value;
                    OnPropertyChanged("TimeFrom");
                }
            }
        }
        [Column]
        public DateTime TimeTo
        {
            get { return timeTo; }
            set
            {
                if (timeTo != value)
                {
                    timeTo = value;
                    OnPropertyChanged("TimeTo");
                }
            }
        }

        #endregion

        #region Constructors

        public RentalFeesInfo()
        {
        }

        public RentalFeesInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion

    }

    
}
