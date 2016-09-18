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
    /// 车位信息
    /// </summary>
    [Table("ParkingLotInfo")]
    public class ParkingLotInfo : EntityBase, IIdObject
    {
        #region Fields

        //  停车位Id
        private string id;

        //  关联的区域Id
        private string relatedRoomId;
        private string locationDescription;
        private int status;


        private double lotWidth;

        private double lotLength;
        private double price;





        //  备注
        private string notes;

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
        /// 获得或者设置关联的区域Id
        /// </summary>
        [Column]
        public string RelatedRoomId
        {
            get { return relatedRoomId; }
            set
            {
                if (relatedRoomId != value)
                {
                    relatedRoomId = value;
                    OnPropertyChanged("RelatedRoomId");
                }
            }
        }
          [Column]
        public string LocationDescription
        {
            get { return locationDescription; }
            set
            {
                if (relatedRoomId != value)
                {
                    locationDescription = value;
                    OnPropertyChanged("LocationDescription");
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
        public double LotWidth
        {
            get { return lotWidth; }
            set
            {
                if (lotWidth != value)
                {
                    lotWidth = value;
                    OnPropertyChanged("LotWidth");
                }
            }
        }
          [Column]
        public double LotLength
        {
            get { return lotLength; }
            set
            {
                if (lotLength != value)
                {

                    lotLength = value;
                    OnPropertyChanged("LotLength");
                }

            }
        }
          [Column]
        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged("Price");
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

        public ParkingLotInfo()
        {
        }

        public ParkingLotInfo(string id)
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
