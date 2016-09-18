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
    /// 员工信息
    /// </summary>
    [Table("EmployeeInfo")]
    public class EmployeeInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
         #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;
        //  身份证号
        private string idCardNo;
        //  出生日期
        private DateTime? birthDate;
        //  出生地
        private string birthPlace;
        //  性别编码
        private string genderCode;

        //  合同就职日期
        private DateTime? enterDate;
        //  离职日期
        private DateTime? leaveDate;
        //  离职原因代码
        private string leaveReasonCode;

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

        /// <summary>
        /// 获得或者设置身份证号
        /// </summary>
        [Column]
        public string IdCardNo
        {
            get { return idCardNo; }
            set
            {
                if (idCardNo != value)
                {
                    idCardNo = value;
                    OnPropertyChanged("IdCardNo");
                }
            }
        }

        [Column]
        public DateTime? BirthDate
        {
            get { return birthDate; }
            set
            {
                if (birthDate != value)
                {
                    birthDate = value;
                    OnPropertyChanged("BirthDate");
                }
            }
        }

        [Column]
        public string BirthPlace
        {
            get { return birthPlace; }
            set
            {
                if (birthPlace != value)
                {
                    birthPlace = value;
                    OnPropertyChanged("BirthPlace");
                }
            }
        }

        [Column]
        public string GenderCode
        {
            get { return genderCode; }
            set
            {
                if (genderCode != value)
                {
                    genderCode = value;
                    OnPropertyChanged("GenderCode");
                }
            }
        }

        [Column]
        public DateTime? EnterDate
        {
            get { return enterDate; }
            set
            {
                if (enterDate != value)
                {
                    enterDate = value;
                    OnPropertyChanged("EnterDate");
                }
            }
        }

        [Column]
        public DateTime? LeaveDate
        {
            get { return leaveDate; }
            set
            {
                if (leaveDate != value)
                {
                    leaveDate = value;
                    OnPropertyChanged("LeaveDate");
                }
            }
        }

        [Column]
        public string LeaveReasonCode
        {
            get { return leaveReasonCode; }
            set
            {
                if (leaveReasonCode != value)
                {
                    leaveReasonCode = value;
                    OnPropertyChanged("LeaveReasonCode");
                }
            }
        }

        #endregion

        #region Constructors

        public EmployeeInfo()
        {
        }

        public EmployeeInfo(string id)
        {
            this.Id = id;
        }

        public EmployeeInfo(string id, string name = null, string description = null)
            : this(id)
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion

        #region Methods

        //  TODO

        #endregion
    }
}
