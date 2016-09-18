using JinHong.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniGuy.Entity;
using JinHong.Services;
using JinHong.ServiceContract;
using JinHong.View.Dialogs;
using UniGuy.Corek;
using UniGuy.Commands;
using JinHong.Helper;
using System.Windows;
using System.Windows.Threading;

namespace JinHong.ViewModel
{
    /// <summary>
    /// 单元管理VM  
    /// </summary>
    public class RoomInfoVM : BaseViewModel
    {

        #region  private Fields

        private RoomInfo _selectedRoom;

        private static readonly Lazy<IRoomService> lazy = new Lazy<IRoomService>(() => new RoomService());

        #endregion

        #region Properties
        public RoomInfo SelectedRoom
        {
            get { return _selectedRoom; }
            set
            {
                if (_selectedRoom != value)
                {
                    _selectedRoom = value;

                    OnPropertyChanged("SelectedRoom");

                }
            }
        }
        public static IRoomService Service { get { return lazy.Value; } }


        #endregion


        #region Constructors

        public RoomInfoVM(MainWindowViewModel parentVM)
            : base(parentVM)
        {
        }

        #endregion



        #region Methods

        public void Initialize()
        {
            this.RefreshCommand = new DelegateCommand(OnRefreshCommand);
            this.EditCommand = new DelegateCommand(OnEditCommand, CanExecute);
            this.AddNewCommand = new DelegateCommand(OnAddNewCommand);
            this.RemoveCommand = new DelegateCommand(OnRemoveCommand, CanExecute);
            this.ExportToExcelCommand = new DelegateCommand(OnExportToExcelCommand);
            this.ExportToPdfCommand = new DelegateCommand(OnExportToPdfCommand);
        }

        private void OnExportToPdfCommand()
        {
            this.ExportToPdf(SourceTbl, ModuleName);
        }

        private void OnExportToExcelCommand()
        {
            this.ExportToExcel(SourceTbl, ModuleName);

        }

        private void OnRefreshCommand()
        {
            Guid id = GlobalVariables.AppStatusInfo.AddBusyTaskContent("正在查询...");
            this.Query(WhereName, () => Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Normal, new Action(() => GlobalVariables.AppStatusInfo.RemoveBusyTaskItem(id))));

        }

        private void OnAddNewCommand()
        {
            var ard = new AddRoomDialog();
            ard.ViewModel.Room = new RoomInfo { Id = Guid.NewGuid().ToString() };
            ard.ViewModel.OperateMode = OperateModeEnum.New;
            ard.ViewModel.RefreshParentForm = OnRefreshCommand;
            ard.ShowDialog();


        }

        private void OnRemoveCommand()
        {
            if (MsgHelper.ConfirmDel()) return;
            if (Service.DelRoom(this.SelectedRoom.Id))
            {
                MessageBox.Show("删除成功！", "系统提示");
                OnRefreshCommand();
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
            var ard = new AddRoomDialog();
            ard.ViewModel.Room = this.SelectedRoom;
            ard.ViewModel.OperateMode = OperateModeEnum.Edit;
            ard.ViewModel.RefreshParentForm = OnRefreshCommand;
            ard.ShowDialog();


        }
        #endregion


        private void Query(string roomName, Action actCompleted)
        {
            Task.Factory.StartNew(() =>
            {
                lock (_syncRoot)
                {
                    if (string.IsNullOrEmpty(roomName))
                    {
                        SourceTbl = new RoomService().GetAllRooms();
                    }
                    else
                    {
                        SourceTbl = new RoomService().GetRoomByName(roomName);

                    }

                    if (actCompleted != null)
                        actCompleted();
                }
            });

        }
    }
}
