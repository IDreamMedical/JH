using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using UniGuy.Core;
using UniGuy.Core.Data;
using System.Xml;
using System.Xml.Serialization;

namespace UniGuy.Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    [Serializable]
    [Table("User")]
    public class User : EntityBase, IIdObject
    {
        #region Fields

        public static Func<string, Role> GetRoleFunc;

        private const string SYSTEM_NAME = "sys";
        private const string DEFAULT_PASSWORD = "888888";

        private string id;
        //  用户名
        private string userName;
        //  密码
        private string password;

        private int status;
        private string _description;




        //  角色
        private ObservableCollection<string> roleIds;
        private IEnumerable<Role> roles;

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
        /// 获得用户名
        /// </summary>
        [Column]
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        /// <summary>
        /// 获得或者设置密码
        /// </summary>
        [Column]
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
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

        //  TODO... Not tested.
        [Column(SerializationType = SerializationType.Xml)]
        public ObservableCollection<string> RoleIds
        {
            get { return roleIds; }
            set
            {
                if (roleIds != value)
                {
                    roleIds = value;
                    OnPropertyChanged("RoleIds");
                }
            }
        }

        public IEnumerable<Role> Roles
        {
            get
            {
                return roles ?? (RoleIds == null ? null : (roles = roleIds.Select(id => GetRole(id))));
            }
        }

        /// <summary>
        /// 是否是系统用户
        /// </summary>
        public bool IsSystem
        {
            get { return UserName == SYSTEM_NAME; }
        }

        #endregion

        #region Constructors
        public User() { }
        public User(string username, string password = DEFAULT_PASSWORD)
            : base()
        {
            this.UserName = username;
            this.Password = password;
        }

        #endregion

        #region Methods
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <remarks>
        /// 建议账号登陆流程为:
        /// 1. 根据用户名查找Account, 找不到就是用户名不存在;
        /// 2. 使用Verify确认password正确
        /// </remarks>
        public bool Verify(string password)
        {
            return this.Password == password;
        }

        public override string ToString()
        {
            return UserName;
        }

        private Role GetRole(string roleId)
        {
            if (GetRoleFunc == null)
                throw new Exception("'GetRoleFunc not assigned.");
            return GetRoleFunc(roleId);
        }

        #endregion
    }
}
