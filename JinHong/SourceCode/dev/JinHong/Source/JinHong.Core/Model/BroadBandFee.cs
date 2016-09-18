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
    /// 宽带费用信息
    /// </summary>
    public class BroadBandFee : EntityBase, IIdObject
    {
        #region Fields

        //  Id, Guid自动生成
        private string id;
        //  关联租赁活动Id
        private string leasingInfoId;
        //  关联日期
        private DateTime? date;
        //  公司或个人名称(实际应该为Id)(冗余字段, 可从LeasingInfoId关联SocialUnitInfo查询)
        private string socialUnitName;

        /// <summary>
        /// 公司或个人Id
        /// </summary>
        private string socialUnitId;

        //  费用类别
        private string category;
        //  收费单号
        private string receiptNo;
        //  金额
        private double? amount;
        //  区域(冗余字段, 可从LeasingInfoId关联RoomInfo查询)
        private string _roomId;
        //  联系人(冗余字段, 可从LeasingInfoId关联SocialUnitInfo查询)
        private string contactName;


        //  电话(冗余字段, 可从LeasingInfoId关联SocialUnitInfo查询)
        private string phoneNo;

        //  备注
        private string notes;

        private string _roomName;



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
        /// 获得或者设置关联租赁活动Id
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

        /// <summary>
        /// 获得或者设置关联日期
        /// </summary>
        [Column]
        public DateTime? Date
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
        /// 获得或者设置费用类别
        /// </summary>
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

        /// <summary>
        /// 获得或者设置收费单号
        /// </summary>
        [Column]
        public string ReceiptNo
        {
            get { return receiptNo; }
            set
            {
                if (receiptNo != value)
                {
                    receiptNo = value;
                    OnPropertyChanged("ReceiptNo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置金额
        /// </summary>
        [Column]
        public double? Amount
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
        /// 获得或者设置区域
        /// </summary>
        [Column]
        public string RoomId
        {
            get { return _roomId; }
            set
            {
                if (_roomId != value)
                {
                    _roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }
        /// <summary>
        /// 区域名称
        /// </summary>
        [Column]
        public string RoomName
        {
            get { return _roomName; }
            set
            {
                if (_roomName != value)
                {
                    _roomName = value;
                    OnPropertyChanged("RoomName");
                }
            }
        }

        /// <summary>
        /// 获得或者设置联系人
        /// </summary>
        [Column]
        public string ContactName
        {
            get { return contactName; }
            set
            {
                if (contactName != value)
                {
                    contactName = value;
                    OnPropertyChanged("ContactName");
                }
            }
        }

       

        /// <summary>
        /// 获得或者设置电话
        /// </summary>
        [Column]
        public string PhoneNo
        {
            get { return phoneNo; }
            set
            {
                if (phoneNo != value)
                {
                    phoneNo = value;
                    OnPropertyChanged("PhoneNo");
                }
            }
        }

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

        #endregion

        #region Constructors

        public BroadBandFee()
        {
        }

        public BroadBandFee(string id)
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
