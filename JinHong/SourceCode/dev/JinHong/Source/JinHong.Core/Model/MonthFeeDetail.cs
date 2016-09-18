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
    [Table("MonthFeeDetail")]
    public class MonthFeeDetail : EntityBase, IIdObject
    {
        #region Fields

        //  交费活动Id
        private string id;

        //  交费日期
        private DateTime rcdDate;



        private int month;


        private string itemId;



        /// <summary>
        /// 总金额
        /// </summary>
        private double amount;



        private int typeId;

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
        /// 获得或者设置交费日期
        /// </summary>
        [Column]
        public DateTime RcdDate
        {
            get { return rcdDate; }
            set
            {
                if (rcdDate != value)
                {
                    rcdDate = value;
                    OnPropertyChanged("RcdDate");
                }
            }
        }



        /// <summary>
        /// 13 代表上半年的明细，14 代表下半年的明细，15 代表一年的明细
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


        [Column]
        public string ItemId
        {
            get { return itemId; }
            set
            {
                if (itemId != value)
                {
                    itemId = value;
                    OnPropertyChanged("ItemId");
                }

            }
        }
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

        /// <summary>
        /// 0 代表月份明细，1 代表半年明细，2 年度明细
        /// </summary>
        [Column]

        public int TypeId
        {
            get { return typeId; }
            set
            {
                if (typeId != value)
                {
                    typeId = value;
                    OnPropertyChanged("TypeId");
                }
            }
        }



        #endregion

        #region Constructors

        public MonthFeeDetail()
        {
        }

        public MonthFeeDetail(string id)
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
