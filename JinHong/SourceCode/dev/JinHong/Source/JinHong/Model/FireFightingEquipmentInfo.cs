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
    /// 消防设备信息
    /// </summary>
    [Table("FireFightingEquipmentInfo")]
    public class FireFightingEquipmentInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
         #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        //  座号
        private string buildingId;
        //  消防铁箱号
        private string fireFightingBox;
        //  二氧化碳
        private string carbonDioxideExtinguisher;
        //  重量
        private string weight;
        //  干粉
        private string dryChemicalExtinguisher;
        //  消防栓
        private string fireHydrant;
        //  水带
        private string fireHose;
        //  水枪
        private string fireBranch;
        //  露天消防栓
        private string outdoorFireHydrant;
        //  消防泵房
        private string firePumpRoom;
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
        /// 获得或者设置消防铁箱号
        /// </summary>
        [Column]
        public string FireFightingBox
        {
            get { return fireFightingBox; }
            set
            {
                if (fireFightingBox != value)
                {
                    fireFightingBox = value;
                    OnPropertyChanged("FireFightingBox");
                }
            }
        }

        /// <summary>
        /// 获得或者设置二氧化碳灭火器
        /// </summary>
        [Column]
        public string CarbonDioxideExtinguisher
        {
            get { return carbonDioxideExtinguisher; }
            set
            {
                if (carbonDioxideExtinguisher != value)
                {
                    carbonDioxideExtinguisher = value;
                    OnPropertyChanged("CarbonDioxideExtinguisher");
                }
            }
        }

        /// <summary>
        /// 获得或者设置二氧化碳灭火器
        /// </summary>
        [Column]
        public string Weight
        {
            get { return weight; }
            set
            {
                if (weight != value)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }

        /// <summary>
        /// 获得或者设置干粉灭火器
        /// </summary>
        [Column]
        public string DryChemicalExtinguisher
        {
            get { return dryChemicalExtinguisher; }
            set
            {
                if (dryChemicalExtinguisher != value)
                {
                    dryChemicalExtinguisher = value;
                    OnPropertyChanged("DryChemicalExtinguisher");
                }
            }
        }

        /// <summary>
        /// 获得或者设置消防栓
        /// </summary>
        [Column]
        public string FireHydrant
        {
            get { return fireHydrant; }
            set
            {
                if (fireHydrant != value)
                {
                    fireHydrant = value;
                    OnPropertyChanged("FireHydrant");
                }
            }
        }

        /// <summary>
        /// 获得或者设置水带
        /// </summary>
        [Column]
        public string FireHose
        {
            get { return fireHose; }
            set
            {
                if (fireHose != value)
                {
                    fireHose = value;
                    OnPropertyChanged("FireHose");
                }
            }
        }

        /// <summary>
        /// 获得或者设置水枪
        /// </summary>
        [Column]
        public string FireBranch
        {
            get { return fireBranch; }
            set
            {
                if (fireBranch != value)
                {
                    fireBranch = value;
                    OnPropertyChanged("FireBranch");
                }
            }
        }

        /// <summary>
        /// 获得或者设置露天消防栓
        /// </summary>
        [Column]
        public string OutdoorFireHydrant
        {
            get { return outdoorFireHydrant; }
            set
            {
                if (outdoorFireHydrant != value)
                {
                    outdoorFireHydrant = value;
                    OnPropertyChanged("OutdoorFireHydrant");
                }
            }
        }

        /// <summary>
        /// 获得或者设置消防泵房
        /// </summary>
        [Column]
        public string FirePumpRoom
        {
            get { return firePumpRoom; }
            set
            {
                if (firePumpRoom != value)
                {
                    firePumpRoom = value;
                    OnPropertyChanged("FirePumpRoom");
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

        public FireFightingEquipmentInfo()
        {
        }

        public FireFightingEquipmentInfo(string id)
        {
            this.Id = id;
        }

        public FireFightingEquipmentInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO
    }
}
