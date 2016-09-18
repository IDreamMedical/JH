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
    /// 社会成员信息
    /// </summary>
    ///
    [Table("SocialUnitInfo")]
    public class SocialUnitInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        /// <summary>
        /// 法人代表
        /// </summary>
        private string socialUnitLeader;


        /// <summary>
        /// 企业性质
        /// </summary>
        private string socialUnitType;


        /// <summary>
        /// 电话
        /// </summary>
        private string telNo;

        /// <summary>
        /// 注册地址
        /// </summary>
        private string socialUnitAddress;



        /// <summary>
        /// 注册资金
        /// </summary>
        private string socialUnitCapital;

        private string status;

        /// <summary>
        /// 营业执照日期
        /// </summary>
        private DateTime? licenceDate;

        private int trendId;






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

        [Column]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
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
        public string SocialUnitLeader
        {
            get { return socialUnitLeader; }
            set
            {
                if (socialUnitLeader != value)
                {
                    socialUnitLeader = value;
                    OnPropertyChanged("SocialUnitLeader");
                }
            }
        }
        [Column]
        public string SocialUnitType
        {
            get { return socialUnitType; }
            set
            {
                if (socialUnitType != value)
                {
                    socialUnitType = value;
                    OnPropertyChanged("SocialUnitType");
                }
            }
        }

        [Column]

        public string TelNo
        {
            get { return telNo; }
            set
            {
                if (telNo != value)
                {
                    telNo = value;
                    OnPropertyChanged("TelNo");
                }
            }
        }


        [Column]
        public string SocialUnitAddress
        {
            get { return socialUnitAddress; }
            set
            {
                if (socialUnitAddress != value)
                {
                    socialUnitAddress = value;
                    OnPropertyChanged("SocialUnitAddress");
                }
            }
        }
        [Column]
        public string SocialUnitCapital
        {
            get { return socialUnitCapital; }
            set
            {
                if (socialUnitCapital != value)
                {
                    socialUnitCapital = value;
                    OnPropertyChanged("SocialUnitCapital");
                }
            }
        }

        [Column]
        public string Status
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
        public DateTime? LicenceDate
        {
            get { return licenceDate; }
            set
            {
                if (licenceDate != value)
                {
                    licenceDate = value;
                    OnPropertyChanged("LicenceDate");
                }
            }
        }

        [Column]
        public int TrendId
        {
            get { return trendId; }
            set
            {
                if (trendId != value)
                {
                    trendId = value;
                    OnPropertyChanged("TrendId");
                }
            }
        }


        #endregion

        #region Constructors

        public SocialUnitInfo()
        {
        }

        public SocialUnitInfo(string id)
        {
            this.Id = id;
        }

        public SocialUnitInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO
    }
}
