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
    /// 仓库租赁信息
    /// </summary>
    public class WareHouseLeasingInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  Id, 一般由Guid生成
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        //  仓库编号
        private string wareHouseId;
        //  单位编号
        private string socialUnitId;
        //  单位名称
        private string socialUnitName;

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

        #endregion

        #region Constructors

        public WareHouseLeasingInfo()
        {
        }

        public WareHouseLeasingInfo(string id)
        {
            this.Id = id;
        }

        public WareHouseLeasingInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO... Id, etc
    }
}
