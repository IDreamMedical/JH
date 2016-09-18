using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Entity;
using UniGuy.Core.Data;

namespace JinHong.Model
{
    /// <summary>
    /// 合同信息
    /// </summary>
    /// 
    [Table("ContractInfo")]
    public class ContractInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

        //  生效日期
        private DateTime? effectiveDate;
        //  失效日期
        private DateTime? expirateDate;

        //合同状态
        private int status;

        //合同号
        private string contractNo;

        //
        private string notes;

        //  租赁期
        private int warrantPeriod;

        /// <summary>
        /// 租金
        /// </summary>
        private double rentalFees;

        /// <summary>
        /// 营业执照日期
        /// </summary>
        private DateTime? licenceDate;

        /// <summary>
        /// 营业执照日期
        /// </summary>
        private DateTime preDate;






        /// <summary>
        ///  0 ,表示单元租赁， 1 表示仓库租赁
        /// </summary>
        private int leaseType;



        /// <summary>
        /// 总金额
        /// </summary>
        private double totalRentalFees;



        private DateTime? rcdDate;

        private string rcdId;


        private DateTime? contractDate;

        private string customerTel;



        private string employeeId;


        private string customerName;



        private string socialUnitName;

        private string socialUnitId;

        /// <summary>
        /// 押金
        /// </summary>
        private double depositFee;

        /// <summary>
        /// 是否已交押金
        /// </summary>
        private int isDeposited;

        /// <summary>
        /// 支付方式
        /// </summary>
        private string payWay;







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
        public DateTime? EffectiveDate
        {
            get { return effectiveDate; }
            set
            {
                if (effectiveDate != value)
                {
                    effectiveDate = value;
                    OnPropertyChanged("EffectiveDate");
                }
            }
        }

        [Column]
        public DateTime? ExpirateDate
        {
            get { return expirateDate; }
            set
            {
                if (expirateDate != value)
                {
                    expirateDate = value;
                    OnPropertyChanged("ExpirateDate");
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
        public string ContractNo
        {
            get { return contractNo; }
            set
            {
                if (contractNo != value)
                {
                    contractNo = value;

                    OnPropertyChanged("ContractNo");
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
        public string CustomerTel
        {
            get { return customerTel; }
            set
            {
                if (customerTel != value)
                {
                    customerTel = value;
                    OnPropertyChanged("CustomerTel");
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
        public DateTime? ContractDate
        {
            get { return contractDate; }
            set
            {

                if (contractDate != value)
                {
                    contractDate = value;
                    OnPropertyChanged("ContractDate");
                }
            }
        }
        [Column]
        public string EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (employeeId != value)
                {
                    employeeId = value;
                    OnPropertyChanged("EmployeeId");
                }

            }
        }
        [Column]
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                if (customerName != value)
                {
                    customerName = value;
                    OnPropertyChanged("CustomerName");
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
                    OnPropertyChanged("WarrantPeriod");
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
        public double TotalRentalFees
        {
            get { return totalRentalFees; }
            set
            {
                if (totalRentalFees != value)
                {
                    totalRentalFees = value;
                    OnPropertyChanged("TotalRentalFees");
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
        public string PayWay
        {
            get { return payWay; }
            set
            {
                if (payWay != value)
                {
                    payWay = value;
                    OnPropertyChanged("PayWay");
                }
            }
        }

        [Column]
        public int IsDeposited
        {
            get { return isDeposited; }
            set
            {
                if (isDeposited != value)
                {
                    isDeposited = value;
                    OnPropertyChanged("IsDeposited");
                }
            }
        }
        [Column]
        public double DepositFee
        {
            get { return depositFee; }
            set
            {
                if (depositFee != value)
                {

                    depositFee = value;
                    OnPropertyChanged("DepositFee");
                }
            }
        }


        [Column]
        public int LeaseType
        {
            get { return leaseType; }
            set
            {
                if (leaseType != value)
                {
                    leaseType = value;
                    OnPropertyChanged("LeaseType");
                }
            }
        }


        public DateTime PreDate
        {
            get { return preDate; }
            set
            {
                if (preDate != value)
                {
                    preDate = value;
                    OnPropertyChanged("PreDate");
                }
            }
        }

        #endregion

        #region Constructors

        public ContractInfo()
        {
        }

        public ContractInfo(string id)
        {
            this.Id = id;
        }

        public ContractInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
        //  TODO... Id, Format等
    }
}
