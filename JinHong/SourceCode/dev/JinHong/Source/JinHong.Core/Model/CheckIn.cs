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
   /// 入住登记信息表
   /// </summary>
    [Table("CheckIn")]
    public class CheckIn : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

        //  座号
        private string buildingId;
        //  室号
        private string roomId;
        //  租赁户Id
        private string socialUnitId;
        //  租赁户名称
        private string socialUnitName;
        //  租赁户电话
        private string phoneNo;
        //  入租时间
        private DateTime? leaseBeginDate;
        //  退租时间
        private DateTime? leaseEndDate;

        //  合同编号(租赁生效日期和到期时期在合同信息里面)
        private string contractId;

        /// <summary>
        /// 收件人
        /// </summary>
        private string receiveName;
        /// <summary>
        /// 确认人
        /// </summary>
        private string sureName;


        /// <summary>
        /// 联系人电话
        /// </summary>
        private string contactsName;

        private int keysCount;
        private double powerCount;

        //  备注
        private string notes;

        private string unLeaseName;

        private string wYEmpName;
        private DateTime? wYDate;

        private string zSEmpName;

        private DateTime? zSDate;

        private int isExpirate;



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
        /// 获得或者设置租赁户Id
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
        /// 获得或者设置租赁户名称
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
        /// 获得或者设置租赁户电话
        /// </summary>
        [Column]
        public string PhoneNo
        {
            get { return phoneNo; }
            set
            {
                if (phoneNo != value)
                {
                    phoneNo = value;
                    OnPropertyChanged("PhoneNo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置入租时间
        /// </summary>
        [Column]
        public DateTime? LeaseBeginDate
        {
            get { return leaseBeginDate; }
            set
            {
                if (leaseBeginDate != value)
                {
                    leaseBeginDate = value;
                    OnPropertyChanged("LeaseBeginDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置退租时间
        /// </summary>
        [Column]
        public DateTime? LeaseEndDate
        {
            get { return leaseEndDate; }
            set
            {
                if (leaseEndDate != value)
                {
                    leaseEndDate = value;
                    OnPropertyChanged("LeaseEndDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置合同编号
        /// </summary>
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
        public string ReceiveName
        {
            get { return receiveName; }
            set
            {
                if (receiveName != value)
                {
                    receiveName = value;
                    OnPropertyChanged("ReceiveName");
                }
            }
        }
        [Column]
        public string SureName
        {
            get { return sureName; }
            set
            {
                if (sureName != value)
                {
                    sureName = value;
                    OnPropertyChanged("SureName");
                }
            }
        }

        public string ContactsName
        {
            get { return contactsName; }
            set
            {
                if (contactsName != value)
                {
                    contactsName = value;
                    OnPropertyChanged("ContactsName");
                }
            }
        }
        [Column]
        public int KeysCount
        {
            get { return keysCount; }
            set
            {
                if (keysCount != value)
                {
                    keysCount = value;
                    OnPropertyChanged("KeysCount");
                }
            }
        }
        [Column]
        public double PowerCount
        {
            get { return powerCount; }
            set
            {
                if (powerCount != value)
                {
                    powerCount = value;
                    OnPropertyChanged("PowerCount");
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
        public string UnLeaseName
        {
            get { return unLeaseName; }
            set
            {
                if (unLeaseName != value)
                {
                    unLeaseName = value;
                    OnPropertyChanged("UnLeaseName");
                }
            }
        }
        [Column]
        public string WYEmpName
        {
            get { return wYEmpName; }
            set
            {
                if (wYEmpName != value)
                {

                    wYEmpName = value;
                    OnPropertyChanged("WYEmpName");

                }
            }
        }
        [Column]
        public DateTime? WYDate
        {
            get { return wYDate; }
            set
            {
                if (wYDate != value)
                {

                    wYDate = value;
                    OnPropertyChanged("WYDate");

                }
            }
        }
        [Column]
        public string ZSEmpName
        {
            get { return zSEmpName; }
            set
            {
                if (zSEmpName != value)
                {

                    zSEmpName = value;
                    OnPropertyChanged("ZSEmpName");

                }
            }
        }
        [Column]
        public DateTime? ZSDate
        {
            get { return zSDate; }
            set
            {
                if (zSDate != value)
                {

                    zSDate = value;
                    OnPropertyChanged("ZSDate");

                }
            }
        }
        [Column]
        public int IsExpirate
        {
            get { return isExpirate; }
            set
            {
                if (isExpirate != value)
                {
                    isExpirate = value;
                    OnPropertyChanged("IsExpirate");

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

        public CheckIn()
        {
        }

        public CheckIn(string id)
        {
            this.Id = id;
        }

        #endregion

       
    }
}
