using JinHong.Helper;
using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{

    /// <summary>
    /// 租赁企业首页信息
    /// </summary>

    public class SocialUnitIndexViewModel : BaseViewModel
    {

        #region private var

        private static readonly Lazy<ISocialUnitService> lazy = new Lazy<ISocialUnitService>(() => new SocialUnitService());

        private SocialUnitInfo _selectedSocialUnit;


        /// <summary>
        /// 查看明细
        /// </summary>
        private ICommand _viewCommand;

        /// <summary>
        /// 租赁业务
        /// </summary>
        private ICommand _rentalCommand;


        #endregion

        public SocialUnitIndexViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #region public  var

        public static ISocialUnitService Service { get { return lazy.Value; } }

        public SocialUnitInfo SelectedSocialUnit
        {
            get { return _selectedSocialUnit; }
            set
            {
                if (_selectedSocialUnit != value)
                {
                    _selectedSocialUnit = value;

                    OnPropertyChanged("SelectedSocialUnit");

                }
            }
        }
        public ICommand ViewCommand
        {
            get { return _viewCommand; }
            set
            {
                if (_viewCommand != value)
                {
                    _viewCommand = value;
                    OnPropertyChanged("ViewCommand");
                }
            }
        }

        public ICommand RentalCommand
        {
            get { return _rentalCommand; }
            set
            {
                if (_rentalCommand != value)
                {
                    _rentalCommand = value;
                    OnPropertyChanged("RentalCommand");
                }
            }
        }

        #endregion

        #region Public Method
        public void Initialize()
        {
            this.ModuleName = "客户基本情况列表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.ViewCommand = new DelegateCommand(OnViewCommand, CanExecute);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute); 
            this.RentalCommand = new DelegateCommand(OnRentalCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();
        }
        #endregion

        #region Private Method




        /// <summary>
        /// 查看明细表
        /// </summary>

        /// <summary>
        /// 租赁业务
        /// </summary>

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }
        private void OnAddNewCommand()
        {
            var dlg = new AddSocialUnitInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.SocialUnit = new SocialUnitInfo { Id = Guid.NewGuid().ToString(), Status = "0" };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        /// <summary>
        /// 编辑企业信息
        /// </summary>
        private void OnEditCommand()
        {
            //var dlg = new AddSocialUnitInfoDialog();
            //dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            //dlg.ViewModel.SocialUnit = this.SelectedSocialUnit;
            //dlg.ShowDialog();
        }
        private void OnViewCommand()
        {
            var dlg = new CompanyIndexDetail();
            dlg.ShowDialog();

        }
        private void OnRemoveCommand()
        {
            var dlg = new CompanyIndexDetail();
            dlg.ShowDialog();

        }
        private void OnRentalCommand()
        {
            //根据状态来显示按钮//退租或者续租或者租仓库
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelSocialUnit(this.SelectedSocialUnit.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("删除失败！", "系统提示");
            }

        }
        private void OnExportToPdfCommand()
        {
            base.ExportToExcel(this.SourceTbl, this.ModuleName);
        }
        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(this.SourceTbl, this.ModuleName);
        }



        public override bool CanExecute()
        {
            return IsCanExecute;
        }


        private void Query(string socialUnitName, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(socialUnitName))
                    {
                        SourceTbl = Service.GetAllSocialUnits();
                    }
                    else
                    {
                        SourceTbl = Service.GetSocialUnitById(socialUnitName);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion

    }
}
