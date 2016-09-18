using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using JinHong.Model;
using UniGuy.Commands;
using JinHong.ServiceContract;
using JinHong.Services;
using JinHong.Helper;
using Microsoft.Windows.Controls;
using JinHong.View.Dialogs;
using UniGuy.Corek;
using System.Windows.Threading;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 停车费ViewModel
    /// </summary>
    public class ParkingFeeViewModel : BaseViewModel
    {
        #region Fields

        private ParkingFeesInfo _selectedParkingFeesInfo;
        private static readonly Lazy<IParkingFeeService> lazy = new Lazy<IParkingFeeService>(() => new ParkingFeeService());

        #endregion

        #region public  Prop

        public static IParkingFeeService Service { get { return lazy.Value; } }

        public ParkingFeesInfo SelectedParkingFeesInfo
        {
            get { return _selectedParkingFeesInfo; }
            set
            {
                if (_selectedParkingFeesInfo != value)
                {
                    _selectedParkingFeesInfo = value;
                    OnPropertyChanged("SelectedParkingFeesInfo");
                }
            }
        }



        #endregion

        #region Constructors

        public ParkingFeeViewModel(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion

        #region Methods



        public void Initialize()
        {
            this.ModuleName = "停车费收费汇总";
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
            OnRefreshCommand();

        }

        private void OnExportToPdfCommand()
        {
            base.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToExcel(SourceTbl, ModuleName);

        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelParkingFee(this._selectedParkingFeesInfo.Id))
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
            var dlg = new AddOrEditParkingFeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.ParkingFee =  new ParkingFeesInfo { Id = Guid.NewGuid().ToString() };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnEditCommand()
        {
            var dlg = new AddOrEditParkingFeeDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.Edit;
            dlg.ViewModel.ParkingFee = this.SelectedParkingFeesInfo;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));
        }

        public void Query(string name, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(name))
                    {
                        SourceTbl = Service.GetParkingFeeDetail(string.Empty);
                    }
                    else
                    {
                        SourceTbl = Service.GetParkingFeeDetail(name);
                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });
        }
        #endregion
    }
}
