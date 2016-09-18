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
    /// 停车费交费信息
    /// </summary>
    public class ParkingFeesInfo : EntityBase, IIdObject
    {
        #region Fields

        //  交费活动Id
        private string id;

        //  关联的租赁信息Id
        private string feeRecordId;

        //  交费日期
        private DateTime payDate;

        //  实交金额
        private double amount;

        private int isPay;

        private DateTime? timeFrom;
        //  时间段结束
        private DateTime? timeTo;

        //  备注
        private string notes;
        private string socialUnitId;

        private string parkingLotRentalInfoId;




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

        /// 获得或者设置关联的费用信息Id
        /// </summary>
        [Column]
        public string FeeRecordId
        {
            get { return feeRecordId; }
            set
            {
                if (feeRecordId != value)
                {
                    feeRecordId = value;
                    OnPropertyChanged("FeeRecordId");
                }
            }
        }

        /// <summary>
        /// 获得或者设置交费日期
        /// </summary>
        [Column]
        public DateTime PayDate
        {
            get { return payDate; }
            set
            {
                if (payDate != value)
                {
                    payDate = value;
                    OnPropertyChanged("PayDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置实交金额
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
        public int IsPay
        {
            get { return isPay; }
            set
            {
                if (isPay != value)
                {
                    isPay = value;
                    OnPropertyChanged("IsPay");
                }

            }
        }


        [Column]
        public string SocialUnitId
        {
            get { return socialUnitId; }
            set
            {
                if (this.socialUnitId != value)
                {
                    this.socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }
        [Column]
        public string ParkingLotRentalInfoId
        {
            get { return parkingLotRentalInfoId; }
            set
            {
                if (parkingLotRentalInfoId != value)
                {
                    parkingLotRentalInfoId = value;
                    OnPropertyChanged("ParkingLotRentalInfoId");
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



        #endregion

        #region Constructors

        public ParkingFeesInfo()
        {
        }

        public ParkingFeesInfo(string id)
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
