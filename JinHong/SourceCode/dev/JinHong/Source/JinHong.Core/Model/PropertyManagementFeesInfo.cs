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
    /// 物业管理费交费信息
    /// </summary>
    public class PropertyManagementFeesInfo : EntityBase, IIdObject
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

        //  是否划转
        private bool isTransferred;

        private int isPay;

        private string socialUnitId;

        private string socialUnitName;


        private DateTime timeFrom;

        private DateTime timeTo;




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

        /// <summary>
        /// 获得或者设置是否划转
        /// </summary>
        [Column]
        public bool IsTransferred
        {
            get { return isTransferred; }
            set
            {
                if (isTransferred != value)
                {
                    isTransferred = value;
                    OnPropertyChanged("IsTransferred");
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

        public PropertyManagementFeesInfo()
        {
        }

        public PropertyManagementFeesInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion

        //  TODO
    }
}
