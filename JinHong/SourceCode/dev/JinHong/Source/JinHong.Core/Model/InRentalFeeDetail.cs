using System;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;
namespace JinHong.Model
{
    /// <summary>
    /// InRentalFeeDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class InRentalFeeDetail : EntityBase, IIdObject
    {
        public InRentalFeeDetail()
        { }
        #region Model
        private string _id;
        private string _socalunitid;
        private DateTime? _ysdate;
        private decimal? _rentalfeeamout;
        private decimal? _wyfeeamout;
        private DateTime? _factdate;
        private decimal? _factrentalfeeeamout;
        private decimal? _factwyamout;
        private string _timeslot;

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        ///     
        [Column]
        public string SocalUnitId
        {
            set
            {
                if (_socalunitid != value)
                {
                    _socalunitid = value;
                    OnPropertyChanged("SocalUnitId");
                }
            }
            get { return _socalunitid; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public DateTime? YSDate
        {
            set
            {
                if (_ysdate != value)
                {
                    _ysdate = value;
                    OnPropertyChanged("SocalUnitId");
                }
            }
            get { return _ysdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal? RentalFeeAmout
        {
            set
            {
                if (_rentalfeeamout != value)
                {
                    _rentalfeeamout = value;
                    OnPropertyChanged("SocalUnitId");
                }
            }
            get { return _rentalfeeamout; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Column]
        public decimal? WYFeeAmout
        {
            set
            {
                if (_wyfeeamout != value)
                {
                    _wyfeeamout = value;
                    OnPropertyChanged("WYFeeAmout");
                }
            }
            get { return _wyfeeamout; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        [Column]
        public DateTime? FactDate
        {
            set
            {
                if (_factdate != value)
                {
                    _factdate = value;
                    OnPropertyChanged("FactDate");
                }
            }
            get { return _factdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal? FactRentalFeeeAmout
        {
            set
            {
                if (_factrentalfeeeamout != value)
                {
                    _factrentalfeeeamout = value;
                    OnPropertyChanged("FactRentalFeeeAmout");
                }
            }
            get { return _factrentalfeeeamout; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public decimal? FactWYAmout
        {
            set
            {
                if (_factwyamout != value)
                {
                    _factwyamout = value;
                    OnPropertyChanged("FactWYAmout");
                }
            }
            get { return _factwyamout; }
        }
        /// <summary>
        /// 
        /// </summary>
        [Column]
        public string Timeslot
        {
            set
            {
                if (_timeslot != value)
                {
                    _timeslot = value;
                    OnPropertyChanged("Timeslot");
                }
            }
            get { return _timeslot; }
        }
        #endregion Model

    }
}

