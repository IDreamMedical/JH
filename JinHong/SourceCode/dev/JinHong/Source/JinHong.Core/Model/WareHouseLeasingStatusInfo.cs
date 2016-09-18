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
    /// 仓库租赁状态信息
    /// 表示当前租赁情况, 租赁收入的记录是累加的, 本表则会根据最新的数据修改
    /// </summary>
    [Table("WareHouseLeasingStatusInfo")]
    public class WareHouseLeasingStatusInfo : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

        //  对应的租赁信息Id
        private string leasingInfoId;
        //  仓库编号(冗余)
        private string wareHouseId;
        //  租赁户Id(冗余)
        private string socialUnitId;
        //  租赁户名称(冗余)
        private string socialUnitName;
        //  租赁户电话(冗余)
        private string phoneNo;

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
        /// 获得或者设置对应的租赁信息Id
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
        /// 获得或者设置仓库编号
        /// </summary>
        [Column]
        public string WareHouseId
        {
            get { return wareHouseId; }
            set
            {
                if (wareHouseId != value)
                {
                    wareHouseId = value;
                    OnPropertyChanged("WareHouseId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置租赁户编号
        /// </summary>
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

        #endregion

        #region Constructors

        public WareHouseLeasingStatusInfo()
        {
        }

        public WareHouseLeasingStatusInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
