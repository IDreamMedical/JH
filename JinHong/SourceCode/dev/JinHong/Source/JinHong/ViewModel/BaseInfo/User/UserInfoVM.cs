using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGuy.Entity;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 角色VM
    /// </summary>
    public class UserInfoVM : BaseViewModel
    {

        private static readonly Lazy<UserService> lazy = new Lazy<UserService>(() => new UserService());

        public static UserService Service { get { return lazy.Value; } }

        #region Fields



        //  当前选中的角色状态
        private User selectedUser;

        #endregion

        #region Constructors

        public UserInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion


        #region Properties


        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;

                    OnPropertyChanged("SelectedUser");

                }
            }
        }

        #endregion

        #region Methods

        public void Query(string userName, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(userName))
                    {
                        SourceTbl = Service.GetAllUsers();
                    }
                    else
                    {
                        SourceTbl = Service.GetUserByName(userName);

                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        public bool HasUser(string id, string name)
        {
            return Service.HasUser(id, name);
        }

        public void Save(Role item)
        {
            GlobalVariables.Smc.Insert<Role>(item);

        }

        public void Edit(Role item)
        {
            GlobalVariables.Smc.Update<Role>(item);
        }

        #endregion

    }
}
