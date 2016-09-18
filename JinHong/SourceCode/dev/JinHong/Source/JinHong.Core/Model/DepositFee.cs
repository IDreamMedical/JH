using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Model
{
    /// <summary>
    /// 押金交费信息
    /// </summary>
    public class DepositFeeInfo : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string _id;

        /// <summary>
        /// 预计入住时间
        /// </summary>
        private DateTime _preDate;


        //  交费日期
        private DateTime _payDate;

        //  实交金额
        private double _amount;

        private int _isPay;

        private string _socialUnitId;

        private string _socialUnitName;


        private string _notes;

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
        /// 获得或者设置交费日期
        /// </summary>
        [Column]
        public DateTime PayDate
        {
            get { return _payDate; }
            set
            {
                if (_payDate != value)
                {
                    _payDate = value;
                    OnPropertyChanged("PayDate");
                }
            }
        }

        [Column]
        public DateTime PreDate
        {
            get { return _preDate; }
            set
            {
                if (_preDate != value)
                {
                    _preDate = value;
                    OnPropertyChanged("PreDate");
                }
            }
        }

        /// <summary>
        /// 获得或者设置实交金额
        /// </summary>
        [Column]
        public double Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }
        [Column]
        public int IsPay
        {
            get { return _isPay; }
            set
            {
                if (_isPay != value)
                {
                    _isPay = value;
                    OnPropertyChanged("IsPay");
                }

            }
        }
        /// <summary>
        /// 获得或者设置备注
        /// </summary>
        [Column]
        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        [Column]
        public string SocialUnitId
        {
            get { return _socialUnitId; }
            set
            {
                if (this._socialUnitId != value)
                {
                    this._socialUnitId = value;
                    OnPropertyChanged("SocialUnitId");
                }
            }
        }

        [Column]
        public string SocialUnitName
        {
            get { return _socialUnitName; }
            set
            {
                if (this._socialUnitName != value)
                {
                    this._socialUnitName = value;
                    OnPropertyChanged("SocialUnitName");
                }
            }
        }


        #endregion

        #region Constructors

        public DepositFeeInfo()
        {
        }

        public DepositFeeInfo(string id)
        {
            this.Id = id;
        }

        #endregion


    }
}
