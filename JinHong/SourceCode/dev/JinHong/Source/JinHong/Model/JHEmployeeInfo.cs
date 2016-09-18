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
    /// 锦宏员工信息
    /// </summary>
    [Table("EmployeeInfo")]
    public class JHEmployeeInfo:EmployeeInfo
    {
        #region Fields
        //  工号
        private string workNo;
        //  区
        private string district;
        //  街道
        private string street;
        //  家庭地址
        private string address;
        //  电话号码
        private string phoneNo;
        //  邮编
        private string zipCode;
        //  备注
        private string notes;

        #endregion

        #region Properties

        /// <summary>
        /// 获得或者设置工号
        /// </summary>
        [Column]
        public string WorkNo
        {
            get { return workNo; }
            set
            {
                if (workNo != value)
                {
                    workNo = value;
                    OnPropertyChanged("WorkNo");
                }
            }
        }

        /// <summary>
        /// 获得或者设置区
        /// </summary>
        [Column]
        public string District
        {
            get { return district; }
            set
            {
                if (district != value)
                {
                    district = value;
                    OnPropertyChanged("District");
                }
            }
        }

        /// <summary>
        /// 获得或者设置街道
        /// </summary>
        [Column]
        public string Street
        {
            get { return street; }
            set
            {
                if (street != value)
                {
                    street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        /// <summary>
        /// 获得或者设置家庭地址
        /// </summary>
        [Column]
        public string Address
        {
            get { return address; }
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        /// <summary>
        /// 获得或者设置电话号码
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
        /// 获得或者设置邮编
        /// </summary>
        [Column]
        public string ZipCode
        {
            get { return zipCode; }
            set
            {
                if (zipCode != value)
                {
                    zipCode = value;
                    OnPropertyChanged("ZipCode");
                }
            }
        }

        /// <summary>
        /// 获得退休日期
        /// </summary>
        public DateTime? RetireDate
        {
            get
            {
                if (LeaveDate.HasValue && LeaveReasonCode == "3")
                    return LeaveDate;
                return null;
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

        public JHEmployeeInfo()
        {
        }

        public JHEmployeeInfo(string id)
        {
            this.Id = id;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
