using JinHong.Model;
using JinHong.Services;
using JinHong.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGuy.Commands;

namespace JinHong.ViewModel
{
    public class RoleOfUserManagementVM : BaseViewModel
    {
        #region private Field


        private RoleMapToUser _roleMapToUser;
        #endregion

        #region Public Prop

        public RoleMapToUser RoleMapToUser
        {
            get { return _roleMapToUser; }
            set
            {
                if (_roleMapToUser != value)
                {
                    _roleMapToUser = value;
                    OnPropertyChanged("RoleMapToUser");

                }
            }
        }

        #endregion



        #region Constructors

        public RoleOfUserManagementVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {

        }

        #endregion


        #region Methods

        public void Initialize()
        {
            this.AddNewCommand = new DelegateCommand(ShowAddDialog);
            this.RemoveCommand = new DelegateCommand(DelRoleOfUser, CanExecute);
            this.Query(string.Empty, null);

        }

        public void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = new RoleOfUserManagementService().GetAllRoleOfUsers();
                    }
                    else
                    {
                        SourceTbl = new AirConditionerService().GetAirConditionerByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }


        public void ShowAddDialog()
        {
            var dialog = new AddRoleOfUserManagementDialog();
            dialog.ShowDialog();
            this.Query(string.Empty, null);
        }

        public void DelRoleOfUser()
        {



        }

        private bool CanExecute()
        {
            return this.IsCanExecute;
        }


        #endregion


    }
}
