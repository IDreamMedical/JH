using JinHong.Model;
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
    /// 权限VM
    /// </summary>
    public class PrivilegeVM : AbstractPageViewModel
    {
        #region Fields

        readonly object _syncRoot = new object();

        private DataTable privilegeTbl;

        //  当前选中的角色状态
        private Privilege selectedprivilege;




        #endregion

        #region Constructors

        public PrivilegeVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion


        #region Properties


        public DataTable PrivilegeTbl
        {
            get { return privilegeTbl; }
            set
            {
                if (privilegeTbl != value)
                {
                    privilegeTbl = value;

                    OnPropertyChanged("PrivilegeTbl");

                }

            }
        }
        public Privilege SelectedPrivilege
        {
            get { return selectedprivilege; }
            set
            {
                if (selectedprivilege != value)
                {
                    selectedprivilege = value;

                    OnPropertyChanged("SelectedPrivilege");

                }
            }
        }

        #endregion

        #region Methods

        public void Query(Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    // 查询并设置BuildingInfoTbl
                    DataSet ds = GlobalVariables.Smc.Select("SELECT * FROM privilege", null);
                    PrivilegeTbl = ds == null ? null : ds.Tables[0];

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        //  TODO

        #endregion

    }
}
