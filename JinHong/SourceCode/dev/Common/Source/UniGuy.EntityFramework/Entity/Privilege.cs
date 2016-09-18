using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UniGuy.Core;
using UniGuy.Core.Data;
using System.Xml.Serialization;

namespace UniGuy.Entity
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table("Privilege")]
    public class Privilege : EntityBase, IIdObject
    {
        #region Fields

        private string id;
        //  角色名
        private string name;
        //  描述
        private string description;

        #endregion

        #region Properties

        [XmlIgnore]
        [PrimaryKey]
        public string Id
        {
            get { return id; }
            private set
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
                    Id = value;
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
        public Privilege() { }

        public Privilege(string name, string description = null)
            : base()
        {
            this.Name = name;
            this.Description = description;
        }

        #endregion
    }
}
