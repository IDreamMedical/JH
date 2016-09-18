using JinHong.Model;
using JinHong.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using UniGuy.Commands;
using UniGuy.Entity;
using JinHong.Extensions;
using System.Windows;

namespace JinHong.ViewModel
{
    public class AddRoleOfUserManagementDialogVM : BaseViewModel
    {

        #region private Field

        private RoleMapToUser _InsertRoleMapToUser;
        private ObservableCollection<Role> _roles;

        private ObservableCollection<User> _users;

        private Window _viewDialog;














        #region Public Prop

        public RoleMapToUser InsertRoleMapToUser
        {
            get { return _InsertRoleMapToUser; }
            set
            {
                if (_InsertRoleMapToUser != value)
                {
                    _InsertRoleMapToUser = value;
                    OnPropertyChanged("InsertRoleMapToUser");

                }
            }

        }

        public ObservableCollection<Role> Roles
        {
            get { return _roles; }
            set
            {

                if (_roles != value)
                {
                    _roles = value;
                    OnPropertyChanged("Roles");

                }
            }
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged("Users");

                }
            }
        }

        public Window ViewDialog
        {
            get { return _viewDialog; }
            set { _viewDialog = value; }
        }
        #endregion



        #endregion



        #region Constructors

        public AddRoleOfUserManagementDialogVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            Initialize();
        }

        #endregion


        #region Methods
        public void CreateRoleOfUser()
        {
            GlobalVariables.Smc.Insert<RoleMapToUser>(InsertRoleMapToUser);
            this.ViewDialog.DialogResult = true;
        }


        public void Initialize()
        {
            InitializeCommand();
            InitializeEntity();
            InitializeRoles();
            InitializeUsers();
        }

        public void InitializeEntity()
        {

            InsertRoleMapToUser = new RoleMapToUser
            {
                Id = Guid.NewGuid().ToString()
            };

        }

        private void InitializeCommand()
        {
            this.BtnOKCommand = new DelegateCommand(CreateRoleOfUser);
            this.BtnCancelCommand = new DelegateCommand(base.Cancel);

        }

        private void InitializeRoles()
        {
            Roles = new ObservableCollection<Role>();
            var dt = new RoleService().GetAllRoles();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Roles.Add(item.BuildEntity<Role>());
                }
            }

        }
        private void InitializeUsers()
        {
            Users = new ObservableCollection<User>();
            var dt = new UserService().GetAllUsers();
            if (null != dt)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Users.Add(item.BuildEntity<User>());
                }
            }
        }

        #endregion
    }
}
