using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UniGuy.Core;
using System.Xml.Serialization;
using UniGuy.Core.Data;

namespace UniGuy.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("Role")]
    public class Role : EntityBase, IIdObject
    {
        #region Fields

        public static Func<string, Privilege> GetPrivilegeFunc;

        private string id;
        //  角色名
        private string name;
        //  描述
        private string description;

        private int status;


        //  权限
        private ObservableCollection<string> privilegeIds;
        private IEnumerable<Privilege> privileges;

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


        [Column]
        public int Status
        {
            get { return status; }
            set
            {


                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        [Column(SerializationType = SerializationType.Xml)]
        public ObservableCollection<string> PrivilegeIds
        {
            get { return privilegeIds; }
            set
            {
                if (privilegeIds != value)
                {
                    privilegeIds = value;
                    OnPropertyChanged("PrivilegeIds");
                }
            }
        }

        public IEnumerable<Privilege> Privileges
        {
            get
            {
                return privileges ?? (privilegeIds == null ? null : (privileges = privilegeIds.Select(id => GetPrivilegeFunc(id))));
            }
        }

        #endregion

        #region Constructors
        public Role() { }
        public Role(string name)
            : base()
        {
            this.Name = name;
        }

        #endregion

    }
}
