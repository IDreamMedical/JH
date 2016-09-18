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
    /// 楼宇信息
    /// </summary>
    [Table("ParkingLotRentalInfo")]
    public class ParkingLotRentalInfo : EntityBase, IIdObject
    {
        #region Fields

        //  停车位Id
        private string id;

        //  座室号
        private string buildingId;
        //  单位
        private string socialUnitName;
        //  金额
        private double amount;
        //  时间段开始
        private DateTime? timeFrom;
        //  时间段结束
        private DateTime? timeTo;

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

        /// <summary>
        /// 获得或者设置座室号
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
        /// 获得或者设置单位
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
        /// 获得或者设置时间段开始
        /// </summary>
        [Column]
        public DateTime? TimeFrom
        {
            get { return timeFrom; }
            set
            {
                if (timeFrom != value)
                {
                    timeFrom = value;
                    OnPropertyChanged("TimeFrom");
                }
            }
        }

        /// <summary>
        /// 获得或者设置时间段结束
        /// </summary>
        [Column]
        public DateTime? TimeTo
        {
            get { return timeTo; }
            set
            {
                if (timeTo != value)
                {
                    timeTo = value;
                    OnPropertyChanged("TimeTo");
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

        public ParkingLotRentalInfo()
        {
        }

        public ParkingLotRentalInfo(string id)
        {
            this.Id = id;
        }


        #endregion

        #region Methods

        //  TODO

        #endregion

        //  TODO... Id, etc
    }
}
