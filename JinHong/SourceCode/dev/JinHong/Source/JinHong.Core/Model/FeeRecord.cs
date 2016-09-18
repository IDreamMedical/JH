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
    /// 费用交费主记录信息
    /// </summary>
    /// 
    [Table("FeeRecord")]
    public class FeeRecord : EntityBase, IIdObject
    {
        #region Fields

        //  交费活动Id
        private string id;

        //  交费日期
        private DateTime? payDate;

        private DateTime? startDate;
        private DateTime? endDate;

        private string socialUnitId;
        //  费用所涉单位名称(冗余)
        private string socialUnitName;
        //  备注
        private string notes;

        /// <summary>
        /// 总金额
        /// </summary>
        private double totalAmount;

        /// <summary>
        /// 总面积
        /// </summary>
        private double totalArea;

        /// <summary>
        /// 租赁部位
        /// </summary>
        private string leasePart;


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
        /// 获得或者设置交费日期
        /// </summary>
        [Column]
        public DateTime? PayDate
        {
            get { return payDate; }
            set
            {
                if (payDate != value)
                {
                    payDate = value;
                    OnPropertyChanged("PayDate");
                }
            }
        }



        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        [Column]
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
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
        public double TotalAmount
        {
            get { return totalAmount; }
            set
            {
                if (totalAmount != value)
                {
                    totalAmount = value;
                    OnPropertyChanged("TotalAmount");
                }
            }
        }
        [Column]
        public double TotalArea
        {
            get { return totalArea; }
            set
            {
                if (totalArea != value)
                {
                    totalArea = value;
                    OnPropertyChanged("TotalArea");
                }
            }
        }
        [Column]
        public string LeasePart
        {
            get { return leasePart; }
            set
            {
                if (leasePart != value)
                {
                    leasePart = value;
                    OnPropertyChanged("LeasePart");
                }
            }
        }


        #endregion

        #region Constructors

        public FeeRecord()
        {
        }

        public FeeRecord(string id)
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
