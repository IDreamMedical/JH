using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Entity;
using UniGuy.Core.Data;

namespace JinHong.Model
{
    /// <summary>
    /// 租赁单位安全管理协议信息
    /// </summary>
    public class SecurityManagementContractInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        //  租赁单位名称
        private string socialUnitName;
        //  区域
        private string buildingId;
        private string roomId;
        //  面积
        private string area;
        //  生效日期
        private DateTime? effectiveDate;
        //  失效日期
        private DateTime? expirateDate;
        //  租赁性质
        private string leasingType;
        //  责任人
        private string liablePerson;
        //  备注
        private string notes;

        //  TODO...

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
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        [Column]
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// 获得或者设置租赁单位名称
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
        /// 获得或者设置区域
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
        /// 获得或者设置区域室号
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
        /// 获得或者设置面积
        /// </summary>
        [Column]
        public string Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    OnPropertyChanged("Area");
                }
            }
        }

        /// <summary>
        /// 获得或者设置生效日期
        /// </summary>
        [Column]
        public DateTime? EffectiveDate
        {
            get { return effectiveDate; }
            set
            {
                if (effectiveDate != value)
                {
                    effectiveDate = value;
                    OnPropertyChanged("EffectiveDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置失效日期
        /// </summary>
        [Column]
        public DateTime? ExpirateDate
        {
            get { return expirateDate; }
            set
            {
                if (expirateDate != value)
                {
                    expirateDate = value;
                    OnPropertyChanged("ExpirateDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置租赁性质
        /// </summary>
        [Column]
        public string LeasingType
        {
            get { return leasingType; }
            set
            {
                if (leasingType != value)
                {
                    leasingType = value;
                    OnPropertyChanged("LeasingType");
                }
            }
        }

        /// <summary>
        /// 获得或者设置责任人
        /// </summary>
        [Column]
        public string LiablePerson
        {
            get { return liablePerson; }
            set
            {
                if (liablePerson != value)
                {
                    liablePerson = value;
                    OnPropertyChanged("LiablePerson");
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

        #endregion

        #region Constructors

        public SecurityManagementContractInfo()
        {
        }

        public SecurityManagementContractInfo(string id)
        {
            this.Id = id;
        }

        public SecurityManagementContractInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO... Id, Format等
    }
}
