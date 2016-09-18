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
    /// 企业入租退租信息
    /// 表示当前租赁情况, 租赁收入的记录是累加的, 本表则会根据最新的数据修改
    /// </summary>
    [Table("LeasingInfo")]
    public class LeasingInfo : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

        //  座号
        private string buildingId;
        //  室号
        private string roomId;
        //  租赁户名称
        private string socialUnitName;
        //  租赁户电话
        private string phoneNo;
        //  入租时间
        private DateTime? leaseBeginDate;
        //  退租时间
        private DateTime? leaseEndDate;

        //  TODO

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
        /// 获得或者设置座号
        /// </summary>
        [Column]
        public string BuildingId
        {
            get { return buildingId; }
            set
            {
                if (buildingId != value)
                {
                    buildingId = value;
                    OnPropertyChanged("BuildingId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置室号
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
        /// 获得或者设置租赁户名称
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
        /// 获得或者设置租赁户电话
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

        /// <summary>
        /// 获得或者设置入租时间
        /// </summary>
        [Column]
        public DateTime? LeaseBeginDate
        {
            get { return leaseBeginDate; }
            set
            {
                if (leaseBeginDate != value)
                {
                    leaseBeginDate = value;
                    OnPropertyChanged("LeaseBeginDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置退租时间
        /// </summary>
        [Column]
        public DateTime? LeaseEndDate
        {
            get { return leaseEndDate; }
            set
            {
                if (leaseEndDate != value)
                {
                    leaseEndDate = value;
                    OnPropertyChanged("LeaseEndDate");
                }
            }
        }

        #endregion

        #region Constructors

        public LeasingInfo()
        {
        }

        public LeasingInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
