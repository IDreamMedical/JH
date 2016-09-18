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
    /// 退租费用记录信息表
    /// 
    /// </summary>
    [Table("UnLeaseFeeDetail")]
    public class UnLeaseFeeDetail : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

        /// <summary>
        /// 费用类型
        /// </summary>
        private string feeType;

        // 费用
        private string feeAmout;
        //  费用交至时间
        private string feeTimeTo;

        //  备注
        private string notes;

        //  TODO

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
        public string FeeType
        {
            get { return feeType; }
            set
            {
                if (feeType != value)
                {
                    feeType = value;
                    OnPropertyChanged("FeeType");
                }
            }
        }
          [Column]
        public string FeeAmout
        {
            get { return feeAmout; }
            set
            {
                if (feeAmout != value)
                {
                    feeAmout = value;
                    OnPropertyChanged("FeeAmout");
                }
            }
        }
          [Column]
        public string FeeTimeTo
        {
            get { return feeTimeTo; }
            set
            {
                if (feeTimeTo != value)
                {
                    feeTimeTo = value;
                    OnPropertyChanged("FeeTimeTo");
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
       

        #endregion

        #region Constructors

        public UnLeaseFeeDetail()
        {
        }

        public UnLeaseFeeDetail(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
