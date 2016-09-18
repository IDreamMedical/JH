using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;
using JinHong;

namespace JinHong.Model
{
    [Table("ContractDetail")]
    public class ContractDetail : EntityBase, IIdObject, INamedObject, IDescribable
    {

        #region Fields

        //  楼宇Id
        private string id;
        //  单元
        private string roomId;

        private string roomName;


        //  描述
        private string description;

        private string name;

        // 楼宇
        private string buildingId;

        private string buildingName;



        private string contractId;

        //  租赁期
        private int warrantPeriod;



        /// <summary>
        /// 租金
        /// </summary>
        private double rentalFees;


        /// <summary>
        /// 开始日期
        /// </summary>
        private double startDate;

        // 面积
        private double area;



        private string notes;


        private DateTime? rcdDate;

        private string rcdId;

        private string socialUnitName;

        private string socialUnitId;


        /// <summary>
        /// 每日租金
        /// </summary>
        private double dayRentalFee;
        /// <summary>
        /// 每月租金
        /// </summary>
        private double monthRentalFee;

        /// <summary>
        /// 每日物业费
        /// </summary>
        private double dayPropManageFee;


        /// <summary>
        /// 每月物业费
        /// </summary>
        private double monthPropManageFee;

        private DataView roomInfos;






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

        [System.Xml.Serialization.XmlIgnore]
        public string Name
        {
            get { return roomId; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore]
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
        [Column]
        public string ContractId
        {
            get { return contractId; }
            set
            {
                if (contractId != value)
                {
                    contractId = value;
                    OnPropertyChanged("ContractId");
                }
            }
        }
        [Column]
        public double RentalFees
        {
            get { return rentalFees; }
            set
            {
                if (rentalFees != value)
                {
                    rentalFees = value;
                    OnPropertyChanged("RentalFees");
                }
            }
        }
        [Column]
        public double StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }
        [Column]
        public double Area
        {
            get { return area; }
            set
            {
                if (area != value)
                {
                    area = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("Area");
                }
            }
        }


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
        public DateTime? RcdDate
        {
            get { return rcdDate; }
            set
            {
                if (rcdDate != value)
                {
                    rcdDate = value;
                    OnPropertyChanged("RcdDate");
                }
            }
        }
        [Column]
        public string RcdId
        {
            get { return rcdId; }
            set
            {
                if (rcdId != value)
                {
                    rcdId = value;
                    OnPropertyChanged("RcdId");
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
        public int WarrantPeriod
        {
            get { return warrantPeriod; }
            set
            {
                if (warrantPeriod != value)
                {
                    warrantPeriod = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }
        [Column]
        public double DayPropManageFee
        {
            get { return dayPropManageFee; }
            set
            {

                if (dayPropManageFee != value)
                {
                    dayPropManageFee = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("DayPropManageFee");
                }

            }
        }
        [Column]
        public double MonthPropManageFee
        {
            get { return monthPropManageFee; }
            set
            {
                if (monthPropManageFee != value)
                {
                    monthPropManageFee = value;
                    OnPropertyChanged("MonthPropManageFee");
                }
            }
        }
        [Column]
        public double MonthRentalFee
        {
            get { return monthRentalFee; }
            set
            {
                if (monthRentalFee != value)
                {
                    monthRentalFee = value;
                    OnPropertyChanged("MonthRentalFee");
                }
            }
        }
        [Column]
        public double DayRentalFee
        {
            get { return dayRentalFee; }
            set
            {
                if (dayRentalFee != value)
                {
                    dayRentalFee = value;
                    SetMonthPropManageFee();
                    SetMonthRentalFee();
                    OnPropertyChanged("DayRentalFee");
                }
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public DataView RoomInfos
        {
            get { return roomInfos; }
            set
            {
                if (roomInfos != value)
                {
                    roomInfos = value;
                    OnPropertyChanged("RoomInfos");
                }
            }
        }

        public string RoomName
        {
            get { return roomName; }
            set
            {
                if (roomName != value)
                {
                    roomName = value;

                    OnPropertyChanged("RoomName");
                }
            }
        }

        public string BuildingName
        {
            get { return buildingName; }
            set
            {
                if (buildingName != value)
                {
                    buildingName = value;

                    OnPropertyChanged("BuildingName");
                }
            }
        }


        #endregion

        #region Constructors

        public ContractDetail()
        {
        }

        public ContractDetail(string id)
        {
            this.Id = id;
        }

        public ContractDetail(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
        }

        #endregion


        #region Common Method
        private void SetMonthPropManageFee()
        {
            MonthPropManageFee = DayPropManageFee * Area;
        }

        private void SetMonthRentalFee()
        {
            MonthRentalFee = DayRentalFee * Area;
        }
        #endregion

    }
}
