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
    /// 电信相关费用信息
    /// </summary>
    public class WaterFeeInfo : EntityBase, IIdObject
    {
        #region Fields

        //  Id, Guid自动生成
        private string id;

        //  关联日期
        private DateTime rcdDate;

        /// <summary>
        /// 公司或个人Id
        /// </summary>
        private string socialUnitId;


        //  收费单号
        private string invoiceType;
        //  金额
        private double amount;



        private int isPay;

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
        /// 获得或者设置关联日期
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


        /// <summary>
        /// 获得或者设置收费单号
        /// </summary>
        [Column]
        public string InvoiceType
        {
            get { return invoiceType; }
            set
            {
                if (invoiceType != value)
                {
                    invoiceType = value;
                    OnPropertyChanged("InvoiceType");
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
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
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
        #endregion

        #region Constructors

        public WaterFeeInfo()
        {
        }

        public WaterFeeInfo(string id)
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
