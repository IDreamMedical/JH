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
    /// 抄电表数信息
    /// </summary>
    [Table("AmmeterDataInfo")]
    public class AmmeterDataInfo : EntityBase, IIdObject
    {
        #region Fields

        private string id;

        //  建筑Id
        private string buildingId;
        //  室Id
        private string roomId;
        //  年
        private int year;
        //  月
        private int month;

        //  电表数
        private double powerValue;

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
        /// 获得或者设置年份
        /// </summary>
        [Column]
        public int Year
        {
            get { return year; }
            set
            {
                if (year != value)
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        /// <summary>
        /// 获得或者设置月份
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

        /// <summary>
        /// 获得或者设置抄电表数
        /// </summary>
        [Column]
        public double PowerValue
        {
            get { return powerValue; }
            set
            {
                if (powerValue != value)
                {
                    powerValue = value;
                    OnPropertyChanged("PowerValue");
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

        public AmmeterDataInfo()
        {
        }

        public AmmeterDataInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
