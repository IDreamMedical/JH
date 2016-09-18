using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Model
{
    public class GrantKey : EntityBase, IIdObject
    {
        #region Fields


        private string _id;


        private string _roomId;


        private string socialUnitId;


        private string _tel;

        private string _keysCount;

        private DateTime _grantDate;



        private DateTime _grantId;


        /// <summary>
        /// 联系人
        /// </summary>
        private string _contractName;

        private string _costumerId;
        private string _remark;




        #endregion

        #region Properties

        [PrimaryKey]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
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
            get { return _roomId; }
            set
            {
                if (_roomId != value)
                {
                    _roomId = value;
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
        /// 获得或者设置租赁户电话
        /// </summary>
        [Column]
        public string Tel
        {
            get { return _tel; }
            set
            {
                if (_tel != value)
                {
                    _tel = value;
                    OnPropertyChanged("Tel");
                }
            }
        }

        /// <summary>
        /// 获得或者设置入租时间
        /// </summary>
        [Column]
        public DateTime GrantDate
        {
            get { return _grantDate; }
            set
            {
                if (_grantDate != value)
                {
                    _grantDate = value;
                    OnPropertyChanged("GrantDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置入租时间
        /// </summary>
        [Column]
        public DateTime GrantId
        {
            get { return _grantId; }
            set
            {
                if (_grantId != value)
                {
                    _grantId = value;
                    OnPropertyChanged("GrantId");
                }
            }
        }



        /// <summary>
        /// 获得或者设置合同编号
        /// </summary>
        [Column]
        public string ContractName
        {
            get { return _contractName; }
            set
            {
                if (_contractName != value)
                {
                    _contractName = value;
                    OnPropertyChanged("ContractName");
                }
            }
        }



        [Column]
        public string KeysCount
        {
            get { return _keysCount; }
            set
            {
                if (_keysCount != value)
                {
                    _keysCount = value;
                    OnPropertyChanged("KeysCount");
                }
            }
        }

        public string CostumerId
        {
            get { return _costumerId; }
            set
            {
                if (_costumerId != value)
                {
                    _costumerId = value;
                    OnPropertyChanged("CostumerId");
                }
            }
        }


        public string Remark
        {
            get { return _remark; }
            set
            {
                if (_remark != value)
                {
                    _remark = value;
                    OnPropertyChanged("Remark");
                }
            }
        }


        #endregion

        #region Constructors

        public GrantKey()
        {
        }

        public GrantKey(string id)
        {
            this.Id = id;
        }

        #endregion

    }
}
