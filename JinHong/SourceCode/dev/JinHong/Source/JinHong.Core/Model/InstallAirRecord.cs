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
    [Table("InstallAirRecord")]
    public class InstallAirRecord : EntityBase, IIdObject
    {
        #region Fields

        private string id;

        //  建筑Id
        private string socialUnitId;
        //  建筑Id
        private string socialUnitName;



        //  室Id
        private string roomId;
        //  年
        private DateTime installDate;
        //  月
        private int isSigned;

        //  电表数
        private int amout;


        //  备注
        private string notes;

        private string airConditionerId;

        private int status;




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
        public DateTime InstallDate
        {
            get { return installDate; }
            set
            {
                if (installDate != value)
                {
                    installDate = value;
                    OnPropertyChanged("InstallDate");
                }
            }
        }
        [Column]
        public int IsSigned
        {
            get { return isSigned; }
            set
            {
                if (isSigned != value)
                {
                    isSigned = value;
                    OnPropertyChanged("IsSigned");
                }
            }
        }

        [Column]
        public int Amout
        {
            get { return amout; }
            set
            {
                if (amout != value)
                {
                    amout = value;
                    OnPropertyChanged("Amout");
                }
            }
        }
        [Column]

        public string AirConditionerId
        {
            get { return airConditionerId; }
            set
            {
                if (airConditionerId != value)
                {
                    airConditionerId = value;
                    OnPropertyChanged("AirConditionerId");
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


        #endregion

        #region Constructors

        public InstallAirRecord()
        {
        }

        public InstallAirRecord(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
