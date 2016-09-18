using JinHong.ServiceContract;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;
using JinHong.Helper;

namespace JinHong.ViewModel
{

    /// <summary>
    /// 社会单元
    /// </summary>
    public class SocialUnitInfoVM : BaseViewModel
    {
        #region private var

        private static readonly Lazy<ISocialUnitService> lazy = new Lazy<ISocialUnitService>(() => new SocialUnitService());

        private SocialUnitInfo _selectedSocialUnit;

        #endregion

        #region Properties

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

        public static ISocialUnitService Service { get { return lazy.Value; } }

        #endregion

        public SocialUnitInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
            OnRefreshCommand();
        }

        #region Methods

        #region Public Method
        public void Initialize()
        {
            this.ModuleName = "客户管理列表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
        }
        #endregion

        #region Private Method


        private void OnExportToPdfCommand()
        {
            base.ExportToExcel(this.SourceTbl, this.ModuleName);
        }
        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(this.SourceTbl, this.ModuleName);
        }

        private void OnRemoveCommand()
        {
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

        private void OnAddNewCommand()
        {
            var dlg = new AddSocialUnitInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.SocialUnit = new SocialUnitInfo { Id = Guid.NewGuid().ToString(), Status = "0" };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnEditCommand()
        {
            var dlg = new AddSocialUnitInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.SocialUnit = this.SelectedSocialUnit;
            dlg.ShowDialog();
        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
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

        #endregion

    }
}
