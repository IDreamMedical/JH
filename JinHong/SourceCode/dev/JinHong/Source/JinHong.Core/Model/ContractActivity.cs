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
    /// 合同操作活动, 包括合同的订立, 变更, 解除, 重新订立, 和终止等操作
    /// </summary>
    [Table("ContractActivity")]
    public class ContractActivity: ActivityBase
    {
        #region Fields

        private string id;
        private string contractId;
        private string partyA;
        private string partyB;
        //  活动原因(变更原因等)
        private string reason;
        //  平方租金/日
        private double amountPerSquareMeterPerDay;
        //  平方物业费/日, Estate service
        private double esfPerSquareMeterPerDay;
        private DateTime? date;

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
        /// 获得或者设置合同Id
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

        /// <summary>
        /// 获得或者设置甲方Id或名称
        /// </summary>
        [Column]
        public string PartyA
        {
            get { return partyA; }
            set
            {
                if (partyA != value)
                {
                    partyA = value;
                    OnPropertyChanged("PartyA");
                }
            }
        }

        /// <summary>
        /// 获得或者设置乙方Id或名称
        /// </summary>
        [Column]
        public string PartyB
        {
            get { return partyB; }
            set
            {
                if (partyB != value)
                {
                    partyB = value;
                    OnPropertyChanged("PartyB");
                }
            }
        }

        /// <summary>
        /// 获得或者设置活动原因(变更原因等)
        /// </summary>
        [Column]
        public string Reason
        {
            get { return reason; }
            set
            {
                if (reason != value)
                {
                    reason = value;
                    OnPropertyChanged("Reason");
                }
            }
        }

        /// <summary>
        /// 获得或者设置平方租金/日
        /// </summary>
        [Column]
        public double AmountPerSquareMeterPerDay
        {
            get { return amountPerSquareMeterPerDay; }
            set
            {
                if (amountPerSquareMeterPerDay != value)
                {
                    amountPerSquareMeterPerDay = value;
                    OnPropertyChanged("AmountPerSquareMeterPerDay");
                }
            }
        }

        /// <summary>
        /// 获得或者设置平方物业费/日
        /// </summary>
        [Column]
        public double EsfPerSquareMeterPerDay
        {
            get { return esfPerSquareMeterPerDay; }
            set
            {
                if (esfPerSquareMeterPerDay != value)
                {
                    esfPerSquareMeterPerDay = value;
                    OnPropertyChanged("EsfPerSquareMeterPerDay");
                }
            }
        }

        /// <summary>
        /// 获得或者设置发生日
        /// </summary>
        [Column]
        public DateTime? Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        #endregion

        #region Constructors

        public ContractActivity()
        {
        }

        public ContractActivity(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }

    public enum ContractActivityCategory
    {
        Signing,
        Modification,
        Cancellation,
        Revision,
        Termination
    }
}
