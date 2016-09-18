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
    /// 退租房间状态记录表
    /// 
    /// </summary>
    [Table("UnLeaseRoomStatusDetail")]
    public class UnLeaseRoomStatusDetail : EntityBase, IIdObject
    {
        #region Fields

        //  楼宇Id
        private string id;

        /// <summary>
        /// 费用类型
        /// </summary>
        private string socialUnitId;

        // 费用
        private string xmlStr;

        //  费用交至时间
        private string name;

        //  费用交至时间
        private string sureEmp;



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
        public string XmlStr
        {
            get { return xmlStr; }
            set
            {
                if (xmlStr != value)
                {
                    xmlStr = value;
                    OnPropertyChanged("XmlStr");
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
        public string SureEmp
        {
            get { return sureEmp; }
            set
            {
                if (sureEmp != value)
                {
                    sureEmp = value;
                    OnPropertyChanged("SureEmp");
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

        public UnLeaseRoomStatusDetail()
        {
        }

        public UnLeaseRoomStatusDetail(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
