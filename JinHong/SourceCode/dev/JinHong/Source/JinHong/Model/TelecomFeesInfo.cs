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
    public class TelecomFeesInfo : EntityBase, IIdObject
    {
        #region Fields

        //  Id, Guid自动生成
        private string id;
        //  关联日期
        private DateTime? date;
        //  公司或个人名称(实际应该为Id)
        private string socialUnitName;
        //  费用类别
        private string category;
        //  收费单号
        private string receiptNo;
        //  金额
        private string amount;
        //  区域
        private string roomId;
        //  联系人
        private string liason;
        //  电话
        private string phoneNo;

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
        public string Amount
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
            get { return roomId; }
            set
            {
                if (roomId != value)
                {
                    roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置联系人
        /// </summary>
        [Column]
        public string Liason
        {
            get { return liason; }
            set
            {
                if (liason != value)
                {
                    liason = value;
                    OnPropertyChanged("Liason");
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

        #endregion

        #region Constructors

        public TelecomFeesInfo()
        {
        }

        public TelecomFeesInfo(string id)
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
