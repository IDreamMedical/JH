using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniGuy.Core;
using UniGuy.Core.Data;
using UniGuy.Entity;

namespace JinHong.Core
{
    /// <summary>
    /// 
    /// 模型类
    /// </summary>
    /// 
    [Table("Module")]

    public class Module : EntityBase, IIdObject, INamedObject, IDescribable
    {

        #region Fields
        private string _id;

        private string _name;
        private string _status;


        private string _pId;



        private string _description;

        private int _isSys;

        private string _viewModelName;
        private string _viewName;

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

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        [Column]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// 模块状态 0是启用，1是禁用
        /// </summary>
        [Column]

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        [Column]

        public string PId
        {
            get { return _pId; }
            set
            {
                if (_pId != value)
                {
                    _pId = value;
                    OnPropertyChanged("PId");
                }
            }
        }
        [Column]
        /// <summary>
        /// 是否系统参数 0,是，1 不是 默认是1
        /// </summary>
        public int IsSys
        {
            get
            {
                return _isSys;
            }

            set
            {

                if (_isSys != value)
                {
                    _isSys = value;
                    OnPropertyChanged("IsSys");
                }
            }
        }

       // [Column]
        /// <summary>
        /// viewModel名称
        /// </summary>
        public string ViewModelName
        {
            get
            {
                return _viewModelName;
            }

            set
            {
                if (_viewModelName != value)
                {
                    _viewModelName = value;
                    OnPropertyChanged("ViewModelName");
                }
                
            }
        }

        /// <summary>
        /// 视图名
        /// </summary>
        public string ViewName
        {
            get
            {
                return _viewName;
            }

            set
            {
                if (_viewName != value)
                {
                    _viewName = value;
                    OnPropertyChanged("ViewName");
                }
               
            }
        }

        #endregion

    }
}
