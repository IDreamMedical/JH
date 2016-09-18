using JinHong.Helper;
using JinHong.ServiceContract.Estate;
using JinHong.Model;
using JinHong.Services;
using JinHong.View.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using UniGuy.Commands;
using UniGuy.Corek;

namespace JinHong.ViewModel
{
    public class RepairViewModel : BaseViewModel
    {
        #region Fields
      
        private RepairRecord _selectedRepair;
        private static readonly Lazy<IRepairService> lazy = new Lazy<IRepairService>(() => new RepairService());

        #endregion

        #region Properties

        public static IRepairService Service { get { return lazy.Value; } }

        /// <summary>
        /// 获得或者设置当前选中电信费信息
        /// </summary>
        public RepairRecord SelectedRepair
        {
            get { return _selectedRepair; }
            set
            {
                if (_selectedRepair != value)
                {
                    _selectedRepair = value;
                    OnPropertyChanged("SelectedRepair");
                }
            }
        }
        #endregion



        #region Constructors

        public RepairViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

    
        #region Methods



        public void Initialize()
        {
            this.ModuleName = "维修记录表";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();

        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));

        }

        private void OnExportToPdfCommand()
        {
            base.ExportToExcel(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnAddNewCommand()
        {

            var dlg = new WpfRepairDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Repair = new RepairRecord { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelRepairRecord(this.SelectedRepair.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
            }
            else
            {
                MessageBox.Show("删除失败！", "系统提示");
            }

        }

        public override bool CanExecute()
        {
            return this.IsCanExecute;
        }

        private void OnEditCommand()
        {
            var dlg = new WpfRepairDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.Repair = this.SelectedRepair;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();

        }

        private void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetAllRepairRecords();
                    }
                    else
                    {
                        SourceTbl = Service.GetRepairRecordByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion

    }
}
