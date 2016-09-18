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
    [Table("RepairRecord")]
    public class RepairRecord : EntityBase, IIdObject
    {
        #region Fields

        private string id;


        private string socialUnitId;

        private string socialUnitEmpName;
        private string socialUnitName;


        private string roomName;


        private string description;


        private string content;


        private string result;




        private string roomId;

        private DateTime repairDate;

        private string notes;

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
        /// <summary>
        /// 获得或者设置备注
        /// </summary>
        [Column]
        public string SocialUnitEmpName
        {
            get { return socialUnitEmpName; }
            set
            {
                if (socialUnitEmpName != value)
                {
                    socialUnitEmpName = value;
                    OnPropertyChanged("SocialUnitEmpName");
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
        public string Content
        {
            get { return content; }
            set
            {
                if (content != value)
                {
                    content = value;
                    OnPropertyChanged("Content");
                }
            }
        }
        [Column]
        public string Result
        {
            get { return result; }
            set
            {
                if (result != value)
                {
                    result = value;
                    OnPropertyChanged("Result");
                }
            }
        }

        [Column]

        public DateTime RepairDate
        {
            get { return repairDate; }
            set
            {
                if (repairDate != value)
                {
                    repairDate = value;
                    OnPropertyChanged("RepairDate");
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

        public RepairRecord()
        {
        }

        public RepairRecord(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
