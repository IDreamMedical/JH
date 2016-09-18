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
    public class ContractInfo : EntityBase, IIdObject, INamedObject, IDescribable
    {
        #region Fields

        //  楼宇Id
        private string id;
        //  名称
        private string name;
        //  描述
        private string description;

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
