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
    ///ParkingLotInfoVM
    /// </summary>
    public class ParkingLotInfoVM : BaseViewModel
    {

        #region Fields

        private ParkingLotInfo _selectedParking;
        private static readonly Lazy<IParkingLotService> lazy = new Lazy<IParkingLotService>(() => new ParkingLotService());


        #endregion

        #region Properties


        public ParkingLotInfo SelectedParking
        {
            get { return _selectedParking; }
            set
            {
                if (_selectedParking != value)
                {
                    _selectedParking = value;
                    OnPropertyChanged("SelectedParking");

                }
            }
        }

        public static IParkingLotService Service { get { return lazy.Value; } }


        #endregion

        #region Constructors

        public ParkingLotInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion


        #region Methods
        public void Query(string parkingNo, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(parkingNo))
                    {
                        SourceTbl = new ParkingLotRentalService().GetAllParkingRentals();
                    }
                    else
                    {
                        SourceTbl = new ParkingLotRentalService().GetParkingRentalByWhereStr(parkingNo);

                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });


        }







        public void Initialize()
        {
            RefreshCommand = new DelegateCommand(OnRefreshCommand);
            EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            AddNewCommand = new DelegateCommand(OnAddNewCommand);
            RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);

        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));

        }

        private void OnExportToPdfCommand()
        {
            base.ExportToExcel(SourceTbl, this.ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            base.ExportToPdf(SourceTbl, this.ModuleName);
        }

        private void OnAddNewCommand()
        {
            var dlg = new AddParkingLotInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Parking = new ParkingLotInfo { Id = Guid.NewGuid().ToString(), Status = 0 };
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();
        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelParkingLot(this.SelectedParking.Id))
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

            var dlg = new AddParkingLotInfoDialog();
            dlg.ViewModel.OperateMode = OperateModeEnum.New;
            dlg.ViewModel.Parking = this.SelectedParking;
            dlg.ViewModel.RefreshParentForm = OnRefreshCommand;
            dlg.ShowDialog();

        }


        #endregion
    }
}
