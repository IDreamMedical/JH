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
    /// 楼宇房间信息
    /// </summary>

    [Table("RoomInfo")]

    public class RoomInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  区域Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        //  所属楼宇Id
        private string buildingId;
        //  面积(平方米)
        private double area;
        /// <summary>
        /// 状态 0 空闲， 1 表示占用（或者已租）
        /// </summary>
        private int status;
        private int _type;



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
        /// 获得或者设置所属楼宇Id
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
        /// 获得或者设置面积, 平方米
        /// </summary>
        [Column]
        public double Area
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


        [Column]
        public int Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        [Column]
        public int Type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        #endregion

        #region Constructors

        public RoomInfo()
        {
        }

        public RoomInfo(string id)
        {
            this.Id = id;
        }

        public RoomInfo(string id, string name = null, string description = null)
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
