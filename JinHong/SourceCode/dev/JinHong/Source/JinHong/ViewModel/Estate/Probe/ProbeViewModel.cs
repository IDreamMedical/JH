using JinHong.Helper;
using JinHong.ServiceContract;
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
    /// <summary>
    /// 探头ViewModel
    /// </summary>
    public class ProbeViewModel : BaseViewModel
    {

        
        #region Fields

        private static readonly Lazy<IMonitorProbeService> lazy = new Lazy<IMonitorProbeService>(() => new MonitorProbeService());

        /// <summary>
        /// 选择的摄像头
        /// </summary>
        private MonitorProbe _selectedMonitorProbe;

        #endregion


        #region Properties
        /// <summary>
        /// 选择的摄像头
        /// </summary>
        public MonitorProbe SelectedMonitorProbe
        {
            get { return _selectedMonitorProbe; }
            set
            {
                if (_selectedMonitorProbe != value)
                {
                    _selectedMonitorProbe = value;
                    OnPropertyChanged("SelectedMonitorProbe");

                }
            }
        }

        public static IMonitorProbeService Service { get { return lazy.Value; } }


        #endregion

        #region Constructors

        public ProbeViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion



        #region Methods



        public void Initialize()
        {
            this.ModuleName = "摄像头登记表";
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

            var dlg = new AddMonitorProbeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.MonitorProbe = new MonitorProbe { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelMonitorProbe(this.SelectedMonitorProbe.Id))
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
            var dlg = new AddMonitorProbeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.MonitorProbe = this.SelectedMonitorProbe;
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
                        SourceTbl = Service.GetAllMonitorProbes();
                    }
                    else
                    {
                        SourceTbl = Service.GetMonitorProbeByName(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }

        #endregion

    }
}
