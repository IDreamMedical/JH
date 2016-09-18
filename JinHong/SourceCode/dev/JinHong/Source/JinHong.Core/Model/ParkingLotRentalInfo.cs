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
    ///车位租赁信息
    /// </summary>
    [Table("ParkingLotRentalInfo")]
    public class ParkingLotRentalInfo : EntityBase, IIdObject, ICloneable
    {
        #region Fields

        //  Id
        private string id;

        //  停车位Id
        private string parkingLotId;

        //  单位Id
        private string socialUnitId;

        // 月金额
        private double mothAmount;


        // 月金额
        private double totalAmount;


        //  时间段开始
        private DateTime timeFrom;
        //  时间段结束
        private DateTime timeTo;

        //  停车证号码
        private string parkingPermitNo;
        //  车辆号码
        private string licensePlateNo;

        //  备注
        private string notes;


        private string rentalName;

        private string tel;

        private string areaId;

        






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
        /// 获得或者设置车位Id
        /// </summary>
        [Column]
        public string ParkingLotId
        {
            get { return parkingLotId; }
            set
            {
                if (parkingLotId != value)
                {
                    parkingLotId = value;
                    OnPropertyChanged("ParkingLotId");
                }
            }
        }



        /// <summary>
        /// 获得或者设置单位Id
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
        /// 获得或者设置时间段开始
        /// </summary>
        [Column]
        public DateTime TimeFrom
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
        public DateTime TimeTo
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
        /// 获得或者设置停车证号码
        /// </summary>
        [Column]
        public string ParkingPermitNo
        {
            get { return parkingPermitNo; }
            set
            {
                if (parkingPermitNo != value)
                {
                    parkingPermitNo = value;
                    OnPropertyChanged("ParkingPermitNo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置车辆号码
        /// </summary>
        [Column]
        public string LicensePlateNo
        {
            get { return licensePlateNo; }
            set
            {
                if (licensePlateNo != value)
                {
                    licensePlateNo = value;
                    OnPropertyChanged("LicensePlateNo");
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



        [Column]
        public string RentalName
        {
            get { return rentalName; }
            set
            {
                if (rentalName != value)
                {
                    rentalName = value;
                    OnPropertyChanged("RentalName");
                }
            }
        }
        [Column]
        public string Tel
        {
            get { return tel; }
            set
            {

                if (tel != value)
                {
                    tel = value;
                    OnPropertyChanged("Tel");
                }
            }
        }
        [Column]
        public double MothAmount
        {
            get { return mothAmount; }
            set
            {
                mothAmount = value;
                if (mothAmount != value)
                {
                    mothAmount = value;
                    OnPropertyChanged("MothAmount");
                }
            }
        }
        [Column]
        public double TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                if (totalAmount != value)
                {
                    TotalAmount = value;
                    OnPropertyChanged("TotalAmount");
                }
            }
        }


        public string AreaId
        {
            get { return areaId; }
            set
            {
                if (areaId != value)
                {
                    areaId = value;
                    OnPropertyChanged("AreaId");
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

        public ParkingLotRentalInfo(ParkingLotRentalInfo other)
        {
            this.Id = other.Id;
            this.ParkingLotId = other.ParkingLotId;
            this.SocialUnitId = other.SocialUnitId;
            this.TimeFrom = other.TimeFrom;
            this.TimeTo = other.TimeTo;
            this.ParkingPermitNo = other.ParkingPermitNo;
            this.LicensePlateNo = other.LicensePlateNo;
            this.Notes = other.Notes;
        }


        #endregion

        #region Methods

        public object Clone()
        {
            return new ParkingLotRentalInfo(this);
        }



        #endregion


    }
}
